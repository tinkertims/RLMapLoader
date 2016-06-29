using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace RLMapLoader
{
    /// <summary>
    /// Represents an access to a remote process memory
    /// </summary>
    public class Memory : IDisposable
    {
        private Process process;
        private IntPtr processHandle;
        private bool isDisposed;

        public const string OffsetPattern = "(\\+|\\-){0,1}(0x){0,1}[a-fA-F0-9]{1,}";

        /// <summary>
        /// Initializes a new instance of the Memory
        /// </summary>
        /// <param name="process">Remote process</param>
        public Memory(Process process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            this.process = process;
            processHandle = Win32.OpenProcess(
                Win32.ProcessAccessType.PROCESS_VM_READ | Win32.ProcessAccessType.PROCESS_VM_WRITE |
                Win32.ProcessAccessType.PROCESS_VM_OPERATION, true, (uint)process.Id);
            if (processHandle == IntPtr.Zero)
                throw new InvalidOperationException("Could not open the process");
        }

        #region IDisposable

        ~Memory()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (isDisposed)
                return;
            Win32.CloseHandle(processHandle);
            process = null;
            processHandle = IntPtr.Zero;
            isDisposed = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the process to which this memory is attached to
        /// </summary>
        public Process Process
        {
            get
            {
                return process;
            }
        }

        #endregion

        /// <summary>
        /// Finds module with the given name
        /// </summary>
        /// <param name="name">Module name</param>
        /// <returns></returns>
        protected ProcessModule FindModule(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName.ToLower() == name.ToLower())
                    return module;
            }
            return null;
        }

        /// <summary>
        /// Gets module based address
        /// </summary>
        /// <param name="moduleName">Module name</param>
        /// <param name="baseAddress">Base address</param>
        /// <param name="offsets">Collection of offsets</param>
        /// <returns></returns>
        public IntPtr GetAddress(string moduleName, IntPtr baseAddress, int[] offsets)
        {
            if (string.IsNullOrEmpty(moduleName))
                throw new ArgumentNullException("moduleName");

            ProcessModule module = FindModule(moduleName);
            if (module == null)
                return IntPtr.Zero;
            else
            {
                int address = module.BaseAddress.ToInt32() + baseAddress.ToInt32();
                return GetAddress((IntPtr)address, offsets);
            }
        }

        /// <summary>
        /// Gets address
        /// </summary>
        /// <param name="baseAddress">Base address</param>
        /// <param name="offsets">Collection of offsets</param>
        /// <returns></returns>
        public IntPtr GetAddress(IntPtr baseAddress, int[] offsets)
        {
            if (baseAddress == IntPtr.Zero)
                throw new ArgumentException("Invalid base address");

            int address = baseAddress.ToInt32();

            if (offsets != null && offsets.Length > 0)
            {
                byte[] buffer = new byte[4];
                foreach (int offset in offsets)
                    address = ReadInt32((IntPtr)address) + offset;
            }

            return (IntPtr)address;
        }

        /// <summary>
        /// Gets address pointer
        /// </summary>
        /// <param name="address">Address</param>
        /// <returns></returns>
        public IntPtr GetAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException("address");

            string moduleName = null;
            int index = address.IndexOf('"');
            if (index != -1)
            {
                // Module name at the beginning
                int endIndex = address.IndexOf('"', index + 1);
                if (endIndex == -1)
                    throw new ArgumentException("Invalid module name. Could not find matching \"");
                moduleName = address.Substring(index + 1, endIndex - 1);
                address = address.Substring(endIndex + 1);
            }

            int[] offsets = GetAddressOffsets(address);
            int[] _offsets = null;
            IntPtr baseAddress = offsets != null && offsets.Length > 0 ?
                (IntPtr)offsets[0] : IntPtr.Zero;
            if (offsets != null && offsets.Length > 1)
            {
                _offsets = new int[offsets.Length - 1];
                for (int i = 0; i < offsets.Length - 1; i++)
                    _offsets[i] = offsets[i + 1];
            }

            if (moduleName != null)
                return GetAddress(moduleName, baseAddress, _offsets);
            else
                return GetAddress(baseAddress, _offsets);
        }

        /// <summary>
        /// Gets address offsets
        /// </summary>
        /// <param name="address">Address</param>
        /// <returns></returns>
        protected static int[] GetAddressOffsets(string address)
        {
            if (string.IsNullOrEmpty(address))
                return new int[0];
            else
            {
                MatchCollection matches = Regex.Matches(address, OffsetPattern);
                int[] offsets = new int[matches.Count];
                string value;
                char ch;
                for (int i = 0; i < matches.Count; i++)
                {
                    ch = matches[i].Value[0];
                    if (ch == '+' || ch == '-')
                        value = matches[i].Value.Substring(1);
                    else
                        value = matches[i].Value;
                    offsets[i] = Convert.ToInt32(value, 16);
                    if (ch == '-')
                        offsets[i] = -offsets[i];
                }
                return offsets;
            }
        }

        /// <summary>
        /// Reads memory at the address
        /// </summary>
        /// <param name="address">Memory address</param>
        /// <param name="buffer">Buffer</param>
        /// <param name="size">Size in bytes</param>
        public void ReadMemory(IntPtr address, byte[] buffer, int size)
        {
            if (isDisposed)
                throw new ObjectDisposedException("Memory");
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            if (size <= 0)
                throw new ArgumentException("Size must be greater than zero");
            if (address == IntPtr.Zero)
                throw new ArgumentException("Invalid address");

            uint read = 0;
            if (!Win32.ReadProcessMemory(processHandle, address, buffer, (uint)size, ref read) ||
                read != size)
                throw new AccessViolationException();
        }

        /// <summary>
        /// Writes memory at the address
        /// </summary>
        /// <param name="address">Memory address</param>
        /// <param name="buffer">Buffer</param>
        /// <param name="size">Size in bytes</param>
        public void WriteMemory(IntPtr address, byte[] buffer, int size)
        {
            if (isDisposed)
                throw new ObjectDisposedException("Memory");
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            if (size <= 0)
                throw new ArgumentException("Size must be greater than zero");
            if (address == IntPtr.Zero)
                throw new ArgumentException("Invalid address");

            uint write = 0;
            if (!Win32.WriteProcessMemory(processHandle, address, buffer, (uint)size, ref write) ||
                write != size)
                throw new AccessViolationException();
        }

        /// <summary>
        /// Reads 32 bit signed integer at the address
        /// </summary>
        /// <param name="address">Memory address</param>
        /// <returns></returns>
        public int ReadInt32(IntPtr address)
        {
            byte[] buffer = new byte[4];
            ReadMemory(address, buffer, 4);
            return BitConverter.ToInt32(buffer, 0);
        }

        /// <summary>
        /// Reads 32 bit unsigned integer at the address
        /// </summary>
        /// <param name="address">Memory address</param>
        /// <returns></returns>
        public uint ReadUInt32(IntPtr address)
        {
            byte[] buffer = new byte[4];
            ReadMemory(address, buffer, 4);
            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>
        /// Reads single precision value at the address
        /// </summary>
        /// <param name="address">Memory address</param>
        /// <returns></returns>
        public float ReadFloat(IntPtr address)
        {
            byte[] buffer = new byte[4];
            ReadMemory(address, buffer, 4);
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// Reads double precision value at the address
        /// </summary>
        /// <param name="address">Memory address</param>
        /// <returns></returns>
        public double ReadDouble(IntPtr address)
        {
            byte[] buffer = new byte[8];
            ReadMemory(address, buffer, 8);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Writes 32 bit unsigned integer at the address
        /// </summary>
        /// <param name="address">Memory address</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public void WriteUInt32(IntPtr address, uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            WriteMemory(address, buffer, 4);
        }

        /// <summary>
        /// Writes 32 bit signed integer at the address
        /// </summary>
        /// <param name="address">Memory address</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public void WriteInt32(IntPtr address, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            WriteMemory(address, buffer, 4);
        }

        /// <summary>
        /// Writes single precision value at the address
        /// </summary>
        /// <param name="address">Memory address</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public void WriteFloat(IntPtr address, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            WriteMemory(address, buffer, 4);
        }

        /// <summary>
        /// Writes double precision value at the address
        /// </summary>
        /// <param name="address">Memory address</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public void WriteDouble(IntPtr address, double value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            WriteMemory(address, buffer, 8);
        }
    }
}
