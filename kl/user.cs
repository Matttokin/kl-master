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
    public partial class user : Form
    {
        ArrayList edl = new ArrayList();
        ArrayList role = new ArrayList();
        int id;

        public user(int ids)
        {
            InitializeComponent();

            int[] coalesce = request.getUser1Coalesce(ids);

            

            addRolelist();

            
            if (coalesce[0] != -1)
            {
                if (coalesce[0] > 0)
                    button1.Enabled = true;
                    comboBox1.DataSource = role;
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


            coalesce = request.getUser2Coalesce(ids);


            if (coalesce[0] != -1)
            {
                if (coalesce[5] > 0)
                    button4.Enabled = true;
                if (coalesce[6] > 0)
                    button5.Enabled = true;
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


            id = ids;

        }

        public void readlist()
        {
            List<userInfo> uiList = request.getUserList();

            
            var column = dataGridView1.Columns[2] as DataGridViewComboBoxColumn;
           
            column.DataSource = role;

            if (uiList.Count > 0)
            {
                for (int i = 0; i < uiList.Count; i++)
                {
                    dataGridView1.Rows.Add(uiList[i].idUser, uiList[i].loginUser, role[role.IndexOf(uiList[i].nameRole)]);
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
                string role = comboBox1.Text;

                string response = request.addUser(name, role);

                if (response.Equals("-200"))
                    dataGridView1.Rows.Add(getid(), name, role);

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
            if (!edl.Contains(e.RowIndex))
            {
                edl.Add(e.RowIndex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < edl.Count; i++)
            {
                request.updateUser(dataGridView1[1, Convert.ToInt32(edl[i])].Value.ToString(), (role.IndexOf(dataGridView1[2, Convert.ToInt32(edl[i])].Value.ToString()) + 1).ToString(), dataGridView1[0, Convert.ToInt32(edl[i])].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            request.delUser(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
            dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
        }

        private void addRolelist()
        {
            List<authorInfo> aiList = request.getroleUserList();

            if (aiList.Count > 0)
            {
                for (int i = 0; i < aiList.Count; i++)
                {
                    role.Add(aiList[i].name);
                }
            }
            else
            {
                MessageBox.Show(
                "Нет данных о ролях пользователей",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form aces = new access(id);
            aces.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form ruli = new rule(id);
            ruli.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
