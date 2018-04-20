using System;
using System.Collections.Generic;

namespace Bot_Application1.Models
{
    [Serializable]
    public sealed class QuestionsFactory
    {
        private QuestionsFactory() { }
        public static QuestionsFactory Instance { get; } = new QuestionsFactory();

        public static List<Question> GetQuestions()
        {
            var result = new List<Question>
            {
                new Question
                {
                    Number = 1,
                    Text = "1. ¿Quién es la chica de HR?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Leticia", OptionLetter = "a", Correct = false},
                        new Option {Text = "Mariasol", OptionLetter = "b",  Correct = true},
                        new Option {Text = "Mavi", OptionLetter = "c",  Correct = true}
                    }
                },
                new Question
                {
                    Number = 2,
                    Text = "2. ¿Cómo es el nombre de la empresa?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Onetree", OptionLetter = "a", Correct = true},
                        new Option {Text = "Altimetrik", OptionLetter = "b", Correct = false},
                        new Option {Text = "Takeoff", OptionLetter = "c", Correct = false}
                    }
                },
                new Question
                {
                    Number = 3,
                    Text = "3. ¿Cómo es el nombre de la empresa?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Onetree", OptionLetter = "a", Correct = true},
                        new Option {Text = "Altimetrik", OptionLetter = "b", Correct = false},
                        new Option {Text = "Takeoff", OptionLetter = "c", Correct = false}
                    }
                },
                new Question
                {
                    Number = 4,
                    Text = "4. ¿Cómo es el nombre de la empresa?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Onetree", OptionLetter = "a", Correct = true},
                        new Option {Text = "Altimetrik", OptionLetter = "b", Correct = false},
                        new Option {Text = "Takeoff", OptionLetter = "c", Correct = false}
                    }
                },
                new Question
                {
                    Number = 5,
                    Text = "5. ¿Cómo es el nombre de la empresa?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Onetree", OptionLetter = "a", Correct = true},
                        new Option {Text = "Altimetrik", OptionLetter = "b", Correct = false},
                        new Option {Text = "Takeoff", OptionLetter = "c", Correct = false}
                    }
                },
                new Question
                {
                    Number = 6,
                    Text = "6. ¿Cómo es el nombre de la empresa?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Onetree", OptionLetter = "a", Correct = true},
                        new Option {Text = "Altimetrik", OptionLetter = "b", Correct = false},
                        new Option {Text = "Takeoff", OptionLetter = "c", Correct = false}
                    }
                },
                new Question
                {
                    Number = 7,
                    Text = "7. ¿Cómo es el nombre de la empresa?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Onetree", OptionLetter = "a", Correct = true},
                        new Option {Text = "Altimetrik", OptionLetter = "b", Correct = false},
                        new Option {Text = "Takeoff", OptionLetter = "c", Correct = false}
                    }
                }
            };

            return result;
        }
    }
}