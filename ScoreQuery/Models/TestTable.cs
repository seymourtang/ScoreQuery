using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreQuery.Models
{
    public class TestTable
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 教学班级
        /// </summary>
        public string TeachingClass { get; set; }
        /// <summary>
        /// 考试教室
        /// </summary>
        public string Classroom { get; set; }
        /// <summary>
        /// 考试时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 考试性质
        /// </summary>
        public string TestNature { get; set; }
        /// <summary>
        /// 主考老师
        /// </summary>
        public string Teacher { get; set; }
    }
}
