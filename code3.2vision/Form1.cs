using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using File = System.IO.File;

namespace installFiles
{
    public partial class Form1 : Form
    {
        string[] args = null;
        public Form1()
        {
            InitializeComponent();
        }

        public string[] data1;
        public Form1(string[] args)
        {
            InitializeComponent();
            this.args = args;
            data1 = args;
        }

        public string filepath = "";
        public string sc = "";
        public string sc1 = "";
        public string[] data3;
        public bool abc1 = false;
        public string data2;
        private void Form1_Load(object sender, EventArgs e)
        {
            data2 = data1[0];
            MessageBox.Show("本版本为beta版本!", "Confirm 确认");
            abc1 = true;
            
            fffff();
            backgroundWorker1.WorkerReportsProgress = true;
        }


        public void fffff()
        {
            int datar = int.Parse(File.ReadAllText("config\\data.Fcl"));
            found1(datar);
            if (datar > 100)
            {
                MessageBox.Show("缓存太多了，你需要清理一下！", "Message 消息");
            }
            datar++;
            File.WriteAllText("config\\data.Fcl", datar.ToString());

        }

        public void found1(int datag)
        {
            string[] disks = Directory.GetLogicalDrives();
            Directory.CreateDirectory("config\\data\\" + datag + "\\");

            ZipFile.ExtractToDirectory(data2, "config\\data\\" + datag + "\\");

            data3 = File.ReadAllLines("config\\data\\" + datag + "\\install.Fcl", Encoding.Default);
            label1.Text = data3[0];
            label2.Text = data3[1];
            this.Text = data3[2];
            richTextBox1.Text = data3[3];
            sc = data3[4];
            sc1 = data3[5];

            for (int ii = 0; ii < disks.Length; ii++)
            {
                //循环添加到列表中
                comboBox1.Items.Add(disks[ii]);
                //设置属性默认选择第1个
                comboBox1.SelectedIndex = 0;
            }
            abc1 = false;
        }

        public string drive = "C:";

        private void button2_Click(object sender, EventArgs e)
        {
            this.Height = this.Height * 2;
            button2.Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            drive = comboBox1.SelectedItem.ToString();
            MessageBox.Show("成功选择磁盘分区!", "Done 完成");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
            this.Height = this.Height / 2;
        }

        public int li = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            li = 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("del.bat", "config\\data");
            Application.Restart();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("App Installer\n 版本: beta vision 3.2 \n 本程序所有权归FxtStudio所属，源代码以开源，遵循BSD协议 \n 更新内容:\n 1.优化架构，减少出错率\n2.效率更高,优化卡死问题 \n 3.修改缓存机制 \n 4.修改文件格式支持，增加新的支持格式.\n 5.修改ui \n 6.更新程序图标，让程序更加优秀\n Our Website : http://Fxtstudio.epizy.com");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (li == 1) {
                if (Directory.Exists(drive + @"\\App"))
                {
                    ZipFile.ExtractToDirectory("config\\data\\install\\install.App", drive + "\\App\\");
                    System.Diagnostics.Process.Start(drive + "\\App\\" + sc + "\\" + sc1 + ".exe");
                    string DesktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);//得到桌面文件夹
                    IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();



                    IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(DesktopPath + "\\" + sc1 + ".lnk");
                    shortcut.TargetPath = drive + "\\App\\" + sc + "\\" + sc1 + ".exe";
                    shortcut.Arguments = "";// 参数
                    shortcut.Description = "Applications";
                    shortcut.WorkingDirectory = drive + "\\App\\" + sc;//程序所在文件夹，在快捷方式图标点击右键可以看到此属性
                    shortcut.IconLocation = drive + "\\App\\" + sc + "\\" + sc1 + ".exe" + ",0";//图标
                    shortcut.Hotkey = "";//热键
                    shortcut.WindowStyle = 1;
                    shortcut.Save();

                    shortcut.Save();//保存快捷方式
                    MessageBox.Show("安装成功", "Done 完成");
                }
                else
                {
                    Directory.CreateDirectory(drive + @"\\App");
                }
                li = 0;
            }
            else
            {

            }
        }
    }
}
