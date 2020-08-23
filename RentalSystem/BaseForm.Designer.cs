namespace RentalSystem
{
    partial class BaseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlMiddle = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlleft = new System.Windows.Forms.Panel();
            this.btnBackupRestore = new System.Windows.Forms.Button();
            this.btnItemSettings = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnPayment = new System.Windows.Forms.Button();
            this.btnNewOrder = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.Button();
            this.pnlBody.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlleft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.Black;
            this.pnlBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBody.Controls.Add(this.pnlMain);
            this.pnlBody.Controls.Add(this.pnlleft);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(1065, 551);
            this.pnlBody.TabIndex = 0;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Gray;
            this.pnlMain.Controls.Add(this.pnlMiddle);
            this.pnlMain.Controls.Add(this.pnlBottom);
            this.pnlMain.Controls.Add(this.pnlTop);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(161, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(902, 549);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.BackColor = System.Drawing.Color.White;
            this.pnlMiddle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMiddle.Location = new System.Drawing.Point(0, 30);
            this.pnlMiddle.Name = "pnlMiddle";
            this.pnlMiddle.Size = new System.Drawing.Size(902, 489);
            this.pnlMiddle.TabIndex = 2;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBottom.Controls.Add(this.txtMessage);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 519);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(902, 30);
            this.pnlBottom.TabIndex = 1;
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.Color.Black;
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.ForeColor = System.Drawing.Color.Red;
            this.txtMessage.Location = new System.Drawing.Point(0, 0);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(900, 28);
            this.txtMessage.TabIndex = 3;
            this.txtMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.txtTitle);
            this.pnlTop.Controls.Add(this.btnMinimize);
            this.pnlTop.Controls.Add(this.btnClose);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(902, 30);
            this.pnlTop.TabIndex = 0;
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.Color.Black;
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTitle.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.ForeColor = System.Drawing.Color.GreenYellow;
            this.txtTitle.Location = new System.Drawing.Point(0, 0);
            this.txtTitle.Multiline = true;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(836, 28);
            this.txtTitle.TabIndex = 2;
            this.txtTitle.Text = "SHREEJI LED DISPLAY & LIVE";
            this.txtTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnMinimize
            // 
            this.btnMinimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Image = global::RentalSystem.Properties.Resources.minimize_blue11;
            this.btnMinimize.Location = new System.Drawing.Point(836, 0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(32, 28);
            this.btnMinimize.TabIndex = 1;
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::RentalSystem.Properties.Resources.close_box_red;
            this.btnClose.Location = new System.Drawing.Point(868, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(32, 28);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlleft
            // 
            this.pnlleft.BackColor = System.Drawing.Color.Black;
            this.pnlleft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlleft.Controls.Add(this.btnBackupRestore);
            this.pnlleft.Controls.Add(this.btnItemSettings);
            this.pnlleft.Controls.Add(this.btnReport);
            this.pnlleft.Controls.Add(this.btnPayment);
            this.pnlleft.Controls.Add(this.btnNewOrder);
            this.pnlleft.Controls.Add(this.btnDashboard);
            this.pnlleft.Controls.Add(this.btnMenu);
            this.pnlleft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlleft.Location = new System.Drawing.Point(0, 0);
            this.pnlleft.Name = "pnlleft";
            this.pnlleft.Size = new System.Drawing.Size(161, 549);
            this.pnlleft.TabIndex = 0;
            // 
            // btnBackupRestore
            // 
            this.btnBackupRestore.BackColor = System.Drawing.Color.Black;
            this.btnBackupRestore.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBackupRestore.FlatAppearance.BorderSize = 0;
            this.btnBackupRestore.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnBackupRestore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue;
            this.btnBackupRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackupRestore.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.btnBackupRestore.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnBackupRestore.Image = global::RentalSystem.Properties.Resources.restore;
            this.btnBackupRestore.Location = new System.Drawing.Point(0, 429);
            this.btnBackupRestore.Name = "btnBackupRestore";
            this.btnBackupRestore.Size = new System.Drawing.Size(159, 80);
            this.btnBackupRestore.TabIndex = 8;
            this.btnBackupRestore.Text = "Backup && Restore";
            this.btnBackupRestore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBackupRestore.UseVisualStyleBackColor = false;
            this.btnBackupRestore.Click += new System.EventHandler(this.btnBackupRestore_Click);
            // 
            // btnItemSettings
            // 
            this.btnItemSettings.BackColor = System.Drawing.Color.Black;
            this.btnItemSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnItemSettings.FlatAppearance.BorderSize = 0;
            this.btnItemSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnItemSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue;
            this.btnItemSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnItemSettings.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.btnItemSettings.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnItemSettings.Image = global::RentalSystem.Properties.Resources.Settings2;
            this.btnItemSettings.Location = new System.Drawing.Point(0, 349);
            this.btnItemSettings.Name = "btnItemSettings";
            this.btnItemSettings.Size = new System.Drawing.Size(159, 80);
            this.btnItemSettings.TabIndex = 7;
            this.btnItemSettings.Text = "Settings";
            this.btnItemSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnItemSettings.UseVisualStyleBackColor = false;
            this.btnItemSettings.Click += new System.EventHandler(this.btnItemSettings_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.Black;
            this.btnReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReport.FlatAppearance.BorderSize = 0;
            this.btnReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.btnReport.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnReport.Image = global::RentalSystem.Properties.Resources.Report;
            this.btnReport.Location = new System.Drawing.Point(0, 269);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(159, 80);
            this.btnReport.TabIndex = 6;
            this.btnReport.Text = "Report";
            this.btnReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnPayment
            // 
            this.btnPayment.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPayment.FlatAppearance.BorderSize = 0;
            this.btnPayment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnPayment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue;
            this.btnPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayment.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.btnPayment.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnPayment.Image = global::RentalSystem.Properties.Resources.payment2;
            this.btnPayment.Location = new System.Drawing.Point(0, 189);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(159, 80);
            this.btnPayment.TabIndex = 5;
            this.btnPayment.Text = "Payment";
            this.btnPayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPayment.UseVisualStyleBackColor = true;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // btnNewOrder
            // 
            this.btnNewOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNewOrder.FlatAppearance.BorderSize = 0;
            this.btnNewOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnNewOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue;
            this.btnNewOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewOrder.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.btnNewOrder.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnNewOrder.Image = global::RentalSystem.Properties.Resources.order;
            this.btnNewOrder.Location = new System.Drawing.Point(0, 109);
            this.btnNewOrder.Name = "btnNewOrder";
            this.btnNewOrder.Size = new System.Drawing.Size(159, 80);
            this.btnNewOrder.TabIndex = 4;
            this.btnNewOrder.Text = "New Order";
            this.btnNewOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNewOrder.UseVisualStyleBackColor = true;
            this.btnNewOrder.Click += new System.EventHandler(this.btnNewOrder_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnDashboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.btnDashboard.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnDashboard.Image = global::RentalSystem.Properties.Resources.DashBoard21;
            this.btnDashboard.Location = new System.Drawing.Point(0, 29);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(159, 80);
            this.btnDashboard.TabIndex = 3;
            this.btnDashboard.Text = "DashBoard";
            this.btnDashboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // btnMenu
            // 
            this.btnMenu.BackColor = System.Drawing.Color.Black;
            this.btnMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenu.FlatAppearance.BorderSize = 0;
            this.btnMenu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue;
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenu.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.btnMenu.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnMenu.Location = new System.Drawing.Point(0, 0);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(159, 29);
            this.btnMenu.TabIndex = 0;
            this.btnMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMenu.UseVisualStyleBackColor = false;
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1065, 551);
            this.Controls.Add(this.pnlBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "BaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BaseForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BaseForm_Load);
            this.pnlBody.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlleft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlleft;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Button btnBackupRestore;
        private System.Windows.Forms.Button btnItemSettings;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnPayment;
        private System.Windows.Forms.Button btnNewOrder;
        private System.Windows.Forms.Button btnDashboard;
        protected System.Windows.Forms.Panel pnlMiddle;
        public System.Windows.Forms.TextBox txtTitle;
        public System.Windows.Forms.TextBox txtMessage;
    }
}

