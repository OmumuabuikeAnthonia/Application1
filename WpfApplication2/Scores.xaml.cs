using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for Scores.xaml
    /// </summary>
    public partial class Scores : Window
    {
        public Scores()
        {
            InitializeComponent();
            if (File.Exists("Scores"))
            {
                LoadScores();
            }
            else
            {
                File.Create("Scores");
            }
        }
        private void LoadScores()
        {
            string path = "Scores";
            StreamReader sr = new StreamReader(path);

            for (int i = 0; sr.EndOfStream == false; i++) { 
                listbox.Items.Add(sr.ReadLine());
            }

            sr.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e) { 
            Window startscreen = new MainWindow();
            startscreen.Show();
            this.Close();
        }
    }
}
