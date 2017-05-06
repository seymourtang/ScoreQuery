using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreQuery.Http
{
    public static class ServiceURL
    {
        /// <summary>
        /// 普通登录：学号+密码
        /// </summary>
        public static string StudentLoginPostUrl = "http://202.120.108.14/ecustedu/K_StudentQuery/K_StudentQueryLogin.aspx";
        /// <summary>
        /// 成绩大表GET
        /// </summary>
        public static string BigScoreUrl = "http://202.120.108.14/ecustedu/K_StudentQuery/K_BigScoreTableDetail.aspx";
        /// <summary>
        /// 考试表POST
        /// </summary>
        public static string TestDetailUrl = "http://202.120.108.14/ecustedu/K_StudentQuery/K_TestTableDetail.aspx";
        /// <summary>
        /// 学期成绩查询POST
        /// </summary>
        public static string TermScoreUrl = "http://202.120.108.14/ecustedu/K_StudentQuery/K_ScoreTableYearTerm.aspx ";
    }
}
