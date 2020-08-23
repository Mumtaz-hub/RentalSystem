namespace RentalSystem
{
    partial class LedMaster
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvLED = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLED)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLED
            // 
            this.dgvLED.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvLED.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLED.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLED.ColumnHeadersHeight = 25;
            this.dgvLED.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLED.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvLED.Location = new System.Drawing.Point(0, 0);
            this.dgvLED.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvLED.Name = "dgvLED";
            this.dgvLED.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvLED.RowHeadersVisible = false;
            this.dgvLED.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLED.Size = new System.Drawing.Size(547, 516);
            this.dgvLED.TabIndex = 0;
            this.dgvLED.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLED_RowValidated);
            this.dgvLED.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvLED_UserDeletedRow);
            // 
            // LedMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(547, 516);
            this.Controls.Add(this.dgvLED);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LedMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Price Setings";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LedMaster_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LedMaster_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLED)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLED;
    }
}