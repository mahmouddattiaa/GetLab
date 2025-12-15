using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControllerClass = GetLab.Controller.Controller;

namespace GetLab.Forms.Professor
{
    public partial class teacherReservation : Form
    {
        string currentUserID;
        ControllerClass controller;
        public teacherReservation(string currentUserID)
        {
            InitializeComponent();
            this.currentUserID = currentUserID;
            controller = new ControllerClass();
        }

        private void teacherReservation_Load(object sender, EventArgs e)
        {
            makeReserTeacherGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            makeReserTeacherGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            makeReserTeacherGrid.ReadOnly = true;
            makeReserTeacherGrid.AllowUserToAddRows = false;
            makeReserTeacherGrid.AllowUserToDeleteRows = false;
            makeReserTeacherGrid.MultiSelect = false;
            DataTable dt = controller.getRoomName();
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "RoomName";
            comboBox1.ValueMember = "LocationID";
            string selectedRoom = comboBox1.SelectedValue.ToString();
            DataTable equipment = controller.GetAvailableEquipmentByLab(selectedRoom);
            makeReserTeacherGrid.DataSource = equipment;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                string selectedRoom = comboBox1.SelectedValue.ToString();
                DataTable equipment = controller.GetAvailableEquipmentByLab(selectedRoom);
                makeReserTeacherGrid.DataSource = equipment;
            
        }

        private void makeReserTeacherGrid_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
