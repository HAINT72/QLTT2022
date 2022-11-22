using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace App
{
    public enum MessageType { Success, Information, Error, Confirmation }
    public class ScreenSize
    {
        public static int Width = Screen.PrimaryScreen.WorkingArea.Width;
        public static int Height = Screen.PrimaryScreen.WorkingArea.Height;
    }

    public class ShareVar : INotifyPropertyChanged
    {
        private static ShareVar instance;

        public static ShareVar Instance
        {
            get { if (instance == null) instance = new ShareVar(); return instance; }
            private set { instance = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _ID = string.Empty;
        public string ID
        {
            get { return _ID; }
            set { _ID = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID")); }
        }        
    }

    public class Functions
    {
        public static DialogResult MsgBox(string stLabel = "Hoàn thành công việc", MessageType enumMsgType = MessageType.Success, Action actYesNextEvent = null, Action actNoNextEvent = null)
        {
            string stCaption = "Qlhs2021 Thông báo";
            MessageBoxButtons button = MessageBoxButtons.OK; ;
            MessageBoxIcon icon = new MessageBoxIcon();

            switch (enumMsgType)
            {
                case MessageType.Success:
                    icon = MessageBoxIcon.Asterisk;
                    break;

                case MessageType.Error:
                    icon = MessageBoxIcon.Error;
                    break;

                case MessageType.Information:
                    icon = MessageBoxIcon.Information;
                    break;

                case MessageType.Confirmation:
                    button = MessageBoxButtons.YesNo;
                    icon = MessageBoxIcon.Question;
                    break;
            }

            DialogResult answer = MessageBox.Show(stLabel, stCaption, button, icon);
            if (enumMsgType == MessageType.Confirmation)
                if (answer == DialogResult.Yes)
                    actYesNextEvent?.Invoke();
                else
                    actNoNextEvent?.Invoke();
            return answer;
        }
    }

    public class Utilities
    {
        private static Utilities instance;

        public static Utilities Instance
        {
            get { if (instance == null) instance = new Utilities(); return instance; }
            private set { instance = value; }
        }
        
        public string stDirectoryOfFileAttch //Biến lưu thư mục temp lưu file đính kèm tại máy local
        {
            get { return Path.Combine(Application.StartupPath, DataProvider.Instance.stMSTS); }
        } 

        public string GetFullFileName(string extFileName = null)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (string.IsNullOrEmpty(extFileName))
                dlg.Filter = "|All file (*.*)|*.*";
            else
                dlg.Filter = extFileName + "|All file (*.*)|*.*";

            dlg.Multiselect = false;
            if (dlg.ShowDialog() == DialogResult.OK)
                return dlg.FileName;
            else
                return string.Empty;
        }

        public string GetFullFileExcelName(string stTitle = "Nhập file để xuất ra MS.Excel")
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                Title = stTitle,
                Filter = "Excel | *.xlsx | Excel 2003 | *.xls"
            };
            if (dlg.ShowDialog() == DialogResult.OK)
                return dlg.FileName;
            else
                return string.Empty;
        }

        public string GetFolderName()
        {
            FolderBrowserDialog fdlg = new FolderBrowserDialog();
            if (fdlg.ShowDialog() == DialogResult.OK)
                return fdlg.SelectedPath;
            else
                return string.Empty;
        }

        public string stToMD5(string stTmp)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(stTmp);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string stMD5 = "";

            foreach (byte item in hasData)
            {
                stMD5 += item;
            }
            return stMD5;
        }

        private string GetPathFile()
        {
            string stQuery = $"SELECT STR_PATH FROM tThongSo WHERE MSTS ='{DataProvider.Instance.stMSTS}'";
            object data = DataProvider.Instance.ExecuteScalar(stQuery);
            return data.ToString();
        }

        public string [,] ReadFileNameFromServerDirectory() //Đọc toàn bộ tên file trong thư mục PDF trên Server
        {
            string stPath = GetPathFile();
            string[] tmp = Directory.GetFiles(stPath);
            int iLength = tmp.Length;
            string[,] kq = new string[2,iLength];
            for (int i = 0; i < iLength; i++)
            {
                kq[0,i] = Path.GetFileName(tmp[i]);
                kq[1, i] = Path.GetFileNameWithoutExtension(tmp[i]);
            }
            return kq;
        }

        public void OpenFileAtch(string stFileName)
        {
            if (string.IsNullOrEmpty(stFileName)) return;

            if (!stFileName.Contains(@"\"))
            {                
                if (CopyFileFromServer(stFileName, stDirectoryOfFileAttch)) //Copy File trên Server vào thư mục temp trên máy local                    
                    stFileName = Path.Combine(stDirectoryOfFileAttch, stFileName); // Tạo đường dẫn file local 
                else
                    return;
            }

            if (!File.Exists(stFileName))
            {
                string stMsg = $"Không tìm thấy file {stFileName} trên server!";
                Functions.MsgBox(stMsg, MessageType.Error);
                return;
            }
            Process.Start(stFileName);
        }

        public bool CopyFileToServer(string stFullFileNameSource, string stFileNameDest) //Trả về true nếu Copy thành công và ngược lại
        {
            if (!stFullFileNameSource.Contains(@"\")) return true; //Trường hợp giữ nguyên file đã đính kèm
            string stDestPath = GetPathFile();
            string stFullFileNameDest = Path.Combine(stDestPath, stFileNameDest);
            if (!File.Exists(stFullFileNameSource))
            {
                string stMsg = $"Không tìm thấy file {stFullFileNameSource} !";
                Functions.MsgBox(stMsg, MessageType.Error);
                return false;
            }
            try
            {
                File.Copy(stFullFileNameSource, stFullFileNameDest, true);
            }
            catch
            {
                Functions.MsgBox("Không thể thực hiện do File đang được mở hoặc đã có lỗi");
                return false;
            }
            return true;
        }

        public bool CopyFileFromServer(string stFileName, string stFolderDest) //Copy file từ server về thư mục (giữ nguyên tên file server)
        {
            string stServerPath = GetPathFile();
            string stFullFileNameSource = Path.Combine(stServerPath, stFileName);
            string stFullFileNameDest = Path.Combine(stFolderDest, stFileName);
            if (!File.Exists(stFullFileNameSource))
            {
                string stMsg = $"Không tìm thấy file {stFullFileNameSource} !";
                Functions.MsgBox(stMsg, MessageType.Error);
                return false;
            }
            try
            {
                File.Copy(stFullFileNameSource, stFullFileNameDest, true);
            }
            catch 
            {
                Functions.MsgBox("Không thể thực hiện do File đang được mở hoặc đã có lỗi");
                return false;
            }
            return true;
        }

        public bool DeleteFileServer(string stFileName) //Trả về true nếu Delete thành công và ngược lại
        {
            string stServerPath = GetPathFile();
            string stFullFileName = Path.Combine(stServerPath, stFileName);
            if (!File.Exists(stFullFileName))
            {
                string stMsg = $"Không tìm thấy file {stFullFileName} !";
                Functions.MsgBox(stMsg, MessageType.Error);
                return false;
            }
            File.Delete(stFullFileName);
            return true;
        }

        public int GetIndexCombobox(ComboBox cb, string stSearch)
        {
            if (string.IsNullOrEmpty(stSearch)) return 0;
            DataTable data = (DataTable)cb.DataSource;
            int iIndex = 0;
            foreach (DataRow item in data.Rows)
            {
                if (item[0].ToString() == stSearch)
                    break;
                else
                    iIndex++;
            }
            return iIndex;
        }

        public bool IsLoadForm(string stFormName)
        {
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                if (frm.Name == stFormName)
                {
                    return true;
                }
            }
            return false;
        }

        public void SendMail(string stEmail, string stSubject, string stComment, string stFileName)
        {
            string usenameMail = ConfigurationManager.AppSettings["mailName"].ToString();
            string passwordMail = ConfigurationManager.AppSettings["passMail"].ToString();
            string smtpHost = ConfigurationManager.AppSettings["smtpHost"].ToString();
            int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(usenameMail);
            mail.To.Add(stEmail);
            mail.Body = string.IsNullOrEmpty(stComment) ? string.Empty : stComment;
            mail.Subject = stSubject;
            mail.IsBodyHtml = true;
            if (stFileName != null)
            {
                string stPath = GetPathFile();
                string stFullFiname = string.Empty;
                stFullFiname = Path.Combine(stPath, stFileName);
                mail.Attachments.Add(new Attachment(stFullFiname));
            }
            SmtpClient client = new SmtpClient(smtpHost, smtpPort); 
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(usenameMail, passwordMail);
            client.EnableSsl = true;
            client.Send(mail);
        }

        //public void ExportToExcel(DataTable table, string stFilePath, string stSheetName)
        //{
        //    if (table.Rows.Count <= 0)
        //    {
        //        string stMsg = "Không tìm thấy dữ liệu!";
        //        Functions.MsgBox(stMsg, MessageType.Information);
        //        return;
        //    }
        //    var fileInfo = new FileInfo(stFilePath);
        //    using (ExcelPackage pck = new ExcelPackage(fileInfo))
        //    {
        //        string stMSNV = ShareVar.Instance.NV.MSNV;
        //        pck.Workbook.Properties.Author = stMSNV;
        //        pck.Workbook.Properties.Company = "HPRC";
        //        pck.Workbook.Properties.Title = $"Exported by '{stMSNV}'";
        //        ExcelWorksheet ws = pck.Workbook.Worksheets.Add(stSheetName);
        //        ws.Cells["A1"].LoadFromDataTable(table, true);
        //        pck.Save();
        //    }
        //}
    }
}
