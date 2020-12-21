# Wasm3DotNet
A .NET binding for [wasm3](https://github.com/wasm3/wasm3) WebAssembly interpreter.

## Structure
Wasm3DotNet consists of two layers:
- Direct, (almost) one-to-one mapping of `wasm3` C API using P/Invoke ([Wasm3DotNet.NativeFunctions](https://github.com/tana/Wasm3DotNet/blob/master/Wasm3DotNet/NativeFunctions.cs))
- C#-style, class-based wrapper ([Wasm3DotNet.Wrapper namespace](https://github.com/tana/Wasm3DotNet/tree/master/Wasm3DotNet/Wrapper))
