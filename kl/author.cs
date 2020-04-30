using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
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
    public partial class author : Form
    {
        ArrayList edl = new ArrayList();

        public author(int ids)
        {
            InitializeComponent();

            int[] coalesce = request.getAuthorCoalesce(ids);

            if (coalesce[0] != -1)
            {
                if (coalesce[0] > 0)
                    button1.Enabled = true;
                if (coalesce[1] > 0)
                    readlist();
                if (coalesce[2] > 0)
                    button2.Enabled = true;
                if (coalesce[3] > 0)
                    button3.Enabled = true;

            }
            else
            {
                MessageBox.Show(
                "Нет данных о правах пользователя",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        public void readlist()
        {
            List<authorInfo> aiList = request.getAuthorList();

            if (aiList.Count > 0)
            {                
                for(int i=0;i< aiList.Count; i++)
                {
                        dataGridView1.Rows.Add(aiList[i].id, aiList[i].name);
                }
            }
            else
            {
                MessageBox.Show(
                "Нет данных для вывода на экран",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Count() > 0)
            {
                string name = textBox1.Text;

                string response = request.addAuthor(name);

                if (response.Equals("-200"))
                    dataGridView1.Rows.Add(getid(), name);

                new ans().message(response);
            }
            else
            {
                MessageBox.Show(
                "Вы не ввели данные",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private int getid()
        {
            int id;
            try
            {
                id = Convert.ToInt32(dataGridView1[0, dataGridView1.RowCount - 1].Value);
            }
            catch
            {
                id = 0;
            }
            return id;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(!edl.Contains(e.RowIndex))
            {
                edl.Add(e.RowIndex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for(int i = 0; i<edl.Count; i++)
            {
                request.updateAuthor(dataGridView1[1, Convert.ToInt32(edl[i])].Value.ToString(), dataGridView1[0, Convert.ToInt32(edl[i])].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            { 
                request.delAuthor(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }
            catch
            {
                MessageBox.Show(
                "Нельзя удалить запись",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
}

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
