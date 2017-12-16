using KetabKhan.DB;
using KetabKhan.Linq;
using KetabKhan.Models;
using NetTelegramBotApi.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace KetabKhan.Keyboards
{
    public class MyKeyboards
    {
        DBTest1DataContext db = new DBTest1DataContext();

        Functions func = new Functions();

        DB_Helper db_helper = new DB_Helper();

        public ReplyKeyboardMarkup Menu()
        {
            ReplyKeyboardMarkup key = new ReplyKeyboardMarkup();
            key.Keyboard = new KeyboardButton[][]
            {
                new KeyboardButton[]
                {
                    new KeyboardButton("🖊ایجاد مسابقه"),
                    new KeyboardButton("🔎اطلاعات بیشتر")
                },
                new KeyboardButton[]
                {
                    new KeyboardButton("📝شرکت در مسابقه"),
                    new KeyboardButton("☎️ارتباط با ما")
                }
            };
            key.ResizeKeyboard = true;
            return key;
        }

        public ReplyKeyboardMarkup GoMenu()
        {
            ReplyKeyboardMarkup key = new ReplyKeyboardMarkup();
            key.Keyboard = new KeyboardButton[][]
            {
                new KeyboardButton[]
                {
                    new KeyboardButton("منو اصلی")
                }
            };
            key.ResizeKeyboard = true;
            return key;
        }


        public ReplyKeyboardMarkup RightAns(List<long> l, int cnt)
        {
            ReplyKeyboardMarkup key = new ReplyKeyboardMarkup();
            List<string> txt = db_helper.GetAnswers(l, cnt);
            key.Keyboard = new KeyboardButton[txt.Count()][];
            Console.WriteLine(txt.Count);
            for (int j = 0; j < txt.Count(); j++)
            {
                Console.WriteLine(txt[j]);
                key.Keyboard[j] = new KeyboardButton[1];
                key.Keyboard[j][0] = new KeyboardButton(txt[j]);
            }
            key.ResizeKeyboard = true;
            return key;
        }


        public ReplyKeyboardMarkup QuestionChoices(long id)
        {
            //List<ExamChoice> txt = db.ExamChoices.Where(x => x.QuestionID == id).ToList();
            List<string> txt = db_helper.GetAnswers2(id);
            ReplyKeyboardMarkup key = new ReplyKeyboardMarkup();
            key.Keyboard = new KeyboardButton[txt.Count()][];
            for (int j = 0; j < txt.Count(); j++)
            {
                Console.WriteLine(txt[j]);
                key.Keyboard[j] = new KeyboardButton[1];
                key.Keyboard[j][0] = new KeyboardButton(txt[j]);
            }
            key.ResizeKeyboard = true;
            return key;
        }



        /**
        public ReplyKeyboardMarkup RightAns(List<long> l, int cnt)
        {
            ReplyKeyboardMarkup key = new ReplyKeyboardMarkup();
            //key.Keyboard = new KeyboardButton[l.Count()][];
            Console.WriteLine(l[cnt]);
            List<ExamChoice> txt = db.ExamChoices.Where(x => x.QuestionID == l[cnt]).ToList();
            key.Keyboard = new KeyboardButton[txt.Count()][];
            Console.WriteLine(txt.Count);
            for (int j = 0; j < txt.Count(); j++)
            {
                Console.WriteLine(txt[j].Choice);
                key.Keyboard[j] = new KeyboardButton[1];
                key.Keyboard[j][0] = new KeyboardButton(txt[j].Choice);
            }
            key.ResizeKeyboard = true;
            return key;
        }
        /**/



    }
}
