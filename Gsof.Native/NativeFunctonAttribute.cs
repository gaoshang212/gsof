﻿using System;

namespace Gsof.Native
{
    [AttributeUsage(AttributeTargets.Delegate)]
    public class NativeFunctonAttribute : Attribute
    {
        public string FunctionName { get; set; } = null;

        public NativeFunctonAttribute()
        {

        }

        public NativeFunctonAttribute(string p_functionName)
        {
            FunctionName = p_functionName;
        }
    }
}