using System;
using System.Runtime.InteropServices;

namespace Wasm3DotNet
{
    public class IM3Function : SafeHandle
    {
        IM3Function()
            : base(IntPtr.Zero, false)
        {
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            return true;
        }
    }
}
