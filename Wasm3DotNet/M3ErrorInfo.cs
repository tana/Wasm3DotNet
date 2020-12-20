using System;
using System.Runtime.InteropServices;

namespace Wasm3DotNet
{
    [StructLayout(LayoutKind.Sequential)]
    public struct M3ErrorInfo
    {
        string result;
        IM3Runtime runtime;
        IM3Module module;
        IM3Function function;
        string file;
        uint line;
        string message;
    }
}
