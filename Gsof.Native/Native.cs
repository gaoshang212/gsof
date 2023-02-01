using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using Gsof.Dynamic;
using Gsof.Emit;
using Gsof.Extensions;
using Gsof.Native.Library;

namespace Gsof.Native
{
    public class Native : SafeHandle, IDynamicMetaObjectProvider, INative
    {
        private DelegateBuilder? _delegateBuilder;
        private readonly IDisposable? _disposable;
        private readonly CallingConvention _calling = CallingConvention.Cdecl;

        public string FileName { get; private set; }

        public bool HasDisposed { get; protected set; }

        internal Native(string p_filename) : base(IntPtr.Zero, true)
        {
            FileName = this.CreateFilePath(p_filename);
            LoadLibrary();
        }

        internal Native(string p_filename, IDisposable p_disposable) : this(p_filename)
        {
            _disposable = p_disposable;
            LoadLibrary();
        }

        internal Native(string p_filename, CallingConvention p_calling) : this(p_filename)
        {
            _calling = p_calling;
        }

        public T GetFunction<T>()
        {
            var attribute = GetNativeFunctonAttribute(typeof(T));
            return GetFunction<T>(attribute.FunctionName);
        }

        public T GetFunction<T>(string p_funName)
        {
            var dg = GetFunctionDelegate<T>(p_funName);
            var t = (T)(object)dg;
            return t;
        }

        public bool HasFunction<T>()
        {
            var attribute = GetNativeFunctonAttribute(typeof(T));
            return HasFunctionDelegate<T>(attribute.FunctionName);
        }

        public TResult Invoke<TResult, T>(params object[] p_params)
        {
            if (IsInvalid)
            {
                throw new Exception("The library can not be loaded.");
            }

            var attribute = GetNativeFunctonAttribute(typeof(T));

            return Invoke<TResult>(attribute.FunctionName, p_params);
        }

        public TResult Invoke<TResult>(string p_funName, params object[] p_params)
        {
            return Invoke<TResult>(p_funName, _calling, p_params);
        }

        public TResult Invoke<TResult>(string p_funName, CallingConvention p_calling, params object[] p_params)
        {
            return (TResult)Invoke(p_funName, p_calling, typeof(TResult), p_params);
        }

        public object Invoke(string p_funName, Type p_returnType, params object[] p_params)
        {
            return Invoke(p_funName, _calling, p_returnType, p_params);
        }

        public object Invoke(string p_funName, CallingConvention p_calling, Type p_returnType, params object[] p_params)
        {
            if (IsInvalid)
            {
                throw new Exception("The library can not be loaded.");
            }

            var type = CreateDelegateType(p_params, p_returnType, p_calling);

            var dg = GetFunctionDelegate(type, p_funName);
            if (dg == null)
            {
                return p_returnType == null ? null : p_returnType.CreateInstance();
            }

            return dg.DynamicInvoke(p_params);
        }

        public void Call(string p_funName, params object[] p_params)
        {
            Call(p_funName, _calling, p_params);
        }

        public void Call(string p_funName, CallingConvention p_calling, params object[] p_params)
        {
            if (IsInvalid)
            {
                throw new Exception("The library can not be loaded.");
            }

            var type = CreateDelegateType(p_params, null, p_calling);
            var dg = GetFunctionDelegate(type, p_funName);
            if (dg != null)
            {
                dg.DynamicInvoke(p_params);
            }
        }

        public void Call<T>(params object[] p_params)
        {
            if (IsInvalid)
            {
                throw new Exception("The library can not be loaded.");
            }

            var type = typeof(T);

            var attribute = GetNativeFunctonAttribute(type);
            var dg = GetFunctionDelegate(type, attribute.FunctionName);

            if (dg == null)
            {
                throw new EntryPointNotFoundException();
            }

            dg.DynamicInvoke(p_params);
        }

        #region Private Methods

        private string CreateFilePath(string filepath)
        {
            var path = Path.GetFullPath(filepath);
            var dir = Path.GetDirectoryName(path);
            var filename = Path.GetFileName(filepath);

            string real = Path.Combine(dir, Environment.Is64BitProcess ? "x64" : "x86", filename);

            if (File.Exists(real))
            {
                return real;
            }

            return filepath;
        }

        private void LoadLibrary()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                throw new ArgumentNullException();
            }

            Console.WriteLine("file path：{0}", FileName);

