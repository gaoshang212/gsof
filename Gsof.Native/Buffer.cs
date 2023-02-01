using Gsof.Native.Library;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Gsof.Native
{
    public class Buffer : IDisposable, ICloneable
    {
        private IntPtr _point;

        public IntPtr Point
        {
            get { return _point; }
            protected set { _point = value; }
        }

        public int Length { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="size">size, the memory alloc </param>
        public Buffer(int size)
        {
            Init(size);
        }

        public Buffer(IntPtr ptr, int size)
        {
            this.Point = ptr;
            this.Length = size;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="buffer">memory alloc with the buffer</param>
        /// <param name="start">strat index</param>
        /// <param name="length">length</param>
        public Buffer(byte[] buffer, int start, int length)
        {
            Init(buffer, start, length);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="buffer">memory alloc with the buffer</param>
        /// <param name="start"></param>
        public Buffer(byte[] buffer, int start = 0) : this(buffer, start, buffer?.Length ?? 0 - 1 - start)
        {

        }


        #region Public

        public byte ReadByte(int pos)
        {
            CheckPointZero();

            return Marshal.ReadByte(this.Point, pos);
        }

        public byte[] ReadBytes(int pos, int length)
        {
            CheckPointZero();

            if (length < 0)
            {
                throw new ArgumentException("the buffer's length less than zero");
            }
            var buffer = new byte[length];

            Marshal.Copy(_point + pos, buffer, 0, length);
            return buffer;
        }

        public byte[] ReadBytes(int pos = 0)
        {
            return ReadBytes(pos, this.Length - pos);
        }

        public short ReadInt16(int offset = 0)
        {
            CheckPointZero();
            return Marshal.ReadInt16(Point, offset);
        }

        public int ReadInt32(int offset = 0)
        {
            CheckPointZero();
            return Marshal.ReadInt32(Point, offset);
        }

        public long ReadInt64(int offset = 0)
        {
            CheckPointZero();
            return Marshal.ReadInt64(Point, offset);
        }

        public string ReadString(Encoding encoding, int offset = 0)
        {
            CheckPointZero();
            return ReadString(encoding, offset, this.Length - offset);
        }

        public string ReadString(Encoding encoding, int offset, int length)
        {
            CheckPointZero();
            if (encoding == null)
            {
                throw new ArgumentException("Encoding can not be null");
            }
            var bytes = ReadBytes(offset, length);

            return encoding.GetString(bytes);
        }

        public T GetStruct<T>()
        {
            CheckPointZero();
            return (T)Marshal.PtrToStructure(Point, typeof(T))!;
        }

        public void CopyTo(Buffer buffer, int index, int length)
        {
            CheckPointZero();

            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (length + index + 1 > this.Length)
            {
                throw new RankException();
            }

            NativeLoader.Instance.memcpy(buffer.Point, this.Point + index, (uint)length);
        }

        public void CopyTo(Buffer buffer, int index = 0)
        {
            CopyTo(buffer, index, this.Length - index);
        }

        public T ToStruct<T>()
        {
            return this.GetStruct<T>();
        }

        public byte[] ToBytes()
        {
            return this.ReadBytes();
        }

        #endregion



        #region Private

        /// <summary>
        /// Memory alloc with size
        /// </summary>
        /// <param name="size">size</param>
        /// <returns></returns>
        private IntPtr Init(int size)
        {
            Point = InternalAlloc(size);
            Length = size;

            return Point;
        }

        /// <summary>
        /// Init Memory with buffer
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        private void Init(byte[] buffer, int index, int length)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (index >= buffer.Length || length + index >= buffer.Length)
            {
                throw new RankException();
            }

            var ptr = Init(length);
            Marshal.Copy(buffer, index, ptr, length);
        }

        /// <summary>
        /// Check Point is not zero
        /// </summary>
        private void CheckPointZero()
        {
            if (_point != IntPtr.Zero)
            {
                return;
            }

            throw new ArgumentException("the memory may not be alloc or free.");
        }

        /// <summary>
        /// Alloc Memery Use AllocHGlobal
        /// </summary>
        /// <param name="p_size">size</param>
        /// <returns></returns>
        private IntPtr InternalAlloc(int p_size)
        {
            IntPtr p = Marshal.AllocHGlobal(p_size);
            return p;
        }

        /// <summary>
        /// Free Point
        /// </summary>
        /// <param name="p_intPtr"></param>
        private void InternalFree(IntPtr p_intPtr)
        {
            if (p_intPtr == IntPtr.Zero)
            {
                return;
            }

            Marshal.FreeHGlobal(p_intPtr);
        }

        #endregion

        #region Dispose

        public void Free()
        {
            InternalFree(Point);
            Point = IntPtr.Zero;
        }

        public void Dispose()
        {
            Free();
        }

        #endregion

        #region Static

        public static Buffer Alloc(int p_size)
        {
            return new Buffer(p_size);
        }

        public static void Free(Buffer buffer)
        {
            buffer?.Dispose();
        }


        #endregion

        #region Clone

        public Buffer Clone()
        {
            var nbuffer = new Buffer(this.Length);
            this.CopyTo(nbuffer);
            return nbuffer;
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion


        #region implicit

        public static implicit operator IntPtr(Buffer buffer) => buffer.Point;

        #endregion
    }
}
