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
        private string name;
        private int finalResult;
        private List<Question> _questions = GetQuestions();

        public async Task StartAsync(IDialogContext context)
        {
            /* Wait until the first message is received from the conversation and call MessageReceviedAsync 
             *  to process that message. */
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
                this.name = await result;

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
                context.Call(new Questionary(finalResult, _questions[0]), Question2);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question1(context, result);
            }
        }

        private async Task Question2(IDialogContext context, IAwaitable<int> result)
        {

            try
            {
                context.Call(new Questionary(finalResult, _questions[1]), Question3);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question2(context, result);
            }
        }

        private async Task Question3(IDialogContext context, IAwaitable<int> result)
        {
            try
            {
                this.finalResult = await result;

                context.Call(new Questionary(finalResult, _questions[2]), Question4);

            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question3(context, result);
            }
        }

        private async Task Question4(IDialogContext context, IAwaitable<int> result)
        {
            try
            {
                this.finalResult = await result;

                context.Call(new Questionary(finalResult, _questions[3]), Question5);
                
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question4(context, result);
            }
        }

        private async Task Question5(IDialogContext context, IAwaitable<int> result)
        {
            try
            {
                this.finalResult = await result;

                context.Call(new Questionary(finalResult, _questions[4]), Question6);
                
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question5(context, result);
            }
        }

        private async Task Question6(IDialogContext context, IAwaitable<int> result)
        {
            try
            {
                this.finalResult = await result;

                context.Call(new Questionary(finalResult, _questions[5]), QuestionFinal);
                
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question6(context, result);
            }
        }

        private async Task QuestionFinal(IDialogContext context, IAwaitable<int> result)
        {
            try
            {
                this.finalResult = await result;

                context.Call(new Questionary(finalResult, _questions[6]), Farewell);

            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await QuestionFinal(context, result);
            }
        }

        private async Task Farewell(IDialogContext context, IAwaitable<int> result)
        {
            try
            {
                this.finalResult = await result;

                await context.PostAsync($"{ name } Gracias por participar.");
                this.finalResult = await result;
                await context.PostAsync($"Tu resultado final es { finalResult }.");

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