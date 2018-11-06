using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Lab4
{
    public partial class Form1 : Form
    {
        private Color prevColor;
        private DataGridViewRow lastSelectedRow;
        private bool linked = false;
        private bool filtered = false;
        DataGridViewRow row = new DataGridViewRow();

        public Form1()
        {
            InitializeComponent();
            prevColor = BackColor;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "avtodorDataSet.Assignment". При необходимости она может быть перемещена или удалена.
            this.assignmentTableAdapter.Fill(this.avtodorDataSet.Assignment);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "avtodorDataSet.Department". При необходимости она может быть перемещена или удалена.
            this.departmentTableAdapter.Fill(this.avtodorDataSet.Department);

        }

        private void ascendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedIndex < 0)
                toolStripComboBox1.SelectedIndex = 0;

            departmentDataGridView.Sort(departmentDataGridView.Columns[toolStripComboBox1.SelectedIndex], System.ComponentModel.ListSortDirection.Ascending);
        }

        private void descendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedIndex < 0)
                toolStripComboBox1.SelectedIndex = 0;

            departmentDataGridView.Sort(departmentDataGridView.Columns[toolStripComboBox1.SelectedIndex], System.ComponentModel.ListSortDirection.Descending);
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            if (!filtered)
            {
                string filter = toolStripComboBox1.SelectedItem.ToString();
                departmentBindingSource.Filter = filter + " = '" + toolStripTextBox2.Text + "'";
                filtered = true;
            } else
            {
                departmentBindingSource.RemoveFilter();
                filtered = false;
            }
        }

        private void changeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BackColor.Equals(prevColor))
                BackColor = Color.Aqua;
            else
                BackColor = prevColor;
        }

        private void changeFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            departmentDataGridView.Columns[0].HeaderCell.Style.Font = new Font("Times New Roman", 9, FontStyle.Italic);
            departmentDataGridView.Columns[1].HeaderCell.Style.Font = new Font("Times New Roman", 9, FontStyle.Italic);
            departmentDataGridView.Columns[2].HeaderCell.Style.Font = new Font("Times New Roman", 9, FontStyle.Italic);
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            departmentBindingSource.MoveFirst();
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            departmentBindingSource.MovePrevious();
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            departmentBindingSource.MoveNext();
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            departmentBindingSource.MoveLast();
        }

 
        private void departmentDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DepartmentIdTextField.Text = departmentDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            DepartmentNameTextField.Text = departmentDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            DepartmentDescriptionTextField.Text = departmentDataGridView.SelectedRows[0].Cells[2].Value.ToString();

            lastSelectedRow = departmentDataGridView.SelectedRows[0];
        }

        private void ConfirmDepartmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                departmentDataGridView.SelectedRows[0].Cells[0].Value = int.Parse(DepartmentIdTextField.Text);
                departmentDataGridView.SelectedRows[0].Cells[1].Value = DepartmentNameTextField.Text;
                departmentDataGridView.SelectedRows[0].Cells[2].Value = DepartmentDescriptionTextField.Text;

                this.Validate();
                this.departmentBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.avtodorDataSet);

            }
            catch
            {
                MessageBox.Show("Make sure your input is of correct data type");
            }
        }

 /*       private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (departmentDataGridView.SelectedRows.Count == 0)
                departmentDataGridView.Rows.Remove(departmentDataGridView.Rows[0]);
             else
                departmentDataGridView.Rows.Remove(departmentDataGridView.SelectedRows[0]);
            this.tableAdapterManager.UpdateAll(this.avtodorDataSet);
        }*/

        private void deleteBindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (departmentDataGridView.SelectedRows[0] != row)
                {
                    assignmentDataGridView.DataSource = ((DataRowView)departmentDataGridView.SelectedRows[0].DataBoundItem).Row.GetChildRows("FK_Assignment_Department").CopyToDataTable();
                    row = departmentDataGridView.SelectedRows[0];
                }
                else
                {
                assignmentDataGridView.DataSource = assignmentDataGridView;
                    linked = false;
                }
        }

        private void ConfirmAssignmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                assignmentDataGridView.SelectedRows[0].Cells[0].Value = int.Parse(AssignmentIdTextField.Text);
                assignmentDataGridView.SelectedRows[0].Cells[1].Value = AssignmentDescriptionTextField.Text;
                assignmentDataGridView.SelectedRows[0].Cells[2].Value = AssignmentCreationDatePicker.Text;
                assignmentDataGridView.SelectedRows[0].Cells[3].Value = AssignmentEndDatePicker.Text;
                assignmentDataGridView.SelectedRows[0].Cells[4].Value = AssignmentRecomendationsTextField.Text;
                assignmentDataGridView.SelectedRows[0].Cells[5].Value = int.Parse(AssignmentAssigneeIdTextField.Text);
                assignmentDataGridView.SelectedRows[0].Cells[6].Value = int.Parse(AssignmentDepartmentIdTextField.Text);

                this.Validate();
                this.assignmentBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.avtodorDataSet);

            }
            catch
            {
                MessageBox.Show("Make sure your input is of correct data type");
            }
        }

        private void assignmentDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            AssignmentIdTextField.Text = assignmentDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            AssignmentDescriptionTextField.Text = assignmentDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            AssignmentCreationDatePicker.Text = assignmentDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            AssignmentEndDatePicker.Text = assignmentDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            AssignmentRecomendationsTextField.Text = assignmentDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            AssignmentAssigneeIdTextField.Text = assignmentDataGridView.SelectedRows[0].Cells[5].Value.ToString();
            AssignmentDepartmentIdTextField.Text = assignmentDataGridView.SelectedRows[0].Cells[6].Value.ToString();

        }

        private void createLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            row = departmentDataGridView.SelectedRows[0];
            assignmentDataGridView.DataSource = ((DataRowView)departmentDataGridView.SelectedRows[0].DataBoundItem).Row.GetChildRows("FK_Assignment_Department").CopyToDataTable();
            linked = true;
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            bool success = false;
            if (toolStripComboBox2.SelectedItem.ToString().Equals("Id"))
            {

             success = int.TryParse(toolStripTextBox3.Text, out int pk);
                if (!success)
                {
                    MessageBox.Show("Input must be digit for Id");
                    toolStripTextBox3.Text = String.Empty;
                } else
                    departmentBindingSource.Position = departmentBindingSource.Find("Id", pk);
            }
            else if (toolStripComboBox2.SelectedItem.ToString().Equals("Name"))
            {
                departmentBindingSource.Position = departmentBindingSource.Find("Name", toolStripTextBox3.Text);
            } else if (toolStripComboBox2.SelectedItem.ToString().Equals("Description"))
            {
                departmentBindingSource.Position = departmentBindingSource.Find("Description", toolStripTextBox3.Text);
            }


        }
    }
}
