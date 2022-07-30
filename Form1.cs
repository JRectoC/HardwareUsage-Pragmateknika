using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Common;
using System.Runtime.InteropServices;
using Microsoft.TeamFoundation.Common.Internal;

namespace ArduinoLCD16x2
{
    public partial class Form1 : Form
    {
        PerformanceCounter cpuCounter;
        PerformanceCounter perfMemCount;
        PerformanceCounter perfDiskUsage;
        PerformanceCounter perfGpuUtil;
        String outputToWrite = null;
        String CPU = null;
        String RAM = null;
        String SSD = null;
        String GPU = null;
        float GPUF;
        ulong installedMemory;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private class MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;
            public MEMORYSTATUSEX()
            {
                this.dwLength = (uint)Marshal.SizeOf(typeof(NativeMethods.MEMORYSTATUSEX));
            }
        }


        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

        #region ShadowEffect
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        #endregion
        #region DragWindows
        const int WM_LBUTTONDBLCLK = 0x0203;//client area
        const int WM_NCLBUTTONDBLCLK = 0x00A3;//non-client area
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDBLCLK)

                return;

            if (m.Msg == WM_NCLBUTTONDBLCLK)

                return;
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }
        #endregion
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        internal struct LASTINPUTINFO
        {
            public uint cbSize;

            public uint dwTime;
        }
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("Kernel32.dll")]
        private static extern uint GetLastError();
        public static int IdleTime() //In seconds
        {
            LASTINPUTINFO lastinputinfo = new LASTINPUTINFO();
            lastinputinfo.cbSize = (uint)Marshal.SizeOf(lastinputinfo);
            GetLastInputInfo(ref lastinputinfo);
            return (int)(((Environment.TickCount & int.MaxValue) - (lastinputinfo.dwTime & int.MaxValue)) & int.MaxValue) / 1000;
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // height of ellipse
           int nHeightEllipse // width of ellipse
       );

        public Form1()
        {

            InitializeComponent();
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
            perfMemCount = new PerformanceCounter("Memory", "Available MBytes");
            perfDiskUsage = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
            perfGpuUtil = new PerformanceCounter("GPU Engine", "Utilization Percentage"); ;

            MEMORYSTATUSEX memStatus = new MEMORYSTATUSEX();
            if (GlobalMemoryStatusEx(memStatus))
            {
                installedMemory = memStatus.ullTotalPhys;
            }
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            btnMinimize.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnMinimize.Width,
           btnMinimize.Height, 10, 10));
            btnClose.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClose.Width,
           btnClose.Height, 10, 10));
            btnWrite.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnWrite.Width,
           btnWrite.Height, 10, 10));
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            btnWrite.Enabled = false;
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                cmbPorts.Items.Add(port);

            }
            serialPort1.Close();
            if(Properties.Settings.Default.Automatic == true)
            {
                cbAutomatic.CheckState = CheckState.Checked;
                getCpuInfo.Start();
                panelManual.Enabled = false;
            }
            else
            {
                panelManual.Enabled = true;
            }
            
        }

        private void cmbPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();
                serialPort1.PortName = cmbPorts.Text;
                serialPort1.Open();
                btnWrite.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                getCpuInfo.Start();
            }
        }

        public String formatingString()
        {
            String format = null;
            format = " " + outputToWrite + "%";
            return format;
        }

        private async void getCpuInfo_Tick(object sender, EventArgs e)
        {
            //CPU
            var value = cpuCounter.NextValue();
            // In most cases you need to call .NextValue() twice
            if (Math.Abs(value) <= 0.00)
                value = cpuCounter.NextValue();

            int cpuVal = (int)value;
            if (cpuVal <= 9)
            {
                CPU = cpuVal + "%  ";
            }
            else if (cpuVal == 100)
            {
                CPU = cpuVal + "%";
            }
            else if (cpuVal >= 10 && cpuVal <= 99)
            {
                CPU = cpuVal + "% ";
            }
            else
            {
                CPU = "100%";
            }

            //RAM
            perfMemCount.NextValue();
            short currentAvailableMemory = (short)perfMemCount.NextValue();
            int TotalRamInstalledMB = ((((int)(installedMemory * 1.0) / 1000) / 1000) * 2) * -1;
            int ramUsagePercent = (100 * (TotalRamInstalledMB - (int)perfMemCount.NextValue())) / TotalRamInstalledMB;
            //ulong totalMemoryInBytes = new ComputerInfo().TotalPhysicalMemory;
            //float totalMemoryInMegabytes = (float)((double)totalMemoryInBytes / (1024 * 1024));
            int ramVal = ramUsagePercent;
            if (ramVal <= 9)
            {
                RAM = ramVal + "%  ";
            }
            else if (ramVal == 100)
            {
                RAM = ramVal + "%";
            }
            else if (ramVal >= 10 && ramVal <= 99)
            {
                RAM = ramVal + "% ";
            }
            else
            {
                RAM = "100%";
            }

            //SSD
            int ssdUsagePercent = Convert.ToInt32(perfDiskUsage.NextValue());
            //Console.WriteLine(j);
            int ssdVal = ssdUsagePercent;
            if (ssdVal <= 9)
            {
                SSD = ssdVal + "%  ";
            }
            else if (ssdVal == 100)
            {
                SSD = ssdVal + "%";
            }
            else if (ssdVal >= 10 && ssdVal <= 99)
            {
                SSD = ssdVal + "% ";
            }
            else
            {
                SSD = "100%";
            }

            //GPU\
            await GetGPUUsage();
            //int gpuUsage = (int)perfGpuUtil.RawValue;
            int gpuF = ((int)GPUF);
            int gpuVal = gpuF;
            if (gpuVal <= 9)
            {
                GPU = gpuVal + "%  ";
            }
            else if (gpuVal == 100)
            {
                GPU = gpuVal + "%";
            }
            else if (gpuVal >= 10 && gpuVal <= 99)
            {
                GPU = gpuVal + "% ";
            }
            else
            {
                GPU = "100%";
            }
            outputToWrite = "CPU:" + CPU + "SSD:" + SSD + "GPU:" + GPU + "RAM:" + RAM;
            String opt = formatingString();
            //cpuInfo.Text = opt;
            
            if (Properties.Settings.Default.Automatic == true)
            {
                automaticCommand();
            }else if(serialPort1.IsOpen)
            {
                textSerial.Invoke(new Action(() => textSerial.AppendText("\r\n" + opt)));
                serialPort1.Write(opt);
            }


        }

        public async Task<float> GetGPUUsage()
        {
            try
            {
                var category = new PerformanceCounterCategory("GPU Engine");
                var counterNames = category.GetInstanceNames();
                var gpuCounters = new List<PerformanceCounter>();
                var result = 0f;

                foreach (string counterName in counterNames)
                {
                    if (counterName.EndsWith("engtype_3D"))
                    {
                        foreach (PerformanceCounter counter in category.GetCounters(counterName))
                        {
                            if (counter.CounterName == "Utilization Percentage")
                            {
                                gpuCounters.Add(counter);
                            }
                        }
                    }
                }

                gpuCounters.ForEach(x =>
                {
                    _ = x.NextValue();
                });

                await Task.Delay(1000);

                gpuCounters.ForEach(x =>
                {
                    result += x.NextValue();
                });
                GPUF = result;
                return result;
            }
            catch
            {
                return 0f;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            notifyIcon1.Visible = true;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void textSerial_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public void automaticCommand()
        {
            cmbPorts.Items.Clear();
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                cmbPorts.Items.Add(port);

            }
            String opt = null;
            foreach (var stringS in cmbPorts.Items)
            {
                serialPort1.Close();
                serialPort1.PortName = stringS.ToString();
                serialPort1.Open();
                opt = formatingString();
                serialPort1.Write(opt);
            }
            textSerial.Invoke(new Action(() => textSerial.AppendText("\r\n" + opt)));
        }

        private void cbAutomatic_CheckedChanged(object sender, EventArgs e)
        {
            
            if (cbAutomatic.CheckState  == CheckState.Checked)
            {
                Properties.Settings.Default.Automatic = true;
                Properties.Settings.Default.Save();
                getCpuInfo.Start();
                panelManual.Enabled = false;

            }
            else
            {
                Properties.Settings.Default.Automatic = false;
                Properties.Settings.Default.Save();
                getCpuInfo.Stop();
                panelManual.Enabled = true;
                cmbPorts.Items.Clear();
                string[] ports = SerialPort.GetPortNames();

                foreach (string port in ports)
                {
                    cmbPorts.Items.Add(port);

                }
                foreach (var stringS in cmbPorts.Items)
                {
                    serialPort1.Close();
                    serialPort1.PortName = stringS.ToString();
                    serialPort1.Close();
                }
            }
        }
    }
}
