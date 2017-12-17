using KetabKhan.DB;
using KetabKhan.Keyboards;
using KetabKhan.Linq;
using NetTelegramBotApi;
using NetTelegramBotApi.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KetabKhan.Models
{
    public class UserState
    {
        MyKeyboards keyboard = new MyKeyboards();
        public void CheckState(Person person, TelegramBot Bot, DBTest1DataContext db, long ExamIDs, long QuestionIDs, long ChoiceIDs)
        {
            Exam e = new Exam();
            ExamQuestion eq = new ExamQuestion();
            ExamChoice ec = new ExamChoice();

            var regex = new Regex(@"^\d+$");

            var arabicregex = new Regex(@"^[\u0600-\u06FF]+$");

            Functions func = new Functions();

            MyKeyboards mykeyboard = new MyKeyboards();

            DB_Helper db_helper = new DB_Helper();

            if (person.State == "start" || person.Text == "/start" || person.Text == "منو اصلی")
            {
                string message = "😁به بات کتابخوان خوش آمدید" + "\n" + "برای  بوجود آوردن مسابقه جدید به ایجاد مسابقه بروید و طبق دستورالعمل گفته شده مسابقه را ایجاد کنید.";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = mykeyboard.Menu()};
                Bot.MakeRequestAsync(reg);
                //person.State = "EnterNumQ";
                person.State = "menu";
                /*inserting to DB Users*/
                //e.ExamID = ExamIDs;
                //person.ExamID = ExamIDs;
                //try
               // {
                //    db_helper.InsertToExam(ExamIDs, person.ChatID);
               // }
               // catch (Exception e1) { }
                //e.UserID = person.ChatID;
                //db.Exams.InsertOnSubmit(e);
                /**
                try
                {
                    db.SubmitChanges();
                }
                catch { }
                /**/

            }
            else if (person.State == "menu" && person.Text == "🖊ایجاد مسابقه")
            {
                person.ExamID = ExamIDs;
                try
                {
                    db_helper.InsertToExam(ExamIDs, person.ChatID);
                }
                catch (Exception e1) { }
                string message = "برای ایجاد مسابقه لطفا ابتدا تعداد سوالات خود را وارد کنید:";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = mykeyboard.GoMenu()};
                Bot.MakeRequestAsync(reg);
                person.State = "EnterNumQ";
            }
            else if (person.State == "menu" && person.Text == "📝شرکت در مسابقه")
            {
                string message = "برای شروع مسابقه کد مسابقه را وارد کنید:";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = mykeyboard.GoMenu() };
                Bot.MakeRequestAsync(reg);
                person.State = "BeginExam";
            }
            else if (person.State == "BeginExam" && regex.IsMatch(person.Text))
            {
                if (arabicregex.IsMatch(person.Text))
                {
                    person.TakeExamID = func.arabictonum(person.Text);
                }
                else
                {
                    person.TakeExamID = Convert.ToInt32(person.Text);
                }
                person.startq = 0;
                person.ExamQuestions = db.ExamQuestions.Where(x => x.ExamID == person.TakeExamID).ToList();
                string message = person.ExamQuestions[person.startq].Question;
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = mykeyboard.QuestionChoices(person.ExamQuestions[person.startq].QuestionID) };
                Bot.MakeRequestAsync(reg);
                person.State = "InExam";
                person.startq++;
            }
            else if (person.State == "InExam")
            {
                if (person.startq < person.ExamQuestions.Count)
                {
                    //insert to usertake exam
                    db_helper.InsertToUserTakeExam(person.ChatID, person.TakeExamID, person.ExamQuestions[person.startq - 1].QuestionID, person.Text);
                    string message = person.ExamQuestions[person.startq].Question;
                    var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = mykeyboard.QuestionChoices(person.ExamQuestions[person.startq].QuestionID) };
                    Bot.MakeRequestAsync(reg);
                    person.startq++;
                }
                else
                {
                    //insert to usertake exam
                    db_helper.InsertToUserTakeExam(person.ChatID, person.TakeExamID, person.ExamQuestions[person.startq - 1].QuestionID, person.Text);
                    string message = "مسابقه پایان یافت! با تشکر از شرکت شما در مسابقه.";
                    var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = mykeyboard.Menu() };
                    Bot.MakeRequestAsync(reg);
                    person.startq = 0;
                    person.State = "menu";
                }
            }
            else if (person.State == "EnterNumQ" && regex.IsMatch(person.Text))
            {
                if (arabicregex.IsMatch(person.Text))
                {
                    person.Qnum = func.arabictonum(person.Text);
                }
                else
                {
                    person.Qnum = Convert.ToInt32(person.Text);
                }
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
                //inserting to DB Questions
                //eq.Question = person.Text;
                //eq.QuestionID = QuestionIDs;
                person.NowQuestionID = QuestionIDs;
                person.ListOfQuestion.Add(QuestionIDs);
                try
                {
                    db_helper.InsertToExamQuestion(person.Text, QuestionIDs, person.ExamID);
                }
                catch (Exception e2) { }
                    //eq.ExamID = person.ExamID;
                //db.ExamQuestions.InsertOnSubmit(eq);
                /**
                try
                {
                    db.SubmitChanges();
                }
                catch { }
                /**/
            }
            else if (person.State == "EnterNumC" && regex.IsMatch(person.Text))
            {
                if (arabicregex.IsMatch(person.Text))
                {
                    person.Cnum = func.arabictonum(person.Text);
                }
                else
                {
                    person.Cnum = Convert.ToInt32(person.Text);
                }
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
                    string message = "تمامی سوالات با گزینه‌ها به درستی ثبت شدند حال گزینه‌های درست را وارد کنید. سوال اول:.";
                    //string message = "با تشکر تمامی سوالات ثبت شد!";
                    var reg = new SendMessage(person.ChatID, message) /**/{ ReplyMarkup = mykeyboard.RightAns(person.ListOfQuestion,0)}/**/;
                    Bot.MakeRequestAsync(reg);
                    person.cntC = 0;
                    person.cntQ = 1;
                    person.State = "GetRightAnswer";
                    //person.State = "Over";
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
                //insert to DB choices
                try
                {
                    db_helper.InsertToExamChoice(person.Text, ChoiceIDs, person.NowQuestionID);
                }
                catch (Exception e3) { }
                    /**
                ec.Choice = person.Text;
                ec.ChoiceID = ChoiceIDs;
                ec.QuestionID = person.NowQuestionID;
                db.ExamChoices.InsertOnSubmit(ec);
                try
                {
                    db.SubmitChanges();
                }
                catch { }
                /**/
            }
            else if (person.State == "GetRightAnswer")
            {
                long? l = person.ListOfQuestion[person.cntQ - 1];
                db_helper.SetRightAnswer(l, person.Text);
                /**
                ExamQuestion eq1 = db.ExamQuestions.FirstOrDefault(x => x.QuestionID == l);
                eq1.RightAnswer = person.Text;
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception e1) { }
                /**/

                if (person.cntQ < person.Qnum)
                {
                    string message = "گزینه درست سوال " + (person.cntQ+1) + " را وارد کنید:";
                    var reg = new SendMessage(person.ChatID, message)/**/ { ReplyMarkup = mykeyboard.RightAns(person.ListOfQuestion, person.cntQ) }/**/;
                    Bot.MakeRequestAsync(reg);
                    person.cntQ++;
                }
                else
                {
                    long id = db.Exams.FirstOrDefault(x => x.UserID == person.ChatID).ExamID;
                    string message = "با تشکر تمامی سوالات همراه با گزینه آنها و جواب صحیح آنها به درستی ذخیره شد." + "\n" + "کد مسابقه: " + id;
                    var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = mykeyboard.Menu()};
                    Bot.MakeRequestAsync(reg);
                    person.cntC = 0;
                    person.cntQ = 0;
                    person.State = "menu";
                }
            }
            else
            {
                string message = "لطفا فرمت ورودی‌های خود را کنترل کنید!";
                var reg = new SendMessage(person.ChatID, message);
                Bot.MakeRequestAsync(reg);
            }
            //t.State = person.State;
            /**
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e1) { }
    /**/
            //db.SubmitChanges();
        }
    }
}
