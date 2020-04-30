using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kl
{
    static class security
    {
        public static int genCodeAuth() //генерируем код для рукопожатия
        {
            Random rD = new Random();
            return rD.Next(10000000, 99999999); //генерируем 8ми символьное число
        }
        public static string genPassword(int codeAuth) //генерируем пароль по коду
        {
            string pass = "";
            /*Random rD = new Random(codeAuth); //инициализируем ПРГЧ c ключом 


            while (pass.Length < 16)
            {
                int rand = rD.Next(46, 122);

                //банальный выбор символов из цифр, латиницы любого регистра, _ . и некоторых других символов
                if ((rand >= 48 && rand <= 57) || (rand >= 65 && rand <= 90) || (rand >= 97 && rand <= 122) || (rand == 46 || rand == 95))
                {
                    pass += (char)rand;
                }
            }*/
            DateTime dateValue = DateTime.Today;

            int dayOfWeek = (int)dateValue.DayOfWeek;

            for (int i = 0; i<  codeAuth.ToString().Length; i++)
            {
                //Console.WriteLine(codeAuth.ToString()[i]);
                pass += (((9 - Int32.Parse((codeAuth.ToString())[i].ToString())) * 2 + dayOfWeek)).ToString();
            }

            

            Console.WriteLine(pass);
            return pass;
        }
    }
}
