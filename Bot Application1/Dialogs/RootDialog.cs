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
        private Group group;
        private readonly List<Question> _questions = QuestionsFactory.GetQuestions();

        public async Task StartAsync(IDialogContext context)
        {
            /* Wait until the first message is received from the conversation and call MessageReceviedAsync 
             *  to process that message. */
            group = new Group();
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
                group.Name = await result;

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
                context.Call(new Questionary(group.TotalScore, _questions[0], group.Name), Question2);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question1(context, result);
            }
        }

        private async Task Question2(IDialogContext context, IAwaitable<int> result)
        {
            group.QuestionScores.Add(
                new QuestionScore
                {
                    Question = "1",
                    Score = await result - group.TotalScore
                });

            group.TotalScore = await result;
            try
            {
                context.Call(new Questionary(group.TotalScore, _questions[1], group.Name), Question3);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question2(context, result);
            }
        }

        private async Task Question3(IDialogContext context, IAwaitable<int> result)
        {
            group.QuestionScores.Add(
                new QuestionScore
                {
                    Question = "2",
                    Score = await result - group.TotalScore
                });

            group.TotalScore = await result;
            try
            {
                context.Call(new Questionary(group.TotalScore, _questions[2], group.Name), Question4);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question3(context, result);
            }
        }

        private async Task Question4(IDialogContext context, IAwaitable<int> result)
        {
            group.QuestionScores.Add(
                new QuestionScore
                {
                    Question = "3",
                    Score = await result - group.TotalScore
                });

            group.TotalScore = await result;
            try
            {
                context.Call(new Questionary(group.TotalScore, _questions[3], group.Name), Question5);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question4(context, result);
            }
        }

        private async Task Question5(IDialogContext context, IAwaitable<int> result)
        {
            group.QuestionScores.Add(
                new QuestionScore
                {
                    Question = "4",
                    Score = await result - group.TotalScore
                });

            group.TotalScore = await result;
            try
            {
                context.Call(new Questionary(group.TotalScore, _questions[4], group.Name), Question6);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question5(context, result);
            }
        }

        private async Task Question6(IDialogContext context, IAwaitable<int> result)
        {
            group.QuestionScores.Add(
                new QuestionScore
                {
                    Question = "5",
                    Score = await result - group.TotalScore
                });

            group.TotalScore = await result;
            try
            {
                context.Call(new Questionary(group.TotalScore, _questions[5], group.Name), QuestionFinal);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question6(context, result);
            }
        }

        private async Task QuestionFinal(IDialogContext context, IAwaitable<int> result)
        {
            group.QuestionScores.Add(
                new QuestionScore
                {
                    Question = "6",
                    Score = await result - group.TotalScore
                });

            group.TotalScore = await result;
            try
            {
                context.Call(new Questionary(group.TotalScore, _questions[6], group.Name), Farewell);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await QuestionFinal(context, result);
            }
        }

        private async Task Farewell(IDialogContext context, IAwaitable<int> result)
        {
            group.QuestionScores.Add(
                new QuestionScore
                {
                    Question = "7",
                    Score = await result - group.TotalScore
                });

            group.TotalScore = await result;
            try
            {
                await context.PostAsync($"Tu resultado final es { group.TotalScore }.");
                await context.PostAsync($"{ group.Name }, gracias por participar!");

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
        
    }
}