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
    public partial class rule : Form
    {
        ArrayList edl = new ArrayList();
        ArrayList role = new ArrayList();

        int id;

        public rule(int ids)
        {
            InitializeComponent();

            addRolelist();
            int[] coalesce = request.getRuleCoalesce(ids);

            
            id = ids;

            comboBox1.DataSource = role;

            if (coalesce[0] != -1)
            {
                if (coalesce[1] > 0)
                    read(ids);
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

            dataGridView2.ReadOnly = true;


            }

            public void read(int id)
            {

            Color[] col = new Color[] { Color.LightSeaGreen, Color.SpringGreen, Color.GreenYellow, Color.Honeydew, Color.Lavender, Color.LightPink, Color.Wheat, Color.Violet };

            col[id] = Color.White;

            BackColor = col[0];

            dataGridView1.Rows.Clear();

            List<authorInfo> tiList = request.getTableList();

            if (tiList.Count > 0)
            {
                for (int n = 0; n < tiList.Count; n++)
                    dataGridView1.Rows.Add(tiList[n].id, tiList[n].name, 0, 0, 0, 0);

                List<authorInfo> aiList = request.getRoleList(id.ToString());

                
                if (aiList.Count > 0)
                {

                    for (int i = 0; i < aiList.Count; i++)
                    {

                        dataGridView2.Rows.Add(aiList[i].name);
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0].Style.BackColor = col[aiList[i].id];

                        
                        List<controlActionList> riList = request.getRoleControlList(aiList[i].id.ToString());
                        if (riList.Count > 0)
                        {
                            for (int j = 0; j < riList.Count; j++)
                            {
                                foreach( DataGridViewRow rows in dataGridView1.Rows)
                                {
                                   
                                    if (Convert.ToInt32(rows.Cells[0].Value) == riList[j].idTable)
                                    {
                                        if (riList[j].W.Equals("1"))
                                        {
                                            rows.Cells[2].Value = riList[j].W;
                                            if (aiList[i].id != id)
                                            {
                                                rows.Cells[2].ReadOnly = true;
                                                rows.Cells[2].Style.BackColor = col[aiList[i].id];
                                            }
                                        }
                                        if (riList[j].R.Equals("1"))
                                        {
                                            rows.Cells[3].Value = riList[j].R;
                                            if (aiList[i].id != id)
                                            {
                                                rows.Cells[3].ReadOnly = true;
                                                rows.Cells[3].Style.BackColor = col[aiList[i].id];
                                            }
                                        }
                                        if (riList[j].D.Equals("1"))
                                        {
                                            rows.Cells[4].Value = riList[j].D;
                                            if (aiList[i].id != id)
                                            {
                                                rows.Cells[4].ReadOnly = true;
                                                rows.Cells[4].Style.BackColor = col[aiList[i].id];
                                            }
                                        }
                                        if (riList[j].E.Equals("1"))
                                        {
                                            rows.Cells[5].Value = riList[j].E;
                                            if (aiList[i].id != id)
                                            {
                                                rows.Cells[5].ReadOnly = true;
                                                rows.Cells[5].Style.BackColor = col[aiList[i].id];
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                dataGridView2.Rows.Add(" ");
                dataGridView2.Rows[dataGridView2.Rows.Count-1].Cells[0].Selected = true;
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

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!edl.Contains(e.RowIndex))
            {
                edl.Add(e.RowIndex);
            }
        }

        private void addRolelist()
        {
            List<authorInfo> rolList = request.getroleUserList();

            if (rolList.Count > 0)
            {
                for (int i = 0; i < rolList.Count; i++)
                    role.Add(rolList[i].name);
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

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < edl.Count; i++)
            {
                int idtables = Convert.ToInt32(dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[0].Value);

                string write = "1";
                string read = "1";
                string delete = "1";
                string edit = "1";

                if (dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[2].Value.ToString().Equals("0") | dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[2].ReadOnly)
                    write = "0";
                if (dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[3].Value.ToString().Equals("0") | dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[3].ReadOnly)
                    read = "0";
                if (dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[4].Value.ToString().Equals("0") | dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[4].ReadOnly)
                    delete = "0";
                if (dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[5].Value.ToString().Equals("0") | dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[5].ReadOnly)
                    edit = "0";


                string response = request.updRulsRoles(id, idtables, write, read, delete, edit);

                new ans().message(response);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            edl.Clear();
            id = comboBox1.SelectedIndex;
            id++;
            read(id);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}