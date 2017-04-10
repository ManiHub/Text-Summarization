using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace NLP_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // globals
        static int counter = 0;
        static Articles artlist = null;
        static string[] indextracker = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_update_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedtag = ((System.Windows.FrameworkElement)cbox.SelectedValue).Tag.ToString();

            switch (selectedtag)
            {
                case "C":
                    //readJson("Sample.json");
                    readJson("udayavani_cinema_news.json");
                    break;
                case "P":
                    readJson("udayavani_sports_news.json");
                    break;
                case "S":
                    readJson("udayavani_state_news.json");
                    break;
            }

        }

        bool readJson(String filename)
        {
            try
            {
                string jsontext = File.ReadAllText(filename, Encoding.UTF8);

                if (jsontext.Length > 0)
                {
                    artlist = JsonConvert.DeserializeObject<Articles>(jsontext);

                    Updatecontent(0);
                }
                
                

            }
            catch(Exception exp)
            {

            }

            return true;
        }

        public void Updatecontent(int index)
        {
            if(index>= 0 && artlist!=null && artlist.articles!=null && artlist.articles.Count > 0 && artlist.articles.Count>index)
            {
                Article art = artlist.articles[index];

                string[] content = art.content.Split('.');

                grid_content.Children.Clear();

                url.Content ="URL : "+ art.url.ToString();
                title.Content ="Title : "+ art.title.ToString();

                for (int i = 0; i < content.Length; i++)
                {
                    CheckBox cb = new CheckBox();
                    cb.Name = "CB_"+i.ToString();
                    cb.Content = content[i];
                    cb.Margin = new System.Windows.Thickness { Left = 10, Top = (i+1)*20, Right = 0, Bottom = 0 };
                    cb.Checked += Cb_Checked;
                    cb.Unchecked += Cb_Unchecked;
                    grid_content.Children.Add(cb);
                }

                indextracker = new string[content.Length];
            }
        }

        private void Cb_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            string name = cb.Name.ToString().Split('_')[1];

            int index = Convert.ToInt16(name);

            indextracker[index] = "";
        }

        private void Cb_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            string name = cb.Name.ToString().Split('_')[1];

            int index = Convert.ToInt16(name);

            indextracker[index] = cb.Content.ToString();
            
        }

        
        private void btn_json_Click(object sender, RoutedEventArgs e)
        {
            if (indextracker.Length > 0)
            {
                string summary = "";

                for (int i = 0; i < indextracker.Length; i++)
                {
                    if (indextracker[i] != null && indextracker[i].Length > 0)
                    {
                        summary += indextracker[i];
                    }
                }

                if (summary.Length > 0)
                {
                    artlist.articles[counter].summary = summary;

                    string jsontext = JsonConvert.SerializeObject(artlist);

                    File.WriteAllText("Sample.json", jsontext);
                }
            }
        }

        private void btn_next_Click(object sender, RoutedEventArgs e)
        {
            if (artlist != null && artlist.articles != null && artlist.articles.Count > counter)
            {
                counter++;
                Updatecontent(counter);
            }
        }

        private void btn_previous_Click(object sender, RoutedEventArgs e)
        {
            if (artlist != null && artlist.articles != null && artlist.articles.Count > counter && counter > 0)
            {
                counter--;
                Updatecontent(counter);
            }
        }
    }
}
