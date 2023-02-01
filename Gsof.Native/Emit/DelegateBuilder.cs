using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using Gsof.Extensions;

namespace Gsof.Emit
{
    public class DelegateBuilder
    {
        private const string DefaultAssemblyName = "Default.Delegates";
        private const string DefaultModuleName = "Default.Delegates.DynamicDelegate";
        //private const string DefaultDllName = DefaultAssemblyName + ".dll";

        private Dictionary<string, Type?> _dic = new Dictionary<string, Type?>();

        private ModuleBuilder? _moduleBuilder;

        public DelegateBuilder()
        {
            CreateModule();
        }

        public DelegateBuilder(ModuleBuilder p_moduleBuilder)
        {
            _moduleBuilder = p_moduleBuilder;
        }

        protected ModuleBuilder CreateModule()
        {
            if (_moduleBuilder == null)
            {
                var assemblyName = new AssemblyName(DefaultAssemblyName);
#if !NET40
                var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
#else
                var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
#endif
                _moduleBuilder = assemblyBuilder.DefineDynamicModule(DefaultModuleName);
            }

            return _moduleBuilder;
        }

        public Type? CreateDelegate(Type[] p_paramTypes, Type p_retrunType, Func<CustomAttributeBuilder[]> p_func)
        {
            var paramTypes = p_paramTypes;
            var retrunType = p_retrunType;

            var md5 = CreateSign(paramTypes, retrunType);

            return InternalCreateDelegate(md5, paramTypes, retrunType, p_func);
        }

        public Type? InternalCreateDelegate(string p_sign, Type[]? p_paramTypes, Type? p_retrunType, Func<CustomAttributeBuilder[]> p_func)
        {
            var paramTypes = p_paramTypes;
            var retrunType = p_retrunType;

            if (CheckSign(p_sign))
            {
                throw new ArgumentException("The delegate is exsit. the same name and same paramters.");
            }

            return InternalCreateDelegate(paramTypes, retrunType, p_func);
        }

        protected Type? InternalCreateDelegate(Type[]? p_paramTypes, Type? p_retrunType, Func<CustomAttributeBuilder[]> p_func)
        {
            var mbuilder = _moduleBuilder;
            if (mbuilder == null)
            {
                throw new ArgumentException("ModuleBuilder is null");
            }

            var typeName = CreateClassName();

            var delegateBuilder = mbuilder.DefineType(typeName, TypeAttributes.Public | TypeAttributes.AutoLayout | TypeAttributes.AnsiClass | TypeAttributes.Sealed, typeof(MulticastDelegate));

            foreach (var cbuilder in p_func())
            {
                delegateBuilder.SetCustomAttribute(cbuilder);
            }

            var constructorBuilder = delegateBuilder.DefineConstructor(MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, CallingConventions.Standard, new[] { typeof(object), typeof(IntPtr) });
            constructorBuilder.SetImplementationFlags(MethodImplAttributes.Runtime);

            var methodBuilder = delegateBuilder.DefineMethod("Invoke", MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, CallingConventions.Standard, p_retrunType, p_paramTypes);
            methodBuilder.SetImplementationFlags(MethodImplAttributes.Runtime);

            var delegateType = delegateBuilder.CreateType();

            return delegateType;
        }

        public Type? CreateDelegateBySingle(Type[]? p_paramTypes, Type? p_retrunType, Func<CustomAttributeBuilder[]> p_func)
        {
            var paramTypes = p_paramTypes ?? new Type[0];
            var retrunType = p_retrunType;

            var md5 = CreateSign(paramTypes, retrunType);

            if (_dic.ContainsKey(md5))
            {
                return _dic[md5];
            }

            var type = InternalCreateDelegate(md5, paramTypes, retrunType, p_func);
            _dic.Add(md5, type);

            return type;
        }

        private bool CheckSign(string p_sign)
        {
            return _dic.TryGetValue(p_sign, out _);
        }

        private string CreateSign(Type?[] p_paramTypes, Type? p_retrunType)
        {
            var paramTypes = p_paramTypes;
            var retrunType = p_retrunType;

            var paramters = paramTypes;
            if (p_retrunType != null)
            {
                paramters = new Type?[paramTypes.Length + 1];
                paramTypes.CopyTo(paramters, 0);

                paramters[paramTypes.Length] = retrunType;
            }

            var prstring = string.Join("", paramters.Select(i => i?.ToString()));
            var md5 = Encoding.UTF8.GetBytes(prstring).ToMd5().ToHex();

            return md5;
        }

        private int _index = 0;

        [MethodImpl(MethodImplOptions.Synchronized)]
        private string CreateClassName()
        {
            return $"T_Delegate_{_index++}";
        }
    }
}
