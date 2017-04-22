using Http;
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
            TestConnetion();
        }
        HttpHelper httpHelper = new HttpHelper();
        const string LoginUrl = "http://202.120.108.14/ecustedu/K_StudentQuery/K_Default.aspx";
        const string StudentLoginPostUrl = "http://202.120.108.14/ecustedu/K_StudentQuery/K_StudentQueryLeft.aspx";
        private string TestConnetion()
        {
            return Properties.Resources.__EVENTTARGET;
        }

        private void Get__EVENTTARGET()
        {

        }

        private void Get__EVENTARGUMENT()
        {

        }
        private void Get__VIEWSTATE()
        {

        }

        private void Get__EVENTVALIDATION()
        {

        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
