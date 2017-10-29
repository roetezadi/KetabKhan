using KetabKhan.Keyboards;
using KetabKhan.Linq;
using NetTelegramBotApi;
using NetTelegramBotApi.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetabKhan.Models
{
    public class UserState
    {
        MyKeyboards keyboard = new MyKeyboards();
        public void CheckState(Person person, TelegramBot Bot, DBTest1DataContext db, long ExamIDs, long QuestionIDs)
        {
            Exam e = new Exam();
            ExamQuestion eq = new ExamQuestion();
            ExamChoice ec = new ExamChoice();
            /**
            Test t = db.Tests.FirstOrDefault(x => x.Id == person.ChatID);
            Console.WriteLine("nazi");
            if (t == null)
            {
                Console.WriteLine("nazi2");
                Test tt = new Test();
                tt.Id = person.ChatID;
                tt.Message = person.Text;
                tt.State = person.State;
                db.Tests.InsertOnSubmit(tt);
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
    /**/
            if (person.State == "start" || person.Text == "/start")
            {
                string message = "با سلام به بات خوش آمدید. برای ایجاد مسابقه لطفا ابتدا تعداد سوالات خود را وارد کنید:";
                var reg = new SendMessage(person.ChatID, message);
                Bot.MakeRequestAsync(reg);
                person.State = "EnterNumQ";
                /**
                e.ExamID = ExamIDs;
                person.ExamID = ExamIDs;
                ExamIDs++;
                e.UserID = person.ChatID;
                db.Exams.InsertOnSubmit(e);
                try
                {
                    db.SubmitChanges();
                }
                catch { }
    /**/

            }
            else if (person.State == "EnterNumQ")
            {
                person.Qnum = Convert.ToInt32(person.Text);
                string message = "شما به تعداد " + person.Text + "سوال باید وارد کنید. ابتدا سوال اول خود را وارد کنید:";
                var reg = new SendMessage(person.ChatID, message);
                Bot.MakeRequestAsync(reg);
                person.cntQ++;
                person.State = "EnterQ";

            }
            else if (person.State == "EnterQ")
            {
                string message = "حال تعداد گزینه مورد نظر خود  را وارد کنید:";
                var reg = new SendMessage(person.ChatID, message);
                Bot.MakeRequestAsync(reg);
                person.State = "EnterNumC";
                //eq.Question = person.Text;
                //eq.QuestionID = QuestionIDs;
                //eq.ExamID = person.ExamID;
                //QuestionIDs++;
            }
            else if (person.State == "EnterNumC")
            {
                person.Cnum = Convert.ToInt32(person.Text);
                Console.WriteLine(person.Cnum);
                string message = "شما به تعداد " + person.Text + "گزینه میخواهید وارد کنید. ایتدا گزینه‌ي " + (person.cntC+1) + "را وارد کنید:";
                var reg = new SendMessage(person.ChatID, message);
                Bot.MakeRequestAsync(reg);
                person.cntC++;
                person.State = "EnterC";
            }
            else if (person.State == "EnterC")
            {
                if (person.cntC + 1 > person.Cnum && person.cntQ + 1 > person.Qnum)
                {
                    string message = "تمامی سوالات با گزینه‌ها به درستی ثبت شدند.";
                    var reg = new SendMessage(person.ChatID, message);
                    Bot.MakeRequestAsync(reg);
                    person.State = "Over";
                }
                else if (person.cntC + 1 > person.Cnum )
                {
                    string message = "تمام گزینه‌ها ثبت شد. حال سوال " + (person.cntQ+1) + "را وارد کنید.";
                    var reg = new SendMessage(person.ChatID, message);
                    Bot.MakeRequestAsync(reg);
                    person.cntQ++;
                    person.cntC = 0;
                    person.State = "EnterQ";
                }
                else
                {
                    string message = "گزینه‌ی " + (person.cntC + 1) + "ام را وارد کنید:";
                    var reg = new SendMessage(person.ChatID, message);
                    Bot.MakeRequestAsync(reg);
                    person.cntC++;
                    person.State = "EnterC";
                }
            }
            else if(person.State == "Over")
            {
                person.cntC = 0;
                person.cntQ = 0;
                string message = "برای شروع مجدد /start را بزنید!";
                var reg = new SendMessage(person.ChatID, message);
                Bot.MakeRequestAsync(reg);
            }
            //t.State = person.State;
            //db.SubmitChanges();
        }
    }
}
