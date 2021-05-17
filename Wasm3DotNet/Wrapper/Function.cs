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
        public readonly int RetCount;

        internal Function(IM3Function handle)
        {
            Handle = handle;

            ArgCount = (int)NativeFunctions.m3_GetArgCount(Handle);
            RetCount = (int)NativeFunctions.m3_GetRetCount(Handle);
        }

        public unsafe object[] CallMultiValue(params object[] args)
        {
            // Check number of arguments
            if (args.Length != ArgCount)
            {
                throw new ArgumentException($"Wrong number of arguments: expected ${ArgCount}, but got ${args.Length}");
            }

            const int bytesPerValue = 8;

            var argPtrs = new IntPtr[ArgCount];
            // Allocate a buffer to store arguments
            var buf = stackalloc byte[bytesPerValue * ArgCount];

            for (int i = 0; i < ArgCount; i++)
            {
                var ptr = buf + bytesPerValue * i;
                switch (args[i])
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
                        throw new ArgumentException($"Argument #{i} has unsupported type: {args[i].GetType()}");
                }
                argPtrs[i] = (IntPtr)ptr;
            }

            var result = NativeFunctions.m3_Call(Handle, (uint)ArgCount, argPtrs);
            if (result != null)
            {
                throw new Wasm3Exception(result);
            }

            // Allocate a buffer to store returned values
            var retBuf = stackalloc byte[bytesPerValue * RetCount];
            var retPtrs = new IntPtr[RetCount];
            for (int i = 0; i < RetCount; i++)
            {
                retPtrs[i] = (IntPtr)(retBuf + bytesPerValue * i);
            }

            // Retrieve returned values
            result = NativeFunctions.m3_GetResults(Handle, (uint)RetCount, retPtrs);
            if (result != null)
            {
                throw new Wasm3Exception(result);
            }

            var returns = new object[RetCount];
            for (int i = 0; i < RetCount; i++)
            {
                var ptr = retPtrs[i];
                var retType = NativeFunctions.m3_GetRetType(Handle, (uint)i);
                switch (retType)
                {
                    case M3ValueType.I32:
                        returns[i] = *(int*)ptr;
                        break;
                    case M3ValueType.I64:
                        returns[i] = *(long*)ptr;
                        break;
                    case M3ValueType.F32:
                        returns[i] = *(float*)ptr;
                        break;
                    case M3ValueType.F64:
                        returns[i] = *(double*)ptr;
                        break;
                    default:
                        throw new NotImplementedException($"Unknown return type: {retType}");
                }
            }

            return returns;
        }

        public object Call(params object[] args)
        {
            var returns = CallMultiValue(args);
            return (returns.Length >= 1) ? returns[0] : null;
        }
    }
}
