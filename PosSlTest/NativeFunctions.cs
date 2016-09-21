using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PosSlTest
{
    public static class NativeFunctions
    {
        public const string MY_DLL_PATH = @"c:\project\150521_DotNetTest\MyDll\Debug\MyDll.dll";

        [DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern MessageBoxResult MessageBox(IntPtr hWnd, String text, String caption, int options);

        [DllImport("kernel32.dll")]
        public static extern uint WinExec(string lpCmdLine, uint uCmdShow);

        //MYDLL_API int GetStringLength(LPCTSTR sz)
        [DllImport(MY_DLL_PATH, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetStringLength(string sz);



        //MYDLL_API char* GetSampleString();
        [DllImport(MY_DLL_PATH, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string GetSampleString();

        //MYDLL_API wchar_t* GetSampleWString();
        [DllImport(MY_DLL_PATH, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPWStr)]
        public static extern string GetSampleWString();



        //typedef void(*OnCallbackDelegate) (wchar_t*);
        public delegate void OnCallbackDelegate(string message);

        //MYDLL_API void SayHello(wchar_t* wName, OnCallbackDelegate callback)
        [DllImport(MY_DLL_PATH, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void SayHello(string name, OnCallbackDelegate callback);

        //MYDLL_API void SayHello(wchar_t* wName, OnCallbackDelegate callback)
        [DllImport(MY_DLL_PATH, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPWStr)]
        public static extern void SayHelloW(string wName, OnCallbackDelegate callback);
    }
}
