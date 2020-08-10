using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;


namespace WpfApplication2 { 
    public class QuesAnsw { 
        public int intQuestNumber = 0;
        public int intRow = 0;
        public int intCorrect = 0;
        public static string strName;
        public static bool quizResume = false;


        public static string[,] strQuestions = {{"What is the capital of Algeria?", "Algiers", "Luanda", "Porto-Novo", "Gaborone", "1", String.Empty},
                                       {"What is the capital of Angola?", "Algiers", "Djibouti", "Luanda", "Malabo", "3", String.Empty},
                                       {"What is the capital of Benin?", "Porto-Novo", "Yaounde", "Cairo", "Asmara", "1", String.Empty},
                                       {"What is the capital of Botswana?", "Kampala", "Harare", "Gaborone", "Yamoussoukro", "3", String.Empty},
                                       {"What is the capital of Burkina Faso?", "Moroni", "Ouagadougou", "Brazzaville", "Kinshasa", "2", String.Empty},
                                       {"What is the capital of Burundi?", "Bangui", "Bujumbura", "Cairo", "Praia", "2", String.Empty},
                                       {"What is the capital of Cameroon?", "Djibouti", "Gaborone", "Yaounde", "Kinshasa", "3", String.Empty},
                                       {"What is the capital of The Republic of Cabo Verde?", "Algiers", "Malabo", "Bujumbura", "Praia", "4", String.Empty},
                                       {"What is the capital of The Central African Republic?", "Tripoli", "Bamako", "Bangui", "Lilongwe", "3", String.Empty},
                                       {"What is the capital of Chad?", "N'Djamena", "Conakry", "Accra", "Asmara", "1", String.Empty}};
                                      
                                       


        public static int TestScore() { 
            int Score = 0;
            for (int i = 0; i <= strQuestions.GetUpperBound(0); i++) { 
                if (strQuestions[i, 5] == strQuestions[i, 6]) { 
                    Score++;
                }
            }
            return Score;
        }

        public void ResetAnswers() { 
            for (int i = 0; i <= strQuestions.GetUpperBound(0); i++) { 
                strQuestions[i, 6] = String.Empty;
            }
        }
    }
    public class Timer { 
        public int timerTickCount = 0;
        private bool hasSelectionChanged = false;
        private DispatcherTimer timer;

        public Timer() { 
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1); // will 'tick' once every second
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e) { 
            DispatcherTimer timer = (DispatcherTimer)sender;
            if (++timerTickCount == 2)
            {
                if (hasSelectionChanged) timer.Stop();
            }
        }
    }
}
