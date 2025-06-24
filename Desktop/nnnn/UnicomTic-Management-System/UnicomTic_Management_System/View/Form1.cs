using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnicomTic_Management_System.View
{
    public partial class Loginform : Form
    {
        public Loginform()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Student");
            comboBox1.Items.Add("Admin");
            comboBox1.Items.Add("Lecturer");
            comboBox1.Items.Add("Staff");

            string selectedRole = comboBox1.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedRole))
            {
                MessageBox.Show("You selected: " + selectedRole);
            }
            
        }
        private void comboBoxRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            string role = comboBox1.SelectedItem.ToString();
            MessageBox.Show("Role selected: " + role);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dashboard = new UnicomTic_Management_System.View.Dashboard();
            this.Hide();
            dashboard.ShowDialog();
            this.Close();

        }
    }
}
