using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreQuery.Models
{
    public class TermScore
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 平台类别
        /// </summary>
        public string PlatformClass { get; set; }
        /// <summary>
        /// 修读性质
        /// </summary>
        public string NatureofReading { get; set; }
        /// <summary>
        /// 课程性质
        /// </summary>
        public string ClassNature { get; set; }
        /// <summary>
        /// 平时成绩
        /// </summary>
        public string DailyScore { get; set; }
        /// <summary>
        ///卷面成绩
        /// </summary>
        public string ExaminationScore { get; set; }
        /// <summary>
        /// 总成绩
        /// </summary>
        public string FinalScore { get; set; }
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
