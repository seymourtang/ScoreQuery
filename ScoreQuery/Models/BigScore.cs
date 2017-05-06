using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreQuery.Models
{
    public class BigScore
    {
        /// <summary>
        /// 学期
        /// </summary>
        public string Term { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 课程性质
        /// </summary>
        public string ClassNature { get; set; }
        /// <summary>
        /// 平台类别
        /// </summary>
        public string Platform { get; set; }
        /// <summary>
        /// 成绩
        /// </summary>
        public string Grade { get; set; }
        /// <summary>
        /// 学分
        /// </summary>
        public string Credit { get; set; }
        /// <summary>
        /// 绩点
        /// </summary>
        public string GradePoint { get; set; }
    }
}
