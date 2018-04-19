using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bot_Application1.Models;
using Microsoft.Bot.Builder.Dialogs;

namespace Bot_Application1.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private List<Group> _groups = new List<Group>();
        private Group newGroup;
        private List<Question> _questions = GetQuestions();

        public async Task StartAsync(IDialogContext context)
        {
            /* Wait until the first message is received from the conversation and call MessageReceviedAsync 
             *  to process that message. */
            newGroup = new Group();
            context.Wait(this.MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;

            await SendWelcomeMessageAsync(context);
        }

        private async Task SendWelcomeMessageAsync(IDialogContext context)
        {

            context.Call(new Welcome(), FormalitiesDialog);
        }

        private async Task FormalitiesDialog(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                newGroup.Name = await result;

                context.Call(new Formalities(), Question1);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await FormalitiesDialog(context, result);
            }
        }

        private async Task Question1(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                context.Call(new Questionary(newGroup.TotalScore, _questions[0]), Question2);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question1(context, result);
            }
        }

        private async Task Question2(IDialogContext context, IAwaitable<int> result)
        {
            newGroup.QuestionScores.Add(
                new QuestionScore
                {
                    Question = "1",
                    Score = await result - newGroup.TotalScore
                });

            newGroup.TotalScore = await result;
            try
            {
                context.Call(new Questionary(newGroup.TotalScore, _questions[1]), Question3);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question2(context, result);
            }
        }

        private async Task Question3(IDialogContext context, IAwaitable<int> result)
        {
            newGroup.QuestionScores.Add(
                new QuestionScore
                {
                    Question = "2",
                    Score = await result - newGroup.TotalScore
                });

            newGroup.TotalScore = await result;
            try
            {
                context.Call(new Questionary(newGroup.TotalScore, _questions[2]), Question4);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question3(context, result);
            }
        }

        private async Task Question4(IDialogContext context, IAwaitable<int> result)
        {
            newGroup.QuestionScores.Add(
                new QuestionScore
                {
                    Question = "3",
                    Score = await result - newGroup.TotalScore
                });

            newGroup.TotalScore = await result;
            try
            {
                context.Call(new Questionary(newGroup.TotalScore, _questions[3]), Question5);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question4(context, result);
            }
        }

        private async Task Question5(IDialogContext context, IAwaitable<int> result)
        {
            newGroup.QuestionScores.Add(
                new QuestionScore
                {
                    Question = "4",
                    Score = await result - newGroup.TotalScore
                });

            newGroup.TotalScore = await result;
            try
            {
                context.Call(new Questionary(newGroup.TotalScore, _questions[4]), Question6);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question5(context, result);
            }
        }

        private async Task Question6(IDialogContext context, IAwaitable<int> result)
        {
            newGroup.QuestionScores.Add(
                new QuestionScore
                {
                    Question = "5",
                    Score = await result - newGroup.TotalScore
                });

            newGroup.TotalScore = await result;
            try
            {
                context.Call(new Questionary(finalResult, _questions[5]), Question7);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question6(context, result);
            }
        }

        private async Task Question7(IDialogContext context, IAwaitable<int> result)
        {
            try
            {
                this.finalResult = await result;

                context.Call(new Questionary(finalResult, _questions[6]), Question8);

            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question7(context, result);
            }
        }

        private async Task Question8(IDialogContext context, IAwaitable<int> result)
        {
            try
            {
                this.finalResult = await result;

                context.Call(new Questionary(finalResult, _questions[7]), Question9);

            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question8(context, result);
            }
        }

        private async Task Question9(IDialogContext context, IAwaitable<int> result)
        {
            try
            {
                this.finalResult = await result;

                context.Call(new Questionary(finalResult, _questions[8]), QuestionFinal);

            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question9(context, result);
            }
        }

        private async Task QuestionFinal(IDialogContext context, IAwaitable<int> result)
        {
            newGroup.QuestionScores.Add(
                new QuestionScore
                {
                    Question = "6",
                    Score = await result - newGroup.TotalScore
                });

            newGroup.TotalScore = await result;
            try
            {
                context.Call(new Questionary(finalResult, _questions[9]), Farewell);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await QuestionFinal(context, result);
            }
        }

        private async Task Farewell(IDialogContext context, IAwaitable<int> result)
        {
            newGroup.QuestionScores.Add(
                new QuestionScore
                {
                    Question = "7",
                    Score = await result - newGroup.TotalScore
                });

            newGroup.TotalScore = await result;
            try
            {
                await context.PostAsync($"Equipo { newGroup.Name }, gracias por participar!");
                await context.PostAsync($"Tu resultado final es { newGroup.TotalScore }.");
                //TODO: remove:
                await context.PostAsync(newGroup.QuestionScores.Count.ToString());
                //TODO: save data for group

                context.Done("");
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
            }
        }

        //FAIL MESSAGE

        private async Task FailMessage(IDialogContext context)
        {
            await context.PostAsync("Lo lamento, no te entendi. Tratemos de nuevo.");
        }

        // QUESTIONS FACTORY
        private static List<Question> GetQuestions()
        {
            var result = new List<Question>
            {
                new Question
                {
                    Text = "1. ¿Cuál es el rol de una project manager?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Persona responsable para liderar a un equipo  para alcanzar objetivos.", OptionLetter = "a", Correct = true},
                        new Option {Text = "Supervisor directo del equipo de desarrollo", OptionLetter = "b",  Correct = false},
                        new Option {Text = "No es responsable, solo dirige al equipo", OptionLetter = "c",  Correct = true}
                    }
                },
                new Question
                {
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
                    Text = "5. ¿Cuál es la tarea principal de un FE?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Onetree", OptionLetter = "a", Correct = true},
                        new Option {Text = "Altimetrik", OptionLetter = "b", Correct = false},
                        new Option {Text = "Takeoff", OptionLetter = "c", Correct = false}
                    }
                },
                new Question
                {
                    Text = "6. ¿De quién depende un FE para poder desarrollar bien su trabajo?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Kachi", OptionLetter = "a", Correct = true},
                        new Option {Text = "Carlos", OptionLetter = "b", Correct = false},
                        new Option {Text = "Puchet", OptionLetter = "c", Correct = false}
                    }
                },
                new Question
                {
                    Text = "7. ¿Cómo es el nombre de la empresa?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Onetree", OptionLetter = "a", Correct = true},
                        new Option {Text = "Altimetrik", OptionLetter = "b", Correct = false},
                        new Option {Text = "Takeoff", OptionLetter = "c", Correct = false}
                    }
                },
                new Question
                {
                    Text = "8. ¿Cómo es el nombre de la empresa?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Onetree", OptionLetter = "a", Correct = true},
                        new Option {Text = "Altimetrik", OptionLetter = "b", Correct = false},
                        new Option {Text = "Takeoff", OptionLetter = "c", Correct = false}
                    }
                },
                new Question
                {
                    Text = "9. ¿Por que es necesario incluir pruebas en el proceso de creación de un software?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Para complicar al equipo de Desarrollo", OptionLetter = "a", Correct = false},
                        new Option {Text = "Para identificar defectos/errores en el sistema, acortar costos, reducir tiempos y entregar un producto de buena calidad.", OptionLetter = "b", Correct = true},
                        new Option {Text = "Para cobrarle más al cliente", OptionLetter = "c", Correct = false}
                    }
                },
                new Question
                {
                    Text = "10. ¿Que hace el equipo de pruebas cuando identifica un error en el sistema?",
                    Options = new List<Option>
                    {
                        new Option {Text = "Busca la manera de que el cliente no ejecute esa parte asi no lo ve", OptionLetter = "a", Correct = false},
                        new Option {Text = "Registra el defecto en un Excel o una herramienta pero no necesita informarle al equipo de desarrollo", OptionLetter = "b", Correct = false},
                        new Option {Text = "Registra el defecto incluyendo evidencia del mismo y los pasos para reproducirlo. Se lo informa al equipo de desarrollo", OptionLetter = "c", Correct = true}
                    }
                }
            };

            return result;
        }
    }
}