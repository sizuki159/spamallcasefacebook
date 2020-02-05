using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;

namespace CaseFacebook
{
    public partial class Form1 : Form
    {
        string path_img = String.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numericUpDown_numberspam.Value = 1;
            numericUpDown_delay.Value = 10;   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            path_img = ChooseFile();
            if (path_img == String.Empty)
            {
                MessageBox.Show("Không Tìm Thấy File Ảnh");
                this.Close();
            }
            else
            {
                ThreadStart start_play = new ThreadStart(start);
                Thread Begin = new Thread(start_play);
                Begin.Start();
            }
        }

        void start()
        {
            for (int i = 1; i <= Convert.ToInt32(numericUpDown_numberspam.Value); i++)
            {
                UploadFile(path_img);
                Thread.Sleep(Convert.ToInt32(numericUpDown_delay.Value) * 1000);
            }
            MessageBox.Show("Done!");
        }

        string UploadData(HttpRequest http, string url, MultipartContent data = null, string contentType = null, string userArgent = null)
        {
            if (http == null)
            {
                http = new HttpRequest();
            }

            if (!string.IsNullOrEmpty(userArgent))
            {
                http.UserAgent = userArgent;
            }

            string html = http.Post(url, data).ToString();
            return html;
        }

        void UploadFile(string path)
        {
            MultipartContent data = new MultipartContent() {
                { new FileContent(path), "attach", Path.GetFileName(path)}
            };

            UploadData(null, "https://www.facebook.com/support/support_case/do_reply/?eid=" + textBox1.Text, data, null, "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:72.0) Gecko/20100101 Firefox/72.0");
        }

        string ChooseFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            return String.Empty;
        }
    }
}
