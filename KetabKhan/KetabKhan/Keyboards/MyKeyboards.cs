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
        public ReplyKeyboardMarkup GetEmkanat()
        {
            ReplyKeyboardMarkup key = new ReplyKeyboardMarkup();
            key.Keyboard = new KeyboardButton[][]
            {
                new KeyboardButton[]
                {
                    new KeyboardButton("دریافت امکانات"),
                    new KeyboardButton("انصراف")
                }
            };
            key.ResizeKeyboard = true;
            return key;
        }
    }
}
