# Wasm3DotNet
A .NET binding for [wasm3](https://github.com/wasm3/wasm3) WebAssembly interpreter.

## Structure
Wasm3DotNet consists of two layers:
- Direct, (almost) one-to-one mapping of `wasm3` C API using P/Invoke ([Wasm3DotNet.NativeFunctions](https://github.com/tana/Wasm3DotNet/blob/master/Wasm3DotNet/NativeFunctions.cs))
- C#-style, class-based wrapper ([Wasm3DotNet.Wrapper namespace](https://github.com/tana/Wasm3DotNet/tree/master/Wasm3DotNet/Wrapper))

## Building
1. Open `Wasm3DotNet.sln` in Visual Studio.
2. Build `wasm3_dll` (native code library)
3. Build `Wasm3DotNet` (managed code library)
4. To test the demo application, build and run `Wasm3DotNetDemo`.

## Usage
After build, two important files will be generated.
- `wasm3_dll/Debug/wasm3.dll` is the native code library that contains wasm3 interpreter.
- `Wasm3DotNet/bin/Debug/netstandard2.0/Wasm3DotNet.dll` is the managed (.NET) library.

(These paths are relative to the root of this repository. `Debug` is replaced to `Release` for release build)

To use Wasm3DotNet for your project, add reference to `Wasm3DotNet.dll`, and copy `wasm3.dll` to executable directory (or native plugin folder for Unity).
