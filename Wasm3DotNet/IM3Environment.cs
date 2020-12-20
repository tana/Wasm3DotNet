using System;
using System.Runtime.InteropServices;

namespace Wasm3DotNet
{
    public class IM3Environment : SafeHandle
    {
        IM3Environment()
            : base(IntPtr.Zero, true)
        {
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            NativeFunctions.m3_FreeEnvironment(handle);

            return true;
        }
    }
}
