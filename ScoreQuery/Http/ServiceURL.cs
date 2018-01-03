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
        /// API 域名，统一配置
        /// </summary>
        public static string API_HOST = "http://inquiry.ecust.edu.cn";
        /// <summary>
        /// 普通登录：学号+密码
        /// </summary>
        public static string StudentLoginPostUrl = API_HOST+"/ecustedu/K_StudentQuery/K_StudentQueryLogin.aspx";
        /// <summary>
        /// 家长登录
        /// </summary>
        public static string ParentsLoginPostUrl = API_HOST+"/ecustedu/K_StudentQuery/K_PatriarchQueryLogin.aspx";
        /// <summary>
        /// 成绩大表GET
        /// </summary>
        public static string BigScoreUrl = API_HOST + "/ecustedu/K_StudentQuery/K_BigScoreTableDetail.aspx";
        /// <summary>
        /// 考试表POST
        /// </summary>
        public static string TestDetailUrl = API_HOST + "/ecustedu/K_StudentQuery/K_TestTableDetail.aspx";
        /// <summary>
        /// 学期成绩查询POST
        /// </summary>
        public static string TermScoreUrl = API_HOST+"/ecustedu/K_StudentQuery/K_ScoreTableYearTerm.aspx ";
    }
}
