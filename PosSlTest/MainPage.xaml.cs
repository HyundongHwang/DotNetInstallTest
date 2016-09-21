using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;

namespace PosSlTest
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.BtnPInvoke.Click += _BtnPInvoke_OnClick;
            this.BtnPInvoke2.Click += _BtnPInvoke2_Click;
            this.BtnPInvoke3.Click += BtnPInvoke3_Click;

            this.BtnPinvokeGetSampleString.Click += BtnPinvokeGetSampleString_OnClick;
            this.BtnPinvokeGetSampleStringW.Click += BtnPinvokeGetSampleStringW_OnClick;
            this.BtnPinvokeSayHello.Click += BtnPinvokeSayHello_OnClick;
            this.BtnPinvokeSayHelloW.Click += BtnPinvokeSayHelloW_OnClick;

            this.BtnReadFile.Click += BtnReadFile_Click;
            this.BtnWriteFile.Click += BtnWriteFile_Click;
            this.BtnUploadImage.Click += BtnUploadImage_Click;
        }

        void BtnPinvokeGetSampleString_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var result = NativeFunctions.GetSampleString();
            MessageBox.Show("NativeFunctions.GetSampleString() : " + result);
        }

        void BtnPinvokeGetSampleStringW_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var result = NativeFunctions.GetSampleWString();
            MessageBox.Show("NativeFunctions.GetSampleString() : " + result);
        }

        void BtnPinvokeSayHello_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            NativeFunctions.SayHello("황현동", OnCallback);
        }

        void BtnPinvokeSayHelloW_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            NativeFunctions.SayHelloW("황현동", OnCallback);
        }

        [AllowReversePInvokeCalls]
        public static void OnCallback(string message)
        {
            MessageBox.Show("OnCallback : " + message);
        }

        void BtnPInvoke3_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            var count = NativeFunctions.GetStringLength("hello안녕하세요");
            MessageBox.Show("count : " + count);
        }

        void BtnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            upload_file();
        }

        void BtnWriteFile_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(@"c:\temp\test.txt", "hello world", Encoding.UTF8);
        }

        void BtnReadFile_Click(object sender, RoutedEventArgs e)
        {
            var readAllText = File.ReadAllText(@"c:\temp\test.txt", Encoding.UTF8);
            MessageBox.Show(readAllText);
        }

        void _BtnPInvoke_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var result = NativeFunctions.MessageBox(IntPtr.Zero, "win32 messagebox", "win32 messagebox", 0);
        }

        void _BtnPInvoke2_Click(object sender, RoutedEventArgs e)
        {
            NativeFunctions.WinExec("notepad.exe", 5);
        }

        private async Task run_async()
        {
            var restClient = new RestClient("");
            await TaskEx.Run(() => { Thread.Sleep(5000); });
        }

        const string _HOST_NAME = "http://bookohalpha.cloudapp.net/";

        public void upload_file()
        {
            try
            {
                {
                    var client = new RestClient(_HOST_NAME);
                    var request = new RestRequest("api/files", Method.POST);
                    Debug.WriteLine("Environment.CurrentDirectory : " + Environment.CurrentDirectory);
                    var testJpgBuf = File.ReadAllBytes("test.png");
                    request.AddFile("file", testJpgBuf, "file");
                    var response = client.ExecuteAsync(request, restResponse =>
                    {
                        NativeFunctions.WinExec("open " + restResponse.Content, 5);
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ex : " + ex);
            }
        }
    }
}
