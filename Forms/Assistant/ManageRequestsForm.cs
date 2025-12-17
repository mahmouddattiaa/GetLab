using System;
using System.Data;
using System.Windows.Forms;
using GetLab.Controller;
using ControllerClass = GetLab.Controller.Controller;

namespace GetLab.Forms.Assistant
    {
    public partial class ManageRequestsForm : Form
        {
        private ControllerClass controller;
        private string adminID;

        public ManageRequestsForm ( string adminID )
            {
            InitializeComponent ( );
            controller = new ControllerClass ( );
            this.adminID = adminID;
            }

        private void ManageRequestsForm_Load ( object sender, EventArgs e )
            {
            LoadRequests ( );
            }

        private void LoadRequests ( )
            {
            DataTable dt = controller.GetPendingRequests ( );
            dgvRequests.DataSource = dt;
            dgvRequests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRequests.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRequests.MultiSelect = false;
            }

        private void ProcessRequest ( string status )
            {
            if ( dgvRequests.SelectedRows.Count == 0 )
                {
                MessageBox.Show ( "Please select a request." );
                return;
                }

            int requestID = Convert.ToInt32 ( dgvRequests.SelectedRows[0].Cells["RequestID"].Value );

            bool success = controller.UpdateRequestStatus ( requestID, status, adminID );

            if ( success )
                {
                MessageBox.Show ( $"Request {status} Successfully!" );
                LoadRequests ( ); // Refresh grid
                }
            else
                {
                MessageBox.Show ( "Error updating request." );
                }
            }

        private void btnApprove_Click ( object sender, EventArgs e )
            {
            ProcessRequest ( "Approved" );
            }

        private void btnDeny_Click ( object sender, EventArgs e )
            {
            ProcessRequest ( "Denied" );
            }

        private void btnClose_Click ( object sender, EventArgs e )
            {
            Close ();
            }
        }
    }