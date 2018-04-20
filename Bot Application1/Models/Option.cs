using System;

namespace Bot_Application1.Models
{
    [Serializable]
    public class Option
    {
        public string Text { get; set; }
        public string OptionLetter { get; set; }
        public bool Correct { get; set; }
    }
}