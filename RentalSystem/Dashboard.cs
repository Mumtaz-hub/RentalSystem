using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RentalSystem
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            BindMonthCalendar();
            LoadGrid();
            LoadChartForItem();
            LoadChartForPayment();
            LoadChartForMonthlyPayment();
        }

        private void BindMonthCalendar()
        {
            string query = "select Deliverydate,YEAR(Deliverydate) as 'Year',MONTH(Deliverydate) as 'Month',DAY(Deliverydate) as 'Day' from OrderMain order by Deliverydate";
            DataTable dt = new DataTable();
            dt = DBClass.GetTableByQuery(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DateTime DeliveryDate = new DateTime(int.Parse(row["Year"].ToString()), int.Parse(row["Month"].ToString()), int.Parse(row["Day"].ToString()));
                    monthCalendar1.AddBoldedDate(DeliveryDate);
                    monthCalendar1.UpdateBoldedDates();
                }
            }

            monthCalendar1.TitleBackColor = System.Drawing.Color.Blue;
            monthCalendar1.TrailingForeColor = System.Drawing.Color.Red;
            monthCalendar1.TitleForeColor = System.Drawing.Color.Yellow;
        }

        private void LoadGrid()
        {
            string query = @"select o.CustomerName,o.OrderID,o.Deliverydate,
                        (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID)  as 'TotalAmt',o.Discount,o.Advance_Amt,
                        (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt as 'NetAmt',
                        (select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID) as 'PaidAmount',
                        'Paid' as 'Select'
                        from OrderMain o 
                        where (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt-(select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID)=0
						and O.Deliverydate='" + monthCalendar1.SelectionRange.Start.ToString("MM/dd/yyyy") + @"'
                        union

                        select o.CustomerName,o.OrderID,o.Deliverydate,
                        (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID)  as 'TotalAmt',o.Discount,o.Advance_Amt,
                        (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt as 'NetAmt',
                        (select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID) as 'PaidAmount',
                        'Payment Due' as 'Select'
                        from OrderMain o 
                        where (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt-(select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID)>0
						and O.Deliverydate='" + monthCalendar1.SelectionRange.Start.ToString("MM/dd/yyyy")+ @"'
						
                        union

                        select o.CustomerName,o.OrderID,o.Deliverydate,
                        (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID)  as 'TotalAmt',o.Discount,o.Advance_Amt,
                        (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt as 'NetAmt',
                        (select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID) as 'PaidAmount',
                        'InComplete' as 'Select'
                        from OrderMain o 
                        where o.OrderId NOT IN (select OrderId from PaymentDetail)
                        and O.Deliverydate='" + monthCalendar1.SelectionRange.Start.ToString("MM/dd/yyyy") + @"'";
            
            DataTable dt = new DataTable();
            dt = DBClass.GetTableByQuery(query);
            dataGridView1.DataSource = null;
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
                dataGridView1.ReadOnly = true;
            }
        }
        private void LoadChartForItem()
        {
            string query = @"select I.ItemCode,I.ItemName +' OrderNo->'+ CONVERT(varchar(10),(D.OrderId)) as 'ItemName',Sum(D.Qty) as 'Counter' 
                                from ItemMaster I
                                inner join OrderDetail D on D.ItemCode=I.ItemCode
                                and D.OrderId in(select OrderId from OrderMain M where M.Deliverydate='" + monthCalendar1.SelectionRange.Start.ToString("MM/dd/yyyy") + @"')
                                group by I.ItemCode,I.ItemName,D.OrderId
                                order by I.ItemCode,I.ItemName";

            DataTable dt = new DataTable();
            dt = DBClass.GetTableByQuery(query);
            ChartOrder.Update();
            //if (dt.Rows.Count > 0)
            //{

                
                ChartOrder.Series["Item"].XValueMember = dt.Columns[1].ColumnName;
                ChartOrder.Series["Item"].YValueMembers = dt.Columns[2].ColumnName;
                ChartOrder.DataSource = dt;
                ChartOrder.DataBind();
                this.ChartOrder.Titles.Clear();
                this.ChartOrder.Titles.Add("Item Chart for Order Booked");
                ChartOrder.Series["Item"].ChartType = SeriesChartType.Doughnut;
                //chart1.Series["Item"].
                ChartOrder.Series["Item"].IsValueShownAsLabel = true;
             
            //}
        }

        private void LoadChartForPayment()
        {
            string query = @"select o.OrderId, 
                    (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID)  as 'TotalAmt',
                    o.Discount,o.Advance_Amt,
                    (select sum(Qty*UnitPrice) from OrderDetail where OrderId =o.OrderID) -o.Discount-o.Advance_Amt as 'NetAmt',
                    (select sum(Paid_Amt) from PaymentDetail where OrderId =o.OrderID) as 'PaidAmount'
                    from OrderMain o 
                    where o.Deliverydate='" + monthCalendar1.SelectionRange.Start.ToString("MM/dd/yyyy") + @"'";
            DataTable dt = new DataTable();
            dt = DBClass.GetTableByQuery(query);
            //if (dt.Rows.Count > 0)
            //{

                chartPayment.DataSource = dt;

                chartPayment.Series["NetAmt"].XValueMember = "OrderId";
                chartPayment.Series["NetAmt"].YValueMembers = "NetAmt";

                chartPayment.Series["PaidAmt"].XValueMember = "OrderId";
                chartPayment.Series["PaidAmt"].YValueMembers = "PaidAmount";
                
                chartPayment.DataBind();

                this.chartPayment.Titles.Clear();
                this.chartPayment.Titles.Add("Payment Chart for Order Booked");

               // chart2.Series["NetAmt"].ChartType = SeriesChartType.Pie;
                chartPayment.Series["NetAmt"].IsValueShownAsLabel = true;

                //chart2.Series["PaidAmt"].ChartType = SeriesChartType.Bar;
                chartPayment.Series["PaidAmt"].IsValueShownAsLabel = true;

            //}
        }
        private void LoadChartForMonthlyPayment()
        {
            DateTime d = monthCalendar1.SelectionRange.Start;
            string month = d.Month.ToString();

//            string query = @"select  Month(o.Deliverydate) as 'Order',sum(d.Qty*d.UnitPrice) as 'TotalAmt',
//                                sum((d.Qty*d.UnitPrice)-o.Discount-o.Advance_Amt) as 'NetAmt',SUM(p.Paid_Amt) as 'PaidAmt'     
//                                from OrderMain o 
//                                inner join OrderDetail d on o.OrderID=d.OrderId
//                                left join PaymentDetail p on o.OrderID=p.OrderId
//                                where Month(o.Deliverydate)='" + month + @"'
//                                group by Month(o.Deliverydate)";

            string query = @"select '"+ month + @"' as 'Month',
                                (select sum(Qty*UnitPrice) from OrderDetail 
	                                where OrderId in (select OrderID from OrderMain where month(Deliverydate)='"+ month +@"'))  as 'TotalAmt',
	
                                 ((select sum(Qty*UnitPrice) from OrderDetail 
		                                where OrderId in (select OrderID from OrderMain where month(Deliverydate)='" + month + @"'))
		                                -(select SUM(M.Discount+M.Advance_Amt)  from OrderMain M where month(Deliverydate)='" + month + @"')) as 'NetAmt',
		
                                (select sum(Paid_Amt) from PaymentDetail where OrderId in (select OrderID from OrderMain where month(Deliverydate)='" + month + @"')) as 'PaidAmt'";

            DataTable dt = new DataTable();
            dt = DBClass.GetTableByQuery(query);
            //if (dt.Rows.Count > 0)
            //{

            chartMonthlyPayment.DataSource = dt;

            chartMonthlyPayment.Series["NetAmt"].XValueMember = "Month";
            chartMonthlyPayment.Series["NetAmt"].YValueMembers = "NetAmt";

            chartMonthlyPayment.Series["PaidAmt"].XValueMember = "Month";
            chartMonthlyPayment.Series["PaidAmt"].YValueMembers = "PaidAmt";

            chartMonthlyPayment.DataBind();

            this.chartMonthlyPayment.Titles.Clear();
            this.chartMonthlyPayment.Titles.Add("Monthly Payment Chart");

            // chart2.Series["NetAmt"].ChartType = SeriesChartType.Pie;
            chartMonthlyPayment.Series["NetAmt"].IsValueShownAsLabel = true;

            //chart2.Series["PaidAmt"].ChartType = SeriesChartType.Bar;
            chartMonthlyPayment.Series["PaidAmt"].IsValueShownAsLabel = true;

            //}
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            LoadGrid();
            LoadChartForItem();
            LoadChartForPayment();
            LoadChartForMonthlyPayment();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var s = new Series();
            //s.ChartType = SeriesChartType.Pie;

            //var d = new DateTime(2018, 01, 01);

            //s.Points.AddXY(d, 3);
            //s.Points.AddXY(d.AddMonths(-1), 2);
            //s.Points.AddXY(d.AddMonths(-2), 1);
            //s.Points.AddXY(d.AddMonths(-3), 4);

            //chart2.Series.Clear();
            //chart2.Series.Add(s);


            //chart2.Series[0].XValueType = ChartValueType.DateTime;
            //chart2.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd";
            //chart2.ChartAreas[0].AxisX.Interval = 1;
            //chart2.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Months;
            //chart2.ChartAreas[0].AxisX.IntervalOffset = 1;

            //chart2.Series[0].XValueType = ChartValueType.DateTime;
            //DateTime minDate = new DateTime(2018, 01, 01).AddSeconds(-1);
            //DateTime maxDate = new DateTime(2018, 12, 31); // or DateTime.Now;
            //chart2.ChartAreas[0].AxisX.Minimum = minDate.ToOADate();
            //chart2.ChartAreas[0].AxisX.Maximum = maxDate.ToOADate();
        }
    }

}
