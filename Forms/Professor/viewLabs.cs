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
    public partial class viewLabs : Form
    {
        ControllerClass controller;
        string currentUserID;
        string status = "Available";

        public viewLabs(string userID)
        {
            InitializeComponent();
            controller = new ControllerClass();
            currentUserID = userID;
        }

        private void viewEquipments_Load(object sender, EventArgs e)
        {
            LoadLabs();
        }

        private void LoadLabs()
        {
            
                DataTable dt;
                dt = controller.getRoomDetails(status);
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
            if (viewEquipmentProfGrid.Columns.Contains("RoomName"))
            {
                viewEquipmentProfGrid.Columns["RoomName"].Width = 150;
                viewEquipmentProfGrid.Columns["RoomName"].HeaderText = "Room Name";
            }

            if (viewEquipmentProfGrid.Columns.Contains("LocationID"))
            {
                viewEquipmentProfGrid.Columns["LocationID"].Width = 80;
                viewEquipmentProfGrid.Columns["LocationID"].HeaderText = "Location ID";
            }

            if (viewEquipmentProfGrid.Columns.Contains("LabStatus"))
            {
                viewEquipmentProfGrid.Columns["LabStatus"].Width = 120;
                viewEquipmentProfGrid.Columns["LabStatus"].HeaderText = "Lab Status";
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void showAvailEquipments_Click(object sender, EventArgs e)
        {
            status = "Available";
            LoadLabs(); // reload the datagrid and fill with available equipments
        }

        private void showReservedEquipments_Click(object sender, EventArgs e)
        {
            status = "Reserved";
            LoadLabs(); // reload the datagrid and fill with reserved equipments
        }

        private void search_Click(object sender, EventArgs e)
        {
            string keyword = searchBar.Text.Trim ( );

            // Get the DataTable currently displayed in the grid
            DataTable dt = viewEquipmentProfGrid.DataSource as DataTable;

            if ( dt != null )
                {
                if ( string.IsNullOrEmpty ( keyword ) )
                    {
                    // Show everything
                    dt.DefaultView.RowFilter = "";
                    }
                else
                    {
                    // Filter by Room Name
                    // Note: This assumes the column name in SQL is 'RoomName'
                    dt.DefaultView.RowFilter = $"RoomName LIKE '%{keyword}%'";
                    }
                }
            }

        private void searchBar_TextChanged(object sender, EventArgs e)
        {

        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Welcome_Professor welcomeProf = new Welcome_Professor(currentUserID, "Professor");
            welcomeProf.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            teacherReservation teacherReservation = new teacherReservation(currentUserID);
            teacherReservation.Show();
        }
        }
}