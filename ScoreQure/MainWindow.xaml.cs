using Http;
using MahApps.Metro.Controls;
using System;
using System.Configuration;
using System.Threading;
using System.Windows;
namespace ScoreQuery
{
    /// <summary>
    /// Interaction logic for  MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        HttpHelper httpHelper = new HttpHelper();
        const string LoginUrl = "http://202.120.108.14/ecustedu/K_StudentQuery/K_Default.aspx";
        const string StudentLoginPostUrl = "http://202.120.108.14/ecustedu/K_StudentQuery/K_StudentQueryLogin.aspx";
        static string __EVENTTARGET = Properties.Resources.__EVENTTARGET;
        static string __EVENTARGUMENT = Properties.Resources.__EVENTARGUMENT;
        static string __VIEWSTATE = Properties.Resources.__VIEWSTATE;
        static string BtnLogin = "登录";
        static string __EVENTVALIDATION = Properties.Resources.__EVENTVALIDATION;
        /// <summary>
        /// 读取客户设置
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public static string GetSettingString(string settingName)
        {
            try
            {
                string settingString = ConfigurationManager.AppSettings[settingName].ToString();
                return settingString;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 更新客户设置
        /// </summary>
        /// <param name="settingName"></param>
        /// <param name="valueName"></param>
        public static void UpdateSettingString(string settingName, string valueName)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (ConfigurationManager.AppSettings[settingName] != null)
            {
                config.AppSettings.Settings.Remove(settingName);
            }
            config.AppSettings.Settings.Add(settingName, valueName);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            btLogin.Content = "正在登录";
            Thread t = new Thread(DoLogin)
            {
                IsBackground = true
            };
            t.Start();
            btLogin.Content = "登录";
        }

        private void DoLogin()
        {
            string postData = string.Empty;
            this.Invoke(new Action(()=> {             
                postData= string.Format("__EVENTTARGET={0}&__EVENTARGUMENT={1}&__VIEWSTATE={2}&TxtStudentId={3}&TxtPassword={4}&BtnLogin={5}&__EVENTVALIDATION={6}", __EVENTTARGET, __EVENTARGUMENT, __VIEWSTATE, tbStudentNo.Text, tbPassword.Password, BtnLogin, __EVENTVALIDATION);
            }));         
            var result = httpHelper.HttpPost(StudentLoginPostUrl,postData);
            if (result.Contains("您好"))
            {
                this.Invoke(new Action(()=> {
                    Window1 page = new Window1();
                    Application.Current.MainWindow = page;
                    this.Close();
                    page.Show();
                }));
                
            }
            else
            {
                MessageBox.Show("登录失败");
            }
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tbStudentNo.Text = GetSettingString("tbStudentNo");
            tbPassword.Password = GetSettingString("tbPassword");
        }
    }
}
