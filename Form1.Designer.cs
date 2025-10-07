namespace WindowsFormsApp4
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.hopChonCong = new System.Windows.Forms.ComboBox();
            this.nutMoDongCong = new System.Windows.Forms.Button();
            this.nutCheckTinHieu = new System.Windows.Forms.Button();
            this.nutGuiSo = new System.Windows.Forms.Button();
            this.richTextBoxRawData = new System.Windows.Forms.RichTextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // hopChonCong
            // 
            this.hopChonCong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.hopChonCong.FormattingEnabled = true;
            this.hopChonCong.Location = new System.Drawing.Point(40, 13);
            this.hopChonCong.Margin = new System.Windows.Forms.Padding(4);
            this.hopChonCong.Name = "hopChonCong";
            this.hopChonCong.Size = new System.Drawing.Size(160, 24);
            this.hopChonCong.TabIndex = 0;
            // 
            // nutMoDongCong
            // 
            this.nutMoDongCong.Location = new System.Drawing.Point(227, 13);
            this.nutMoDongCong.Margin = new System.Windows.Forms.Padding(4);
            this.nutMoDongCong.Name = "nutMoDongCong";
            this.nutMoDongCong.Size = new System.Drawing.Size(120, 28);
            this.nutMoDongCong.TabIndex = 1;
            this.nutMoDongCong.Text = "Mở cổng";
            this.nutMoDongCong.UseVisualStyleBackColor = true;
            // 
            // nutCheckTinHieu
            // 
            this.nutCheckTinHieu.Location = new System.Drawing.Point(42, 45);
            this.nutCheckTinHieu.Margin = new System.Windows.Forms.Padding(4);
            this.nutCheckTinHieu.Name = "nutCheckTinHieu";
            this.nutCheckTinHieu.Size = new System.Drawing.Size(120, 28);
            this.nutCheckTinHieu.TabIndex = 2;
            this.nutCheckTinHieu.Text = "Check tín hiệu";
            this.nutCheckTinHieu.UseVisualStyleBackColor = true;
            this.nutCheckTinHieu.Click += new System.EventHandler(this.nutCheckTinHieu_Click_1);
            // 
            // nutGuiSo
            // 
            this.nutGuiSo.Location = new System.Drawing.Point(227, 49);
            this.nutGuiSo.Margin = new System.Windows.Forms.Padding(4);
            this.nutGuiSo.Name = "nutGuiSo";
            this.nutGuiSo.Size = new System.Drawing.Size(120, 28);
            this.nutGuiSo.TabIndex = 3;
            this.nutGuiSo.Text = "Gửi số";
            this.nutGuiSo.UseVisualStyleBackColor = true;
            // 
            // richTextBoxRawData
            // 
            this.richTextBoxRawData.Location = new System.Drawing.Point(42, 131);
            this.richTextBoxRawData.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBoxRawData.Name = "richTextBoxRawData";
            this.richTextBoxRawData.ReadOnly = true;
            this.richTextBoxRawData.Size = new System.Drawing.Size(305, 77);
            this.richTextBoxRawData.TabIndex = 6;
            this.richTextBoxRawData.Text = "";
            this.richTextBoxRawData.TextChanged += new System.EventHandler(this.richTextBoxRawData_TextChanged);
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(42, 224);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(305, 219);
            this.chart1.TabIndex = 7;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 455);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.hopChonCong);
            this.Controls.Add(this.nutMoDongCong);
            this.Controls.Add(this.nutCheckTinHieu);
            this.Controls.Add(this.nutGuiSo);
            this.Controls.Add(this.richTextBoxRawData);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Nhận Dữ Liệu Serial";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox hopChonCong;
        private System.Windows.Forms.Button nutMoDongCong;
        private System.Windows.Forms.Button nutCheckTinHieu;
        private System.Windows.Forms.Button nutGuiSo;
        private System.Windows.Forms.RichTextBox richTextBoxRawData;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}

