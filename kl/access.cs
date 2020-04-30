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
    public partial class access : Form
    {
        ArrayList edl = new ArrayList();
        ArrayList role = new ArrayList();

        int id;

        public access(int ids)
        {
            InitializeComponent();



            addRolelist();

            int[] coalesce = request.getAccessCoalesce(ids);

            id = ids;

            comboBox1.DataSource = role;

            if (coalesce[0] != -1)
            {
                if (coalesce[1] > 0)
                    readlist(id);
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

        public void readlist(int id)
        {
            Color[] col = new Color[] { Color.LightSeaGreen, Color.SpringGreen, Color.GreenYellow, Color.Honeydew, Color.Lavender, Color.LightPink, Color.Wheat, Color.Violet };

            col[id] = Color.White;
            
            BackColor = col[0];

            dataGridView1.Rows.Clear();


                dataGridView1.Rows.Add(0, 0, 0, 0, 0, 0, 0);

                List<authorInfo> aiList = request.getRoleList(id.ToString());


                if (aiList.Count > 0)
                {

                    for (int i = 0; i < aiList.Count; i++)
                    {

                        dataGridView2.Rows.Add(aiList[i].name);
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0].Style.BackColor = col[aiList[i].id];

                        List<controlUserList> riList = request.getAccessControlList(aiList[i].id.ToString());
                        if (riList.Count > 0)
                        {
                            for (int j = 0; j < riList.Count; j++)
                            {
                                foreach (DataGridViewRow rows in dataGridView1.Rows)
                                {

                                        if (riList[j].auth.Equals("1"))
                                        {
                                            rows.Cells[0].Value = riList[j].auth;
                                            if (aiList[i].id != id)
                                            {
                                                rows.Cells[0].ReadOnly = true;
                                                rows.Cells[0].Style.BackColor = col[aiList[i].id];
                                            }
                                        }
                                        if (riList[j].gen.Equals("1"))
                                        {
                                            rows.Cells[1].Value = riList[j].gen;
                                            if (aiList[i].id != id)
                                            {
                                                rows.Cells[1].ReadOnly = true;
                                                rows.Cells[1].Style.BackColor = col[aiList[i].id];
                                            }
                                        }
                                        if (riList[j].pub.Equals("1"))
                                        {
                                            rows.Cells[2].Value = riList[j].pub;
                                            if (aiList[i].id != id)
                                            {
                                                rows.Cells[2].ReadOnly = true;
                                                rows.Cells[2].Style.BackColor = col[aiList[i].id];
                                            }
                                        }
                                        if (riList[j].user.Equals("1"))
                                        {
                                            rows.Cells[3].Value = riList[j].user;
                                            if (aiList[i].id != id)
                                            {
                                                rows.Cells[3].ReadOnly = true;
                                                rows.Cells[3].Style.BackColor = col[aiList[i].id];
                                            }
                                        }
                                        if (riList[j].book.Equals("1"))
                                        {
                                            rows.Cells[4].Value = riList[j].book;
                                            if (aiList[i].id != id)
                                            {
                                                rows.Cells[4].ReadOnly = true;
                                                rows.Cells[4].Style.BackColor = col[aiList[i].id];
                                            }
                                        }
                                        if (riList[j].contAct.Equals("1"))
                                        {
                                            rows.Cells[5].Value = riList[j].contAct;
                                            if (aiList[i].id != id)
                                            {
                                                rows.Cells[5].ReadOnly = true;
                                                rows.Cells[5].Style.BackColor = col[aiList[i].id];
                                            }
                                        }
                                        if (riList[j].contUser.Equals("1"))
                                        {
                                            rows.Cells[6].Value = riList[j].contUser;
                                            if (aiList[i].id != id)
                                            {
                                                rows.Cells[6].ReadOnly = true;
                                                rows.Cells[6].Style.BackColor = col[aiList[i].id];
                                            }
                                        }
                                        if (riList[j].jou.Equals("1"))
                                        {
                                            rows.Cells[7].Value = riList[j].jou;
                                            if (aiList[i].id != id)
                                            {
                                                rows.Cells[7].ReadOnly = true;
                                                rows.Cells[7].Style.BackColor = col[aiList[i].id];
                                            }
                                        }
                                }
                            }
                        }
                    }
                }
                dataGridView2.Rows.Add(" ");
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0].Selected = true;

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
                string aths = "1";
                string gnrs = "1";
                string pbls = "1";
                string usrs = "1";
                string blss = "1";
                string cntls = "1";
                string scsls = "1";
                string jrls = "1";

                if (dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[0].Value.ToString().Equals("0") | dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[0].ReadOnly)
                    aths = "0";
                if (dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[1].Value.ToString().Equals("0") | dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[1].ReadOnly)
                    gnrs = "0";
                if (dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[2].Value.ToString().Equals("0") | dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[2].ReadOnly)
                    pbls = "0";
                if (dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[3].Value.ToString().Equals("0") | dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[3].ReadOnly)
                    usrs = "0";
                if (dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[4].Value.ToString().Equals("0") | dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[4].ReadOnly)
                    blss = "0";
                if (dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[5].Value.ToString().Equals("0") | dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[5].ReadOnly)
                    cntls = "0";
                if (dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[6].Value.ToString().Equals("0") | dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[6].ReadOnly)
                    scsls = "0";
                if (dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[7].Value.ToString().Equals("0") | dataGridView1.Rows[Convert.ToInt32(edl[i].ToString())].Cells[7].ReadOnly)
                    jrls = "0";

                string response = request.updRulsRoless(id, aths, gnrs, pbls, usrs, blss, cntls, scsls, jrls);

                new ans().message(response);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            edl.Clear();
            id = comboBox1.SelectedIndex;
            id++;
            readlist(id);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
