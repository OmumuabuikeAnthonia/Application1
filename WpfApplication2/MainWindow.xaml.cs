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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace WpfApplication2
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Window Quiz = new Quiz();
            Quiz.Show();
            QuesAnsw.strName = txtName.Text;
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnScores_Click(object sender, RoutedEventArgs e)
        {
            Window Scores = new Scores();
            Scores.Show();
            this.Close();
        }

        private void btnResume_Click(object sender, RoutedEventArgs e)
        {
            QuesAnsw.quizResume = true;
            Window Quiz = new Quiz();

            Quiz.Show();
            QuesAnsw.strName = txtName.Text;


            this.Close();
        }

        private void Start_Screen_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists("Progress"))
            {
                btnResume.IsEnabled = true;
            }
            else
            {
                btnResume.IsEnabled = true;`
            }
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("African Capitals Game - C# Edition - Version 6.0\nLast update: Aug 8th, 2020\nCreated by : Anthonia O  Omumuabuike\nContact me: toniabuike@gmail.com");
        }
    }
}
