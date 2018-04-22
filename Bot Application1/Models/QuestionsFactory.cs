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
                    Text = "1. ¿Cuál es el rol de una project manager?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Persona responsable de liderar a un equipo para alcanzar objetivos.", OptionLetter = "a", Correct = true},
                        new Option {Text = "Supervisor directo del equipo de desarrollo", OptionLetter = "b",  Correct = false},
                        new Option {Text = "No es responsable, solo dirige al equipo", OptionLetter = "c",  Correct = true}
                    }
                },
                new Question
                {
                    Number = 2,
                    Text = "2. ¿Qué marco de trabajo utilizamos para el proyecto?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Cascada", OptionLetter = "a", Correct = false},
                        new Option {Text = "Evolutivo/Incremental", OptionLetter = "b", Correct = false},
                        new Option {Text = "Scrum", OptionLetter = "c", Correct = true}
                    }
                },
                new Question
                {
                    Number = 3,
                    Text = "3. (BA)",
                    Options = new List<Option>
                    {
                        new Option {Text = "...", OptionLetter = "a", Correct = true},
                        new Option {Text = "...", OptionLetter = "b", Correct = false},
                        new Option {Text = "...", OptionLetter = "c", Correct = false}
                    }
                },
                new Question
                {
                    Number = 4,
                    Text = "4. (BA)",
                    Options = new List<Option>
                    {
                        new Option {Text = "...", OptionLetter = "a", Correct = true},
                        new Option {Text = "...", OptionLetter = "b", Correct = false},
                        new Option {Text = "...", OptionLetter = "c", Correct = false}
                    }
                },
                new Question
                {
                    Number = 5,
                    Text = "5. ¿Cuál es la tarea principal de un FrontEnd?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Programar la funcionalidades del proyecto", OptionLetter = "a", Correct = false},
                        new Option {Text = "Revisar que todo funcione bien", OptionLetter = "b", Correct = false},
                        new Option {Text = "Implementar la parte visible del proyecto basándose en el diseño", OptionLetter = "c", Correct = true}
                    }
                },
                new Question
                {
                    Number = 6,
                    Text = "6. ¿De quién depende un desarrollador FrontEnd para poder desarrollar bien su trabajo?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Del Project Manager", OptionLetter = "a", Correct = false},
                        new Option {Text = "Del desarrollador Backend y del Diseñador", OptionLetter = "b", Correct = true},
                        new Option {Text = "Del equipo de QA", OptionLetter = "c", Correct = false}
                    }
                },
                new Question
                {
                    Number = 7,
                    Text = "7. ¿Qué tareas realiza un desarrollador Backend?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Se encarga de crear la documentación de los requerimientos que deberá tener la aplicación", OptionLetter = "a", Correct = false},
                        new Option {Text = "Comprueba que la aplicación no tenga errores ni defectos y crea la documentación acorde", OptionLetter = "b", Correct = false},
                        new Option {Text = "Implementa la lógica de funcionamiento de una aplicación, así como el acceso y guardado de datos", OptionLetter = "c", Correct = true}
                    }
                },
                new Question
                {
                    Number = 8,
                    Text = "8. ¿Qué herramientas pueden ser utilizadas por un desarrollador BackEnd?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Adobe Illustrator y Photoshop", OptionLetter = "a", Correct = false},
                        new Option {Text = "Microsoft Visual Studio, SQL Server", OptionLetter = "b", Correct = true},
                        new Option {Text = "Microsoft Word y PowerPoint", OptionLetter = "c", Correct = false}
                    }
                },
                new Question
                {
                    Number = 9,
                    Text = "9. ¿Por que es necesario incluir pruebas en el proceso de creación de un software?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Para complicar al equipo de Desarrollo", OptionLetter = "a", Correct = false},
                        new Option {Text = "Para identificar defectos/errores en el sistema, acortar costos, reducir tiempos y entregar un producto de buena calidad.", OptionLetter = "b", Correct = true},
                        new Option {Text = "Para cobrarle más dinero al cliente", OptionLetter = "c", Correct = false}
                    }
                },
                new Question
                {
                    Number = 10,
                    Text = "10. ¿Que hace el equipo de pruebas cuando identifica un error en el sistema?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Busca la manera de que el cliente no llegue a ejecutar esa parte, así no lo ve", OptionLetter = "a", Correct = false},
                        new Option {Text = "Registra el defecto en un Excel o una herramienta y no le informa al equipo de desarrollo", OptionLetter = "b", Correct = false},
                        new Option {Text = "Registra el defecto incluyendo evidencia del mismo y los pasos para reproducirlo. Se lo informa al equipo de desarrollo", OptionLetter = "c", Correct = true}
                    }
                }
            };

            return result;
        }
    }
}