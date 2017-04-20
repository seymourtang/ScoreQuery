using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        const string LoginUrl = "http://202.120.108.14/ecustedu/K_StudentQuery/K_Default.aspx";
        const string StudentLoginPostUrl = "http://202.120.108.14/ecustedu/K_StudentQuery/K_StudentQueryLeft.aspx";
        private void TestConnetion()
        {

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
    }
}
