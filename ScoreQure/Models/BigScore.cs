using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreQuery.Models
{
    public class BigScore
    {
        public string Term { get; set; }//学期
        public string ClassName { get; set; }//课程名称
        public string ClassNature { get; set; }//课程性质
        public string Platform { get; set; }//平台类别
        public string Grade { get; set; }//成绩
        public string Credit { get; set; }//学分
        public string GradePoint { get; set; }//绩点
    }
}
