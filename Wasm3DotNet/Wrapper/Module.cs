using System;
using System.Collections.Generic;
using System.Text;

namespace Wasm3DotNet.Wrapper
{
    public class Module
    {
        public delegate IntPtr LinkableFunction(IntPtr sp, IntPtr mem);

        internal readonly IM3Module Handle;

        // For preventing GC of linked functions
        private List<M3RawCall> linkedRawCalls = new List<M3RawCall>();

        internal Module(IM3Module handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Make a C# function callable from WebAssembly.
        /// The passed delegate is kept alive (not garbage-collected) as long as this Module is alive.
        /// </summary>
        /// <param name="moduleName">Module name of the created WASM function.</param>
        /// <param name="functionName">Name of the created WASM function.</param>
        /// <param name="signature">Function signature in Wasm3's format.</param>
        /// <param name="function">Delegate to the C# function.</param>
        public void LinkRawFunction(string moduleName, string functionName, string signature, M3RawCall function)
        {
            linkedRawCalls.Add(function);

            var result = NativeFunctions.m3_LinkRawFunction(Handle, moduleName, functionName, signature, function);
            if (result != null)
            {
                throw new Wasm3Exception(result);
            }
        }
    }
}
