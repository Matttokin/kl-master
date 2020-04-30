using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kl
{
    public partial class connect : Form
    {
        Form1 f1;
        public connect(Form1 f1)
        {
            InitializeComponent();
            this.f1 = f1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0)
            {
                try
                {
                    request.Host = textBox1.Text.ToString();
                    if (request.testConnect().Equals("okay"))
                    {
                        MessageBox.Show("Успешно");
                        this.Close();
                        f1.button1.Enabled = true;
                        f1.button2.Enabled = true;
                        f1.button3.Enabled = true;
                    } else
                    {
                        MessageBox.Show("Не найден сервер по данному адресу");
                        f1.button1.Enabled = true;
                        f1.button2.Enabled = true;
                        f1.button3.Enabled = true;
                        f1.button5.Enabled = true;
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show("Не удается подключиться");
                    f1.button1.Enabled = true;
                    f1.button2.Enabled = true;
                    f1.button3.Enabled = true;
                    f1.button5.Enabled = true;
                }
            }
        }
    }
}
