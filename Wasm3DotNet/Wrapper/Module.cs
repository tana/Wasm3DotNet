using System;
using System.Collections.Generic;
using System.Text;

namespace Wasm3DotNet.Wrapper
{
    public class Module
    {
        internal readonly IM3Module Handle;

        internal Module(IM3Module handle)
        {
            Handle = handle;
        }

        public void LinkRawFunction(string moduleName, string functionName, string signature, M3RawCall function)
        {
            var result = NativeFunctions.m3_LinkRawFunction(Handle, moduleName, functionName, signature, function);
            if (result != null)
            {
                new Wasm3Exception(result);
            }
        }
    }
}
