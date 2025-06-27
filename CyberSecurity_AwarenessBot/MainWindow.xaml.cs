using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Linq;

namespace CyberSecurity_AwarenessBot
{


    public partial class MainWindow : Window
    {

        //Initialize userName
        private string userName;

        //Initialize response system
        private response_system chatbot = new response_system();
        
        
        public MainWindow()
        {

            InitializeComponent();
            

        }


        //AddBot Message method
        private void AddBotMessage(string message)
        {
            ChatList.Items.Add("ChatBot: " + message);
        }


        //Task Item class
        public class TaskItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? Reminder { get; set; }
            public bool IsCompleted { get; set; } = false;
            public override string ToString()
            {
                string status = IsCompleted ? "[Task Completed] " : "";
                string reminderText = Reminder.HasValue ? $" (Reminder: {Reminder.Value.ToShortDateString()})" : "";
                return $"{status}{Title} - {Description}{reminderText}";
            }

        }
        

        //Initialize Array List taskList and Activity Log
        private List<TaskItem> taskList = new();
        private List<string> activityLog = new();



        //Add Task Click Button
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleBox.Text;
            string desc = TaskDescBox.Text;
            DateTime? reminderDate = TaskReminderDate.SelectedDate;

            if (string.IsNullOrWhiteSpace(title)) return;

            TaskItem task = new() { Title = title, Description = desc, Reminder = reminderDate };
            taskList.Add(task);
            TaskListBox.Items.Add(task);

            AddBotMessage($"Task added: '{title}'. {(reminderDate != null ? $"Reminder set for {reminderDate:MMM dd}." : "")}");
            activityLog.Add($"Task added: {title} ({desc}){(reminderDate != null ? $" [Reminder: {reminderDate:MMM dd}]" : "")}");
        }



        //Show Log Button
        private void ShowLogButton_Click(object sender, RoutedEventArgs e)
        {
            string log = string.Join("\n", activityLog.TakeLast(10));
            ChatList.Items.Add("Bot: Here's a summary of recent actions:\n" + log);
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInput.Text;
            ChatList.Items.Add($"{userName}:" + input);
            UserInput.Clear();

            // Call the function
            ProcessInput(input);

          
            string botReply = chatbot.GetResponse(input);
            AddBotMessage(botReply);
        }


        
        //Create Class for QuizQuestion
        public class QuizQuestion
        {
            public string Question { get; set; }
            public List<string> Answers { get; set; }
            public int CorrectIndex { get; set; }
            public string Explanation { get; set; }
        }


        //Initialize QuizQuesion

        private List<QuizQuestion> quizQuestions = new();
        private int currentQuestionIndex = 0;
        private int quizScore = 0;



        //Button for Start Quiz Button
        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            LoadQuizQuestions();
            currentQuestionIndex = 0;
            quizScore = 0;
            ShowNextQuestion();
            QuizPanel.Visibility = Visibility.Visible;

            activityLog.Add("Quiz started.");
        }


        //Method for loading quiz questions and answers as well as explainations
        private void LoadQuizQuestions()
        {
            quizQuestions = new List<QuizQuestion>
    {
        new QuizQuestion
        {
            Question = "What should you do if you receive an email asking for your password?",
            Answers = new List<string> { "A) Reply with your password", "B) Delete the email", "C) Report the email as phishing", "D) Ignore it" },
            CorrectIndex = 2,
            Explanation = "Reporting phishing emails helps prevent scams."
        },
        new QuizQuestion
        {
            Question = "Choose the best form of a password?",
            Answers = new List<string> { "A) 123456", "B) Password1", "C) qW9!x&2Z", "D) abcdef" },
            CorrectIndex = 2,
            Explanation = "Strong passwords use a mix of characters, numbers, and symbols."
        },
        new QuizQuestion
        {
            Question = "Phishing is a process of getting fish from seas,oceans and rivers?",
            Answers = new List<string> { "A) True", "B)False "},
            CorrectIndex = 1,
            Explanation = "Phishing involves tricking users into revealing sensitive info."

        },
             new QuizQuestion
        {
            Question = "What is a scam?",
            Answers = new List<string> { "A) A type of virus", "B)  dishonest scheme or trick used to cheat someone out of their money, personal information, or access to their accounts.", "C) A firewall feature", "D) A secure login method" },
            CorrectIndex = 1,
            Explanation = "A scam is a dishonest scheme or trick used to cheat someone out of their money, personal information, or access to their accounts.."
        },


              new QuizQuestion
        {
            Question = "What is safe browsing?",
            Answers = new List<string> { "A) Act of looking at something", "B) S", "C) Surfing the interent", "D) practice of using the internet in a way that protects your personal data, devices, and privacy from online threats like malware, scams, or malicious websites." },
            CorrectIndex = 3,
            Explanation = "Refers to using the internet by wills of protecting users personal data"
        },


               new QuizQuestion
        {
            Question = "Privacy is the right to keeping your information safe?",
            Answers = new List<string> { "A) True", "B) False" },
            CorrectIndex = 1,
            Explanation = "Phishing involves tricking users into revealing sensitive info."
        },


                new QuizQuestion
        {
            Question = "Which of these must you consider when creating a password?",
            Answers = new List<string> { "A) It’s important to avoid using easily guessed personal information, such as your name, birthdate, favorite sports team, or pet’s name, in passwords.", "B) Using age ", "C) Using name and surname", "D) Using phone pin number" },
            CorrectIndex = 0,
            Explanation = "Phishing involves tricking users into revealing sensitive info."
        },

                 new QuizQuestion
        {
            Question = "Online privacy means protecting your data—like browsing history, location, and social media activity—from being tracked or misused by websites, companies, or hackers.?",
            Answers = new List<string> { "A) True", "B) False" },
            CorrectIndex = 0,
            Explanation = "Online privacy refers to the protection of users data."
        },


                  new QuizQuestion
        {
            Question = "Phishing is one common form of scam where attackers impersonate trusted sources to steal credentials, financial info, or infect your device with malware.",
            Answers = new List<string> { "A) True", "B) False"},
            CorrectIndex = 1,
            Explanation = "Phishing is the most common scam"
        },

                   new QuizQuestion
        {
            Question = "Browsers release frequent updates to patch security flaws. Enabling automatic updates ensures you have the latest protections against known vulnerabilities.",
            Answers = new List<string> { "A) True", "B) False" },
            CorrectIndex = 0,
            Explanation = "Browsers release frequent updates to patch security flaws which enables automatic updates."
        }



        
    };
        }


