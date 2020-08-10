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
    /// Interaction logic for Review.xaml
    /// </summary>
    public partial class Review : Window
    {
        string strScore;

        public Review()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window MainWindow = new MainWindow();
            MainWindow.Show();
            listMissed.Items.Clear();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            //gets users score
            int intScore = QuesAnsw.TestScore();
            TimeSpan ts = TimeSpan.FromSeconds(Quiz.TimeLeft());
            TimeSpan tg = TimeSpan.FromSeconds(Quiz.intTime);
            TimeSpan tl = tg - ts;
            strScore = String.Format("{0} correctly answered {1}/{2} questions in {3}min and {4}sec",
                QuesAnsw.strName, intScore, QuesAnsw.strQuestions.GetUpperBound(0), tl.Minutes, tl.Seconds);
            textBlock.Text = strScore;
            missedquestions();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            File.AppendAllText("Scores", strScore + Environment.NewLine);
        }

        private void missedquestions()
        {
            for (int i = 0; i <= QuesAnsw.strQuestions.GetUpperBound(0); i++)
            {
                if (QuesAnsw.strQuestions[i, 5] != QuesAnsw.strQuestions[i, 6])
                {
                    listMissed.Items.Add(QuesAnsw.strQuestions[i, 0]);
                }
            }

        }
    }
}

