using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace DoesGetFunctionPointerForDelegateHonorConvention
{
    public static class Program
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int CdeclCallback(int a, int b);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int StdcallCallback(int a, int b);

        [DllImport("NativeCode", CallingConvention = CallingConvention.Cdecl)]
        private static extern int TakesCdecl(CdeclCallback callback);

        [DllImport("NativeCode", CallingConvention = CallingConvention.StdCall)]
        private static extern int TakesStdcall(StdcallCallback callback);

        [DllImport("NativeCode", CallingConvention = CallingConvention.Cdecl, EntryPoint = "TakesCdecl")]
        private static extern int TakesCdecl2(IntPtr callback);

        [DllImport("NativeCode", CallingConvention = CallingConvention.StdCall, EntryPoint = "TakesStdcall")]
        private static extern int TakesStdcall2(IntPtr callback);

        private static int TestCallback(int a, int b)
        {
            int ret = a + b + 1000;
            Console.WriteLine($"Callback({a}, {b}) => {ret}");

            if (a != 7 || b != 42)
            { Debugger.Break(); }

            return ret;
        }

        public static void Main(string[] args)
        {
            CdeclCallback cdecl = TestCallback;
            StdcallCallback stdcall = TestCallback;
            IntPtr cdeclPtr = Marshal.GetFunctionPointerForDelegate(cdecl);
            IntPtr stdcallPtr = Marshal.GetFunctionPointerForDelegate(stdcall);

            Console.WriteLine(TakesCdecl(cdecl));
            Console.WriteLine(TakesStdcall(stdcall));
            Console.WriteLine(TakesCdecl2(cdeclPtr));
            Console.WriteLine(TakesStdcall2(stdcallPtr));

            Console.WriteLine("Press enter to cross the callbacks, will cause a crash due to stack imbalance.");
            Console.ReadLine();

            Console.WriteLine(TakesCdecl2(stdcallPtr));
            Console.WriteLine(TakesStdcall2(cdeclPtr));

            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}
