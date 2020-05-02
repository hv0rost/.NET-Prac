using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NalogDataBase
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlDataAdapter dataAdapter = null;
        private DataTable DataTable = null;
        private SqlCommandBuilder sqlBuilder = null;
        private DataSet dataSet = null;

        private bool flag = false;

        public Form1()
        {
            InitializeComponent();
        }
     
        void LoadDataGridView()
        {
            try
            {
                dataAdapter = new SqlDataAdapter("SELECT * , 'Delete' AS [Control Cell] FROM Nalog", sqlConnection);
                sqlBuilder = new SqlCommandBuilder(dataAdapter);

                sqlBuilder.GetDeleteCommand();
                sqlBuilder.GetInsertCommand();
                sqlBuilder.GetUpdateCommand();

                dataSet = new DataSet();

                dataAdapter.Fill(dataSet, "Nalog");
                dataGridView1.DataSource = dataSet.Tables["Nalog"];

                for(int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[13, i] = linkCell;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void UpdateDataGridView()
        {
            try
            {
                dataSet.Tables["Nalog"].Clear();

                dataAdapter.Fill(dataSet, "Nalog");
                dataGridView1.DataSource = dataSet.Tables["Nalog"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[13, i] = linkCell;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\USER\.NET-Prac\NalogDataBase\Database1.mdf;Integrated Security=True");
            sqlConnection.Open();
            LoadDataGridView();
        }
     
        private void updateDataGridViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateDataGridView();
            MessageBox.Show("DataGridView has updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(e.ColumnIndex == 13)
                {
                    string task = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();


                    if(task == "Delete")
                    {
                        if (MessageBox.Show("Do you want to delete this row?", "Deletion",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            dataGridView1.Rows.RemoveAt(rowIndex);
                            dataSet.Tables["Nalog"].Rows[rowIndex].Delete();
                            dataAdapter.Update(dataSet, "Nalog");
                        }
                    }
                    else if (task == "Insert")
                    {
                        int rowIndex = dataGridView1.Rows.Count - 2;
                        DataRow row = dataSet.Tables["Nalog"].NewRow();
                        DataRow row1 = dataSet.Tables["Nalog"].NewRow();

                        row["count"] = dataGridView1.Rows[rowIndex].Cells["count"].Value;
                        row["price_for_one"] = dataGridView1.Rows[rowIndex].Cells["price_for_one"].Value;
                        row["cost_proiz"] = dataGridView1.Rows[rowIndex].Cells["cost_proiz"].Value;
                        row["other_cost"] = dataGridView1.Rows[rowIndex].Cells["other_cost"].Value;
                        row["cost_paper"] = dataGridView1.Rows[rowIndex].Cells["cost_paper"].Value;
                        row["fines"] = dataGridView1.Rows[rowIndex].Cells["fines"].Value;
                        row["cost_dessolution"] = dataGridView1.Rows[rowIndex].Cells["cost_dessolution"].Value;

                        int income, costs, the_tax_base;

                        row["income"] = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["count"].Value) *
                            Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["price_for_one"].Value) +
                            Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["fines"].Value);
                        income = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["count"].Value) *
                            Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["price_for_one"].Value) +
                            Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["fines"].Value);

                        row["costs"] = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["cost_proiz"].Value) +
                            Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["other_cost"].Value) +
                            Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["cost_paper"].Value) +
                            Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["cost_dessolution"].Value);
                        costs = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["cost_proiz"].Value) +
                            Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["other_cost"].Value) +
                            Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["cost_paper"].Value) +
                            Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["cost_dessolution"].Value);

                        the_tax_base = income - costs;
                        row["the_tax_base"] = the_tax_base;
                        

                        row["simplified_tax_system"] = the_tax_base * 0.1;

                        row["simplified_tax_system_min"] = income * 0.01;


                        dataSet.Tables["Nalog"].Rows.Add(row);
                        dataSet.Tables["Nalog"].Rows.RemoveAt(dataSet.Tables["Nalog"].Rows.Count - 1);
                        dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);
                        dataGridView1.Rows[e.RowIndex].Cells[13].Value = "Delete";

                        dataAdapter.Update(dataSet, "Nalog");

                        flag = false;
                    }
                    else if (task == "Change")
                    {
                        int r = e.RowIndex;

                        dataSet.Tables["Nalog"].Rows[r]["count"] = dataGridView1.Rows[r].Cells["count"].Value;
                        dataSet.Tables["Nalog"].Rows[r]["price_for_one"] = dataGridView1.Rows[r].Cells["price_for_one"].Value;
                        dataSet.Tables["Nalog"].Rows[r]["cost_proiz"] = dataGridView1.Rows[r].Cells["cost_proiz"].Value;
                        dataSet.Tables["Nalog"].Rows[r]["other_cost"] = dataGridView1.Rows[r].Cells["other_cost"].Value;
                        dataSet.Tables["Nalog"].Rows[r]["cost_paper"] = dataGridView1.Rows[r].Cells["cost_paper"].Value;
                        dataSet.Tables["Nalog"].Rows[r]["fines"] = dataGridView1.Rows[r].Cells["fines"].Value;
                        dataSet.Tables["Nalog"].Rows[r]["cost_dessolution"] = dataGridView1.Rows[r].Cells["cost_dessolution"].Value;

                        int temp;
                        double  temp_float;

                        temp = Convert.ToInt32(dataGridView1.Rows[r].Cells["count"].Value)
                        * Convert.ToInt32(dataGridView1.Rows[r].Cells["price_for_one"].Value)
                        + Convert.ToInt32(dataGridView1.Rows[r].Cells["fines"].Value);

                        dataSet.Tables["Nalog"].Rows[r]["income"] = temp;

                        temp = Convert.ToInt32(dataGridView1.Rows[r].Cells["cost_proiz"].Value)
                        + Convert.ToInt32(dataGridView1.Rows[r].Cells["other_cost"].Value)
                        + Convert.ToInt32(dataGridView1.Rows[r].Cells["cost_paper"].Value)
                        + Convert.ToInt32(dataGridView1.Rows[r].Cells["cost_dessolution"].Value);

                        dataSet.Tables["Nalog"].Rows[r]["costs"] = temp;

                        temp = Convert.ToInt32(dataGridView1.Rows[r].Cells["income"].Value)
                        - Convert.ToInt32(dataGridView1.Rows[r].Cells["costs"].Value);

                        dataSet.Tables["Nalog"].Rows[r]["the_tax_base"] = temp;

                        temp_float = Convert.ToDouble(dataSet.Tables["Nalog"].Rows[r]["the_tax_base"]) * 0.1;
                        dataSet.Tables["Nalog"].Rows[r]["simplified_tax_system"] = temp_float;

                        temp_float = Convert.ToDouble(dataSet.Tables["Nalog"].Rows[r]["income"]) * 0.01;
                        dataSet.Tables["Nalog"].Rows[r]["simplified_tax_system_min"] = temp_float;

                        dataAdapter.Update(dataSet, "Nalog");

                        dataGridView1.Rows[e.RowIndex].Cells[13].Value = "Delete";
                    }
                    UpdateDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                if (flag == false)
                {
                    flag = true;

                    int lastRow = dataGridView1.Rows.Count - 2;
                    DataGridViewRow row = dataGridView1.Rows[lastRow];
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[13, lastRow] = linkCell;
                    row.Cells["Control Cell"].Value = "Insert";
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (flag == false)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow editRow = dataGridView1.Rows[rowIndex];

                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[13, rowIndex] = linkCell;
                    editRow.Cells["Control Cell"].Value = "Change";

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(KeyPressed);

            if (dataGridView1.CurrentCell.ColumnIndex < 8)
            {
                TextBox textBox = e.Control as TextBox;

                if (textBox != null)
                {
                    textBox.KeyPress += new KeyPressEventHandler(KeyPressed);
                }
            }
            else
            {
                TextBox textBox = e.Control as TextBox;
                textBox.KeyPress += new KeyPressEventHandler(KeyCanNotBePressed);
                MessageBox.Show("You can`t fill this cell by your", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void KeyCanNotBePressed(object sender, KeyPressEventArgs e)
        {
                e.Handled = true;
        }

        private void exportToFastReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            report1.Show();
        }

    }
}
