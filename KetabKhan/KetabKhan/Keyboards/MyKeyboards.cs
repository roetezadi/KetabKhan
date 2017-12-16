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
            key.Keyboard = new KeyboardButton[l.Count()][];
            Console.WriteLine(l[cnt]);
            List<ExamChoice> txt = db.ExamChoices.Where(x => x.QuestionID == l[cnt]).ToList();
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
    }
}
