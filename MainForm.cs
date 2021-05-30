using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using log4net;
using FaceRecognition.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace upload_files_winform
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            //允许异步写入
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }



        private void btn_update_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtBox_selected.Text))
            {
                MessageBox.Show("请选择上传文件夹");
                return;
            }
            btn_update.Enabled = false;
            
            
            

            //NameValueCollection nvc = new NameValueCollection();
            //nvc.Add("id", "TTR");
            //nvc.Add("btn-submit-photo", "Upload");

            DirectoryInfo dir = new DirectoryInfo(txtBox_selected.Text);
            int i = 0;
            foreach (FileInfo file in dir.GetFiles())
            {
                DateTime startTime = DateTime.Now;
                HttpUploadFile("http://10.60.7.88/marry/marry/rcMarry/electCert/rcMarryElectCertList.do?method=uploadFiles",
                txtBox_Jsession.Text,
                 file.FullName,
                file.Name,
                 "application/pdf",
                 null);
                DateTime endTime = DateTime.Now;
                txtBox_log.AppendText("用时:" + endTime.Subtract(startTime).TotalMilliseconds + "ms \r\n");
                i++;
            }


            txtBox_log.AppendText("所有文件传输完成, 共传输" + i + "个文件");
        }

        public void HttpUploadFile(string url, string jsessionid, string filePath, string fileName, string contentType, NameValueCollection nvc)
        {
            //ILog log = LogManager.GetLogger(typeof(form_main));

           // LogHelper.Debug(typeof(MainForm), string.Format("Uploading {0} to {1}", filePath, url));
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.CookieContainer = new CookieContainer();
            Uri target = new Uri(url);
            Cookie cookie = new Cookie("JSESSIONID", jsessionid) { Domain = target.Host };
            wr.CookieContainer.Add(cookie);
            //wr.Headers["HHCSRFToken"] = "dac102e1-b436-436e-afea-61cb821e64b3";
            

            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream rs = wr.GetRequestStream();

            if (nvc != null)
            {//加入k-v
                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                foreach (string key in nvc.Keys)
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                    //LogHelper.Debug(typeof(MainForm), "\r\n--" + boundary + "\r\n");
                    string formitem = string.Format(formdataTemplate, key, nvc[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                    //LogHelper.Debug(typeof(MainForm), formitem);
                }
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);
            //LogHelper.Debug(typeof(MainForm), "\r\n--" + boundary + "\r\n");

            //准备文件类型的http格式
            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, "file", fileName, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);
            //LogHelper.Debug(typeof(MainForm), header);

            //写入文件
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();
            //数据结束后的分界符
            //LogHelper.Debug(typeof(MainForm), "写文件");
            byte[] trailer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            //LogHelper.Debug(typeof(MainForm), "\r\n--" + boundary + "--\r\n");
            rs.Close();


            WebResponse wresp = null;
            try
            {
                
                //上传并得到应答
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);

                //string info = string.Format("File uploaded, server response is: {0}", reader2.ReadToEnd());
                string info = reader2.ReadToEnd();
                LogHelper.Debug(typeof(MainForm), info);

                //解析json
                JObject jo = (JObject)JsonConvert.DeserializeObject(info);
               
                if (jo["successList"]!= null && jo["successList"][0] != null &&  jo["successList"][0]["fileName"] != null)
                {
                    txtBox_log.AppendText(jo["successList"][0]["fileName"].ToString() + " 上传成功!   ");
                }
                else if (jo["failList"] != null && jo["failList"][0] != null && jo["failList"][0]["fileName"] != null) {
                    txtBox_log.AppendText(jo["failList"][0]["fileName"].ToString() + "-----上传失败!   ");
                }

                
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(MainForm), ex);
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }

        private void btn_selected_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog_selected.ShowDialog() == DialogResult.OK)
            {
                txtBox_selected.Text = folderBrowserDialog_selected.SelectedPath;
            }
        }
    }
}
