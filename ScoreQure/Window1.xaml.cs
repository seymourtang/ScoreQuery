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
        }
        HttpHelper httpHelper=new HttpHelper();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(GetBigScore);
            t.IsBackground = true;
            t.Start();
        }

        private void GetBigScore()
        {
            List<BigScore> bigScore = new List<BigScore>();
            var result = httpHelper.HttpGet("http://202.120.108.14/ecustedu/K_StudentQuery/K_BigScoreTableDetail.aspx?key=0");
            string rowPath= "//*[@id=\"Table1\"]";
            HtmlDocument doc = new HtmlDocument()
            {
                OptionReadEncoding = false
            };
            doc.LoadHtml(result);
            HtmlNode rowAount = doc.DocumentNode.SelectSingleNode(rowPath);
            if (rowAount!=null)
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
                    if (i%7==0)
                    {
                        Term = dt[i].InnerText.Trim();
                    }
                    else if(i%7==1)
                    {
                       ClassName = dt[i].InnerText.Trim();
                    }
                    else if (i%7==2)
                    {
                        ClassNature= dt[i].InnerText.Trim();
                    }
                    else if(i%7==3)
                    {
                        Platform = dt[i].InnerText.Trim();
                    }
                    else if(i%7==4)
                    {
                        Grade= dt[i].InnerText.Trim(); ;
                    }
                    else if(i%7==5)
                    {
                        Credit= dt[i].InnerText.Trim();
                    }
                    else if(i%7==6)
                    {
                        GradePoint= dt[i].InnerText.Trim();
                        bigScore.Add(new BigScore { Term=Term,ClassName=ClassName,ClassNature=ClassNature,Credit=Credit,Grade=Grade,Platform=Platform,GradePoint=GradePoint});
                    }
 
                }
                bigScore.RemoveAt(0);
                this.BeginInvoke(new Action(() => {
                    lv.ItemsSource = bigScore;
                  
                }));
            }
            else
            {
                Console.WriteLine("NULL");
            }
        }
    }
}
