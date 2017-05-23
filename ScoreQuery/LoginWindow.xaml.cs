using Http;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ScoreQuery.Http;
using System;
using System.Configuration;
using System.Threading;
using System.Windows;
namespace ScoreQuery
{
    /// <summary>
    /// Interaction logic for  LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
            
        }
        HttpHelper httpHelper = new HttpHelper();    
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
        /// <summary>
        /// 以异步的方式登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            btLogin.Content = "正在登录";
            btLogin.IsEnabled = false;
             Thread t = new Thread(DoLogin)
            {
                IsBackground = true
            };
            t.Start();
            
        }
        /// <summary>
        /// 登录的具体方法
        /// </summary>
        private void DoLogin()
        {
            try
            {
                string postData = string.Empty;
                this.Invoke(new Action(() =>
                {
                    postData = string.Format("__EVENTTARGET={0}&__EVENTARGUMENT={1}&__VIEWSTATE={2}&TxtStudentId={3}&TxtPassword={4}&BtnLogin={5}&__EVENTVALIDATION={6}", __EVENTTARGET, __EVENTARGUMENT, __VIEWSTATE, tbStudentNo.Text, tbPassword.Password, BtnLogin, __EVENTVALIDATION);
                }));
                var result = httpHelper.HttpPost(ServiceURL.StudentLoginPostUrl, postData);
                if (result.Contains("您好"))
                {
                    this.Invoke(new Action(() =>
                    {
                        MainWindow page = new MainWindow();
                        Application.Current.MainWindow = page;
                        this.Close();
                        page.Show();
                    }));

                }
                else
                {
                    this.Invoke(new Action(async () =>
                    {
                        await this.ShowMessageAsync("登录失败", "请检查账号密码!");
                        btLogin.Content = "登录";
                        btLogin.IsEnabled = true;
                    }));
                }
            }
            catch(Exception)
            {
                this.Invoke(new Action(async () =>
                {
                    await this.ShowMessageAsync("错误", "请检查网络连接!");
                    btLogin.Content = "登录";
                    btLogin.IsEnabled = true;
                }));
            }
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tbStudentNo.Text = GetSettingString("tbStudentNo");
            tbPassword.Password = GetSettingString("tbPassword");
        }
    }
}
