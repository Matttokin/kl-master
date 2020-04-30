using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kl
{
    public class ans
    {
        public void message(String cod)
        {
            string outz = "Некорректный ответ сервера";
            switch (cod)
            {
                case "-1":
                    outz = "пользователь не найден";
                    break;
                case "-302":
                    outz = "автор уже существует";
                    break;
                case "-303":
                    outz = "пользователь не найден";
                    break;
                case "-304":
                    outz = "жанр уже существует";
                    break;
                case "-305":
                    outz = "издательство уже существует";
                    break;
                case "-306":
                    outz = "книга уже существует";
                    break;
                case "-401":
                    outz = "роль не найдена";
                    break;
                case "-403":
                    outz = "автор не существует";
                    break;
                case "-404":
                    outz = "жанр не существует";
                    break;
                case "-405":
                    outz = "издательство не существует";
                    break;
                case "-701":
                    outz = "таблиц для добавления прав не существует";
                    break;
                case "-705":
                    outz = "пользователя для добавления прав не существует";
                    break;
                case "-500":
                    outz = "ошибки типа данных";
                    break;
                case "-200":
                    outz = "успешно";
                    break;

            }

            MessageBox.Show(
             outz,
            "Итог",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        }
    }
}
