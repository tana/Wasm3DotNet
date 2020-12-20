using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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

                byte[] wasmData = File.ReadAllBytes(@"..\..\..\wasm3_dll\wasm3\test\lang\fib.c.wasm");
                result = NativeFunctions.m3_ParseModule(environment, out module, wasmData, (uint)wasmData.Length);
                if (result != null)
                {
                    Console.WriteLine(result);
                    return;
                }

                result = NativeFunctions.m3_LoadModule(runtime, module);
                if (result != null)
                {
                    Console.WriteLine(result);
                    return;
                }

                IM3Function function;
                result = NativeFunctions.m3_FindFunction(out function, runtime, "fib");
                if (result != null)
                {
                    Console.WriteLine(result);
                    return;
                }

                result = NativeFunctions.m3_CallWithArgs(function, 1, new[] { "10" });
                Console.WriteLine(result);
            }

            Console.ReadKey();
        }
    }
}