            if (FileName.Contains(Path.DirectorySeparatorChar) && !File.Exists(FileName))
            {
                throw new FileNotFoundException($"dll is not found.{FileName} ");
            }

            Console.WriteLine("the process is {0}bit", Environment.Is64BitProcess ? 64 : 32);

            var fileanme = Path.GetFullPath(FileName);

            var tmpHandle = NativeLoader.Instance.dlopen(fileanme);
            if (tmpHandle == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            SetHandle(tmpHandle);
        }

        private Delegate GetFunctionDelegate<T>(string p_funName)
        {
            return GetFunctionDelegate(typeof(T), p_funName);
        }

        private Delegate GetFunctionDelegate(Type p_type, string p_funName)
        {
            if (IsInvalid)
            {
                throw new Exception("The library can not be loaded.");
            }

            if (string.IsNullOrEmpty(p_funName))
            {
                throw new ArgumentException("The FunctionName is not be empty or null");
            }

            var type = p_type;
            if (!typeof(Delegate).IsAssignableFrom(type))
            {
                throw new ArgumentException("The T is not a Delegate");
            }

            var address = NativeLoader.Instance.dlsym(this.handle, p_funName);

            if (address == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            var dg = Marshal.GetDelegateForFunctionPointer(address, type);

            return dg;
        }

        private bool HasFunctionDelegate<T>(string p_funName)
        {
            return HasFunctionDelegate(typeof(T), p_funName);
        }

        private bool HasFunctionDelegate(Type p_type, string p_funName)
        {
            if (IsInvalid)
            {
                throw new Exception("The library can not be loaded.");
            }

            if (string.IsNullOrEmpty(p_funName))
            {
                throw new ArgumentException("The FunctionName is not be empty or null");
            }

            var type = p_type;
            if (!typeof(Delegate).IsAssignableFrom(type))
            {
                throw new ArgumentException("The T is not a Delegate");
            }

            var address = NativeLoader.Instance.dlsym(this.handle, p_funName);

            return address != IntPtr.Zero;
        }

        private NativeFunctonAttribute GetNativeFunctonAttribute(Type p_type)
        {
            var type = p_type;
            var attribute = type.GetCustomAttributes(typeof(NativeFunctonAttribute), false).FirstOrDefault() as NativeFunctonAttribute;

            if (attribute == null)
            {
                throw new Exception("The Attribute of [NativeFunctonAttribute] can not found.");
            }

            return attribute;
        }

        private DelegateBuilder GetDelegateBuilder()
        {
            return _delegateBuilder ?? (_delegateBuilder = new DelegateBuilder());
        }

        private Type CreateDelegateType(object[] p_params, Type p_retrunType, CallingConvention p_calling)
        {
            var builder = GetDelegateBuilder();
            var type = builder.CreateDelegateBySingle(p_params.GetTypes(), p_retrunType, CreateCustomAttributeBuilderFunc(p_calling));
            return type;
        }

        private Func<CustomAttributeBuilder[]> CreateCustomAttributeBuilderFunc(CallingConvention p_calling)
        {
            var cinfo = typeof(UnmanagedFunctionPointerAttribute).GetConstructor(new[] { typeof(CallingConvention) });

            var result = cinfo == null
                ? new CustomAttributeBuilder[0]
                : new[] { new CustomAttributeBuilder(cinfo, new object[] { p_calling }) };

            return () => result;
        }

        /// <summary>
        /// 判断函数是否存在
        /// </summary>
        /// <param name="p_funName">函数名</param>
        /// <returns></returns>
        public bool HasFunction(string p_funName)
        {
            var address = NativeLoader.Instance.dlsym(this.handle, p_funName);
            return address != IntPtr.Zero;
        }

        #endregion

        #region Dynamic

        private DynamicNative? _dynamicNative;

        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            if (_dynamicNative == null)
            {
                _dynamicNative = new DynamicNative(this);
            }

            return new DelegatingMetaObject(_dynamicNative, parameter, BindingRestrictions.GetTypeRestriction(parameter, this.GetType()), this);
        }

        #endregion

        #region SafeHandle

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            HasDisposed = true;
        }

        protected override bool ReleaseHandle()
        {
            if (_disposable != null)
            {
                _disposable.Dispose();
            }

            return NativeLoader.Instance.dlclose(this.handle) > 0; 
        }

        public override bool IsInvalid => this.handle == IntPtr.Zero;

        #endregion



    }
}
