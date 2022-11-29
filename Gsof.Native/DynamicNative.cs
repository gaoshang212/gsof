using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Gsof.Native
{
    public class DynamicNative : DynamicObject
    {
        private readonly INative _native;

        public DynamicNative(INative p_native)
        {
            _native = p_native;

            if (_native == null)
            {
                throw new ArgumentNullException(nameof(p_native));
            }

            if (_native.HasDisposed)
            {
                throw new ArgumentException("INative can not be null or disposed.");
            }
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] p_args, out object result)
        {
            var csharpBinder =
                binder.GetType().GetInterface("Microsoft.CSharp.RuntimeBinder.ICSharpInvokeOrInvokeMemberBinder");
            var typeArgs = csharpBinder.GetProperty("TypeArguments").GetValue(binder, null) as IList<Type>;

            Type retrunType = null;
            if (typeArgs != null && typeArgs.Any())
            {
                retrunType = typeArgs.First();
            }

            var funcName = binder.Name;
            result = _native.Invoke(funcName, retrunType, p_args);

            return true;
        }
    }
}