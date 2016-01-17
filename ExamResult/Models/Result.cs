using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamResult.Models
{
    public class Result
    {
        public Guid ResultId { get; set; }
        public string Participator { get; set; }
        public DateTime Time { get; set; }
        public string Exam { get; set; }

        public int NumberOfQuestion { get; set; }
        public int NumberOfRightAnswer { get; set; }
        public int NumberOfWrongAnswer { get; set; }
        public Result()
        {
            this.ResultId = Guid.NewGuid();
        }
    }
}