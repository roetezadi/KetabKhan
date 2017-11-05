using KetabKhan.Linq;
using KetabKhan.Models;
using NetTelegramBotApi;
using NetTelegramBotApi.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetabKhan.MainBot
{
    public class MyBOT
    {
        private static string Token = "461324394:AAEOZZf-tUd3ETK2iWOdbilb-HEQGVQZIZU";

        public static void Run_Bot()
        {
            //making the bot
            var Bot = new TelegramBot(Token);
            var me = Bot.MakeRequestAsync(new GetMe()).Result;

            DBTest1DataContext db = new DBTest1DataContext();

            //creating for each user a specific id to have it's own state
            var UserId = new Dictionary<long, int>();
            int cnt = 0;

            //getting the list of person in ram
            List<Person> persons = new List<Person>();
            long offset = 0;
            long IID = 0;
            long ExamIDs = db.Exams.Count();
            long QuestionIDs = db.ExamQuestions.Count();
            long ChoiceIDs = db.ExamChoices.Count();

            

            while (true)
            {
                var updates = Bot.MakeRequestAsync(new GetUpdates() { Offset = offset }).Result;
                try
                {
                    foreach (var update in updates)
                    {
                        /**/
                        long ChatID = update.Message.Chat.Id;

                        if (!UserId.ContainsKey(ChatID))
                        {
                            UserId.Add(ChatID, cnt);
                            Person p = new Person();
                            p.ChatID = ChatID;
                            p.State = "start";
                            persons.Add(p);
                            cnt++;
                        }
                        persons[UserId[ChatID]].Text = update.Message.Text;
                        Console.WriteLine(persons[UserId[ChatID]].State);
                        UserState User_State = new UserState();
                        Console.WriteLine("h1");
                        User_State.CheckState(persons[UserId[ChatID]], Bot, db, ExamIDs, QuestionIDs, ChoiceIDs);
                        offset = update.UpdateId + 1;
                        ExamIDs++;
                        QuestionIDs++;
                        ChoiceIDs++;
                        IID++;
                    }
                }
                catch(Exception e) { Console.WriteLine(e.Message); }
            }
        }
    }
}
