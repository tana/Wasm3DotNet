using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Wasm3DotNet
{
    /// <summary>
    /// Represents a C# function that is called from WASM.
    /// Low-level access (like macros in m3_api_defs.h) is needed.
    /// </summary>
    /// <param name="runtime">Pointer to IM3Runtime (Currently there is no way to convert it into C# IM3Runtime object)</param>
    /// <param name="sp">WASM stack for parameters and result</param>
    /// <param name="mem"></param>
    /// <returns>IntPtr.Zero should be returned when succeeded.</returns>
    // The attribute is needed for passing a C# function as a "cdecl" function pointer.
    // See: https://blog.sgry.jp/entry/2006/04/22/000000
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr M3RawCall(IntPtr runtime, IntPtr sp, IntPtr mem);
}
