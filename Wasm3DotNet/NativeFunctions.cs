using System;
using System.Runtime.InteropServices;

namespace Wasm3DotNet
{
    public class NativeFunctions
    {
        // CallingConvention.Cdecl is needed to avoid "Stack Imbalance" error
        // See: https://blog.janjan.net/2017/08/08/csharp-pinvoke-stackimbalance/

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        public static extern IM3Environment m3_NewEnvironment();

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        public static extern void m3_FreeEnvironment(IM3Environment environment);

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void m3_FreeEnvironment(IntPtr environment);

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        public static extern IM3Runtime m3_NewRuntime(IM3Environment enviroment, uint stackSizeInBytes, IntPtr unused);

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        public static extern void m3_FreeRuntime(IM3Runtime runtime);

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void m3_FreeRuntime(IntPtr runtime);

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr m3_GetMemory(IM3Runtime runtime, out uint memorySizeInBytes, uint memoryIndex);

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstantStringMarshaler))]
        public static extern string m3_ParseModule(IM3Environment environment, out IM3Module module, byte[] wasmBytes, uint numWasmBytes);

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        public static extern void m3_FreeModule(IM3Module module);

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstantStringMarshaler))]
        public static extern string m3_LoadModule(IM3Runtime runtime, IM3Module module);

        //[DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        //public static extern string m3_LinkRawFunction

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstantStringMarshaler))]
        public static extern string m3_Yield();

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstantStringMarshaler))]
        public static extern string m3_FindFunction(out IM3Function function, IM3Runtime runtime, string functionName);

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        public static extern string m3_Call(IM3Function function);

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        public static extern string m3_CallWithArgs(IM3Function function, uint argc, string[] argv);

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        public static extern void m3_GetErrorInfo(IM3Runtime runtime, out M3ErrorInfo info);

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        public static extern void m3_ResetErrorInfo(IM3Runtime runtime);

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        public static extern void m3_PrintRuntimeInfo(IM3Runtime runtime);

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        public static extern void m3_PrintM3Info();

        [DllImport("wasm3", CallingConvention = CallingConvention.Cdecl)]
        public static extern void m3_PrintProfilerInfo();
    }
}
