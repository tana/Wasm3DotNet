using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using Wasm3DotNet;

namespace Wasm3DotNetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            NativeFunctions.m3_PrintM3Info();

            using (var environment = NativeFunctions.m3_NewEnvironment())
            using (var runtime = NativeFunctions.m3_NewRuntime(environment, 65536, IntPtr.Zero))
            {
                NativeFunctions.m3_PrintRuntimeInfo(runtime);

                string result;

                IM3Module module;

                byte[] wasmData = File.ReadAllBytes(@"../../test.wasm");
                result = NativeFunctions.m3_ParseModule(environment, out module, wasmData, (uint)wasmData.Length);
                if (result != null)
                {
                    throw new Exception(result);
                }

                result = NativeFunctions.m3_LoadModule(runtime, module);
                if (result != null)
                {
                    throw new Exception(result);
                }

                result = NativeFunctions.m3_LinkRawFunction(module, "externals", "print_add", "i(ii)", Output);
                if (result != null)
                {
                    throw new Exception(result);
                }

                IM3Function function;
                result = NativeFunctions.m3_FindFunction(out function, runtime, "test");
                if (result != null)
                {
                    throw new Exception(result);
                }

                result = NativeFunctions.m3_CallWithArgs(function, 1, new[] { "10" });
                Console.WriteLine(result);
            }

            Console.ReadKey();
        }

        static IntPtr Output(IntPtr runtime, IntPtr sp, IntPtr mem)
        {
            // Read values from WASM stack.
            // See: m3_api_defs.h in wasm3
            var x = Marshal.ReadInt32(sp);
            var y = Marshal.ReadInt32(sp, 8);
            Console.WriteLine($"x={x}, y={y}");
            // Write result to WASM stack.
            Marshal.WriteInt32(sp, x + y);

            return IntPtr.Zero; // No error
        }
    }
}
