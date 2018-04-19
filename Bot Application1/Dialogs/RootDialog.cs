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
                context.Call(new Questionary(newGroup.TotalScore, _questions[5]), QuestionFinal);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question6(context, result);
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
                context.Call(new Questionary(newGroup.TotalScore, _questions[6]), Farewell);
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