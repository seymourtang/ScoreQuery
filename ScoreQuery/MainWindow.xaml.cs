using HtmlAgilityPack;
using Http;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ScoreQuery.Http;
using ScoreQuery.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ScoreQuery
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            BtnAbout.Click += (s0, e0) =>
            {
                Flyout.IsOpen = !Flyout.IsOpen;
            };
        }
        HttpHelper httpHelper = new HttpHelper();
        #region 获取成绩大表
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BigScoreBtn.Content = "获取中...";
            BigScoreBtn.IsEnabled = false;
            Thread t = new Thread(GetBigScore)
            {
                IsBackground = true
            };
            t.Start();
        }

        private void GetBigScore()
        {
            try
            {
                List<BigScore> bigScore = new List<BigScore>();
                var result = httpHelper.HttpGet(ServiceURL.BigScoreUrl);
                string rowPath = "//*[@id=\"Table1\"]";
                HtmlDocument doc = new HtmlDocument()
                {
                    OptionReadEncoding = false
                };
                doc.LoadHtml(result);
                HtmlNode rowAount = doc.DocumentNode.SelectSingleNode(rowPath);
                rowAount = HtmlNode.CreateNode(rowAount.OuterHtml);
                HtmlNodeCollection name = rowAount.SelectNodes("//table");
                var str = string.Empty;
                string Term = string.Empty;
                var ClassName = string.Empty;
                var ClassNature = string.Empty;
                var Platform = string.Empty;
                var Grade = string.Empty;
                var Credit = string.Empty;
                var GradePoint = string.Empty;
                var dt = name[0].SelectNodes("//tr//table//td");
                BigScore big = new BigScore();
                for (int i = 0; i < dt.Count; i++)
                {
                    if (i % 7 == 0)
                    {
                        Term = dt[i].InnerText.Trim();
                    }
                    else if (i % 7 == 1)
                    {
                        ClassName = dt[i].InnerText.Trim();
                    }
                    else if (i % 7 == 2)
                    {
                        ClassNature = dt[i].InnerText.Trim();
                    }
                    else if (i % 7 == 3)
                    {
                        Platform = dt[i].InnerText.Trim();
                    }
                    else if (i % 7 == 4)
                    {
                        Grade = dt[i].InnerText.Trim();
                    }
                    else if (i % 7 == 5)
                    {
                        Credit = dt[i].InnerText.Trim();
                    }
                    else if (i % 7 == 6)
                    {
                        GradePoint = dt[i].InnerText.Trim();
                        bigScore.Add(new BigScore
                        {
                            Term = Term,
                            ClassName = ClassName,
                            ClassNature = ClassNature,
                            Credit = Credit,
                            Grade = Grade,
                            Platform = Platform,
                            GradePoint = GradePoint
                        });
                    }

                }
                bigScore.RemoveAt(0);
                this.Invoke(new Action(() =>
                {
                    lv.ItemsSource = bigScore;
                    BigScoreBtn.Content = "获取";
                    BigScoreBtn.IsEnabled = true;
                }));

            }
            catch (Exception ex)
            {
                this.Invoke(new Action(async () =>
                {
                    await this.ShowMessageAsync("错误", ex.ToString());
                    BigScoreBtn.Content = "获取";
                    BigScoreBtn.IsEnabled = true;
                }));
            }
        }
