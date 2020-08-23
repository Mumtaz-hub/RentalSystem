using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RentalSystem
{
    public partial class LedMaster : Form
    {
        public LedMaster()
        {
            InitializeComponent();
        }

        DataSet dsMain = new DataSet();
        SqlDataAdapter _MainAdapter;
        
        private void LedMaster_Load(object sender, EventArgs e)
        {
            displayRecord();
        }

        private void displayRecord()
        {
            _MainAdapter = DBClass.GetAdaptor("ItemMaster");
            dsMain = new DataSet();

            _MainAdapter.Fill(dsMain);
            dsMain.Tables[0].TableName = "ItemMaster";
            dgvLED.DataSource = null;
            dgvLED.DataSource = dsMain.Tables["ItemMaster"];
            dgvLED.Columns["ItemCode"].ReadOnly = true;
            dgvLED.Columns["ItemCode"].HeaderText = "No.";
            dgvLED.Columns["ItemCode"].Width = 50;

            dgvLED.Columns["ItemName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLED.Columns["ItemName"].HeaderText = "Items Name";

            dgvLED.Columns["UnitPrice"].Width = 100;
            dgvLED.Columns["UnitPrice"].HeaderText = "Items Price"; 


        }

        private void LedMaster_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void dgvLED_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!dsMain.HasChanges()) return;
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(_MainAdapter);
            _MainAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
            _MainAdapter.InsertCommand = commandBuilder.GetInsertCommand();
            _MainAdapter.Update(dsMain.Tables["ItemMaster"]);

            dsMain.Tables["ItemMaster"].Clear();
            _MainAdapter = DBClass.GetAdaptor("ItemMaster");
            _MainAdapter.Fill(dsMain.Tables["ItemMaster"]);
        }

        private void dgvLED_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (!dsMain.HasChanges()) return;
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(_MainAdapter);
            _MainAdapter.DeleteCommand = commandBuilder.GetDeleteCommand();
            _MainAdapter.Update(dsMain.Tables["ItemMaster"]);
        }

        
    }
}
