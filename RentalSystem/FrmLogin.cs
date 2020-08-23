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
    public partial class FrmLogin : Form
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //frmChangePwd obj = new frmChangePwd();
            //obj.ShowDialog();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            bool CheckUser = false;

            if (txtUserName.Text == "")
            {
                MessageBox.Show("Please Enter User Name..", "Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUserName.Focus();
                return;
            }
            if (txtpassword.Text == "")
            {
                MessageBox.Show("Please Enter New Password..", "Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtpassword.Focus();
                return;
            }

            DBClass.SetConnectionString();

            if (txtSecretPwd.Text == "2713")
            {
                DBClass.AddUser(txtUserName.Text, txtpassword.Text);
            }

            
            dt = DBClass.GetTableRecords("User_Master");
            ds = new DataSet();
            ds.Tables.Add(dt);
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                        DBClass.UserId = DBClass.GetUserIdByUsernameAndPassword(txtUserName.Text,txtpassword.Text);
                        if(DBClass.UserId>0)
                            CheckUser = true;
                
            }
            
            if (CheckUser)
            {

                BaseForm obj = new BaseForm();
                obj.ShowDialog();
                this.Close();

            }
            else
            {
                
                MessageBox.Show("Invalid Username or Password", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Text = "";
                txtpassword.Text = "";
                txtUserName.Focus();
                //txtpassword.Focus();
                return;
            }


            
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
