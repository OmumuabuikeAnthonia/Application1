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
using System.Windows.Threading;
using System.IO;

namespace WpfApplication2 { 
    /// <summary>
    /// Interaction logic for Quiz.xaml
    /// </summary>
    public partial class Quiz : Window { 
        public static int intQuest = 0;
        public static int intTime = 600;
        public static int timerTickCount = 0;
        private bool quizStart = false;
        private static DispatcherTimer timer;

        public void Timer() { 
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1); // will 'tick' once every second
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e) { 
            DispatcherTimer timer = (DispatcherTimer)sender;
            TimeSpan ts = TimeSpan.FromSeconds(TimeLeft());

            //updates time text block
            tbTime.Text = string.Format("Time left: {0}:{1}", ts.Minutes, ts.Seconds);
            CommandManager.InvalidateRequerySuggested();

            timerTickCount++;
            if (intTime - timerTickCount < 0) { 
                timer.Stop();
                endquiz();
            }
        }

        public static int TimeLeft() { 
            int intTimeLeft = intTime - timerTickCount;
            return intTimeLeft;
        }

        public Quiz() { 
            InitializeComponent();
        }

        private void EnableRad() { 
            rad1.IsEnabled = true;
            rad2.IsEnabled = true;
            rad3.IsEnabled = true;
            rad4.IsEnabled = true;
        }

        private void DisableRad() { 
            rad1.IsEnabled = false;
            rad2.IsEnabled = false;
            rad3.IsEnabled = false;
            rad4.IsEnabled = false;
        }

        private void ResetRadSelection() { 
            rad1.IsChecked = false;
            rad2.IsChecked = false;
            rad3.IsChecked = false;
            rad4.IsChecked = false;

            rad1.Foreground = Brushes.White;
            rad2.Foreground = Brushes.White;
            rad3.Foreground = Brushes.White;
            rad4.Foreground = Brushes.White;
        }

        private void CurrentQuestion() { 
            string strQuestion;

            //Grabs current question from array in QuesAnsw and updates question text block
            strQuestion = QuesAnsw.strQuestions[intQuest, 0];
            tbQuestion.Text = strQuestion;

            //update radio buttons
            CurrentOptions();

            //increments question number and updates current question number in status label
            intQuest++;
            lblQuestionNum.Content = String.Format("Question {0}/{1}", intQuest, QuesAnsw.strQuestions.GetUpperBound(0) + 1); ;
        }

        private void StoreAnswer() { 
            if (rad1.IsChecked == true) { 
                QuesAnsw.strQuestions[intQuest - 1, 6] = "1";
            }
            else if (rad2.IsChecked == true) { 
                QuesAnsw.strQuestions[intQuest - 1, 6] = "2";
            }
            else if (rad3.IsChecked == true) { 
                QuesAnsw.strQuestions[intQuest - 1, 6] = "3";
            }
            else if (rad4.IsChecked == true) { 
                QuesAnsw.strQuestions[intQuest - 1, 6] = "4";
            }
        }

        private void CurrentOptions() { 
            rad1.Content = QuesAnsw.strQuestions[intQuest, 1];
            rad2.Content = QuesAnsw.strQuestions[intQuest, 2];
            rad3.Content = QuesAnsw.strQuestions[intQuest, 3];
            rad4.Content = QuesAnsw.strQuestions[intQuest, 4];
        }

        private void btnAnswer_Click(object sender, RoutedEventArgs e) { 
            int intAnswer = int.Parse(QuesAnsw.strQuestions[intQuest - 1, 5]);
            switch (intAnswer) { 
                case 1:
                    rad1.Foreground = Brushes.Orange;
                    break;
                case 2:
                    rad2.Foreground = Brushes.Orange;
                    break;
                case 3:
                    rad3.Foreground = Brushes.Orange;
                    break;
                case 4:
                    rad4.Foreground = Brushes.Orange;
                    break;
            }
            DisableRad();

        }

        private void btnNext_Click(object sender, RoutedEventArgs e) { 
            StoreAnswer();

            //determines whether or not user has started the quiz by clicking the next button
            if (quizStart != true) { 
                Timer();
                quizStart = true;
            }

            //determines if the user has reached the end of the quiz, else goes to next question
            if (intQuest > QuesAnsw.strQuestions.GetUpperBound(0))
            {
                endquiz();
            }
            else
            {
                CurrentQuestion();
                EnableRad();
                ResetRadSelection();
            }

        }

        public void endquiz()
        {
            EndQuiz();
            deleteProgress();
            this.Close();
        }

        //needs to be static for some reason
        private static void EndQuiz()
        {
            Window Review = new Review();
            Review.Show();
        }

        //Close button
        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            int yas = QuesAnsw.strQuestions.GetUpperBound(0);

            deleteProgress();

            try
            {
                timer.Stop();
            }
            catch { }

            File.WriteAllText("QuestionNumber", intQuest.ToString());
            File.WriteAllText("TimerTick", timerTickCount.ToString());

            //write users answers to file 'Progress'
            for (int i = 0; i <= yas; i++)
            {
                string stringy = QuesAnsw.strQuestions[i, 6];
                File.AppendAllText("Progress", stringy + Environment.NewLine);
            }

            //return to start screen
            Window startScreen = new MainWindow();
            startScreen.Show();
            this.Close();

        }

        private void deleteProgress()
        {
            if (File.Exists("Progress"))
            {
                File.Delete("Progress");
                File.Delete("QuestionNumber");
                File.Delete("TimerTick");
            }
        }

        public void Resume()
        {

            //loads data from previous session
            intQuest = int.Parse(File.ReadAllText("QuestionNumber")) - 1;
            timerTickCount = int.Parse(File.ReadAllText("TimerTick"));
            string[] lines = File.ReadLines("Progress").ToArray();
            for (int i = 0; i <= intQuest; i++)
            {
                if (lines[i] == "")
                {
                    lines[i] = String.Empty;
                }
                QuesAnsw.strQuestions[i, 6] = lines[i];
            }
        }


        //Cancel button
        private void button_Click(object sender, RoutedEventArgs e)
        {
            //deletes previous progress if it exists
            deleteProgress();
            try
            {
                timer.Stop();
            }
            catch { }

            //return to start screen
            Window startScreen = new MainWindow();
            startScreen.Show();
            this.Close();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            //determines whether the user selected start or resume
            if (QuesAnsw.quizResume == true)
            {
                Resume();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            //reset quiz variables
            intQuest = 0;
            timerTickCount = 0;
            QuesAnsw.quizResume = false;
            quizStart = false;

            //reset questions
            for (int i = 0; i <= intQuest; i++)
            {
                QuesAnsw.strQuestions[i, 6] = String.Empty;
            }
        }
    }
}

