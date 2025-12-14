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
using ControllerClass = GetLab.Controller.Controller;

namespace GetLab.Forms.Professor
{
    public partial class viewEquipments : Form
    {
        ControllerClass controller;
        string currentUserID;
        string userRole;
        string status = "Available";

        public viewEquipments(string userID, string role)
        {
            InitializeComponent();
            controller = new ControllerClass();
            currentUserID = userID;
            userRole = role;
        }

        private void viewEquipments_Load(object sender, EventArgs e)
        {
            LoadEquipmentData();
        }

        private void LoadEquipmentData()
        {
            
                DataTable dt;
                dt = controller.getEquipmentUsingStatus(status);
                viewEquipmentProfGrid.DataSource = dt;
                SetupGridView();

        }
         
        

        private void SetupGridView()
        {
            viewEquipmentProfGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            viewEquipmentProfGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            viewEquipmentProfGrid.ReadOnly = true;
            viewEquipmentProfGrid.AllowUserToAddRows = false;
            viewEquipmentProfGrid.AllowUserToDeleteRows = false;
            viewEquipmentProfGrid.MultiSelect = false;

            if (viewEquipmentProfGrid.Columns.Contains("EquipmentID"))
            {
                viewEquipmentProfGrid.Columns["EquipmentID"].Width = 50;
                viewEquipmentProfGrid.Columns["EquipmentID"].HeaderText = "ID";
            }

            // Set column headers to friendly names
            if (viewEquipmentProfGrid.Columns.Contains("EquipmentName"))
            {
                viewEquipmentProfGrid.Columns["EquipmentName"].HeaderText = "Equipment Name";
            }
            if (viewEquipmentProfGrid.Columns.Contains("ModelName"))
                viewEquipmentProfGrid.Columns["ModelName"].HeaderText = "Model";
            if (viewEquipmentProfGrid.Columns.Contains("RoomName"))
            {
                viewEquipmentProfGrid.Columns["RoomName"].HeaderText = "Location";
                viewEquipmentProfGrid.Columns["RoomName"].Width = 150;
            }
            if (viewEquipmentProfGrid.Columns.Contains("CurrentStatus"))
            {
                viewEquipmentProfGrid.Columns["CurrentStatus"].Width = 200;
                viewEquipmentProfGrid.Columns["CurrentStatus"].HeaderText = "Status";
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void showAvailEquipments_Click(object sender, EventArgs e)
        {
            status = "Available";
            LoadEquipmentData(); // reload the datagrid and fill with available equipments
        }

        private void showReservedEquipments_Click(object sender, EventArgs e)
        {
            status = "Reserved";
            LoadEquipmentData(); // reload the datagrid and fill with reserved equipments
        }

        private void showBorrowedEquipment_Click(object sender, EventArgs e)
        {
            status = "Borrowed";
            LoadEquipmentData(); // reload the datagrid and fill with borrowed equipments
        }

        private void search_Click(object sender, EventArgs e)
        {
            string keyword = searchBar.Text;
            DataTable dt;
            dt = controller.SearchEquipment(keyword);
            viewEquipmentProfGrid.DataSource = dt;
        }

        private void searchBar_TextChanged(object sender, EventArgs e)
        {

        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Welcome_Professor welcomeProf = new Welcome_Professor(currentUserID, userRole);
            welcomeProf.Show();
        }
    }
}