//Method for displaying and ending quiz(displays user scores at the end)
        private void ShowNextQuestion()
        {
            if (currentQuestionIndex >= quizQuestions.Count)
            {
                AddBotMessage($"Quiz finished! Score: {quizScore}/{quizQuestions.Count}");
                activityLog.Add($"Quiz completed. Score: {quizScore}/{quizQuestions.Count}");
                QuizPanel.Visibility = Visibility.Collapsed;
                return;
            }

            var question = quizQuestions[currentQuestionIndex];
            QuizQuestionText.Text = question.Question;
            QuizAnswersList.ItemsSource = question.Answers;
        }



        //Button for Submitting Quiz Answer
        private void SubmitQuizAnswer_Click(object sender, RoutedEventArgs e)
        {
            int selected = QuizAnswersList.SelectedIndex;
            if (selected == -1) return;

            var question = quizQuestions[currentQuestionIndex];
            if (selected == question.CorrectIndex)
            {
                quizScore++;
                AddBotMessage("Correct! " + question.Explanation);
            }
            else
            {
                AddBotMessage($"Incorrect. {question.Explanation}");
            }

            currentQuestionIndex++;
            ShowNextQuestion();
        }

        //Npl simulation
        //Methid for processing users input into the chatbot
        private void ProcessInput(string input)
        {
            input = input.ToLower();

            if (input.Contains("add task") || input.Contains("remind me") || input.Contains("set reminder"))
            {
                AddBotMessage("Sure! Please enter a task title and description, then set a reminder.");
            }
            else if (input.Contains("quiz") || input.Contains("game") || input.Contains("test me"))
            {
                StartQuiz_Click(null, null);
            }
            else if (input.Contains("activity log") || input.Contains("what have you done"))
            {
                ShowLogButton_Click(null, null);
            }
            else if(input.Contains("exit"))
            {
                return ; 
            }
           
            }
        


        //Button for Showing activity log
        private void ShowActivityLog_Click(object sender, RoutedEventArgs e)
        {
            ShowActivityLog();
        }


        //Method for showing activity log
        private void ShowActivityLog()
        {
            ActivityLogList.Items.Clear();
            int count = 0;
            foreach (var entry in activityLog.Reverse<string>().Take(5))
            {
                ActivityLogList.Items.Add(entry);
                count++;
            }

            AddBotMessage($"Showing last {count} actions.");
        }


        //Button for continuing to next panel(after entering name also stores the users name)
        public void Continue_Click(object sender, RoutedEventArgs e)
        {
            string name = userNameBox.Text.Trim();

            if (!string.IsNullOrEmpty(name))
            {
                userName = name; // 🔹 Set the class-level userName

                WelcomePanel.Visibility = Visibility.Collapsed;
                MainChatPanel.Visibility = Visibility.Visible;

                // Simulate user saying their name
                ChatList.Items.Add($"{userName}: Hi, I'm {userName}.");
                 new voice_greeting();
                // Bot responds
                AddBotMessage($"Nice to meet you, {userName}!Welcome to the CyberSecurity Awareness Bot Im here to help you with topics such as phishing,safe browsing,.");

               
            }
            else
            {
                MessageBox.Show("Please enter your name to continue.", "Input Required", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //Chat button
        private void MenuChat_Click(object sender, RoutedEventArgs e)
        {
            ChatList.Visibility = Visibility.Visible;
            UserInput.Visibility = Visibility.Visible;
            SendButton.Visibility = Visibility.Visible;

            TaskPanel.Visibility = Visibility.Collapsed;
            QuizPanel.Visibility = Visibility.Collapsed;
            ActivityLogList.Visibility = Visibility.Collapsed;

            AddBotMessage("You're in the chat section.");
        }


        //Task button
        private void MenuTasks_Click(object sender, RoutedEventArgs e)
        {
            TaskPanel.Visibility = Visibility.Visible;
            QuizPanel.Visibility = Visibility.Collapsed;
            ChatList.Visibility = Visibility.Collapsed;
            UserInput.Visibility = Visibility.Collapsed;
            SendButton.Visibility = Visibility.Collapsed;
            ActivityLogList.Visibility = Visibility.Collapsed;

            AddBotMessage("You're in the tasks section.");
        }



        //Quiz button
        private void MenuQuiz_Click(object sender, RoutedEventArgs e)
        {
            TaskPanel.Visibility = Visibility.Collapsed;
            QuizPanel.Visibility = Visibility.Visible;
            ActivityLogList.Visibility = Visibility.Collapsed;

            StartQuiz_Click(null, null);
        }

        //Activity log button
        private void MenuActivityLog_Click(object sender, RoutedEventArgs e)
        {
            TaskPanel.Visibility = Visibility.Collapsed;
            QuizPanel.Visibility = Visibility.Collapsed;
            ActivityLogList.Visibility = Visibility.Visible;

            ShowActivityLog();
            AddBotMessage("Showing your recent activity.");
        }


        //Button for showing completed tasks(user interaction required to mark task completed)
        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem is TaskItem selectedTask)
            {
                selectedTask.IsCompleted = true;
                int index = taskList.IndexOf(selectedTask);
                TaskListBox.Items[index] = null;
                TaskListBox.Items[index] = selectedTask;

                AddBotMessage($"Marked task '{selectedTask.Title}' as completed.");
                activityLog.Add($"Task completed: {selectedTask.Title}");
            }
            else
            {
                MessageBox.Show("Select a task to mark as completed.");
            }
        }
        //Delete Task Button
        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem is TaskItem selectedTask)
            {
                taskList.Remove(selectedTask);
                TaskListBox.Items.Remove(selectedTask);

                AddBotMessage($"Deleted task '{selectedTask.Title}'.");
                activityLog.Add($"Task deleted: {selectedTask.Title}");
            }
            else
            {
                MessageBox.Show("Select a task to delete.");
            }
        }

    }
}

    