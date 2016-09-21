using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PosWpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            upload_file();
        }

        const string _HOST_NAME = "https://bookohalpha.cloudapp.net/";

        public void upload_file()
        {
            try
            {
                {
                    var client = new RestClient(_HOST_NAME);
                    var request = new RestRequest("api/files", Method.POST);
                    var testJpgBuf = File.ReadAllBytes("test.png");
                    request.AddFile("file", testJpgBuf, "file");
                    var response = client.Execute(request);
                    Trace.TraceInformation("response.Content : " + response.Content);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceInformation("ex : " + ex);
            }
        }

    }
}
