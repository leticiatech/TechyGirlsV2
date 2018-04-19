
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bot_Application1.Models
{
    [Serializable]
    public class Question
    {
        public string Text { get; set; }
        public List<Option> Options { get; set; }

        public Option GetCorrectAnswer()
        {
            return Options.FirstOrDefault(a => a.Correct);
        }
    }
}