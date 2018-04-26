using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot_Application1.Models
{
    [Serializable]
    public class QuestionsTieBreakFactory
    {
        private QuestionsTieBreakFactory() { }
        public static QuestionsTieBreakFactory Instance { get; } = new QuestionsTieBreakFactory();

        public static List<Question> GetQuestions()
        {
            var result = new List<Question>
            {
                new Question
                {
                    Number = 1,
                    Text = "1. El primer paso es...",
                    Options = new List<Option>
                    {
                        new Option {Text = "Diseño", OptionLetter = "a", Correct = false},
                        new Option {Text = "Puesta en producción", OptionLetter = "b",  Correct = false},
                        new Option {Text = "Análisis de requerimientos", OptionLetter = "c",  Correct = true},
                        new Option {Text = "Testing", OptionLetter = "d",  Correct = false},
                        new Option {Text = "Desarrollo", OptionLetter = "e",  Correct = false}
                    }
                },
                new Question
                {
                    Number = 2,
                    Text = "2. Luego, del análisis de requeriminetos seguimos con...",
                    Options = new List<Option>
                    {
                        new Option {Text = "Diseño", OptionLetter = "a", Correct = true},
                        new Option {Text = "Puesta en producción", OptionLetter = "b",  Correct = false},
                        new Option {Text = "Testing", OptionLetter = "c",  Correct = false},
                        new Option {Text = "Desarrollo", OptionLetter = "d",  Correct = false}
                    }
                },
                new Question
                {
                    Number = 3,
                    Text = "3. Ya tenemos el análisis de los requerimientos y el diseño, comenzamos con...",
                    Options = new List<Option>
                    {
                        new Option {Text = "Puesta en producción", OptionLetter = "a",  Correct = false},
                        new Option {Text = "Testing", OptionLetter = "b",  Correct = false},
                        new Option {Text = "Desarrollo", OptionLetter = "c",  Correct = true}
                    }
                },
                new Question
                {
                    Number = 4,
                    Text = "4. Luego del desarrollo tenemos...",
                    Options = new List<Option>
                    {
                        new Option {Text = "Puesta en producción", OptionLetter = "a",  Correct = false},
                        new Option {Text = "Testing", OptionLetter = "b",  Correct = true}
                    }
                },
                new Question
                {
                    Number = 5,
                    Text = "5. Ya casi esta pronto el software! Ahora debemos...",
                    Options = new List<Option>
                    {
                        new Option {Text = "Puesta en producción", OptionLetter = "a",  Correct = true}
                    }
                }
            };

            return result;
        }
    }
}