#endregion
        #region 获取指定学期考试表
        private void PreGetYear()
        {
            var result = httpHelper.HttpGet(ServiceURL.TestDetailUrl);
            string rowPath = "//*[@id=\"ddlYearTerm\"]";
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);
            HtmlNode rowAount = doc.DocumentNode.SelectSingleNode(rowPath);
            List<string> ls = new List<string>();
            if (rowAount != null)
            {
                rowAount = HtmlNode.CreateNode(rowAount.OuterHtml);
                HtmlNodeCollection content = rowAount.SelectNodes("//option");
                foreach (var item in content)
                {
                    ls.Add(item.Attributes["value"].Value);
                }
            }
            this.Invoke(new Action(() =>
            {
                cbYear.ItemsSource = ls;
            }));
        }
        private void GetTestTabDetail()
        {
            try
            {
                var yearTerm = string.Empty;
                this.Invoke(new Action( () =>
                {             
                      yearTerm = cbYear.Text;
                }));
                var result = httpHelper.HttpPost(ServiceURL.TestDetailUrl, Properties.Resources.TestPostData_Header + "&ddlYearTerm=" + yearTerm + "&" + Properties.Resources.TestPostData_Footer);
                string rowPath = "//*[@id=\"Table1\"]";
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(result);
                HtmlNode rowAount = doc.DocumentNode.SelectSingleNode(rowPath);
                List<TestTable> ls = new List<TestTable>();
                var ClassName = string.Empty;
                var TeachingClass = string.Empty;
                var Classroom = string.Empty;
                var Time = string.Empty;
                var TestNature = string.Empty;
                var Teacher = string.Empty;
                rowAount = HtmlNode.CreateNode(rowAount.OuterHtml);
                HtmlNodeCollection content = rowAount.SelectNodes("//tr[5]//td[1]//table[1]//tr//td");
                for (int i = 0; i < content.Count; i++)
                {
                    if (i % 6 == 0)
                    {
                        ClassName = content[i].InnerText.Trim();
                    }
                    else if (i % 6 == 1)
                    {
                        TeachingClass = content[i].InnerText.Trim();
                    }
                    else if (i % 6 == 2)
                    {
                        Classroom = content[i].InnerText.Trim();
                    }
                    else if (i % 6 == 3)
                    {
                        Time = content[i].InnerText.Trim();
                    }
                    else if (i % 6 == 4)
                    {
                        TestNature = content[i].InnerText.Trim();
                    }
                    else if (i % 6 == 5)
                    {
                        Teacher = content[i].InnerText.Trim();
                        ls.Add(new TestTable
                        {
                            Classroom = Classroom,
                            TeachingClass = TeachingClass,
                            ClassName = ClassName,
                            Time = Time,
                            TestNature = TestNature,
                            Teacher = Teacher
                        });
                    }
                }
                ls.RemoveAt(0);
                Dispatcher.Invoke(new Action(() =>
                {
                    lvTest.ItemsSource = ls;
                    btnTest.Content = "获取";
                    btnTest.IsEnabled = true;
                }));

            }
            catch (Exception)
            {
                this.Invoke(new Action(async () =>
                {
                    await this.ShowMessageAsync("错误", "学期未选择或学期错误");
                    btnTest.Content = "获取";
                    btnTest.IsEnabled = true;
                }));
            }
        }
        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            btnTest.Content = "获取中...";
            btnTest.IsEnabled = false;
            Thread t = new Thread(GetTestTabDetail)
            {
                IsBackground = true
            };
            t.Start();
        }
        #endregion
        #region 获取指定学期成绩
        private void PreGetTermYear()
        {
            try
            {
                var result = httpHelper.HttpGet(ServiceURL.TermScoreUrl);
                string rowPath = "//*[@id=\"ddlYearTerm\"]";
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(result);
                HtmlNode rowAount = doc.DocumentNode.SelectSingleNode(rowPath);
                List<string> ls = new List<string>();
                rowAount = HtmlNode.CreateNode(rowAount.OuterHtml);
                HtmlNodeCollection content = rowAount.SelectNodes("//option");
                foreach (var item in content)
                {
                    ls.Add(item.Attributes["value"].Value);
                }
                ls.RemoveAt(0);

                this.Invoke(new Action(() =>
                {
                    cbTerm.ItemsSource = ls;
                    
                }));
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(async () =>
                {
                    await this.ShowMessageAsync("错误", ex.ToString());
                }));
            }
        }

        private void GetTermScore()
        {
            try
            {
                var yearTerm = string.Empty;
                this.Invoke(new Action( () =>
                {                                                                                    
                        yearTerm = cbTerm.Text;                    
                }));
                var result = httpHelper.HttpPost(ServiceURL.TermScoreUrl, Properties.Resources.TermPostData_Header + "&ddlYearTerm=" + yearTerm + Properties.Resources.TermPostData_Footer);
                string rowPath = "//*[@id=\"Table1\"]";
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(result);
                HtmlNode rowAount = doc.DocumentNode.SelectSingleNode(rowPath);
                List<TermScore> ls = new List<TermScore>();
                var ClassName = string.Empty;
                var PlatformClass = string.Empty;
                var NatureofReading = string.Empty;
                var ClassNature = string.Empty;
                var DailyScore = string.Empty;
                var ExaminationScore = string.Empty;
                var FinalScore = string.Empty;
                var Credit = string.Empty;
                var GradePoint = string.Empty;
                rowAount = HtmlNode.CreateNode(rowAount.OuterHtml);
                HtmlNodeCollection content = rowAount.SelectNodes("//tr//td//tr//td");
                for (int i = 0; i < content.Count; i++)
                {
                    if (i % 11 == 0)
                    {
                        ClassName = content[i].InnerText.Trim();
                    }
                    else if (i % 11 == 1)
                    {
                        PlatformClass = content[i].InnerText.Trim();
                    }
                    else if (i % 11 == 4)
                    {
                        NatureofReading = content[i].InnerText.Trim();
                    }
                    else if (i % 11 == 5)
                    {
                        ClassNature = content[i].InnerText.Trim();
                    }
                    else if (i % 11 == 6)
                    {
                        DailyScore = content[i].InnerText.Trim();
                    }
                    else if (i % 11 == 7)
                    {
                        ExaminationScore = content[i].InnerText.Trim();
                    }
                    else if (i % 11 == 8)
                    {
                        FinalScore = content[i].InnerText.Trim();
                    }
                    else if (i % 11 == 9)
                    {
                        Credit = content[i].InnerText.Trim();
                    }
                    else if (i % 11 == 10)
                    {
                        GradePoint = content[i].InnerText.Trim();
                        ls.Add(new TermScore
                        {
                            ClassName = ClassName,
                            PlatformClass = PlatformClass,
                            NatureofReading = NatureofReading,
                            ClassNature = ClassNature,
                            DailyScore = DailyScore,
                            ExaminationScore = ExaminationScore,
                            FinalScore = FinalScore,
                            Credit = Credit,
                            GradePoint = GradePoint
                        });
                    }
                }
                ls.RemoveAt(0);
                Dispatcher.Invoke(new Action(() =>
                {
                    lvTerm.ItemsSource = ls;
                    btnTerm.Content = "获取";
                    btnTerm.IsEnabled = true;
                }));
            }
            catch (Exception)
            {
                this.Invoke(new Action(async () =>
                {
                    await this.ShowMessageAsync("错误", "学期未选择");
                    btnTerm.Content = "获取";
                    btnTerm.IsEnabled = true;
                }));
            }
        }
        private void btnTerm_Click(object sender, RoutedEventArgs e)
        {
            btnTerm.Content = "获取中...";
            btnTerm.IsEnabled = false;
            Thread t = new Thread(GetTermScore)
            {
                IsBackground = true
            };
            t.Start();
        }
        #endregion

        #region 选项卡的预获取信息
        private void tbControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tbControl.SelectedIndex == 1)
            {
                Thread t = new Thread(PreGetTermYear)
                {
                    IsBackground = true
                };
                t.Start();
            }
            else if (tbControl.SelectedIndex == 2)
            {
                Thread t = new Thread(PreGetYear)
                {
                    IsBackground = true
                };
                t.Start();
            }
        }
        #endregion

        private void EmailButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("mailto://tangxianmeng@live.com");
        }

        private void WeiboButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://weibo.com/tangxianmeng");
        }


        private void GitHubButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/seymourtang");
        }

        private void TwitterButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://twitter.com/Tangxianmeng");
        }
    }
}
