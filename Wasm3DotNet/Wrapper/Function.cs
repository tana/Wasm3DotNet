using System;
using System.Collections.Generic;
using System.Text;

namespace Wasm3DotNet.Wrapper
{
    public class Function
    {
        internal IM3Function Handle;

        internal Function(IM3Function handle)
        {
            Handle = handle;
        }

        public string Call(string[] args)
        {
            return NativeFunctions.m3_CallWithArgs(Handle, (uint)args.Length, args);
        }
    }
}
