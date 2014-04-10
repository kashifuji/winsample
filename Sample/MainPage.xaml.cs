using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Sample
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            test();
            //this.Frame.Navigate(typeof(BlankPage1), null);
        }
        
        private async void test() {
            Uri uri = new Uri("http://localhost:3000/con1/form2");
            var client = new HttpClient();
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("user", "test"));
            HttpContent content = new FormUrlEncodedContent(postData);

            try
            {
//                HttpResponseMessage response = await client.GetAsync(uri);
                HttpResponseMessage response = await client.PostAsync(uri, content);
                string str = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("response body is [" + str);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(str);
                XmlElement elem = doc.DocumentElement;
                XmlNodeList oldNode = elem.GetElementsByTagName("old");
                System.Diagnostics.Debug.WriteLine("Name; " + oldNode[0].InnerText);  
            }
            catch { }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
