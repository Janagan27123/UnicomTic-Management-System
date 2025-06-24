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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            /*var studentForm = new Student();
            this.Hide();
            studentForm.ShowDialog();
            this.Close();*/

            var Student = new UnicomTic_Management_System.View.Student();
            this.Hide();
            Student.ShowDialog();
            this.Close();
        }

        private void btnExam_Click(object sender, EventArgs e)
        {
            var Exam = new UnicomTic_Management_System.View.Exam();
            this.Hide();
            Exam.ShowDialog();
            this.Close();

        }

        private void btnTimetable_Click(object sender, EventArgs e)
        {
            var Timetable = new UnicomTic_Management_System.View.Timetable();
            this.Hide();
            Timetable.ShowDialog();
            this.Close();

        }

        private void btnCourse_Click(object sender, EventArgs e)
        {
            var Course = new UnicomTic_Management_System.View.Course();
            this.Hide();
            Course.ShowDialog();
            this.Close();

        }

        private void btnMarks_Click(object sender, EventArgs e)
        {
            var Marks = new UnicomTic_Management_System.View.Marks();
            this.Hide();
            Marks.ShowDialog();
            this.Close();

        }
    }
}
