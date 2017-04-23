using HtmlAgilityPack;
using Http;
using MahApps.Metro.Controls;
using ScoreQuery.Models;
using System;
using System.Collections.Generic;
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
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1
    {
        public Window1()
        {
            InitializeComponent();
            BtnAbout.Click += (s0, e0) =>
            {
                Flyout.IsOpen = !Flyout.IsOpen;
            };
        }
        HttpHelper httpHelper = new HttpHelper();
        const string BigScoreUrl = "http://202.120.108.14/ecustedu/K_StudentQuery/K_BigScoreTableDetail.aspx?key=0";
        const string TestDetailUrl = "http://202.120.108.14/ecustedu/K_StudentQuery/K_TestTableDetail.aspx?key=0";
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Thread t = new Thread(GetBigScore)
            {
                IsBackground = true
            };
            t.Start();
        }

        private void GetBigScore()
        {
            List<BigScore> bigScore = new List<BigScore>();
            var result = httpHelper.HttpGet(BigScoreUrl);
            string rowPath = "//*[@id=\"Table1\"]";
            HtmlDocument doc = new HtmlDocument()
            {
                OptionReadEncoding = false
            };
            doc.LoadHtml(result);
            HtmlNode rowAount = doc.DocumentNode.SelectSingleNode(rowPath);
            if (rowAount != null)
            {
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
                        bigScore.Add(new BigScore {
                            Term = Term, ClassName = ClassName, ClassNature = ClassNature, Credit = Credit, Grade = Grade, Platform = Platform, GradePoint = GradePoint
                        });
                    }

                }
                bigScore.RemoveAt(0);
                this.Invoke(new Action(() =>
                {
                    lv.ItemsSource = bigScore;

                }));
            }
            else
            {
                Console.WriteLine("NULL");
            }
        }

        private void PreGetYear()
        {
            var result = httpHelper.HttpGet(TestDetailUrl);
            string rowPath = "//*[@id=\"ddlYearTerm\"]";
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);
            HtmlNode rowAount = doc.DocumentNode.SelectSingleNode(rowPath);
            List<string> ls = new List<string>();
            if (rowAount != null)
            {//*[@id="ddlYearTerm"]/option[2]
                rowAount = HtmlNode.CreateNode(rowAount.OuterHtml);
                HtmlNodeCollection content = rowAount.SelectNodes("//option");
                foreach (var item in content)              
                {
                    ls.Add(item.Attributes["value"].Value);
                }
            }
            this.Invoke(new Action(()=> {
                cbYear.ItemsSource = ls;
            }));
        }
        private void GetTestTabDetail()
        {
            try
            {

                var yearTerm = string.Empty;
                this.Invoke(new Action(() =>
                {
                    yearTerm = cbYear.Text;
                }));
                var result = httpHelper.HttpPost(TestDetailUrl, Properties.Resources.TestPostData_Header + "&ddlYearTerm=" + yearTerm + "&" + Properties.Resources.TestPostData_Footer);
                string rowPath = "//*[@id=\"Table1\"]";
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(result);
                HtmlNode rowAount = doc.DocumentNode.SelectSingleNode(rowPath);
                List<TestTable> ls = new List<TestTable>();
                if (rowAount != null)
                {
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
                    }));
                }
                else
                {
                    return;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(GetTestTabDetail);
            t.IsBackground = true;
            t.Start();
        }

        private void tbControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tbControl.SelectedIndex==2)
            {
                Thread t = new Thread(PreGetYear);
                t.IsBackground = true;
                t.Start();
            }
        }
    }
}
