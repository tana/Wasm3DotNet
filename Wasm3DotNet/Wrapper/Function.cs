using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Wasm3DotNet.Wrapper
{
    public class Function
    {
        internal IM3Function Handle;

        internal Function(IM3Function handle)
        {
            Handle = handle;
        }

        public string CallWithArgs(string[] args)
        {
            return NativeFunctions.m3_CallWithArgs(Handle, (uint)args.Length, args);
        }

        public void Call(params object[] args)
        {
            var strArgs = new string[args.Length];
            var i = 0;
            var ifc = new IntFloatConverter();
            var ldc = new LongDoubleConverter();
            foreach (var arg in args)
            {
                // Convert arguments to "human-unfriendly" argument format
                // See: Implementation of m3_CallWithArgs in m3_env.c
                switch (arg)
                {
                    case int intArg:
                        ifc.Int = intArg;
                        strArgs[i] = ifc.UInt.ToString();
                        break;
                    case long longArg:
                        ldc.Long = longArg;
                        strArgs[i] = ldc.ULong.ToString();
                        break;
                    case float floatArg:
                        ifc.Float = floatArg;
                        strArgs[i] = ifc.UInt.ToString();
                        break;
                    case double doubleArg:
                        ldc.Double = doubleArg;
                        strArgs[i] = ldc.ULong.ToString();
                        break;
                    default:
                        throw new ArgumentException($"Argument #{i} has unsupported type: {arg.GetType()}");
                }
                i++;
            }

            var result = CallWithArgs(strArgs);
            if (result != null)
            {
                throw new Wasm3Exception(result);
            }

            // TODO: currently, return value is not returned because it is not easy.
            // Reading result of a WASM function requires accessing stack using internal knowledge of a runtime struct.
            // See: https://github.com/wasm3/wasm3/blob/824ce5d9e11b800d823703c556732f30eb80a940/platforms/cpp/wasm3_cpp/include/wasm3_cpp.h#L316
        }

        [StructLayout(LayoutKind.Explicit)]
        struct IntFloatConverter
        {
            [FieldOffset(0)]
            public uint UInt;
            [FieldOffset(0)]
            public int Int;
            [FieldOffset(0)]
            public float Float;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct LongDoubleConverter
        {
            [FieldOffset(0)]
            public ulong ULong;
            [FieldOffset(0)]
            public long Long;
            [FieldOffset(0)]
            public double Double;
        }
    }
}
