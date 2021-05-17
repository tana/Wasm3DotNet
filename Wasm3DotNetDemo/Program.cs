using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using Wasm3DotNet.Wrapper;

namespace Wasm3DotNetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var environment = new Wasm3DotNet.Wrapper.Environment())
            using (var runtime = new Runtime(environment))
            {
                byte[] wasmData = File.ReadAllBytes(@"../../test.wasm");
                var module = runtime.ParseModule(wasmData);

                runtime.LoadModule(module);

                module.LinkRawFunction("externals", "print_add", "i(ii)", Output);

                var func = runtime.FindFunction("test");

                var ret = func.Call(10);
                Console.WriteLine($"Result: {ret}");
            }

            Console.ReadKey();
        }

        static IntPtr Output(IntPtr runtime, IntPtr ctx, IntPtr sp, IntPtr mem)
        {
            // Read values from WASM stack.
            // See: m3_api_defs.h in wasm3
            var x = Marshal.ReadInt32(sp, 8);
            var y = Marshal.ReadInt32(sp, 16);
            Console.WriteLine($"x={x}, y={y}");
            // Write result to WASM stack.
            Marshal.WriteInt32(sp, x + y);

            return IntPtr.Zero; // No error
        }
    }
}
