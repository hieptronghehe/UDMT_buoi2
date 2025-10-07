using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        private SerialPort congCom = new SerialPort();
        private bool daMoCong = false;
        private string logFilePath = "serial_log.txt";
        private int dataPointCount = 0;
        private const int MAX_DATA_POINTS = 20;

        public Form1()
        {
            InitializeComponent();
            KhoiTaoBieuDo();
        }

        private void KhoiTaoBieuDo()
        {
            // Cấu hình biểu đồ
            chart1.Titles.Clear();
            chart1.Titles.Add("Biểu đồ Nhiệt độ và Độ ẩm");
            chart1.Titles[0].Font = new Font("Arial", 12, FontStyle.Bold);

            chart1.Series.Clear();

            // Series nhiệt độ
            Series seriesNhietDo = new Series("Nhiệt độ");
            seriesNhietDo.ChartType = SeriesChartType.Line;
            seriesNhietDo.Color = Color.Red;
            seriesNhietDo.BorderWidth = 3;
            seriesNhietDo.MarkerStyle = MarkerStyle.Circle;
            seriesNhietDo.MarkerSize = 8;
            seriesNhietDo.MarkerColor = Color.DarkRed;
            seriesNhietDo.XValueType = ChartValueType.Int32;
            chart1.Series.Add(seriesNhietDo);

            // Series độ ẩm
            Series seriesDoAm = new Series("Độ ẩm");
            seriesDoAm.ChartType = SeriesChartType.Column;
            seriesDoAm.Color = Color.Blue;
            seriesDoAm.BorderWidth = 2;
            seriesDoAm.XValueType = ChartValueType.Int32;
            chart1.Series.Add(seriesDoAm);

            // Cấu hình trục
            chart1.ChartAreas[0].AxisX.Title = "Thời gian";
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            chart1.ChartAreas[0].AxisY.Title = "Giá trị";
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

            chart1.Legends[0].Enabled = true;
            chart1.Legends[0].Docking = Docking.Bottom;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Làm mới danh sách cổng COM
            LamMoiDanhSachCongCOM();

            nutMoDongCong.Click += NutMoDongCong_Click;
            nutCheckTinHieu.Click += NutCheckTinHieu_Click;
            nutGuiSo.Click += NutGuiSo_Click;
            congCom.DataReceived += CongCom_DuLieuNhan;

            // Thêm sự kiện làm mới cổng COM
            var nutLamMoi = new Button();
            nutLamMoi.Text = "Làm mới";
            nutLamMoi.Location = new Point(hopChonCong.Right + 10, hopChonCong.Top);
            nutLamMoi.Size = new Size(80, 25);
            nutLamMoi.Click += (s, ev) => LamMoiDanhSachCongCOM();
            this.Controls.Add(nutLamMoi);
        }

        private void LamMoiDanhSachCongCOM()
        {
            string portHienTai = hopChonCong.SelectedItem?.ToString();
            hopChonCong.Items.Clear();
            hopChonCong.Items.AddRange(SerialPort.GetPortNames());

            if (hopChonCong.Items.Count > 0)
            {
                // Ưu tiên chọn cổng trước đó hoặc chọn đầu tiên
                if (!string.IsNullOrEmpty(portHienTai) && hopChonCong.Items.Contains(portHienTai))
                    hopChonCong.SelectedItem = portHienTai;
                else
                    hopChonCong.SelectedIndex = 0;
            }
        }

        private void NutMoDongCong_Click(object sender, EventArgs e)
        {
            if (!daMoCong)
            {
                if (hopChonCong.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn cổng COM");
                    return;
                }

                congCom.PortName = hopChonCong.SelectedItem.ToString();
                congCom.BaudRate = 9600;
                congCom.DataBits = 8;
                congCom.Parity = Parity.None;
                congCom.StopBits = StopBits.One;
                congCom.Handshake = Handshake.None;
                congCom.ReadTimeout = 1000;
                congCom.WriteTimeout = 1000;

                try
                {
                    congCom.Open();
                    daMoCong = true;
                    nutMoDongCong.Text = "Đóng cổng";
                    richTextBoxRawData.AppendText($"Đã mở cổng {congCom.PortName}\n");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi mở cổng: " + ex.Message);
                }
            }
            else
            {
                try
                {
                    congCom.Close();
                    daMoCong = false;
                    nutMoDongCong.Text = "Mở cổng";
                    richTextBoxRawData.AppendText("Đã đóng cổng\n");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi đóng cổng: " + ex.Message);
                }
            }
        }

        private void NutCheckTinHieu_Click(object sender, EventArgs e)
        {
            if (daMoCong)
            {
                try
                {
                    congCom.WriteLine("CHECK");
                    richTextBoxRawData.AppendText("Đã gửi lệnh CHECK\n");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi gửi lệnh: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Cổng port chưa mở");
            }
        }

        private void NutGuiSo_Click(object sender, EventArgs e)
        {
            // Có thể thêm chức năng gửi số ở đây
        }

        private void CongCom_DuLieuNhan(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                // Đọc tất cả dữ liệu có sẵn
                while (congCom.BytesToRead > 0)
                {
                    string duLieu = congCom.ReadLine().Trim();

                    if (!string.IsNullOrEmpty(duLieu))
                    {
                        // Ghi log
                        File.AppendAllText(logFilePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + duLieu + Environment.NewLine);

                        // Cập nhật UI
                        this.Invoke(new Action<string>(CapNhatUI), duLieu);
                    }
                }
            }
            catch (TimeoutException) { }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    richTextBoxRawData.AppendText($"Lỗi đọc dữ liệu: {ex.Message}\n");
                }));
            }
        }

        private void CapNhatUI(string duLieu)
        {
            try
            {
                // Hiển thị dữ liệu thô
                richTextBoxRawData.AppendText(duLieu + Environment.NewLine);

                // Cuộn xuống cuối
                richTextBoxRawData.SelectionStart = richTextBoxRawData.Text.Length;
                richTextBoxRawData.ScrollToCaret();

                // Phân tích dữ liệu
                PhanTichDuLieu(duLieu);
            }
            catch (Exception ex)
            {
                richTextBoxRawData.AppendText($"Lỗi xử lý UI: {ex.Message}\n");
            }
        }

        private void PhanTichDuLieu(string duLieu)
        {
            // Debug: hiển thị dữ liệu nhận được
            Console.WriteLine($"Dữ liệu nhận được: {duLieu}");

            // Phân tích nhiệt độ và độ ẩm từ các định dạng khác nhau
            double nhietDo = 0;
            double doAm = 0;
            bool coDuLieu = false;

            // Thử các pattern khác nhau
            // Pattern 1: "23.00 C HUmldiy: 66.60 % Temperature:"
            var match1 = Regex.Match(duLieu, @"(\d+\.\d+)\s*C.*?HUmldiy:\s*(\d+\.\d+)");
            if (match1.Success && match1.Groups.Count >= 3)
            {
                nhietDo = double.Parse(match1.Groups[1].Value);
                doAm = double.Parse(match1.Groups[2].Value);
                coDuLieu = true;
            }
            else
            {
                // Pattern 2: "Temperature: 23.00 C, Humidity: 66.60%"
                var match2 = Regex.Match(duLieu, @"Temperature:\s*(\d+\.\d+).*?Humidity:\s*(\d+\.\d+)", RegexOptions.IgnoreCase);
                if (match2.Success && match2.Groups.Count >= 3)
                {
                    nhietDo = double.Parse(match2.Groups[1].Value);
                    doAm = double.Parse(match2.Groups[2].Value);
                    coDuLieu = true;
                }
                else
                {
                    // Pattern 3: Chỉ có số
                    var match3 = Regex.Match(duLieu, @"(\d+\.\d+).*?(\d+\.\d+)");
                    if (match3.Success && match3.Groups.Count >= 3)
                    {
                        nhietDo = double.Parse(match3.Groups[1].Value);
                        doAm = double.Parse(match3.Groups[2].Value);
                        coDuLieu = true;
                    }
                }
            }

            if (coDuLieu)
            {
                richTextBoxRawData.AppendText($"Nhiệt độ={nhietDo}°C, Độ ẩm={doAm}%\n");
                CapNhatBieuDo(nhietDo, doAm);
            }
            else
            {
              
            }
        }

        private void CapNhatBieuDo(double nhietDo, double doAm)
        {
            try
            {
                dataPointCount++;

                // Giới hạn số điểm hiển thị
                if (chart1.Series["Nhiệt độ"].Points.Count >= MAX_DATA_POINTS)
                {
                    chart1.Series["Nhiệt độ"].Points.RemoveAt(0);
                    chart1.Series["Độ ẩm"].Points.RemoveAt(0);
                }

                // Thêm điểm dữ liệu mới
                chart1.Series["Nhiệt độ"].Points.AddXY(dataPointCount, nhietDo);
                chart1.Series["Độ ẩm"].Points.AddXY(dataPointCount, doAm);

                // Cập nhật tiêu đề
                chart1.Titles[0].Text = $"Biểu đồ Nhiệt độ và Độ ẩm\nNhiệt độ: {nhietDo}°C, Độ ẩm: {doAm}%";

                // Tự động điều chỉnh trục
                chart1.ChartAreas[0].RecalculateAxesScale();

                // Cuộn đến điểm mới nhất
                if (dataPointCount > MAX_DATA_POINTS)
                {
                    chart1.ChartAreas[0].AxisX.ScaleView.Position = dataPointCount - MAX_DATA_POINTS;
                }

                // Làm mới biểu đồ
                chart1.Invalidate();

            }
            catch (Exception ex)
            {
               
            }
        }

        // Thêm dữ liệu giả để test
        private void NutThemDuLieuMau_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            double nhietDo = 20 + random.NextDouble() * 15; // 20-35°C
            double doAm = 50 + random.NextDouble() * 30; // 50-80%

            CapNhatBieuDo(Math.Round(nhietDo, 1), Math.Round(doAm, 1));
            richTextBoxRawData.AppendText($"Dữ liệu mẫu: {nhietDo:F1}°C, {doAm:F1}%\n");
        }

        private void richTextBoxRawData_TextChanged(object sender, EventArgs e) { }

        private void nutCheckTinHieu_Click_1(object sender, EventArgs e) { }

        private void chart1_Click(object sender, EventArgs e) { }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (daMoCong)
            {
                congCom.Close();
            }
            base.OnFormClosing(e);
        }
    }
}