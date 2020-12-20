using System;
using System.Collections.Generic;
using System.Text;

namespace Wasm3DotNet.Wrapper
{
    public class Wasm3Exception : Exception
    {
        public Wasm3Exception(string message) : base(message)
        {
        }
    }
}
