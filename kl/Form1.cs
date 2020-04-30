using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kl
{
    public partial class Form1 : Form
    {
        private static int countTryIdentMax = 5; //максимальное кол-во попыток идентефикации
        private static int countTryAuthMax = 5; //максимальное кол-во попыток аутентификации

        private static int countTryIdent = 0; //попыток использовано на данный момент
        private static int countTryAuth = 0;
        private static int codeAuth; //код для генерации пароля

        private static DateTime lockTime = new DateTime(); //время, до которого дейтсвует блокировка || скручено в 0, чтобы потом использовать

        private static bool statusIdent = false; //прошел ли идентификацию
        private static bool statusAuth = false;

        private int idUser;

        public Form1()
        {
            InitializeComponent();
            textBox1.Text += "Вывод: Введите логин \r\n";
        }

        private string workDomain(string dataIn)
        {
            try
            {
                if (!statusAuth)
                {
                    if (DateTime.Now > lockTime)
                    {
                        if (!statusIdent)
                        {
                            idUser = request.getIdUser(dataIn); //получаем id из oracle

                            if (idUser != -1) //если -1, то не найден
                            {
                                statusIdent = true; //помечаем как успех
                                countTryIdent = 0; //сбрасываем количество попыток
                                codeAuth = security.genCodeAuth(); //генерируем код

                                Console.WriteLine(security.genPassword(codeAuth));
                                return "Пользователь найден \r\nСгенерируйте пароль \r\n" +
                                    codeAuth.ToString() + "\r\n";
                            }
                            else
                            {
                                countTryIdent++;
                                if (countTryIdent >= countTryIdentMax) //если привышено кол-во попыток
                                {
                                    DateTime timeNow = DateTime.Now;
                                    lockTime = timeNow.AddMinutes(1); //ставим блокировку в +1 минуту
                                    countTryIdent = 0; //сбрасываем счетчик
                                }
                                return "Пользователь не найден \r\n";
                            }
                        }
                        else
                        {
                            byte[] hash1 = Encoding.ASCII.GetBytes(dataIn);
                            byte[] hash2 = Encoding.ASCII.GetBytes(security.genPassword(codeAuth));
                            MD5 md5 = new MD5CryptoServiceProvider();
                            byte[] hashenc1 = md5.ComputeHash(hash1);
                            byte[] hashenc2 = md5.ComputeHash(hash2);
                            string result1 = "";
                            string result2 = "";
                            foreach (var b in hashenc1)
                            {
                                result1 += b.ToString("x2");
                            }

                            foreach (var b in hashenc2)
                            {
                                result2 += b.ToString("x2");
                            }


                            if (result1 == result2) //принимаем сгенерированный пароль и генерируем тем же алгоритмом, после чего сравниваем
                            {
                                statusAuth = true;
                                countTryAuth = 0;

                                button1.Enabled = false;

                                button5.Enabled = true;

                                return "Пользователь успешно прошел процесс аутентификации \r\n";
                            }
                            else
                            {
                                statusIdent = false; //сбрасываем идентификацию
                                statusAuth = false;
                                countTryAuth++; //увеличиваем счетчик провальных попыток

                                if (countTryAuth >= countTryAuthMax)
                                {
                                    DateTime timeNow = DateTime.Now;
                                    lockTime = timeNow.AddMinutes(1);
                                    countTryAuth = 0;
                                }
                                return "Неверный пароль \r\n" +
                                    "Соединение сброшено \r\n"; ;
                            }
                        }
                    }
                    else
                    {
                        return "Доступ временно ограничен \r\n";
                    }
                } else
                {
                    return "Клиент уже авторизован \r\n";
                }
            }
            catch
            {
                return "Неизвестная ошибка \r\n";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Count() > 0)
            {
                textBox1.Text += "Ввод: " + textBox2.Text.ToString() + "\r\n";
                textBox1.Text += "Вывод: " + workDomain(textBox2.Text.ToString());
                textBox2.Text = "";
            } else
            {
                MessageBox.Show("Введите данные");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Вывод: Соединение сброшено \r\nВывод: Введите логин \r\n";

            countTryAuth = 0;
            countTryIdent = 0;
            lockTime = new DateTime();
            statusIdent = false;
            statusAuth = false;
            button1.Enabled = true;
            textBox2.Text = "";

            button5.Enabled = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 8) //код это 8ми значное число
            {
                try
                {
                    textBox4.Text = security.genPassword(Convert.ToInt32(textBox3.Text)); //генерируем пароль по коду
                }
                catch
                {
                    MessageBox.Show("Введены некорректные символы");
                }
            }
            else
            {
                MessageBox.Show("Неверная длинна кода генерации");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form dannie = new dannie(idUser);
            dannie.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new connect(this).Show();
        }
    }
}
