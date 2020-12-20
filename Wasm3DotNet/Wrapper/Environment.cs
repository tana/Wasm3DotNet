using System;
using System.Collections.Generic;
using System.Text;

namespace Wasm3DotNet.Wrapper
{
    public class Environment : IDisposable
    {
        internal readonly IM3Environment Handle;

        public Environment()
        {
            Handle = NativeFunctions.m3_NewEnvironment();
        }

        public void Dispose()
        {
            Handle.Dispose();
        }
    }
}
