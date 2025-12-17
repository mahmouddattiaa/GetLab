using GetLab.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetLab.Forms.Professor
{
    public partial class Welcome_Professor : GetLab.Forms.BaseForm
    {
        string userID;
        string role;
        public Welcome_Professor(string userID , string role)
        {
            InitializeComponent();
            this.userID = userID;
            this.role = role;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            teacherReservation teacherReservation = new teacherReservation(userID);
            teacherReservation.Show();
        }

        private void Welcome_Professor_Load ( object sender, EventArgs e )
            {

            }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            viewLabs viewLabs = new viewLabs(userID);
            viewLabs.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            requestEquipment requestEquipment = new requestEquipment(userID);
            requestEquipment.Show();
        }
    }
}
