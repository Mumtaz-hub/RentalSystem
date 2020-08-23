using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RentalSystem
{
    

    public partial class BaseForm : Form
    {
        System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        public enum Btn
        {
            DashBoard = 1,
            Order = 2,
            Payment = 3,
            Report = 4,
            Settings = 5,
            BackupRestore = 6
        }

        //int SelectedButtonn = (int)Btn.DashBoard;
        public BaseForm()
        {
            InitializeComponent();
            myTimer.Tick += new System.EventHandler(myTimer_Tick);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            SelectedButtonNew((int)Btn.DashBoard);
            this.pnlMiddle.Controls.Clear();
            Dashboard obj = new Dashboard();
            obj.TopLevel = false;
            obj.AutoScroll = true;
            this.pnlMiddle.Controls.Add(obj);
            obj.Show();
        }

        private void btnNewOrder_Click(object sender, EventArgs e)
        {
            SelectedButtonNew((int)Btn.Order);
            this.pnlMiddle.Controls.Clear();
            frmentry obj = new frmentry();
            obj.TopLevel = false;
            obj.AutoScroll = true;
            this.pnlMiddle.Controls.Add(obj);
            obj.Show();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            SelectedButtonNew((int)Btn.Payment);
            this.pnlMiddle.Controls.Clear();
            frmPayment obj = new frmPayment();
            obj.TopLevel = false;
            obj.AutoScroll = true;
            this.pnlMiddle.Controls.Add(obj);
            obj.Show();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            SelectedButtonNew((int)Btn.Report);
            this.pnlMiddle.Controls.Clear();
            Report obj = new Report();
            obj.TopLevel = false;
            obj.AutoScroll = true;
            this.pnlMiddle.Controls.Add(obj);
            obj.Show();
        }

        private void btnItemSettings_Click(object sender, EventArgs e)
        {
            SelectedButtonNew((int)Btn.Settings);
            this.pnlMiddle.Controls.Clear();
            LedMaster obj = new LedMaster();
            obj.TopLevel = false;
            obj.AutoScroll = true;
            this.pnlMiddle.Controls.Add(obj);
            obj.Show();
        }

        private void btnBackupRestore_Click(object sender, EventArgs e)
        {
            SelectedButtonNew((int)Btn.BackupRestore);
            this.pnlMiddle.Controls.Clear();
            BackupRestore obj = new BackupRestore();
            obj.TopLevel = false;
            obj.AutoScroll = true;
            this.pnlMiddle.Controls.Add(obj);
            obj.Show();
        }

        
        private void SelectedButtonNew(int selected)
        {
            btnDashboard.BackColor = System.Drawing.Color.Black;
            btnNewOrder.BackColor = System.Drawing.Color.Black;
            btnPayment.BackColor = System.Drawing.Color.Black;
            btnReport.BackColor = System.Drawing.Color.Black;
            btnItemSettings.BackColor = System.Drawing.Color.Black;
            btnBackupRestore.BackColor = System.Drawing.Color.Black;

            switch (selected)
            {
                case (int)Btn.DashBoard:
                    btnDashboard.BackColor = System.Drawing.Color.DodgerBlue;
                    break;
                case (int)Btn.Order:
                    btnNewOrder.BackColor = System.Drawing.Color.DodgerBlue;
                    break;
                case (int)Btn.Payment:
                    btnPayment.BackColor = System.Drawing.Color.DodgerBlue;
                    break;
                case (int)Btn.Report:
                    btnReport.BackColor = System.Drawing.Color.DodgerBlue;
                    break;
                case (int)Btn.Settings:
                    btnItemSettings.BackColor = System.Drawing.Color.DodgerBlue;
                    break;
                case (int)Btn.BackupRestore:
                    btnBackupRestore.BackColor = System.Drawing.Color.DodgerBlue;
                    break;
            }
        }

        string msg = @"MNS SoftTech Support By Mumtaz Shaikh,Contact No->91 9724035505                                                                                                                                     ";
         
        int pos = 0;
        //Timer tick event handler
        private void myTimer_Tick(object sender, System.EventArgs e)
        {
            if (pos < msg.Length)
            {
                txtMessage.AppendText(msg.Substring(pos, 1));
                ++pos;
            }
            else
            {
                //myTimer.Stop();
                pos = 0;
                txtMessage.Text = "";
            }

//            txtMessage.Text = msg;
            
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            myTimer.Interval = 300;
            //Start timer
            myTimer.Start();

            SelectedButtonNew((int)Btn.DashBoard);
            this.pnlMiddle.Controls.Clear();
            Dashboard obj = new Dashboard();
            obj.TopLevel = false;
            obj.AutoScroll = true;
            this.pnlMiddle.Controls.Add(obj);
            obj.Show();
        }

        
    }
}
