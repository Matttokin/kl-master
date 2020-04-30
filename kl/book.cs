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
    public partial class book : Form
    {
        ArrayList edl = new ArrayList();
        ArrayList authors = new ArrayList();
        ArrayList genres = new ArrayList();
        ArrayList publishing = new ArrayList();

        public book(int ids, int idu)
        {
            InitializeComponent();

            int[] coalesce = request.getBookCoalesce(ids);

            

            getListAuth();
            getListGenr();
            getListPublishing();

            if (coalesce[0] != -1)
            {
                if (coalesce[0] > 0)
                    button1.Enabled = true;
                if (coalesce[1] > 0)
                    readlist(idu);
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

            comboBox1.DataSource = authors;
            comboBox1.ValueMember = "FIO";
            comboBox1.DisplayMember = "FIO";

            comboBox2.DataSource = genres;
            comboBox2.ValueMember = "FIO";
            comboBox2.DisplayMember = "FIO";

            comboBox3.DataSource = publishing;
            comboBox3.ValueMember = "FIO";
            comboBox3.DisplayMember = "FIO";

        }

        public void readlist(int id)
        {
            List<bookInfo> biList = request.getBookList(id);

            

            var acolumn = dataGridView1.Columns[2] as DataGridViewComboBoxColumn;
            acolumn.DataSource = authors;

            acolumn.ValueMember = "id";

            acolumn.DisplayMember = "FIO";

            var gnrcolumn = dataGridView1.Columns[3] as DataGridViewComboBoxColumn;
            gnrcolumn.DataSource = genres;

            gnrcolumn.ValueMember = "id";

            gnrcolumn.DisplayMember = "FIO";


            var publishcolumn = dataGridView1.Columns[4] as DataGridViewComboBoxColumn;
            publishcolumn.DataSource = publishing;

            publishcolumn.ValueMember = "id";

            publishcolumn.DisplayMember = "FIO";

            if (biList.Count > 0)
            {
                for (int i = 0; i < biList.Count; i++)
                {
                    dataGridView1.Rows.Add(biList[i].idBook, biList[i].nameBook, null, null, null, biList[i].onVip, biList[i].yearPrint);
                    dataGridView1[2, dataGridView1.RowCount - 1].Value = getId(authors, biList[i].nameAuthor);
                    dataGridView1[3, dataGridView1.RowCount - 1].Value = getId(genres, biList[i].nameGenre);
                    dataGridView1[4, dataGridView1.RowCount - 1].Value = getId(publishing, biList[i].namePublishing);
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
            if (textBox1.Text.Count() > 0 & dateTimePicker1.Text.Count() > 0)
            {
                string name = textBox1.Text;
                string author = comboBox1.SelectedValue.ToString();
                string genre = comboBox2.SelectedValue.ToString();
                string publish = comboBox3.SelectedValue.ToString();
                bool vip = checkBox1.Checked;
                int eyarprint = Convert.ToInt32(dateTimePicker1.Text);
                char i;
                if(vip)
                    i = 'Y';
                else
                    i = 'N';

                string response = request.addBook(name, author, genre, publish, eyarprint, i);

                if (response.Equals("-200"))
                {
                    dataGridView1.Rows.Add(getid(), name, null, null, null, vip, eyarprint);
                    dataGridView1[2, dataGridView1.RowCount - 1].Value = getId(authors, author);
                    dataGridView1[3, dataGridView1.RowCount - 1].Value = getId(genres, genre);
                    dataGridView1[4, dataGridView1.RowCount - 1].Value = getId(publishing, publish);
                }

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
                request.updateBook(dataGridView1[1, Convert.ToInt32(edl[i])].Value.ToString(), dataGridView1[2, Convert.ToInt32(edl[i])].Value.ToString(),
                    dataGridView1[3, Convert.ToInt32(edl[i])].Value.ToString(), dataGridView1[4, Convert.ToInt32(edl[i])].Value.ToString(),
                    dataGridView1[5, Convert.ToInt32(edl[i])].Value.ToString(), dataGridView1[6, Convert.ToInt32(edl[i])].Value.ToString(),
                    dataGridView1[0, Convert.ToInt32(edl[i])].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            request.delBook(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
            dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
        }

        private void getListAuth()
        {
            List<authorInfo> aiList = request.getAuthorList();

            if (aiList.Count > 0)
            {
                for (int i = 0; i < aiList.Count; i++)
                {
                    authors.Add(new auth() { id = aiList[i].id, FIO = aiList[i].name });
                }
            }
            else
            {
                MessageBox.Show(
                "Нет данных для значений авторов",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void getListGenr()
        {
            List<authorInfo> genreList = request.getGenreList();

            if (genreList.Count > 0)
            {
                for (int i = 0; i < genreList.Count; i++)
                {
                    genres.Add(new auth() { id = genreList[i].id, FIO = genreList[i].name });
                }
            }
            else
            {
                MessageBox.Show(
                "Нет данных для значений авторов",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void getListPublishing()
        {
            List<authorInfo> pubList = request.getPublishingList();

            if (pubList.Count > 0)
            {
                for (int i = 0; i < pubList.Count; i++)
                {
                    publishing.Add(new auth() { id = pubList[i].id, FIO = pubList[i].name });
                }
            }
            else
            {
                MessageBox.Show(
                "Нет данных для значений авторов",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        public int getId(ArrayList a, string y)
        {
            foreach (auth l in a)
            {
                if (l.FIO.Equals(y))
                    return l.id;
            }

            return 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
