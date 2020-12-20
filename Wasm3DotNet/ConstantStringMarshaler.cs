using System;
using System.Runtime.InteropServices;

namespace Wasm3DotNet
{
    // For Native-to-Managed marshaling only.
    class ConstantStringMarshaler : ICustomMarshaler
    {
        public void CleanUpManagedData(object ManagedObj)
        {
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
        }

        public int GetNativeDataSize()
        {
            return IntPtr.Size;
        }

        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            throw new NotImplementedException();
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            if (pNativeData == IntPtr.Zero)
                return null;
            else
                return Marshal.PtrToStringAnsi(pNativeData);
        }

        public static ICustomMarshaler GetInstance(string cookie) => new ConstantStringMarshaler();
    }
}
