using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertFrm
{
    public partial class Form1 : Form
    {
        private static IDictionary<string, string> dic = null;
        private static GooleTranslator gooleTranslator = new GooleTranslator();
        public Form1()
        {
            InitializeComponent();
        }

        private async void btn_choiceExcel_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel(*.xlsx)|*.xlsx|Excel(*.xls)|*.xls";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    WriteLog("正在加载excel...");
                    string excelPath = openFileDialog.FileName;
                    lb_excelPath.Text = excelPath;
                    await Task.Run(() =>
                     {
                         dic = ExcelHelper.ReadExcelToDataTable(excelPath);
                     });
                }
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
            WriteLog("excel加载完成");
        }

        private void btn_output_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                lb_outputDir.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private async void btn_replace_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lb_outputDir.Text))
                {
                    MessageBox.Show("请选择输出目录");
                    return;
                }
                if (!File.Exists(lb_filepath.Text))
                {
                    MessageBox.Show("文件不存在");
                    return;
                }
                var filename = Path.GetFileName(lb_filepath.Text);
                var path = Path.Combine(lb_outputDir.Text, filename);
                if (File.Exists(path)) {
                    MessageBox.Show("输出目录已存在翻译后的文件,请转移或删除后再进行操作!");
                    return;
                }

                ClearLog();
                var fileText = await GetFileText(lb_filepath.Text);
                ReplaceAndWriteFile(fileText);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_choiceFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "aspx(*.aspx)|*.aspx|全部文件(*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filepath = openFileDialog.FileName;
                lb_filepath.Text = filepath;
            }
        }

        #region Log
        private void ClearLog()
        {
            if (txt_log.InvokeRequired)
            {
                Action action = () =>
                {
                    txt_log.Clear();
                };
                txt_log.Invoke(action);
            }
            else
            {
                txt_log.Clear();
            }
        }

        private void WriteLog(string txt)
        {
            if (txt_log.InvokeRequired)
            {
                Action<string> action = p =>
                {
                    txt_log.AppendText(txt + Environment.NewLine);
                };
                txt_log.Invoke(action, txt);
            }
            else
            {
                txt_log.AppendText(txt + Environment.NewLine);
            }
        }
        #endregion

        #region File
        private async void WriteFileAsync(string fileText)
        {
            try
            {
                var filename = Path.GetFileName(lb_filepath.Text);
                var path = Path.Combine(lb_outputDir.Text, filename);
                using (FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        await sw.WriteAsync(fileText);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private async Task<string> GetFileText(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    return await sr.ReadToEndAsync();
                }
            }
        }
        #endregion

        #region Translate
        private async void ReplaceAndWriteFile(string fileText)
        {
            WriteLog("正在读取文件...");
            var sb = new StringBuilder(fileText);
            WriteLog("正在查找中文字符...");
            //[\u4e00-\u9fa5]
            Regex reg = new Regex(@"([\u4e00-\u9fa5]|\（|\）)+");
            var maches = reg.Matches(fileText);
            int count = maches.Count;
            int current = 1;
            if (count < 1)
            {
                WriteLog("没有检测到中文,翻译结束");
                return;
            }
            WriteLog("正在翻译...");
            foreach (Match v in maches)
            {
                var @new = await Translate(v.Value);
                sb.Replace(v.Value, @new);
                WriteLog($"当前进度:{current}/{count}");
                current++;
            }
            WriteFileAsync(sb.ToString());
            WriteLog("翻译成功");
        }

        private async Task<string> Translate(string str)
        {
            str = str.Trim();
            string result = "";
            if (dic != null)
            {
                if (dic.ContainsKey(str))
                {
                    if (!string.IsNullOrEmpty(dic[str]))
                    {
                        result = dic[str].ToFirstUpper();
                        WriteLog($"{str}-{result}{Environment.NewLine}from excel");
                        return result;
                    }
                }
            }
            result = (await gooleTranslator.GoogleTranslate(str, "auto", "en")).ToFirstUpper();
            WriteLog($"{str}-{result}{Environment.NewLine}from google");
            return result;
        }
        #endregion
    }
}
