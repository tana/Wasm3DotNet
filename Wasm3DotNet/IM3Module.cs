using System;
using System.Runtime.InteropServices;

namespace Wasm3DotNet
{
    public class IM3Module : SafeHandle
    {
        IM3Module()
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
