using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace RentalSystem
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }

        static DataTable dt = new DataTable();
        static DataSet ds = new DataSet();
        ReportClass ReportClassObj = new ReportClass();

        private void Report_Load(object sender, EventArgs e)
        {
            Fill_ItemCombobox();
            Fill_CustomerCombobox();
            Fill_PlaceCombobox();

//            txtFromOrdDate.Text = System.DateTime.Now.ToShortDateString();
  //          txtFromDeliveryDate.Text = System.DateTime.Now.ToShortDateString();
    //        txtFromPayDate.Text = System.DateTime.Now.ToShortDateString();
        }
        #region Fill Combo
        private void Fill_ItemCombobox()
        {
            string Query = "Select *from ItemMaster ";
            DataTable dt = new DataTable();
            dt = DBClass.GetTableByQuery(Query);
            if (dt.Rows.Count > 0)
            {
                CmbItem.DisplayMember = "ItemName";
                CmbItem.ValueMember = "ItemCode";
                CmbItem.DataSource = dt;

                CmbItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                CmbItem.AutoCompleteSource = AutoCompleteSource.ListItems;
                CmbItem.Text = "";
            }
        }
        private void Fill_CustomerCombobox()
        {
            string Query = "Select distinct CustomerId,CustomerName from OrderMain";
            DataTable dt = new DataTable();
            dt = DBClass.GetTableByQuery(Query);
            if (dt.Rows.Count > 0)
            {
                CmbCustomer.DisplayMember = "CustomerName";
                CmbCustomer.ValueMember = "CustomerId";
                CmbCustomer.DataSource = dt;

                CmbCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                CmbCustomer.AutoCompleteSource = AutoCompleteSource.ListItems;
                CmbCustomer.Text = "";
            }
        }
        private void Fill_PlaceCombobox()
        {
            string Query = "select distinct upper(place) as 'place' from ordermain";
            DataTable dt = new DataTable();
            dt = DBClass.GetTableByQuery(Query);
            if (dt.Rows.Count > 0)
            {
                CmbPlace.DisplayMember = "place";
                CmbPlace.ValueMember = "place";
                CmbPlace.DataSource = dt;

                CmbPlace.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                CmbPlace.AutoCompleteSource = AutoCompleteSource.ListItems;
                CmbPlace.Text="";
            }
        }
        #endregion

        #region Controls Events
        private void txtFromOrderNo_TextChanged(object sender, EventArgs e)
        {
            txtToOrderNo.Text = txtFromOrderNo.Text;
        }

        private void txtFromOrdDate_TextChanged(object sender, EventArgs e)
        {
            txtToOrdDate.Text = txtFromOrdDate.Text;
        }

        private void txtFromDeliveryDate_TextChanged(object sender, EventArgs e)
        {
            txtToDeliveryDate.Text = txtFromDeliveryDate.Text;
        }

        private void txtFromPayDate_TextChanged(object sender, EventArgs e)
        {
            txtToPayDate.Text = txtFromPayDate.Text;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (rbOrderwiseDetails.Checked)
            {
                if (rbDetails.Checked)
                    rptOrderDetails("OrderID");
                else
                    rptOrderSummary("OrderId");
            }

            if (rbCustomerwiseOrder.Checked)
            {
                if (rbDetails.Checked)
                    rptOrderDetails("CustomerName");
                else
                    rptOrderSummary("CustomerName");
            }
            
            if (rbCustomerWisePaymentSummary.Checked)
            {
                if (rbDetails.Checked)
                    rptPaymentDetails("CUSTOMERNAME");
                else
                    rptPaymentSummary("CUSTOMERNAME");
            }
            if (rbOrderwisePayment.Checked)
            {
                if (rbDetails.Checked)
                    rptPaymentDetails("OrderID");
                else
                    rptPaymentSummary("OrderID");
            }
            if (rbDatewisePayment.Checked)
            {
                if (rbDetails.Checked)
                    rptPaymentDetails("Deliverydate");
                else
                    rptPaymentSummary("Deliverydate");
            }

            if (rbItemDetails.Checked)
            {
                rptItemsPriceList();
            }
        }
        #endregion

        #region Order Itemwise/Customerwise/DeliveryDatewise  Details/Summary
        private void rptOrderDetails(string Columnwise)
        {
            ReportClassObj.ExcelFileOpen("Commonreport");

            #region BindTable

            string SelectColumns="";
            string TableName="";
            string JoinTable="";
            string GroupBy="";
            string OrderBy="";
            string ReportName = "";
            switch (Columnwise.ToUpper())
            {
                case "ORDERID":
                    SelectColumns = @"M.OrderID,M.Deliverydate,M.Place,M.CustomerName,I.ItemName,D.Qty,(D.UnitPrice*D.Qty) as 'TotalAmt',
                                      M.Discount,(D.UnitPrice*D.Qty)-M.Discount as 'AvgTotalAmt'
                                      , M.Advance_Amt as 'Advance',(D.UnitPrice*D.Qty)-M.Discount-M.Advance_Amt as 'NetAmt'";
                                OrderBy = "Order by M.OrderID,M.CustomerName,M.Deliverydate,I.ItemName";
                                ReportName = "OrderID wise Details";
                                break;
                case "CUSTOMERNAME":
                                SelectColumns = @"M.CustomerName,M.Deliverydate,M.OrderID,M.Place,I.ItemName,D.Qty,(D.UnitPrice*D.Qty) as 'TotalAmt',
                                        M.Discount,(D.UnitPrice*D.Qty)-M.Discount as 'AvgTotalAmt'
                                        , M.Advance_Amt as 'Advance',(D.UnitPrice*D.Qty)-M.Discount-M.Advance_Amt as 'NetAmt'";
                                OrderBy = "Order by M.CustomerName,M.Deliverydate,M.OrderID,I.ItemName";
                                ReportName = "Customer wise Order Details";
                                break;
                case "DELIVERYDATE":
                                SelectColumns = @"M.Deliverydate,M.OrderID,M.CustomerName,M.Place,I.ItemName,D.Qty,(D.UnitPrice*D.Qty) as 'TotalAmt',
                                        M.Discount,(D.UnitPrice*D.Qty)-M.Discount as 'AvgTotalAmt'
                                        , M.Advance_Amt as 'Advance',(D.UnitPrice*D.Qty)-M.Discount-M.Advance_Amt as 'NetAmt'";
                                OrderBy = "Order by M.Deliverydate,M.OrderID,M.CustomerName,I.ItemName";
                                ReportName = "Date wise Order Details";
                    break;
            }
            

            TableName = "OrderDetail D";
            JoinTable = @"inner join OrderMain M on M.OrderID=D.OrderId
                          inner join ItemMaster I on I.ItemCode=D.ItemCode";

            GroupBy = "";
            

            dt = ReportClassObj.ConstructSqlTable(SelectColumns, TableName, JoinTable, "D.ItemCode", (CmbItem.Text != "") ? CmbItem.SelectedValue.ToString() : "",
                                                    "M.CustomerID", (CmbCustomer.Text != "") ? CmbCustomer.SelectedValue.ToString() : "",
                                                    "M.place", (CmbPlace.Text != "") ? CmbPlace.SelectedValue.ToString() : "",
                                                    "M.OrderId", (txtFromOrderNo.Text != "") ? txtFromOrderNo.Text : "", (txtFromOrderNo.Text != "") ? txtToOrderNo.Text : "",
                                                    "M.Deliverydate", (txtFromDeliveryDate.Text != "") ? txtFromDeliveryDate.Text : "", (txtToDeliveryDate.Text != "") ? txtToDeliveryDate.Text : "",
                                                    "M.Orderdate", (txtFromOrdDate.Text != "") ? txtFromOrdDate.Text : "", (txtToOrdDate.Text != "") ? txtToOrdDate.Text : "",
                                                    "", "", "", GroupBy, OrderBy);
            dt.TableName = "Order";
            #endregion

            #region Print
           
            if (dt.Rows.Count > 0)
            {
                int TotalCols = dt.Columns.Count;
                string ColName = ReportClassObj.ColumnIndexToColumnLetter(TotalCols);

                #region "Print ReportName"
                //Set Report Name
                Excel.Range RngReportName;
                RngReportName = ReportClassObj.Worksheet.get_Range("A1", ColName + "1");
                ReportClassObj.SetReportName(RngReportName, ReportName);
                #endregion

                #region "PrintHeader
                //Set Report Header
                Excel.Range RngColumnHeader;
                RngColumnHeader = ReportClassObj.Worksheet.get_Range("A2", ColName + "2");
                ReportClassObj.SetReportHeader(RngColumnHeader, dt);
                #endregion

                #region Print Details
                //Set Report Details
                int TotalRows = dt.Rows.Count + 3;
                Excel.Range RngColumnValue;
                RngColumnValue = ReportClassObj.Worksheet.get_Range("A3", ColName + TotalRows.ToString());

                //////Column Value
                string strColumnwiseName = "";
                int IntRow = 0;                    
                int row = 0;
                double DiscAmt = 0;
                double AdvanceAmt = 0;
                double GDiscAmt = 0;
                double GAdvanceAmt = 0;

                //Print Details
                for (IntRow = 0; IntRow <= dt.Rows.Count - 1; IntRow++)
                {
                    if (strColumnwiseName != dt.Rows[IntRow][Columnwise].ToString())
                    {
                        strColumnwiseName = dt.Rows[IntRow][Columnwise].ToString();
                        RngColumnValue.set_Item(row + 1, 1, strColumnwiseName);
                        DiscAmt = DiscAmt + 0;

                        if (dt.Rows[IntRow]["Discount"].ToString() != "")
                            DiscAmt += Convert.ToDouble(dt.Rows[IntRow]["Discount"].ToString());
                        if (dt.Rows[IntRow]["Advance"].ToString() != "")
                            AdvanceAmt += Convert.ToDouble(dt.Rows[IntRow]["Advance"].ToString());
                        
                    }

                    for (int Intcol = 0; Intcol <= dt.Columns.Count - 1; Intcol++)
                    {
                        if (dt.Columns[Intcol].ColumnName.ToString().ToUpper() != Columnwise.ToUpper() 
                            && dt.Columns[Intcol].ColumnName.ToString().ToUpper() !="DISCOUNT"
                            && dt.Columns[Intcol].ColumnName.ToString().ToUpper() != "ADVANCE"
                            && dt.Columns[Intcol].ColumnName.ToString().ToUpper() != "AVGTOTALAMT"
                            && dt.Columns[Intcol].ColumnName.ToString().ToUpper() != "NETAMT")
                        {
                            RngColumnValue.set_Item(row + 1, Intcol + 1, dt.Rows[IntRow][Intcol].ToString());
                        }
                    }

                    #region Print Subtotal
                    //Find Subtotal and Print

                    bool Print = false;
                    if (dt.Rows.Count > IntRow + 1 && strColumnwiseName != dt.Rows[IntRow + 1][Columnwise].ToString())
                    {

                        row++;
                        Print = true;

                    }
                    else if (dt.Rows.Count <= IntRow + 1)
                    {
                        row++; //  For Final Row subtotal
                        Print = true;
                    }


                    if (Print)
                    {

                        RngColumnValue.set_Item(row + 1, dt.Columns["ItemName"].Ordinal + 1, "SubTotal");
                        

                        var intQty = (from t in dt.AsEnumerable()
                                       where t[Columnwise].ToString().Trim().ToUpper() == strColumnwiseName.ToUpper()
                                       select Convert.ToInt32(t["Qty"])).Sum();
                        RngColumnValue.set_Item(row + 1, dt.Columns["Qty"].Ordinal + 1, intQty.ToString());

                        var dblTAmt = (from t in dt.AsEnumerable()
                                       where t[Columnwise].ToString().Trim().ToUpper() == strColumnwiseName.ToUpper()
                                       select Convert.ToInt32(t["TotalAmt"])).Sum();
                        RngColumnValue.set_Item(row + 1, dt.Columns["TotalAmt"].Ordinal + 1, dblTAmt.ToString());

                        RngColumnValue.set_Item(row + 1, dt.Columns["Discount"].Ordinal + 1, DiscAmt.ToString());

                        RngColumnValue.set_Item(row + 1, dt.Columns["AvgTotalAmt"].Ordinal + 1, (dblTAmt-DiscAmt).ToString());

                        RngColumnValue.set_Item(row + 1, dt.Columns["Advance"].Ordinal + 1, AdvanceAmt.ToString());

                        var dblNetAmt = (from t in dt.AsEnumerable()
                                          where t[Columnwise].ToString().Trim().ToUpper() == strColumnwiseName.ToUpper()
                                          select Convert.ToInt32(t["NetAmt"])).Sum();
                        RngColumnValue.set_Item(row + 1, dt.Columns["NetAmt"].Ordinal + 1, (dblTAmt - DiscAmt-AdvanceAmt).ToString());

                        TotalRows++;

                        Excel.Range RngColSubTotal;
                        RngColSubTotal = ReportClassObj.Worksheet.get_Range("A" + (row + 3).ToString(), ReportClassObj.ColumnIndexToColumnLetter(TotalCols).ToString() + (row + 3).ToString());
                        RngColSubTotal.EntireRow.Font.Bold = true;

                        GDiscAmt += DiscAmt;
                        GAdvanceAmt += AdvanceAmt;
                        DiscAmt = 0; AdvanceAmt = 0;
                    }
                    #endregion
                    row++;
                }

                #endregion

                #region GrandTotal
                //Find Grand Total and Print

                RngColumnValue.set_Item(row + 1, 1, "Grand Total");

                double GTAmt=0;
                object sumObject;
                sumObject = dt.Compute("Sum(Qty)", "");
                RngColumnValue.set_Item(row + 1, dt.Columns["Qty"].Ordinal + 1, sumObject.ToString());

                sumObject = dt.Compute("Sum(TotalAmt)", "");
                GTAmt = Convert.ToDouble(dt.Compute("Sum(TotalAmt)", ""));
                RngColumnValue.set_Item(row + 1, dt.Columns["TotalAmt"].Ordinal + 1, sumObject.ToString());

                //sumObject = dt.Compute("Sum(Discount)", "");
                RngColumnValue.set_Item(row + 1, dt.Columns["Discount"].Ordinal + 1, GDiscAmt);

                //sumObject = dt.Compute("Sum(AvgTotalAmt)", "");
                RngColumnValue.set_Item(row + 1, dt.Columns["AvgTotalAmt"].Ordinal + 1, (GTAmt-GDiscAmt).ToString());

                RngColumnValue.set_Item(row + 1, dt.Columns["Advance"].Ordinal + 1, (GAdvanceAmt).ToString());
                RngColumnValue.set_Item(row + 1, dt.Columns["NetAmt"].Ordinal + 1, (GTAmt - GDiscAmt-GAdvanceAmt).ToString());

                RngColumnValue = ReportClassObj.Worksheet.get_Range("A3", ColName + (TotalRows - 1).ToString());

                ReportClassObj.ApplyStyleRowValue(RngColumnValue);

                Excel.Range RngColGrandTotal;
                RngColGrandTotal = ReportClassObj.Worksheet.get_Range("A" + (TotalRows).ToString(), ReportClassObj.ColumnIndexToColumnLetter(TotalCols).ToString() + (TotalRows).ToString());

                ReportClassObj.ApplyStyleGrandTotalValue(RngColGrandTotal);
                #endregion
            }
            #endregion

            ReportClassObj.SaveAndDisplayExcelFile();
        }
        private void rptOrderSummary(string Columnwise)
        {

            ReportClassObj.ExcelFileOpen("Commonreport");
            string SelectColumns = "";
            string TableName = "";
            string JoinTable = "";
            string GroupBy = "";
            string OrderBy = "";
            string ReportName = "";

            #region BindTable
            switch (Columnwise.ToUpper())
            {
                case "ORDERID":
                    SelectColumns = @"M.OrderID,M.CustomerName,M.Deliverydate,count(M.OrderID) as 'Total Items',Sum(D.Qty) as 'Qty',sum(D.UnitPrice*D.Qty) as 'TotalAmt',
                            (select Discount from OrderMain  where OrderID=M.OrderID) as 'Discount',
                            (sum(D.UnitPrice*D.Qty)-(select Discount from OrderMain  where OrderID=M.OrderID)) as 'AvgTotalAmt',
                            (select Advance_Amt from OrderMain  where OrderID=M.OrderID) as 'Advance',
                            (sum(D.UnitPrice*D.Qty)-(select Discount from OrderMain  where OrderID=M.OrderID)-(select Advance_Amt from OrderMain  where OrderID=M.OrderID)) as 'NetAmt'";
                    GroupBy = "group by M.OrderID,M.CustomerName,M.Deliverydate";
                    OrderBy = "Order by M.OrderID,M.CustomerName,M.Deliverydate";
                            ReportName = "Order wise Summary Details";
                    break;

                case "CUSTOMERNAME":
                    SelectColumns = @"M.CustomerName,M.OrderID,M.Deliverydate,count(M.OrderID) as 'Total Items',Sum(D.Qty) as 'Qty',sum(D.UnitPrice*D.Qty) as 'TotalAmt',
                            (select Discount from OrderMain  where OrderID=M.OrderID) as 'Discount',
                            (sum(D.UnitPrice*D.Qty)-(select Discount from OrderMain  where OrderID=M.OrderID)) as 'AvgTotalAmt',
                            (select Advance_Amt from OrderMain  where OrderID=M.OrderID) as 'Advance',
                            (sum(D.UnitPrice*D.Qty)-(select Discount from OrderMain  where OrderID=M.OrderID)-(select Advance_Amt from OrderMain  where OrderID=M.OrderID)) as 'NetAmt'";
                    GroupBy = "group by M.CustomerName,M.OrderID,M.Deliverydate";
                    OrderBy = "Order by M.CustomerName,M.OrderID,M.Deliverydate";
                    ReportName = "CustomerName wise Summary Details";
                    break;
                case "DELIVERYDATE":
                    SelectColumns = @"M.DeliveryDate,M.OrderID,M.CustomerName,count(M.OrderID) as 'Total Items',Sum(D.Qty) as 'Qty',sum(D.UnitPrice*D.Qty) as 'TotalAmt',
                            (select Discount from OrderMain  where OrderID=M.OrderID) as 'Discount',
                            (sum(D.UnitPrice*D.Qty)-(select Discount from OrderMain  where OrderID=M.OrderID)) as 'AvgTotalAmt',
                            (select Advance_Amt from OrderMain  where OrderID=M.OrderID) as 'Advance',
                            (sum(D.UnitPrice*D.Qty)-(select Discount from OrderMain  where OrderID=M.OrderID)-(select Advance_Amt from OrderMain  where OrderID=M.OrderID)) as 'NetAmt'"; 
                    GroupBy = "group by M.DeliveryDate,M.OrderID,M.CustomerName";
                    OrderBy = "Order by M.DeliveryDate,M.OrderID,M.CustomerName";
                    ReportName = "Date wise Summary Details";
                    break;
            }
            

            TableName = "OrderDetail D";
            JoinTable = @"inner join OrderMain M on M.OrderID=D.OrderId
                            inner join ItemMaster I on I.ItemCode=D.ItemCode";

            dt = ReportClassObj.ConstructSqlTable(SelectColumns, TableName, JoinTable, "D.ItemCode", (CmbItem.Text != "") ? CmbItem.SelectedValue.ToString() : "",
                                                    "M.CustomerID", (CmbCustomer.Text != "") ? CmbCustomer.SelectedValue.ToString() : "",
                                                    "M.place", (CmbPlace.Text != "") ? CmbPlace.SelectedValue.ToString() : "",
                                                    "M.OrderId", (txtFromOrderNo.Text != "") ? txtFromOrderNo.Text : "", (txtFromOrderNo.Text != "") ? txtToOrderNo.Text : "",
                                                    "M.Deliverydate", (txtFromDeliveryDate.Text != "") ? txtFromDeliveryDate.Text : "", (txtToDeliveryDate.Text != "") ? txtToDeliveryDate.Text : "",
                                                    "M.Orderdate", (txtFromOrdDate.Text != "") ? txtFromOrdDate.Text : "", (txtToOrdDate.Text != "") ? txtToOrdDate.Text : "",
                                                    "", "", "", GroupBy, OrderBy);
            dt.TableName = "Order";
            #endregion

            #region Print
            if (dt.Rows.Count > 0)
            {
                int TotalCols = dt.Columns.Count;

                #region Print ReportName
                //Set Report Name
                string LastColName = ReportClassObj.ColumnIndexToColumnLetter(TotalCols);

                Excel.Range RngReportName;
                RngReportName = ReportClassObj.Worksheet.get_Range("A1", LastColName + "1");
                ReportClassObj.SetReportName(RngReportName, ReportName);
                #endregion

                #region Print ReportHeader
                //Set Header
                Excel.Range RngColumnHeader;
                RngColumnHeader = ReportClassObj.Worksheet.get_Range("A2", LastColName + "2");
                ReportClassObj.SetReportHeader(RngColumnHeader, dt);
                #endregion

                #region Print Details
                //Set Column Value
                int TotalRows = dt.Rows.Count + 3;
                Excel.Range RngColumnValue;
                RngColumnValue = ReportClassObj.Worksheet.get_Range("A3", LastColName + TotalRows.ToString());

                string strItem = "";
                int IntRow = 0;
                for (IntRow = 0; IntRow <= dt.Rows.Count - 1; IntRow++)
                {
                    if (strItem != dt.Rows[IntRow][Columnwise].ToString())
                    {
                        strItem = dt.Rows[IntRow][Columnwise].ToString();
                        RngColumnValue.set_Item(IntRow + 1, 1, strItem);
                    }

                    for (int Intcol = 0; Intcol <= dt.Columns.Count - 1; Intcol++)
                    {
                        if (dt.Columns[Intcol].ColumnName.ToString().ToUpper() != Columnwise.ToUpper())
                        {
                            RngColumnValue.set_Item(IntRow + 1, Intcol + 1, dt.Rows[IntRow][Intcol].ToString());
                        }
                    }
                }
                ReportClassObj.ApplyStyleRowValue(RngColumnValue);
                #endregion

                #region Print GrandTotal
                //Set GrandTotal
                
                object sumObject;
                sumObject = dt.Compute("Sum(Qty)", "");
                RngColumnValue.set_Item(IntRow + 1, dt.Columns["Qty"].Ordinal + 1, sumObject.ToString());

                sumObject = dt.Compute("Sum(TotalAmt)", "");
                RngColumnValue.set_Item(IntRow + 1, dt.Columns["TotalAmt"].Ordinal + 1, sumObject.ToString());

                sumObject = dt.Compute("Sum(Discount)", "");
                RngColumnValue.set_Item(IntRow + 1, dt.Columns["Discount"].Ordinal + 1, sumObject.ToString());

                sumObject = dt.Compute("Sum(AvgTotalAmt)", "");
                RngColumnValue.set_Item(IntRow + 1, dt.Columns["AvgTotalAmt"].Ordinal + 1, sumObject.ToString());

                sumObject = dt.Compute("Sum(Advance)", "");
                RngColumnValue.set_Item(IntRow + 1, dt.Columns["Advance"].Ordinal + 1, sumObject.ToString());

                sumObject = dt.Compute("Sum(NetAmt)", "");
                RngColumnValue.set_Item(IntRow + 1, dt.Columns["NetAmt"].Ordinal + 1, sumObject.ToString());

                Excel.Range RngColGrandTotal;
                RngColGrandTotal = ReportClassObj.Worksheet.get_Range("A" + TotalRows, ReportClassObj.ColumnIndexToColumnLetter(TotalCols).ToString() + TotalRows);

                ReportClassObj.ApplyStyleGrandTotalValue(RngColGrandTotal);
                #endregion
            }
            #endregion

            ReportClassObj.SaveAndDisplayExcelFile();

        }
        #endregion

        #region Payment Itemwise.Customerwise.DeliveryDatewise Details/Summary
        private void rptPaymentDetails(string Columnwise)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            ReportClassObj.ExcelFileOpen("Commonreport");
            #region BindTable

            string SelectCol = "";
            string OrderBy = "";
            string ReportName = "";

            if (Columnwise.ToUpper() == "CUSTOMERNAME")
            {
                SelectCol = @"select M.CustomerName,M.OrderID,M.Deliverydate,";
                OrderBy = " Order By M.CustomerName,M.OrderID,M.Deliverydate";
                ReportName = "Customer wise Payment Details";
            }
            else if (Columnwise.ToUpper() == "ORDERID")
            {
                SelectCol = @"select M.OrderID,M.CustomerName,M.Deliverydate,";
                OrderBy = " Order By M.OrderID,M.CustomerName,M.Deliverydate";
                ReportName = "Order wise Payment Details";
            }
            else if (Columnwise.ToUpper() == "DELIVERYDATE")
            {
                SelectCol = @"select M.Deliverydate,M.CustomerName,M.OrderID,";
                OrderBy = " Order By M.Deliverydate,M.CustomerName,M.OrderID";
                ReportName = "Date wise Payment Details";
            }

            string Query = SelectCol + @"M.Place,
                            (select sum(Qty*UnitPrice) from OrderDetail where OrderId =M.OrderID) as 'TotalAmt',
                            M.Discount,M.Advance_Amt as 'Advance',
                            (select sum(Qty*UnitPrice) from OrderDetail where OrderId =M.OrderID)-M.Discount-M.Advance_Amt as 'NetAmt',
                            P.PaymentDate,P.Paid_Amt as 'PaidAmt'
                            ,(((select sum(Qty*UnitPrice) from OrderDetail where OrderId =M.OrderID)-M.Discount-M.Advance_Amt)
                            -case when (Select SUM(Paid_Amt) from PaymentDetail where OrderId =M.OrderID) IS null then 0 
                            else (Select SUM(Paid_Amt) from PaymentDetail where OrderId =M.OrderID) End) as 'PaymentDue'
                            from PaymentDetail P
                            right join OrderMain M on P.OrderId=M.OrderID Where 1=1";

            if (CmbCustomer.Text != "")
            {
                Query = Query + " and M.CustomerID=" + int.Parse(CmbCustomer.SelectedValue.ToString()) + "";
            }
            if (txtFromOrderNo.Text != "")
            {
                Query = Query + " and M.OrderID>=" + int.Parse(txtFromOrderNo.Text) + "";
            }
            if (txtToOrderNo.Text != "")
            {
                Query = Query + " and M.OrderID<=" + int.Parse(txtToOrderNo.Text) + "";
            }
            if (txtFromDeliveryDate.Text != "")
            {
                Query = Query + " and M.Deliverydate>='" + txtFromDeliveryDate.Text + "'";
            }
            if (txtToDeliveryDate.Text != "")
            {
                Query = Query + " and M.Deliverydate<='" + txtToDeliveryDate.Text + "'";
            }

            Query = Query + OrderBy;
            dt = DBClass.GetTableByQuery(Query);
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "Payment";
            #endregion

            #region FilterData
            if (dt.Rows.Count > 0)
            {
                if (rbPaid.Checked)
                {
                    ds.Tables[0].DefaultView.RowFilter = "";
                    ds.Tables[0].DefaultView.RowFilter = " PaymentDue=0";
                    dt = ds.Tables[0].DefaultView.ToTable();
                }
                else if(rbPaymentDue.Checked==true)
                {
                    ds.Tables[0].DefaultView.RowFilter = "";
                    ds.Tables[0].DefaultView.RowFilter = " PaymentDue>0";
                    dt = ds.Tables[0].DefaultView.ToTable();
                }
                else if (rbIncomplete.Checked == true)
                {
                    ds.Tables[0].DefaultView.RowFilter = "";
                    ds.Tables[0].DefaultView.RowFilter = " PaymentDue=NetAmt";
                    dt = ds.Tables[0].DefaultView.ToTable();
                }
            }
            #endregion
            #region Print
            if (dt.Rows.Count > 0)
            {
                int TotalCols = dt.Columns.Count;

                #region Print ReportName
                //Set Report Name
                string LastColName = ReportClassObj.ColumnIndexToColumnLetter(TotalCols);

                Excel.Range RngReportName;
                RngReportName = ReportClassObj.Worksheet.get_Range("A1", LastColName + "1");
                ReportClassObj.SetReportName(RngReportName, ReportName);
                #endregion

                #region Print ReportHeader
                //Set Header
                Excel.Range RngColumnHeader;
                RngColumnHeader = ReportClassObj.Worksheet.get_Range("A2", LastColName + "2");
                ReportClassObj.SetReportHeader(RngColumnHeader, dt);
                #endregion

                #region Print Details
                //Set Column Value
                int TotalRows = dt.Rows.Count + 3;
                Excel.Range RngColumnValue;
                RngColumnValue = ReportClassObj.Worksheet.get_Range("A3", LastColName + TotalRows.ToString());

                string strItem = "";
                //string Columnwise = "CustomerName";
                string strOrderNo = "";
                int IntRow = 0;
                double DblTotalAmt = 0;
                double DblDiscount = 0;
                double DblAdvance = 0;
                double DblNetAmt = 0;
                double DblPaidAmt = 0;
                double DblPaymentDue = 0;
                for (IntRow = 0; IntRow <= dt.Rows.Count - 1; IntRow++)
                {
                    if (strItem != dt.Rows[IntRow][Columnwise].ToString())
                    {
                        strItem = dt.Rows[IntRow][Columnwise].ToString();
                        RngColumnValue.set_Item(IntRow + 1, dt.Columns[Columnwise].Ordinal + 1, strItem);

                    }
                    if (strOrderNo != dt.Rows[IntRow]["OrderID"].ToString())
                    {
                        strOrderNo = dt.Rows[IntRow]["OrderId"].ToString();
                        RngColumnValue.set_Item(IntRow + 1, dt.Columns["OrderId"].Ordinal + 1, strOrderNo);
                        RngColumnValue.set_Item(IntRow + 1, dt.Columns["CustomerName"].Ordinal + 1, dt.Rows[IntRow]["CustomerName"].ToString());
                        RngColumnValue.set_Item(IntRow + 1, dt.Columns["DeliveryDate"].Ordinal + 1, dt.Rows[IntRow]["DeliveryDate"].ToString());
                        RngColumnValue.set_Item(IntRow + 1, dt.Columns["Place"].Ordinal + 1, dt.Rows[IntRow]["Place"].ToString());
                        RngColumnValue.set_Item(IntRow + 1, dt.Columns["TotalAmt"].Ordinal + 1, dt.Rows[IntRow]["TotalAmt"].ToString());
                        RngColumnValue.set_Item(IntRow + 1, dt.Columns["Discount"].Ordinal + 1, dt.Rows[IntRow]["Discount"].ToString());
                        RngColumnValue.set_Item(IntRow + 1, dt.Columns["Advance"].Ordinal + 1, dt.Rows[IntRow]["Advance"].ToString());
                        RngColumnValue.set_Item(IntRow + 1, dt.Columns["NetAmt"].Ordinal + 1, dt.Rows[IntRow]["NetAmt"].ToString());
                        RngColumnValue.set_Item(IntRow + 1, dt.Columns["PaymentDue"].Ordinal + 1, dt.Rows[IntRow]["PaymentDue"].ToString());

                        if (dt.Rows[IntRow]["TotalAmt"].ToString() != "")
                        DblTotalAmt += Convert.ToDouble(dt.Rows[IntRow]["TotalAmt"].ToString());
                        if (dt.Rows[IntRow]["Discount"].ToString() != "")
                        DblDiscount += Convert.ToDouble(dt.Rows[IntRow]["Discount"].ToString());
                        if (dt.Rows[IntRow]["Advance"].ToString() != "")
                        DblAdvance += Convert.ToDouble(dt.Rows[IntRow]["Advance"].ToString());
                        if (dt.Rows[IntRow]["NetAmt"].ToString() != "")
                        DblNetAmt += Convert.ToDouble(dt.Rows[IntRow]["NetAmt"].ToString());
                        if(dt.Rows[IntRow]["PaymentDue"].ToString()!="")
                            DblPaymentDue += Convert.ToDouble(dt.Rows[IntRow]["PaymentDue"].ToString());

                    }
                    for (int Intcol = 0; Intcol <= dt.Columns.Count - 1; Intcol++)
                    {
                        if (dt.Columns[Intcol].ColumnName.ToString().ToUpper() == "PAYMENTDATE" || dt.Columns[Intcol].ColumnName.ToString().ToUpper() == "PAIDAMT")
                        {
                            RngColumnValue.set_Item(IntRow + 1, Intcol + 1, dt.Rows[IntRow][Intcol].ToString());

                            if (dt.Columns[Intcol].ColumnName.ToString().ToUpper() == "PAIDAMT" && dt.Rows[IntRow]["PaidAmt"].ToString()!="")
                            {
                                DblPaidAmt += Convert.ToDouble(dt.Rows[IntRow]["PaidAmt"].ToString());
                            }
                            
                        }
                    }
                }
                ReportClassObj.ApplyStyleRowValue(RngColumnValue);
                #endregion

                #region Print GrandTotal
                //Set GrandTotal

                RngColumnValue.set_Item(IntRow + 1, dt.Columns["TotalAmt"].Ordinal + 1, DblTotalAmt);
                RngColumnValue.set_Item(IntRow + 1, dt.Columns["Discount"].Ordinal + 1, DblDiscount);
                RngColumnValue.set_Item(IntRow + 1, dt.Columns["Advance"].Ordinal + 1, DblAdvance);
                RngColumnValue.set_Item(IntRow + 1, dt.Columns["NetAmt"].Ordinal + 1, DblNetAmt);
                RngColumnValue.set_Item(IntRow + 1, dt.Columns["PaidAmt"].Ordinal + 1, DblPaidAmt);
                RngColumnValue.set_Item(IntRow + 1, dt.Columns["PaymentDue"].Ordinal + 1, DblPaymentDue);

                //object sumObject = dt.Compute("Sum(TotalAmt)", "");
                //RngColumnValue.set_Item(IntRow + 1, dt.Columns["TotalAmt"].Ordinal + 1, sumObject.ToString());
                
                //sumObject = dt.Compute("Sum(Discount)", "");
                //RngColumnValue.set_Item(IntRow + 1, dt.Columns["Discount"].Ordinal + 1, sumObject.ToString());

                //sumObject = dt.Compute("Sum(Advance)", "");
                //RngColumnValue.set_Item(IntRow + 1, dt.Columns["Advance"].Ordinal + 1, sumObject.ToString());

                //sumObject = dt.Compute("Sum(NetAmt)", "");
                //RngColumnValue.set_Item(IntRow + 1, dt.Columns["NetAmt"].Ordinal + 1, sumObject.ToString());

                //sumObject = dt.Compute("Sum(PaidAmt)", "");
                //RngColumnValue.set_Item(IntRow + 1, dt.Columns["PaidAmt"].Ordinal + 1, sumObject.ToString());

                Excel.Range RngColGrandTotal;
                RngColGrandTotal = ReportClassObj.Worksheet.get_Range("A" + TotalRows, ReportClassObj.ColumnIndexToColumnLetter(TotalCols).ToString() + TotalRows);

                ReportClassObj.ApplyStyleGrandTotalValue(RngColGrandTotal);
                #endregion
            }
            #endregion

            ReportClassObj.SaveAndDisplayExcelFile();
        }
        private void rptPaymentSummary(string Columnwise)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            ReportClassObj.ExcelFileOpen("Commonreport");
            #region BindTable

            #region Bindquery
            string SelectCol = "";
            string OrderBy = "";
            string ReportName = "";
            if (Columnwise.ToUpper() == "CUSTOMERNAME")
            {
                SelectCol = @"select o.CustomerName,o.OrderID,o.Deliverydate,";
                OrderBy = " Order By o.CustomerName,o.OrderID,o.Deliverydate";
                ReportName = "Customer wise Payment Summary";
            }
            else if (Columnwise.ToUpper() == "ORDERID")
            {
                SelectCol = @"select o.OrderID,o.Deliverydate,o.CustomerName,";
                OrderBy = " Order By o.OrderID,o.Deliverydate,o.CustomerName";
                ReportName = "Order wise Payment Summary";
            }
            else if (Columnwise.ToUpper() == "DELIVERYDATE")
            {
                SelectCol = @"select o.Deliverydate,o.OrderID,o.CustomerName,";
                OrderBy = " Order By o.Deliverydate,o.OrderID,o.CustomerName";
                ReportName = "Date wise Payment Summary";
            }

             string Query = SelectCol + @"(select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID)  as 'TotalAmt'
                        ,O.Discount,o.Advance_Amt,
                        (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt as 'NetAmt',
                        (select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID) as 'PaidAmt',
                        CASE WHEN ((select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID)) is null 
                        Then (((select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt))
                        ELSE (((select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt-
							(select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID))) END as 'PaymentDue',
                        'Paid' as 'Status'
                        from OrderMain o 
                        where (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt-(select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID)=0";

                        if (CmbCustomer.Text != "")
                        {
                            Query = Query + " and o.CustomerID=" + int.Parse(CmbCustomer.SelectedValue.ToString()) + "";
                        }   
                        if (txtFromOrderNo.Text != "")
                        {
                            Query = Query + " and o.OrderID>=" + int.Parse(txtFromOrderNo.Text) + "";
                        }
                        if (txtToOrderNo.Text != "")
                        {
                            Query = Query + " and o.OrderID<=" + int.Parse(txtToOrderNo.Text) + "";
                        }
                        if (txtFromDeliveryDate.Text != "")
                        {
                            Query = Query + " and o.Deliverydate>='" + txtFromDeliveryDate.Text + "'";
                        }
                        if (txtToDeliveryDate.Text != "")
                        {
                            Query = Query + " and o.Deliverydate<='" + txtToDeliveryDate.Text + "'";
                        }
                        Query = Query + @" union ";
                        Query = Query +  SelectCol +@"

                        
                        (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID)  as 'TotalAmt',
                        O.Discount,o.Advance_Amt,
                        (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt as 'NetAmt',
                        (select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID) as 'PaidAmt',
                        CASE WHEN ((select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID)) is null 
                        Then (((select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt))
                        ELSE (((select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt-
							(select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID))) END as 'PaymentDue',
                        'Payment Due' as 'Status'
                        from OrderMain o 
                        where (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt-(select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID)>0";

                        if (CmbCustomer.Text != "")
                        {
                            Query = Query + " and o.CustomerID=" + int.Parse(CmbCustomer.SelectedValue.ToString()) + "";
                        }
                        if (txtFromOrderNo.Text != "")
                        {
                            Query = Query + " and o.OrderID>=" + int.Parse(txtFromOrderNo.Text) + "";
                        }
                        if (txtToOrderNo.Text != "")
                        {
                            Query = Query + " and o.OrderID<=" + int.Parse(txtToOrderNo.Text) + "";
                        }
                        if (txtFromDeliveryDate.Text != "")
                        {
                            Query = Query + " and o.Deliverydate>='" + txtFromDeliveryDate.Text + "'";
                        }
                        if (txtToDeliveryDate.Text != "")
                        {
                            Query = Query + " and o.Deliverydate<='" + txtToDeliveryDate.Text + "'";
                        }

                        Query = Query + @" union ";
                        Query = Query +  SelectCol +@"
                        
                        (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID)  as 'TotalAmt',
						O.Discount,o.Advance_Amt,
                        (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt as 'NetAmt',
                        (select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID) as 'PaidAmt',
                        CASE WHEN ((select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID)) is null 
                        Then (((select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt))
                        ELSE (((select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt-
							(select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID))) END as 'PaymentDue',
                        'InComplete' as 'Status'
                        from OrderMain o 
                        where o.OrderId NOT IN (select OrderId from PaymentDetail)";

                        if (CmbCustomer.Text != "")
                        {
                            Query = Query + " and o.CustomerID=" + int.Parse(CmbCustomer.SelectedValue.ToString()) + "";
                        }
                        if (txtFromOrderNo.Text != "")
                        {
                            Query = Query + " and o.OrderID>=" + int.Parse(txtFromOrderNo.Text) + "";
                        }
                        if (txtToOrderNo.Text != "")
                        {
                            Query = Query + " and o.OrderID<=" + int.Parse(txtToOrderNo.Text) + "";
                        }
                        if (txtFromDeliveryDate.Text != "")
                        {
                            Query = Query + " and o.Deliverydate>='" + txtFromDeliveryDate.Text + "'";
                        }
                        if (txtToDeliveryDate.Text != "")
                        {
                            Query = Query + " and o.Deliverydate<='" + txtToDeliveryDate.Text + "'";
                        }

                        Query = Query + OrderBy;
            #endregion
            dt = DBClass.GetTableByQuery(Query);
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "Payment";
            #endregion

            #region FilterData
            if (dt.Rows.Count > 0)
            {
                if (rbPaymentDue.Checked)
                {
                    ds.Tables["Payment"].DefaultView.RowFilter = "";
                    ds.Tables[0].DefaultView.RowFilter = " Status='PaymentDue'";
                    dt = ds.Tables[0].DefaultView.ToTable();

                }
                else if (rbIncomplete.Checked)
                {
                    ds.Tables["Payment"].DefaultView.RowFilter = "";
                    ds.Tables[0].DefaultView.RowFilter = " Status='InComplete'";
                    dt = ds.Tables[0].DefaultView.ToTable();
                }
                else if (rbPaid.Checked)
                {
                    ds.Tables["Payment"].DefaultView.RowFilter = "";
                    ds.Tables[0].DefaultView.RowFilter = " Status='Paid'";
                    dt = ds.Tables[0].DefaultView.ToTable();
                }
            }
            #endregion

            #region Print
            if (dt.Rows.Count > 0)
            {
                int TotalCols = dt.Columns.Count;

                #region Print ReportName
                //Set Report Name
                string LastColName = ReportClassObj.ColumnIndexToColumnLetter(TotalCols);

                Excel.Range RngReportName;
                RngReportName = ReportClassObj.Worksheet.get_Range("A1", LastColName + "1");
                ReportClassObj.SetReportName(RngReportName, "Customer wise Payment Summary");
                #endregion

                #region Print ReportHeader
                //Set Header
                Excel.Range RngColumnHeader;
                RngColumnHeader = ReportClassObj.Worksheet.get_Range("A2", LastColName + "2");
                ReportClassObj.SetReportHeader(RngColumnHeader, dt);
                #endregion

                #region Print Details
                //Set Column Value
                int TotalRows = dt.Rows.Count + 3;
                Excel.Range RngColumnValue;
                RngColumnValue = ReportClassObj.Worksheet.get_Range("A3", LastColName + TotalRows.ToString());

                string strItem = "";
                //string Columnwise = "CustomerName";
                int IntRow = 0;
                for (IntRow = 0; IntRow <= dt.Rows.Count - 1; IntRow++)
                {
                    if (strItem != dt.Rows[IntRow][Columnwise].ToString())
                    {
                        strItem = dt.Rows[IntRow][Columnwise].ToString();
                        RngColumnValue.set_Item(IntRow + 1, 1, strItem);
                    }

                    for (int Intcol = 0; Intcol <= dt.Columns.Count - 1; Intcol++)
                    {
                        if (dt.Columns[Intcol].ColumnName.ToString().ToUpper() != Columnwise.ToUpper())
                        {
                            RngColumnValue.set_Item(IntRow + 1, Intcol + 1, dt.Rows[IntRow][Intcol].ToString());
                        }
                    }
                }
                ReportClassObj.ApplyStyleRowValue(RngColumnValue);
                #endregion

                #region Print GrandTotal
                //Set GrandTotal

                RngColumnValue.set_Item(IntRow + 1,  1, "Grand Total");
                object sumObject = dt.Compute("Sum(TotalAmt)", "");
                RngColumnValue.set_Item(IntRow + 1, dt.Columns["TotalAmt"].Ordinal + 1, sumObject.ToString());

                sumObject = dt.Compute("Sum(Discount)", "");
                RngColumnValue.set_Item(IntRow + 1, dt.Columns["Discount"].Ordinal + 1, sumObject.ToString());

                sumObject = dt.Compute("Sum(Advance_Amt)", "");
                RngColumnValue.set_Item(IntRow + 1, dt.Columns["Advance_Amt"].Ordinal + 1, sumObject.ToString());

                sumObject = dt.Compute("Sum(NetAmt)", "");
                RngColumnValue.set_Item(IntRow + 1, dt.Columns["NetAmt"].Ordinal + 1, sumObject.ToString());

                sumObject = dt.Compute("Sum(PaidAmt)", "");
                RngColumnValue.set_Item(IntRow + 1, dt.Columns["PaidAmt"].Ordinal + 1, sumObject.ToString());

                sumObject = dt.Compute("Sum(PaymentDue)", "");
                RngColumnValue.set_Item(IntRow + 1, dt.Columns["PaymentDue"].Ordinal + 1, sumObject.ToString());

                Excel.Range RngColGrandTotal;
                RngColGrandTotal = ReportClassObj.Worksheet.get_Range("A" + TotalRows, ReportClassObj.ColumnIndexToColumnLetter(TotalCols).ToString() + TotalRows);

                ReportClassObj.ApplyStyleGrandTotalValue(RngColGrandTotal);
                #endregion
            }
            #endregion

            ReportClassObj.SaveAndDisplayExcelFile();

        }
        #endregion

        #region Items Price List
        private void rptItemsPriceList()
        {

            ReportClassObj.ExcelFileOpen("Commonreport");
            string SelectColumns = "";
            string TableName = "";
            string JoinTable = "";
            string GroupBy = "";
            string OrderBy = "";
            string ReportName = "";

            #region BindTable
            
                    SelectColumns = "*";
                    GroupBy = "";
                    OrderBy = "Order by I.ItemCode,I.Item_Name";
                    ReportName = "Items Price List";
                    TableName = "ItemMaster ";
                    JoinTable = "";

            dt = ReportClassObj.ConstructSqlTable(SelectColumns, TableName, JoinTable, "ItemCode", (CmbItem.Text != "") ? CmbItem.SelectedValue.ToString() : "",
                                                    "","","","","","", "", "", GroupBy, OrderBy);
            dt.TableName = "ItemMaster";
            #endregion

            #region Print
            if (dt.Rows.Count > 0)
            {
                int TotalCols = dt.Columns.Count;

                #region Print ReportName
                //Set Report Name
                string LastColName = ReportClassObj.ColumnIndexToColumnLetter(TotalCols);

                Excel.Range RngReportName;
                RngReportName = ReportClassObj.Worksheet.get_Range("A1", LastColName + "1");
                ReportClassObj.SetReportName(RngReportName, ReportName);
                #endregion

                #region Print ReportHeader
                //Set Header
                Excel.Range RngColumnHeader;
                RngColumnHeader = ReportClassObj.Worksheet.get_Range("A2", LastColName + "2");
                ReportClassObj.SetReportHeader(RngColumnHeader, dt);
                #endregion

                #region Print Details
                //Set Column Value
                int TotalRows = dt.Rows.Count + 3;
                Excel.Range RngColumnValue;
                RngColumnValue = ReportClassObj.Worksheet.get_Range("A3", LastColName + TotalRows.ToString());

                string strItem = "";
                int IntRow = 0;
                for (IntRow = 0; IntRow <= dt.Rows.Count - 1; IntRow++)
                {
                    for (int Intcol = 0; Intcol <= dt.Columns.Count - 1; Intcol++)
                    {
                            RngColumnValue.set_Item(IntRow + 1, Intcol + 1, dt.Rows[IntRow][Intcol].ToString());
                    }
                }
                ReportClassObj.ApplyStyleRowValue(RngColumnValue);
                #endregion

                #region Print GrandTotal
                //Set GrandTotal

                object sumObject;
                sumObject = dt.Compute("Sum(UnitPrice)", "");
                RngColumnValue.set_Item(IntRow + 1, dt.Columns["UnitPrice"].Ordinal + 1, sumObject.ToString());

                Excel.Range RngColGrandTotal;
                RngColGrandTotal = ReportClassObj.Worksheet.get_Range("A" + TotalRows, ReportClassObj.ColumnIndexToColumnLetter(TotalCols).ToString() + TotalRows);

                ReportClassObj.ApplyStyleGrandTotalValue(RngColGrandTotal);
                #endregion
            }
            #endregion

            ReportClassObj.SaveAndDisplayExcelFile();

        }
        #endregion
    }

 
}
