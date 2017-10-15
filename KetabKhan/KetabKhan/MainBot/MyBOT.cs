using KetabKhan.Linq;
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


            //creating for each user a specific id to have it's own state
            var UserId = new Dictionary<long, int>();
            int cnt = 0;

            //getting the list of person in ram
            //List<Person> persons = new List<Person>();
            long offset = 0;
            long IID = 0;

            DBTestDataContext db = new DBTestDataContext();

            while (true)
            {
                var updates = Bot.MakeRequestAsync(new GetUpdates() { Offset = offset }).Result;
                try
                {
                    foreach (var update in updates)
                    {
                        Console.WriteLine(update.Message.Text);
                        /**
                        long ChatID = update.Message.Chat.Id;
                        if (!UserId.ContainsKey(ChatID))
                        {
                            UserId.Add(ChatID, cnt);
                            cnt++;
                        }
                        else
                        {

                        }
                        offset = update.UpdateId + 1;
                         /**/
                        string text = update.Message.Text;
                        long ChatID = update.Message.Chat.Id;
                        if (text == "/start")
                        {
                            string message = "به بات خوش آمدید :\\" ;
                            var reg = new SendMessage(ChatID, message);
                            Bot.MakeRequestAsync(reg);
                        }
                        else
                        {
                            Test t = new Test();
                            t.Id = IID;
                            t.Message = text;
                            db.Tests.InsertOnSubmit(t);
                            try { db.SubmitChanges(); }
                            catch (Exception e) { Console.WriteLine(e.Message); }
                            string message = "گفتی: " + text + "😐😐";
                            var reg = new SendMessage(ChatID, message);
                            Bot.MakeRequestAsync(reg);
                        }
                        offset = update.UpdateId + 1;
                        IID++;
                    }
                }
                catch(Exception e) { Console.WriteLine(e.Message); }
            }
        }
    }
}
