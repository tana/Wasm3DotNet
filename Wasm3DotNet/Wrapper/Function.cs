using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Wasm3DotNet.Wrapper
{
    public class Function
    {
        internal IM3Function Handle;
        public readonly int ArgCount;

        internal Function(IM3Function handle)
        {
            Handle = handle;

            ArgCount = (int)NativeFunctions.m3_GetArgCount(Handle);
        }

        public unsafe void Call(params object[] args)
        {
            // Check number of arguments
            if (args.Length != ArgCount)
            {
                throw new ArgumentException($"Wrong number of arguments: expected ${ArgCount}, but got ${args.Length}");
            }

            const int bytesPerArg = 8;

            var argPtrs = new IntPtr[bytesPerArg * ArgCount];
            // Allocate a buffer to store arguments
            var buf = stackalloc byte[bytesPerArg * ArgCount];

            var i = 0;
            foreach (var arg in args)
            {
                var ptr = buf + bytesPerArg * i;
                switch (arg)
                {
                    case int intArg:
                        *(int*)ptr = intArg;
                        break;
                    case long longArg:
                        *(long*)ptr = longArg;
                        break;
                    case float floatArg:
                        *(float*)ptr = floatArg;
                        break;
                    case double doubleArg:
                        *(double*)ptr = doubleArg;
                        break;
                    default:
                        throw new ArgumentException($"Argument #{i} has unsupported type: {arg.GetType()}");
                }
                argPtrs[i] = (IntPtr)ptr;
                i++;
            }

            var result = NativeFunctions.m3_Call(Handle, (uint)ArgCount, argPtrs);
            if (result != null)
            {
                throw new Wasm3Exception(result);
            }

            // TODO: currently, return value is not returned because it is not easy.
            // Reading result of a WASM function requires accessing stack using internal knowledge of a runtime struct.
            // See: https://github.com/wasm3/wasm3/blob/824ce5d9e11b800d823703c556732f30eb80a940/platforms/cpp/wasm3_cpp/include/wasm3_cpp.h#L316
        }
    }
}
