using System;
using System.Collections.Generic;

namespace Bot_Application1.Models
{
    [Serializable]
    public class Group
    {
        public string Name { get; set; }
        public int TotalScore { get; set; }
        public List<QuestionScore> QuestionScores { get; set; }

        public Group()
        {
            Name = "";
            TotalScore = 0;
            QuestionScores = new List<QuestionScore>();
        }
    }
}