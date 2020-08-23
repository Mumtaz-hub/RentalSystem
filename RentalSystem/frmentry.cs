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
    public partial class frmentry : Form
    {
        

        public frmentry()
        {
            InitializeComponent();
            //Set the timer tick event
        
        }

        // Common Variables///////////////////////
        BaseForm BaseFormObj = new BaseForm();
        DataSet dsForCombo = new DataSet();
        DataTable _DataTable;
        DataSet dsMain = new DataSet();
        SqlDataAdapter _ItemAdapter;
        SqlDataAdapter _CustomerAdapter;
        SqlDataAdapter _MainAdapter;
        SqlDataAdapter _MainDTAdapter;
        int IntRowIndex = 0;

        #region Form Events
        private void frmentry_Load(object sender, EventArgs e)
        {
            Fill_Combo();
            DisplayRecord();
            Enable_Disable_Controls(true);
        }

        private void frmentry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
        #endregion

        #region Fill All Records
        private void Fill_Combo()
        {

            _DataTable = new DataTable();

            _CustomerAdapter = DBClass.GetAdapterByQuery("Select distinct CustomerID,CustomerName from OrderMain");
            _CustomerAdapter.Fill(_DataTable);
            dsForCombo.Tables.Add(_DataTable);
            dsForCombo.Tables[0].TableName = "Customer";

            cmbcustomername.DisplayMember = "CustomerName";
            cmbcustomername.ValueMember = "CustomerID";
            cmbcustomername.DataSource = dsForCombo.Tables["Customer"];

            _ItemAdapter = DBClass.GetAdaptor("ItemMaster");
            _DataTable = new DataTable();
            _ItemAdapter.Fill(_DataTable);

            dsForCombo.Tables.Add(_DataTable);
            dsForCombo.Tables[1].TableName = "ItemMaster";

        }
        private void DisplayRecord()
        {

            _MainAdapter = DBClass.GetAdaptor("OrderMain");
            _DataTable = new DataTable();
            _MainAdapter.Fill(_DataTable);

            dsMain.Tables.Add(_DataTable);
            dsMain.Tables[0].TableName = "OrderMain";

            _MainDTAdapter = DBClass.GetAdaptor("OrderDetail");
            _DataTable = new DataTable();
            _MainDTAdapter.Fill(_DataTable);

            dsMain.Tables.Add(_DataTable);
            dsMain.Tables[1].TableName = "OrderDetail";

            DataColumn tcolumn = new DataColumn();
            tcolumn.DataType = System.Type.GetType("System.Decimal");
            tcolumn.ColumnName = "Total";
            tcolumn.Expression = "Qty * UnitPrice ";
            dsMain.Tables["OrderDetail"].Columns.Add(tcolumn);

            Set_Grid();
            FillOrders(0);

        }
        private void FillOrders(int Index)
        {
            if (dsMain.Tables["OrderMain"].Rows.Count > 0)
            {
                txtOrderID.Text = dsMain.Tables["OrderMain"].Rows[Index]["OrderID"].ToString();
                txtOrderDate.Text = Convert.ToDateTime(dsMain.Tables["OrderMain"].Rows[Index]["OrderDate"].ToString()).ToShortDateString();
                txtDeliveryDate.Text =Convert.ToDateTime(dsMain.Tables["OrderMain"].Rows[Index]["Deliverydate"].ToString()).ToShortDateString();
                txtCustmorid.Text = dsMain.Tables["OrderMain"].Rows[Index]["CustomerID"].ToString();
                cmbcustomername.Text = dsMain.Tables["OrderMain"].Rows[Index]["CustomerName"].ToString();
                txtContectNo.Text = dsMain.Tables["OrderMain"].Rows[Index]["Contactno"].ToString();
                txtAdders.Text = dsMain.Tables["OrderMain"].Rows[Index]["Place"].ToString();
                txtDiscount.Text = dsMain.Tables["OrderMain"].Rows[Index]["Discount"].ToString();
                txtAdvance.Text = dsMain.Tables["OrderMain"].Rows[Index]["Advance_Amt"].ToString();

                DisplayDetails(txtOrderID.Text);
                DisplayView();
            }
        }
        private void Set_Grid()
        {
            dgvOrderDetail.AutoGenerateColumns = false;
            
            DataGridViewTextBoxColumn DetailId = new DataGridViewTextBoxColumn();
            DetailId.Name = "DetailId";
            DetailId.DataPropertyName = "DetailId";
            DetailId.HeaderText = "DetailId";
            DetailId.ReadOnly = true;
            dgvOrderDetail.Columns.Add(DetailId);

            DataGridViewTextBoxColumn OrderId = new DataGridViewTextBoxColumn();
            OrderId.Name = "OrderId";
            OrderId.DataPropertyName = "OrderId";
            OrderId.HeaderText = "OrderId";
            OrderId.ReadOnly = true;
            dgvOrderDetail.Columns.Add(OrderId);

            DataGridViewComboBoxColumn ItemColumn = new DataGridViewComboBoxColumn();
            ItemColumn.Name = "ItemCode";
            ItemColumn.DataPropertyName = "ItemCode";
            ItemColumn.HeaderText = "Item Name";
            ItemColumn.ReadOnly = false;
            ItemColumn.DataSource = dsForCombo.Tables["ItemMaster"];
            ItemColumn.DisplayMember = "ItemName";
            ItemColumn.ValueMember = "ItemCode";
            dgvOrderDetail.Columns.Add(ItemColumn);

            DataGridViewTextBoxColumn UnitPriceColumn = new DataGridViewTextBoxColumn();
            UnitPriceColumn.Name = "UnitPrice";
            UnitPriceColumn.DataPropertyName = "UnitPrice";
            UnitPriceColumn.HeaderText = "Unit Price";
            UnitPriceColumn.ReadOnly = true;
            UnitPriceColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            UnitPriceColumn.DefaultCellStyle.BackColor = System.Drawing.Color.Silver;
            dgvOrderDetail.Columns.Add(UnitPriceColumn);

            DataGridViewTextBoxColumn QtyColumn = new DataGridViewTextBoxColumn();
            QtyColumn.Name = "Qty";
            QtyColumn.DataPropertyName = "Qty";
            QtyColumn.HeaderText = "Qty";
            QtyColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            QtyColumn.ReadOnly = false;
            dgvOrderDetail.Columns.Add(QtyColumn);

            DataGridViewTextBoxColumn TotalColumn = new DataGridViewTextBoxColumn();
            TotalColumn.Name = "Total";
            TotalColumn.DataPropertyName = "Total";
            TotalColumn.HeaderText = "Total";
            TotalColumn.ReadOnly = false;
            TotalColumn.DefaultCellStyle.BackColor = System.Drawing.Color.Silver;
            TotalColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOrderDetail.Columns.Add(TotalColumn);

            dgvOrderDetail.DataSource = dsMain.Tables["OrderDetail"];

            dgvOrderDetail.Columns["DetailId"].Visible = false;
            dgvOrderDetail.Columns["Orderid"].Visible = false;
            dgvOrderDetail.Columns["Total"].Visible = true;
            dgvOrderDetail.Columns["ItemCode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }
        private void DisplayDetails(string OrderID)
        {
            dsMain.Tables["OrderDetail"].DefaultView.RowFilter = "";
            dsMain.Tables["OrderDetail"].DefaultView.RowFilter = " orderid=" + int.Parse(OrderID);
            //dsMain.Tables["OrderDetail"].DefaultView.RowFilter = string.Concat("CONVERT(", "OrderID", "System.String) LIKE '%", OrderID, "%'");
            dgvOrderDetail.DataSource = dsMain.Tables["OrderDetail"].DefaultView;
            CalculateTotal();
        }
        private void CalculateTotal()
        {
            DataTable DT = new DataTable();
            DT = dsMain.Tables["OrderDetail"].DefaultView.ToTable();
            if (DT.Rows.Count > 0)
            {

                decimal TotalOrder = DT.AsEnumerable().Sum(r => r.Field<decimal?>("Total") ?? 0);
               //decimal TotalOrder = decimal.Parse(DT.Compute("sum(Total)", "").ToString());
                txttotalPrice.Text = TotalOrder.ToString();

                string DiscAmt = (txtDiscount.Text == "") ? "0" : txtDiscount.Text;
                string AdvAmt = (txtAdvance.Text == "") ? "0" : txtAdvance.Text;

                decimal NetAmt = TotalOrder - Convert.ToDecimal(DiscAmt) - Convert.ToDecimal(AdvAmt);
                txtnetamount.Text = NetAmt.ToString();
            }
            else
            {
                txttotalPrice.Text = "0";
                txtnetamount.Text = "0";
            }
        }
        private void DisplayView()
        {
            string Query = @"select o.Deliverydate,o.CustomerName,o.Place,i.ItemName,d.Qty from OrderMain o
                               inner join OrderDetail d on o.OrderID=d.OrderId 
                                left join ItemMaster i on d.ItemCode=i.ItemCode
                                where o.Deliverydate>=GETDATE()-1 
                                order by o.Deliverydate";
            DataTable dtview = new DataTable();
            dtview = DBClass.GetTableByQuery(Query);
            dgvView.DataSource = null;
            dgvView.DataSource = dtview;
        }
        #endregion

        #region Handle Customer Combo
        private int GetNewID(string FieldName, string TableName,DataSet ds)
        {
            int nextId = 0;
            if (ds.Tables[TableName].Rows.Count == 0)
            {
                nextId = 1;
            }
            else
            {
                DataRow row = ds.Tables[TableName].Select("" + FieldName + "=MAX(" + FieldName + ")")[0];
                nextId = Convert.ToInt32(row[FieldName]) + 1;
            }

            return nextId;
        }
        private void FillCustomerDetail(string Code)
        {
            if (dsForCombo.Tables["Customer"].Rows.Count > 0)
            {
                DataRow[] row = dsForCombo.Tables["Customer"].Select("CustomerID=" + Code);
                if (row.Length != 0)
                {
                    if (!row[0].IsNull("CustomerName"))
                    {
                        cmbcustomername.Text = row[0]["CustomerName"].ToString();
                    }
                }
            }
        }
        private void cmbcustomername_Validated(object sender, EventArgs e)
        {
            if (cmbcustomername.SelectedValue != null)
            {
                txtCustmorid.Text = cmbcustomername.SelectedValue.ToString();
            }
            if (txtCustmorid.Text == "" && cmbcustomername.Text != "")
            {
                txtCustmorid.Text = GetNewID("CustomerID", "Customer",dsForCombo).ToString();
            }
            else if (txtCustmorid.Text != "" && cmbcustomername.Text != "")
            {
                FillCustomerDetail(txtCustmorid.Text);
            }

        }
        #endregion

        #region Transaction Functions
        private void Enable_Disable_Controls(bool Val)
        {
            btn_New.Enabled = Val;
            btn_Edit.Enabled = Val;
            btn_Delete.Enabled = Val;
            btn_Clear.Enabled = Val;
            btn_Save.Enabled = !Val;
            btn_Clear.Enabled = !Val;
        }
        private void ClearRecord()
        {
            txtOrderID.Text = "";
            txtOrderDate.Text = "";
            txtDeliveryDate.Text = "";
            txtContectNo.Text = "";
            txtCustmorid.Text = "";
            txtAdders.Text = "";
            cmbcustomername.Text = "";

            txttotalPrice.Text = "0";
            txtDiscount.Text = "0";
            txtAdvance.Text = "0";
            txtnetamount.Text = "0";
        }
        private void NewRecord()
        {
            ClearRecord();

            int maxid = DBClass.GetIdByQuery("SELECT IDENT_CURRENT('OrderMain')+1");
            txtOrderID.Text = maxid.ToString();

            txtOrderDate.Text = System.DateTime.Now.ToShortDateString();
            if (dsForCombo.Tables["Customer"].Rows.Count > 0)
            {
                //txtCustmorid.Text = dsForCombo.Tables["Customer"].Rows[0]["CustomerID"].ToString();
                //cmbcustomername.SelectedValue = txtCustmorid.Text;
            }
           
            dsMain.Tables["OrderDetail"].DefaultView.RowFilter = "";
            dsMain.Tables["OrderDetail"].DefaultView.RowFilter = " Orderid=" + int.Parse(txtOrderID.Text);
            dgvOrderDetail.DataSource = dsMain.Tables["OrderDetail"].DefaultView;

            Enable_Disable_Controls(false);
            cmbcustomername.Focus();
        }
        private void SaveRedord()
        {
            DataRow[] row = dsMain.Tables["OrderMain"].Select("Orderid=" + int.Parse(txtOrderID.Text));
            if (row.Length == 0)
            {
                    _MainAdapter.InsertCommand = new SqlCommand(@"insert into OrderMain(OrderDate,Deliverydate,CustomerID,CustomerName,Contactno,Place,Discount,Advance_Amt) 
                                                                output inserted.Orderid
                                                                Values(@OrderDate,@Deliverydate,@CustomerID,@CustomerName,@Contactno,@Place,@Discount,@Advance_Amt)", DBClass.connection);
                    DBClass.connection.Open();

                    _MainAdapter.InsertCommand.Parameters.AddWithValue("@OrderDate", txtOrderDate.Text);
                    _MainAdapter.InsertCommand.Parameters.AddWithValue("@Deliverydate", txtDeliveryDate.Text);
                    _MainAdapter.InsertCommand.Parameters.AddWithValue("@CustomerID", int.Parse(txtCustmorid.Text));
                    _MainAdapter.InsertCommand.Parameters.AddWithValue("@CustomerName", cmbcustomername.Text);
                    _MainAdapter.InsertCommand.Parameters.AddWithValue("@Contactno", txtContectNo.Text);
                    _MainAdapter.InsertCommand.Parameters.AddWithValue("@Place", txtAdders.Text);
                    string DiscAmt = (txtDiscount.Text == "") ? "0" : txtDiscount.Text;
                    _MainAdapter.InsertCommand.Parameters.AddWithValue("@Discount", Convert.ToDecimal(DiscAmt));
                    string AdvAmt = (txtDiscount.Text == "") ? "0" : txtAdvance.Text;
                    _MainAdapter.InsertCommand.Parameters.AddWithValue("@Advance_Amt", decimal.Parse(AdvAmt));

                    int id = (int)_MainAdapter.InsertCommand.ExecuteScalar();

                    txtOrderID.Text = id.ToString();
                    DBClass.connection.Close();

            }
            else
            {
                    DBClass.connection.Open();

                    _MainAdapter.UpdateCommand = new SqlCommand(@"update OrderMain set OrderDate=@OrderDate,Deliverydate=@Deliverydate,CustomerID=@CustomerID,CustomerName=@CustomerName,
                                                                Contactno=@Contactno,Place=@Place,Discount=@Discount,Advance_Amt=@Advance_Amt 
                                                                where OrderID= @OrderID ", DBClass.connection);

                    _MainAdapter.UpdateCommand.Parameters.AddWithValue("@OrderID", txtOrderID.Text);
                    _MainAdapter.UpdateCommand.Parameters.AddWithValue("@OrderDate", txtOrderDate.Text);
                    _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Deliverydate", txtDeliveryDate.Text);
                    _MainAdapter.UpdateCommand.Parameters.AddWithValue("@CustomerID", int.Parse(txtCustmorid.Text));
                    _MainAdapter.UpdateCommand.Parameters.AddWithValue("@CustomerName", cmbcustomername.Text);
                    _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Contactno", txtContectNo.Text);
                    _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Place", txtAdders.Text);
                    string DiscAmt = (txtDiscount.Text == "") ? "0" : txtDiscount.Text;
                    _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Discount", Convert.ToDecimal(DiscAmt));
                    string AdvAmt = (txtDiscount.Text == "") ? "0" : txtAdvance.Text;
                    _MainAdapter.UpdateCommand.Parameters.AddWithValue("@Advance_Amt", decimal.Parse(AdvAmt));

                    _MainAdapter.UpdateCommand.ExecuteNonQuery();
                    DBClass.connection.Close();
                    
            }


            dsMain.Tables["OrderMain"].Clear();
            _MainAdapter.Fill(dsMain.Tables["OrderMain"]);

            //////////////// Save Order Detail ///////////

            
            //foreach (DataRow row1 in dsMain.Tables["OrderDetail"].Rows)
            //{
            //    row1["OrderId"] = txtOrderID.Text;
                
            //}

            //dsMain.Tables["OrderDetail"].Columns["OrderId"].DefaultValue = txtOrderID.Text;

            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(_MainDTAdapter);
            _MainDTAdapter.UpdateCommand = cmdBuilder.GetUpdateCommand();
            _MainDTAdapter.InsertCommand = cmdBuilder.GetInsertCommand();
            _MainDTAdapter.Update(dsMain.Tables["OrderDetail"]);

            dsMain.Tables["OrderDetail"].Clear();

            _MainDTAdapter = DBClass.GetAdaptor("OrderDetail");
            _MainDTAdapter.Fill(dsMain.Tables["OrderDetail"]);

            dsForCombo.Tables["Customer"].Clear();
            _CustomerAdapter = DBClass.GetAdapterByQuery("Select distinct CustomerID,CustomerName from OrderMain");
            _CustomerAdapter.Fill(dsForCombo.Tables["Customer"]);
            FillCustomerDetail(txtCustmorid.Text);
            FillOrders(dsMain.Tables["OrderMain"].Rows.Count-1);
            Enable_Disable_Controls(true);
            
        }
        private void DeleteRecord()
        {
            if (MessageBox.Show("Are you Sure to delete this record ? ","Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
               

                DataTable dt=DBClass.GetTableByQuery("Select count(*) from PaymentDetail where OrderId=" + Convert.ToInt16(txtOrderID.Text));
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() != "0")
                    {
                        MessageBox.Show("Payment Detail Exist of this order,Can't be delete!");
                        return;
                    }
                }

                DBClass.connection.Open();

                _MainDTAdapter.DeleteCommand = new SqlCommand(@"Delete From OrderDetail where OrderID= @OrderID ", DBClass.connection);
                _MainDTAdapter.DeleteCommand.Parameters.AddWithValue("@OrderID", txtOrderID.Text);
                _MainDTAdapter.DeleteCommand.ExecuteNonQuery();

                dsMain.Tables["OrderDetail"].Clear();
                _MainDTAdapter.Fill(dsMain.Tables["OrderDetail"]);

                _MainAdapter.DeleteCommand = new SqlCommand(@"Delete From OrderMain where OrderID= @OrderID ", DBClass.connection);
                _MainAdapter.DeleteCommand.Parameters.AddWithValue("@OrderID", txtOrderID.Text);
                _MainAdapter.DeleteCommand.ExecuteNonQuery();

                dsMain.Tables["OrderMain"].Clear();
                _MainAdapter.Fill(dsMain.Tables["OrderMain"]);

                DBClass.connection.Close();
                if (dsMain.Tables["OrderMain"].Rows.Count > 0)
                    FillOrders(dsMain.Tables["OrderMain"].Rows.Count - 1);
                else
                    ClearRecord();

                DisplayView();
            }
        }
        #endregion

        #region Navigation Events
        private void btn_First_Click(object sender, EventArgs e)
        {
            FillOrders(0);
            IntRowIndex = 0;
        }
        private void btn_previous_Click(object sender, EventArgs e)
        {
            IntRowIndex--;
            if (IntRowIndex >= 0)
            {
                FillOrders(IntRowIndex);
            }
            else
            {
                FillOrders(0);
                IntRowIndex = 0;
            }
        }
        private void btn_next_Click(object sender, EventArgs e)
        {
            IntRowIndex++;
            if (IntRowIndex <= dsMain.Tables["OrderMain"].Rows.Count - 1)
            {
                FillOrders(IntRowIndex);
            }
            else
            {
                FillOrders(dsMain.Tables["OrderMain"].Rows.Count - 1);
                IntRowIndex = dsMain.Tables["OrderMain"].Rows.Count - 1;
            }
        }
        private void btn_last_Click(object sender, EventArgs e)
        {
            FillOrders(dsMain.Tables["OrderMain"].Rows.Count - 1);
            IntRowIndex = dsMain.Tables["OrderMain"].Rows.Count - 1;
        }
        #endregion
         
        #region Transaction Events
        private void btn_New_Click(object sender, EventArgs e)
        {
            NewRecord();
        }
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            Enable_Disable_Controls(false);
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            SaveRedord();
        }
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            DeleteRecord();
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Enable_Disable_Controls(true);
        }
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearRecord();
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Grid Events
        private void dgvOrderDetail_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            if ((dgvOrderDetail.Rows.Count != 0))
            {
                e.Row.Cells["OrderId"].Value = txtOrderID.Text;
                e.Row.Cells["DetailId"].Value = DBClass.GetIdByQuery("SELECT IDENT_CURRENT('OrderDetail')+1");
            }
        }
        private void dgvOrderDetail_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if ((dgvOrderDetail.Columns[e.ColumnIndex].Name).ToUpper()=="ITEMCODE")
            {
                if (dsForCombo.Tables["ItemMaster"].Rows.Count > 0 && dgvOrderDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()!="")
                {
                    DataRow[] row = dsForCombo.Tables["ItemMaster"].Select("ITEMCODE=" + int.Parse(dgvOrderDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
                    if (row.Length != 0)
                    {
                        if (!row[0].IsNull("UnitPrice"))
                        {
                            dgvOrderDetail.Rows[e.RowIndex].Cells["UnitPrice"].Value = row[0]["UnitPrice"].ToString();
                        }
                    }
                }

            }
            if ((dgvOrderDetail.Columns[e.ColumnIndex].Name).ToUpper() == "QTY")
            {
                if (dsForCombo.Tables["ItemMaster"].Rows.Count > 0 && dgvOrderDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()!="")
                {
                    //dgvOrderDetail.Rows[e.RowIndex].Cells["Total"].Value = decimal.Parse(dgvOrderDetail.Rows[e.RowIndex].Cells["UnitPrice"].Value.ToString()) * int.Parse(dgvOrderDetail.Rows[e.RowIndex].Cells["QTY"].Value.ToString());
                        
                }
                CalculateTotal();
            }
        }
        private void dgvOrderDetail_RowValidated(object sender, DataGridViewCellEventArgs e)
        {

            //if (dsMain.Tables.Contains("OrderDetail"))
            //{
            //    DataTable Dt = dsMain.Tables["OrderDetail"].Clone();

            //    DataRow dr = Dt.NewRow();
            //    DataGridViewRow dgvR = (DataGridViewRow)dgvOrderDetail.CurrentRow.Clone();
            //    dr[0] = dgvR.Cells[0].Clone();
            //    dr[1] = dgvR.Cells[1].Clone();

            //    Dt.Rows.Add(dgvR);
            //    dgvOrderDetail.DataSource = Dt;
            //}
        }
        private void dgvOrderDetail_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        #endregion

        #region Delivery Date
        private void btnDeliveryDate_Click(object sender, EventArgs e)
        {
            if(deliverydateCalendar.Visible == false)
                deliverydateCalendar.Visible = true;
            else
                deliverydateCalendar.Visible = false;
        }
        private void deliverydateCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            
        }
        #endregion

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void txtAdvance_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void deliverydateCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtDeliveryDate.Text = deliverydateCalendar.SelectionEnd.ToShortDateString();
            deliverydateCalendar.Visible = false;
        }

        private void btn_Find_Click(object sender, EventArgs e)
        {

        }

    }
}