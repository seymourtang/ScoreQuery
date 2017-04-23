using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreQuery.Models
{
    public class TestTable
    {
        public string ClassName { get; set; }//课程名称
        public string TeachingClass { get; set; }//教学班级
        public string Classroom { get; set; }//考试教室
        public string Time { get; set; }//考试事件
        public string TestNature { get; set; }//考试性质
        public string Teacher { get; set; }//主考老师
    }
}
