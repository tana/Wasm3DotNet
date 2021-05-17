using System;
using System.Collections.Generic;
using System.Text;

namespace Wasm3DotNet
{
    // Represents value type in WebAssembly.
    // Same as M3ValueType enum in wasm3.h.
    public enum M3ValueType
    {
        None = 0,
        I32 = 1,
        I64 = 2,
        F32 = 3,
        F64 = 4,
        Unknown
    }
}
