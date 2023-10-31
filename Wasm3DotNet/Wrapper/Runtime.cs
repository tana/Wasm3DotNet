using System;

namespace Wasm3DotNet.Wrapper
{
    public class Runtime : IDisposable
    {
        internal readonly IM3Runtime Handle;

        readonly Environment environment;

        public Runtime(Environment environment, uint stackSize = 65536)
        {
            Handle = NativeFunctions.m3_NewRuntime(environment.Handle, stackSize, IntPtr.Zero);
            this.environment = environment;
        }

        public void Dispose()
        {
            Handle.Dispose();
        }

        public Module ParseModule(byte[] wasm)
        {
            IM3Module moduleHandle;
            var result = NativeFunctions.m3_ParseModule(environment.Handle, out moduleHandle, wasm, (uint)wasm.Length);
            if (result != null)
            {
                throw new Wasm3Exception(result);
            }

            return new Module(moduleHandle);
        }

        public void LoadModule(Module module)
        {
            var result = NativeFunctions.m3_LoadModule(Handle, module.Handle);
            if (result != null)
            {
                throw new Wasm3Exception(result);
            }
        }

        public Function FindFunction(string name)
        {
            IM3Function funcHandle;
            var result = NativeFunctions.m3_FindFunction(out funcHandle, Handle, name);
            if (result != null)
            {
                throw new Wasm3Exception(result);
            }

            return new Function(funcHandle);
        }

        public IntPtr GetMemory(out uint memoryByteSize, uint memoryIndex = 0)
        {
            if (memoryIndex != 0) throw new Wasm3Exception("Only memory 0 supported currently");
            return NativeFunctions.m3_GetMemory(Handle, out memoryByteSize, memoryIndex);
        }
    }
}
