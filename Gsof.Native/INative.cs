using System;
using System.Runtime.InteropServices;

namespace Gsof.Native
{
    public interface INative : IDisposable
    {
        bool HasDisposed { get; }

        /// <summary>
        /// 获取函数委托
        /// </summary>
        /// <typeparam name="TDelegate"></typeparam>
        /// <returns></returns>
        TDelegate GetFunction<TDelegate>();

        /// <summary>
        /// 判断函数是否存在
        /// </summary>
        /// <typeparam name="TDelegate"></typeparam>
        /// <returns></returns>
        bool HasFunction<TDelegate>();

        /// <summary>
        /// 函数委托调用方式
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <typeparam name="TDelegate">函数对应的委托类型</typeparam>
        /// <param name="p_params">函数传参</param>
        /// <returns></returns>
        TResult Invoke<TResult, TDelegate>(params object[] p_params);

        /// <summary>
        /// 函数名调用
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="p_funName">函数名</param>
        /// <param name="p_params">函数传参</param>
        /// <returns></returns>
        TResult Invoke<TResult>(string p_funName, params object[] p_params);

        /// <summary>
        /// 函数名调用
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="p_funName">函数名</param>
        /// <param name="p_calling">调用转换方式（同PInvoke CallingConvention）</param>
        /// <param name="p_params">函数传参</param>
        /// <returns></returns>
        TResult Invoke<TResult>(string p_funName, CallingConvention p_calling, params object[] p_params);

        /// <summary>
        /// 函数名调用(非泛型)
        /// </summary>
        /// <param name="p_funName">函数名</param>
        /// <param name="p_retrunType">返回值类型</param>
        /// <param name="p_params">函数传参</param>
        /// <returns></returns>
        object Invoke(string p_funName, Type p_retrunType, params object[] p_params);

        /// <summary>
        /// 函数委托调用方式
        /// </summary>
        /// <typeparam name="TDelegate">函数对应的委托类型</typeparam>
        /// <param name="p_params">函数传参</param>
        void Call<TDelegate>(params object[] p_params);

        /// <summary>
        /// 函数名调用
        /// </summary>
        /// <param name="p_funName">函数名</param>
        /// <param name="p_params">函数传参</param>
        void Call(string p_funName, params object[] p_params);

        /// <summary>
        /// 函数名调用
        /// </summary>
        /// <param name="p_funName">函数名</param>
        /// <param name="p_calling">调用转换方式（同PInvoke CallingConvention）</param>
        /// <param name="p_params">函数传参</param>
        void Call(string p_funName, CallingConvention p_calling, params object[] p_params);

        /// <summary>
        /// 判断函数是否存在
        /// </summary>
        /// <param name="p_funName">函数名</param>
        /// <returns></returns>
        bool HasFunction(string p_funName);
    }
}