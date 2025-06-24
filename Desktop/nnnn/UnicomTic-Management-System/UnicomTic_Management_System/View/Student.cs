using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using UnicomTic_Management_System.Data;


namespace UnicomTic_Management_System.View
{
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
        }

        private void Student_Load(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            dgvStudents.Rows.Clear();
            using (var conn = DatabaseManager.GetConnection())
            {
                string query = "SELECT Id, Name, Address, NIC FROM Students";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dgvStudents.Rows.Add(
                            reader["Id"],
                            reader["Name"],
                            reader["Address"],
                            reader["NIC"]
                        );
                    }
                }
            }
        
        if (dgvStudents.Columns.Count == 0)
        {
            dgvStudents.Columns.Add("Id", "Id");
            dgvStudents.Columns["Id"].Visible = false; // Hide Id column if you want
            dgvStudents.Columns.Add("Name", "Name");
            dgvStudents.Columns.Add("Address", "Address");
            dgvStudents.Columns.Add("NIC", "NIC");
        }
    }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string address = txtAddress.Text.Trim();
            string nic = txtNic.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(nic))
            {
                MessageBox.Show("All fields are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var conn = DatabaseManager.GetConnection())
            {
                string query = "INSERT INTO Students (Name, Address, NIC) VALUES (@Name, @Address, @NIC)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@NIC", nic);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Student added successfully!");
            LoadStudents();
            ClearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a student to delete.");
                return;
            }
            int studentId = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["Id"].Value);

            using (var conn = DatabaseManager.GetConnection())
            {
                string query = "DELETE FROM Students WHERE Id = @Id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", studentId);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Student deleted successfully!");
            LoadStudents();
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a student to update.");
                return;
            }
            int studentId = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["Id"].Value);
            string name = txtName.Text.Trim();
            string address = txtAddress.Text.Trim();
            string nic = txtNic.Text.Trim();

            using (var conn = DatabaseManager.GetConnection())
            {
                string query = "UPDATE Students SET Name = @Name, Address = @Address, NIC = @NIC WHERE Id = @Id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@NIC", nic);
                    cmd.Parameters.AddWithValue("@Id", studentId);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Student updated successfully!");
            LoadStudents();
            ClearFields();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           /* if (dgvStudents.SelectedRows.Count > 0)
            {
                txtName.Text = dgvStudents.SelectedRows[0].Cells["Name"].Value.ToString();
                txtAddress.Text = dgvStudents.SelectedRows[0].Cells["Address"].Value.ToString();
                txtNic.Text = dgvStudents.SelectedRows[0].Cells["NIC"].Value.ToString();
            }*/

        }
        private void ClearFields()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtNic.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
