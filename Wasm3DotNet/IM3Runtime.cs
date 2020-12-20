using System;
using System.Runtime.InteropServices;

namespace Wasm3DotNet
{
    public class IM3Runtime : SafeHandle
    {
        IM3Runtime()
            : base(IntPtr.Zero, true)
        {
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            NativeFunctions.m3_FreeRuntime(handle);

            return true;
        }
    }
}
