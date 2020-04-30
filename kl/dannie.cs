using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace kl
{
    public partial class dannie : Form
    {
        private int id;
        private int idus;

        public dannie(int ids)
        {
            InitializeComponent();

            idus = ids;

            id = request.getIdRole(ids);


            int[] coalesce = request.getDanineCoalesce(id);

            if (coalesce[0] != -1)  
            {
                if (coalesce[0] > 0)
                    button1.Enabled = true;
                if (coalesce[1] > 0)
                    button2.Enabled = true;
                if (coalesce[2] > 0)
                    button3.Enabled = true;
                if (coalesce[3] > 0)
                    button4.Enabled = true;
                if (coalesce[4] > 0)
                    button5.Enabled = true;
                if (coalesce[7] > 0)
                    button7.Enabled = true;

            }
            else
            {
                MessageBox.Show(
                "Нет данных",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form auth = new author(id);
            auth.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form genr = new genre(id);
            genr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form publsh = new publish(id);
            publsh.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form usi = new user(id);
            usi.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form boid= new book(id, idus);
            boid.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form journl = new journal(id, idus);
            journl.Show();
        }

    }
}
