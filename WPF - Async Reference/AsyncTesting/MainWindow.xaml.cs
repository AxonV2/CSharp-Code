using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncTesting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExecuteSync(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            RunDownloadSync();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            L1.Content += $"{elapsedMs}";

        }


        //CAN ONLY RETURN VOID BECAUSE ITS AN EVENT 
        private async void ExecuteAsync(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            //will keep running past this point unless we await here as well 
            //await RunDownloadASync();

            await RunDownloadParallelASync();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            //this would have shown up 1st without await 
            L1.Content += $"{elapsedMs}";
        }

        private List<string> PrepData()
        {
            List<string> output = new List<string>();
            L1.Content = "";

            output.Add("https://www.yahoo.com/");
            output.Add("https://www.google.com/");
            output.Add("https://www.microsoft.com/");
            output.Add("https://www.microsoft.com/");
            output.Add("https://www.cnn.com/");
            output.Add("https://www.codeproject.com/");
            output.Add("https://www.stackoverflow.com/");
            return output;
        }

        private void RunDownloadSync()
        {
            List<string> Websites = PrepData();

            foreach (string site in Websites)
            {
                WebsiteDataModel results = DownloadWebsite(site);
                ReportWebsiteInfo(results);
            }

        }

        //do not return voids on AsyncMethods return Task<string/int/whatever> instead
        //if you dont have anything to return just say Task
        private async Task RunDownloadASync()
        {
            List<string> Websites = PrepData();

            foreach (string site in Websites)
            {
                //await because you cant report without having the results 
                //will do one at a time
                WebsiteDataModel results = await Task.Run(() => DownloadWebsite(site));
                ReportWebsiteInfo(results);
            }
        }


        private async Task RunDownloadParallelASync()
        {
            List<string> Websites = PrepData();
            List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();
            //Task<WebsiteDataModel>[] tasks2 = new Task<WebsiteDataModel>[100];

            foreach (string site in Websites)
                //Task returns Task 
                //does not do them one at a time now, so it is much faster
                tasks.Add(Task.Run(() => DownloadWebsite(site)));

            //becomes array of WebsiteDataModel
            //when all tasks are done grab the results and report them
            var results = await Task.WhenAll(tasks);

            foreach (var item in results)
                //item.WebsiteURL
                ReportWebsiteInfo(item);
        }


        private WebsiteDataModel DownloadWebsite(string web)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            output.WebsiteURL = web;
            output.WebsiteData = client.DownloadString(web);
            return output;
        }

        private void ReportWebsiteInfo(WebsiteDataModel data)
        {
            L1.Content += $"{data.WebsiteURL} downloaded {data.WebsiteData.Length} characters long. \n";
        }
    }
}
