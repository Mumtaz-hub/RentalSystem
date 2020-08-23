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
    public partial class frmPayment : Form
    {
        public frmPayment()
        {
            InitializeComponent();
        }

        DataSet dsForCombo = new DataSet();
        DataTable _DataTable;
        DataSet dsMain = new DataSet();
        SqlDataAdapter _CustomerAdapter;
        SqlDataAdapter _OrderAdapter;
        SqlDataAdapter _PayAdapter;


        string Query = @"select o.CustomerID,o.OrderID,o.Deliverydate,
                        (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID)  as 'TotalAmt',o.Discount,o.Advance_Amt,
                        (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt as 'NetAmt',
                        (select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID) as 'PaidAmt',
                        'Paid' as 'Select'
                        from OrderMain o 
                        where (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt-(select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID)=0

                        union

                        select o.CustomerID,o.OrderID,o.Deliverydate,
                        (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID)  as 'TotalAmt',o.Discount,o.Advance_Amt,
                        (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt as 'NetAmt',
                        (select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID) as 'PaidAmt',
                        'Payment Due' as 'Select'
                        from OrderMain o 
                        where (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt-(select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID)>0

                        union

                        select o.CustomerID,o.OrderID,o.Deliverydate,
                        (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID)  as 'TotalAmt',o.Discount,o.Advance_Amt,
                        (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt as 'NetAmt',
                        (select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID) as 'PaidAmt',
                        'InComplete' as 'Select'
                        from OrderMain o 
                        where o.OrderId NOT IN (select OrderId from PaymentDetail)";

        /*string Query = @"select o.CustomerID,o.OrderID,o.Deliverydate,o.Discount,o.Advance_Amt,
                            SUM(d.Qty*d.UnitPrice) as 'TotalAmt', 
                            SUM(d.Qty*d.UnitPrice)-o.Discount-o.Advance_Amt as 'NetAmt', 
                            SUM(p.Paid_Amt) as 'PaidAmount','Paid' as 'Select'
                            from OrderMain o 
                            left join OrderDetail d on o.OrderID=d.OrderId 
                            left join PaymentDetail p on o.OrderID=p.OrderId
                            group by o.CustomerID,o.OrderID,o.Deliverydate,o.Discount,o.Advance_Amt 
                            having ((SUM(d.Qty*d.UnitPrice)-o.Discount-o.Advance_Amt)-(SUM(p.Paid_Amt))) =0  

                            union

                            select o.CustomerID,o.OrderID,o.Deliverydate,o.Discount,o.Advance_Amt,
                            SUM(d.Qty*d.UnitPrice) as 'TotalAmt', 
                            SUM(d.Qty*d.UnitPrice)-o.Discount-o.Advance_Amt as 'NetAmt', 
                            SUM(p.Paid_Amt) as 'PaidAmount','Payment Due' as 'Select'
                            from OrderMain o 
                            left join OrderDetail d on o.OrderID=d.OrderId 
                            left join PaymentDetail p on o.OrderID=p.OrderId
                            group by o.CustomerID,o.OrderID,o.Deliverydate,o.Discount,o.Advance_Amt 
                            having ((SUM(d.Qty*d.UnitPrice)-o.Discount-o.Advance_Amt)-(SUM(p.Paid_Amt))) >0 

                            union

                            select o.CustomerID,o.OrderID,o.Deliverydate,o.Discount,o.Advance_Amt,
                            SUM(d.Qty*d.UnitPrice) as 'TotalAmt', 
                            SUM(d.Qty*d.UnitPrice)-o.Discount-o.Advance_Amt as 'NetAmt', 
                            SUM(p.Paid_Amt) as 'PaidAmount','InComplete' as 'Select'
                            from OrderMain o 
                            left join OrderDetail d on o.OrderID=d.OrderId 
                            left join PaymentDetail p on o.OrderID=p.OrderId
                            where o.OrderId NOT IN (select OrderId from PaymentDetail)
                            group by o.CustomerID,o.OrderID,o.Deliverydate,o.Discount,o.Advance_Amt 
                            order by o.CustomerID,o.OrderID,o.Deliverydate,o.Discount,o.Advance_Amt";
        */
        private void frmPayment_Load(object sender, EventArgs e)
        {
            Fill_Combo();
            DisplayRecord();
            cmbSelect.SelectedIndex = 0;
            
        }
        
        #region Common Function
        private void Fill_Combo()
        {
            _DataTable = new DataTable();

            _CustomerAdapter = DBClass.GetAdapterByQuery("Select distinct CustomerID,CustomerName from OrderMain");
            _CustomerAdapter.Fill(_DataTable);
            dsForCombo.Tables.Add(_DataTable);
            dsForCombo.Tables[0].TableName = "Customer";

            cmbCustomerName.DisplayMember = "CustomerName";
            cmbCustomerName.ValueMember = "CustomerID";
            cmbCustomerName.DataSource = dsForCombo.Tables["Customer"];
        }
        private void DisplayRecord()
        {
            _OrderAdapter = DBClass.GetAdapterByQuery(Query);
            _OrderAdapter.Fill(dsMain);
            dsMain.Tables[0].TableName = "Order";

            _PayAdapter = DBClass.GetAdaptor("PaymentDetail");
            _PayAdapter.Fill(dsMain);
            dsMain.Tables[1].TableName = "PaymentDetail";
            
            Set_OrderGrid();
            Set_PaymentGrid();
            Filter_OrderDetail();
            Filter_PaymentDetail();

        }
        private void Set_OrderGrid()
        {
            dgvOrder.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn CustomerId = new DataGridViewTextBoxColumn();
            CustomerId.Name = "CustomerId";
            CustomerId.DataPropertyName = "CustomerId";
            CustomerId.HeaderText = "CustomerId";
            CustomerId.ReadOnly = true;
            dgvOrder.Columns.Add(CustomerId);

            DataGridViewTextBoxColumn OrderId = new DataGridViewTextBoxColumn();
            OrderId.Name = "OrderId";
            OrderId.DataPropertyName = "OrderId";
            OrderId.HeaderText = "OrderId";
            OrderId.ReadOnly = true;
            OrderId.DefaultCellStyle.BackColor = System.Drawing.Color.Silver;
            OrderId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderId.Width = 70;
            dgvOrder.Columns.Add(OrderId);

            DataGridViewTextBoxColumn Deliverydate = new DataGridViewTextBoxColumn();
            Deliverydate.Name = "Deliverydate";
            Deliverydate.DataPropertyName = "Deliverydate";
            Deliverydate.HeaderText = "Delivery date";
            Deliverydate.ReadOnly = true;
            Deliverydate.DefaultCellStyle.BackColor = System.Drawing.Color.Silver;
            Deliverydate.Width = 80;
            dgvOrder.Columns.Add(Deliverydate);

            DataGridViewTextBoxColumn TotalAmt = new DataGridViewTextBoxColumn();
            TotalAmt.Name = "TotalAmt";
            TotalAmt.DataPropertyName = "TotalAmt";
            TotalAmt.HeaderText = "Total Amount";
            TotalAmt.ReadOnly = true;
            TotalAmt.DefaultCellStyle.BackColor = System.Drawing.Color.Silver;
            TotalAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOrder.Columns.Add(TotalAmt);

            DataGridViewTextBoxColumn Discount = new DataGridViewTextBoxColumn();
            Discount.Name = "Discount";
            Discount.DataPropertyName = "Discount";
            Discount.HeaderText = "Discount";
            Discount.ReadOnly = true;
            Discount.Width = 80;
            Discount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Discount.DefaultCellStyle.BackColor = System.Drawing.Color.Silver;
            dgvOrder.Columns.Add(Discount);

            DataGridViewTextBoxColumn Advance_Amt = new DataGridViewTextBoxColumn();
            Advance_Amt.Name = "Advance_Amt";
            Advance_Amt.DataPropertyName = "Advance_Amt";
            Advance_Amt.HeaderText = "Advance Amount";
            Advance_Amt.ReadOnly = true;
            Advance_Amt.Width = 80;
            Advance_Amt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Advance_Amt.DefaultCellStyle.BackColor = System.Drawing.Color.Silver;
            dgvOrder.Columns.Add(Advance_Amt);

            DataGridViewTextBoxColumn NetAmt = new DataGridViewTextBoxColumn();
            NetAmt.Name = "NetAmt";
            NetAmt.DataPropertyName = "NetAmt";
            NetAmt.HeaderText = "Net Amount";
            NetAmt.ReadOnly = true;
            NetAmt.DefaultCellStyle.BackColor = System.Drawing.Color.Silver;
            NetAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOrder.Columns.Add(NetAmt);

            DataGridViewTextBoxColumn PaidAmt = new DataGridViewTextBoxColumn();
            PaidAmt.Name = "PaidAmt";
            PaidAmt.DataPropertyName = "PaidAmt";
            PaidAmt.HeaderText = "Paid Amount";
            PaidAmt.ReadOnly = true;
            PaidAmt.DefaultCellStyle.BackColor = System.Drawing.Color.Silver;
            PaidAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOrder.Columns.Add(PaidAmt);

            DataGridViewTextBoxColumn Select = new DataGridViewTextBoxColumn();
            Select.Name = "Select";
            Select.DataPropertyName = "Select";
            Select.HeaderText = "Select";
            Select.ReadOnly = true;
            Select.DefaultCellStyle.BackColor = System.Drawing.Color.Silver;
            Select.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOrder.Columns.Add(Select);

            dgvOrder.DataSource = dsMain.Tables["Order"];

            dgvOrder.Columns["CustomerId"].Visible = false;
            dgvOrder.Columns["Orderid"].Visible = true;
            dgvOrder.Columns["NetAmt"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }
        private void Set_PaymentGrid()
        {
            dgvPayment.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn PaymentId = new DataGridViewTextBoxColumn();
            PaymentId.Name = "PaymentId";
            PaymentId.DataPropertyName = "PaymentId";
            PaymentId.HeaderText = "Payment Id";
            PaymentId.ReadOnly = true;
            PaymentId.DefaultCellStyle.BackColor = System.Drawing.Color.Silver;
            dgvPayment.Columns.Add(PaymentId);

            DataGridViewTextBoxColumn OrderId = new DataGridViewTextBoxColumn();
            OrderId.Name = "OrderId";
            OrderId.DataPropertyName = "OrderId";
            OrderId.HeaderText = "Order Id";
            OrderId.ReadOnly = true;
            OrderId.DefaultCellStyle.BackColor = System.Drawing.Color.Silver;
            dgvPayment.Columns.Add(OrderId);

            DataGridViewTextBoxColumn CustomerId = new DataGridViewTextBoxColumn();
            CustomerId.Name = "CustomerId";
            CustomerId.DataPropertyName = "CustomerId";
            CustomerId.HeaderText = "Customer Id";
            CustomerId.ReadOnly = true;
            CustomerId.DefaultCellStyle.BackColor = System.Drawing.Color.Silver;
            dgvPayment.Columns.Add(CustomerId);

            DataGridViewTextBoxColumn PaymentDate = new DataGridViewTextBoxColumn();
            PaymentDate.Name = "PaymentDate";
            PaymentDate.DataPropertyName = "PaymentDate";
            PaymentDate.HeaderText = "Payment Date";
            PaymentDate.ReadOnly = false;
            PaymentDate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgvPayment.Columns.Add(PaymentDate);

            DataGridViewTextBoxColumn Paid_Amt = new DataGridViewTextBoxColumn();
            Paid_Amt.Name = "Paid_Amt";
            Paid_Amt.DataPropertyName = "Paid_Amt";
            Paid_Amt.HeaderText = "Paid Amount";
            Paid_Amt.ReadOnly = false;
            Paid_Amt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Paid_Amt.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgvPayment.Columns.Add(Paid_Amt);

            dgvPayment.DataSource = dsMain.Tables["PaymentDetail"];

            dgvPayment.Columns["PaymentId"].Visible = false;
            dgvPayment.Columns["CustomerId"].Visible = false;
            dgvPayment.Columns["Orderid"].Visible = false;
            dgvPayment.Columns["Paid_Amt"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        
        private void Filter_OrderDetail()
        {
            if (dsMain.Tables.Contains("Order"))
            {

                dsMain.Tables.Remove("Order");
                _OrderAdapter = DBClass.GetAdapterByQuery(Query);
                DataTable dt = new DataTable("Order");
                _OrderAdapter.Fill(dt);
                dsMain.Tables.Add(dt);

                dsMain.Tables["Order"].DefaultView.RowFilter = "";
                if(cmbCustomerName.Text!="")
                    dsMain.Tables["Order"].DefaultView.RowFilter = " CustomerID=" + cmbCustomerName.SelectedValue.ToString();

                DataTable DT = new DataTable();
                DT = dsMain.Tables["Order"].DefaultView.ToTable();

                if (cmbSelect.SelectedItem != null && cmbSelect.SelectedItem.ToString()!="All")
                {
                    if(cmbCustomerName.Text!="")
                        dsMain.Tables["Order"].DefaultView.RowFilter = " CustomerID=" + cmbCustomerName.SelectedValue.ToString() + " AND Select='" + cmbSelect.SelectedItem.ToString() + "'";
                }
                else
                {
                    if (cmbCustomerName.Text != "")
                    dsMain.Tables["Order"].DefaultView.RowFilter = " CustomerID=" + cmbCustomerName.SelectedValue.ToString();
                }

                dgvOrder.DataSource = dsMain.Tables["Order"].DefaultView;

                /////// Calculate Total///////////////
                //dt.DefaultView.RowFilter = " CustomerID=" + cmbCustomerName.SelectedValue.ToString();

                decimal TotalNetAmt=0;
                if (DT.Rows.Count > 0)
                {
                    TotalNetAmt = DT.AsEnumerable().Sum(r => r.Field<decimal?>("NetAmt") ?? 0);
                    lblTotalNetAmount.Text = TotalNetAmt.ToString();
                }
                else
                {
                    lblTotalNetAmount.Text = "0.0";
                    TotalNetAmt = 0;
                }

                if ((cmbSelect.SelectedItem != null) && (cmbSelect.SelectedItem.ToString() == "All"))
                {
                    DT = new DataTable();

                    dsMain.Tables["PaymentDetail"].DefaultView.RowFilter = "";
                    if (cmbCustomerName.Text != "")
                        dsMain.Tables["PaymentDetail"].DefaultView.RowFilter = " CustomerId=" + int.Parse(cmbCustomerName.SelectedValue.ToString());

                    DT = dsMain.Tables["PaymentDetail"].DefaultView.ToTable();
                    decimal TotalPaidAmt = 0;
                    if (DT.Rows.Count > 0)
                    {
                        TotalPaidAmt = DT.AsEnumerable().Sum(r => r.Field<decimal?>("Paid_Amt") ?? 0);
                        lblTotalPaidAmt.Text = TotalPaidAmt.ToString();
                    }
                    else
                    {
                        lblTotalPaidAmt.Text = "0.0";
                        TotalPaidAmt = 0;
                    }

                    decimal PaymentDueAmt = TotalNetAmt - TotalPaidAmt;
                    lblPaymentDueAmt.Text = PaymentDueAmt.ToString();
                    if (PaymentDueAmt == 0)
                    {
                        lblPaymentDueAmt.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (TotalPaidAmt == 0)
                    {
                        lblPaymentDueAmt.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                      //  lblPaymentDueAmt.ForeColor = System.Drawing.Color.Maroon;
                    }
                }
            }
        }
        private void Filter_PaymentDetail()
        {
            if (dsMain.Tables.Contains("PaymentDetail"))
            {

                if (dgvOrder.SelectedRows.Count > 0 && dgvOrder.SelectedRows[0].Cells["OrderId"].Value != null)
                {
                    dsMain.Tables["PaymentDetail"].DefaultView.RowFilter = "";
                    
                        dsMain.Tables["PaymentDetail"].DefaultView.RowFilter = " OrderID=" + int.Parse(dgvOrder.SelectedRows[0].Cells["OrderId"].Value.ToString());
                    
                    dgvPayment.DataSource = dsMain.Tables["PaymentDetail"].DefaultView;

                    /////// Calculate Total///////////////
                    DataTable DT = new DataTable();
                    DT = dsMain.Tables["PaymentDetail"].DefaultView.ToTable();
                    if (DT.Rows.Count > 0)
                    {
                        decimal TotalAmt = DT.AsEnumerable().Sum(r => r.Field<decimal?>("Paid_Amt") ?? 0);
                        lblPaidAmount.Text = TotalAmt.ToString();

                        decimal TNetAmt = decimal.Parse(lblNetAmount.Text);
                        decimal DueAmt = TNetAmt - TotalAmt;
                        lblDueAmt.Text = DueAmt.ToString();
                    }
                    else
                    {
                        lblPaidAmount.Text = "0.0";
                        lblDueAmt.Text = lblNetAmount.Text ;
                    }

                }
                
            }
        }
        #endregion

        #region Controls Events
        private void cmbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter_OrderDetail();
            Filter_PaymentDetail();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            Filter_OrderDetail();
            Filter_PaymentDetail();
        }
        #endregion

        #region Grid Events
        private void dgvPayment_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            if ((dgvPayment.Rows.Count != 0) && (dgvOrder.RowCount!=0))
            {
                e.Row.Cells["OrderId"].Value = dgvOrder.SelectedRows[0].Cells["OrderId"].Value.ToString();
                e.Row.Cells["CustomerId"].Value = cmbCustomerName.SelectedValue.ToString();
               // e.Row.Cells["PaymentId"].Value = DBClass.GetIdByQuery("SELECT IDENT_CURRENT('PaymentId')+1");
            }
        }
        private void dgvPayment_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!dsMain.HasChanges()) return;
            decimal PAmt = decimal.Parse(dgvPayment.Rows[e.RowIndex].Cells["Paid_Amt"].Value.ToString());
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(_PayAdapter);
            _PayAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
            _PayAdapter.InsertCommand = commandBuilder.GetInsertCommand();
            _PayAdapter.Update(dsMain.Tables["PaymentDetail"]);

            dsMain.Tables["PaymentDetail"].Clear();
            _PayAdapter = DBClass.GetAdaptor("PaymentDetail");
            _PayAdapter.Fill(dsMain.Tables["PaymentDetail"]);
        }
        private void dgvPayment_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (!dsMain.HasChanges()) return;
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(_PayAdapter);
            _PayAdapter.DeleteCommand = commandBuilder.GetDeleteCommand();
            _PayAdapter.Update(dsMain.Tables["PaymentDetail"]);
        }
        private void dgvPayment_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void dgvOrder_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < dgvOrder.RowCount)
            {
                lblNetAmount.Text = dgvOrder.Rows[e.RowIndex].Cells["NetAmt"].Value.ToString(); 
                Filter_PaymentDetail();
            }

        }
        #endregion

        private void cmbSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
