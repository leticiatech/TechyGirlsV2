using System;

namespace Bot_Application1.Models
{
    [Serializable]
    public class QuestionScore
    {
        public string Question { get; set; }
        public int Score { get; set; }
    }
}