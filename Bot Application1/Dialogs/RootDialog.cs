using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bot_Application1.Models;
using Bot_Application1.Storage;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.WindowsAzure.Storage.Table;

namespace Bot_Application1.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private string groupName;
        private int totalScore;
        private readonly List<Question> _questions = QuestionsFactory.GetQuestions();

        public async Task StartAsync(IDialogContext context)
        {
            /* Wait until the first message is received from the conversation and call MessageReceviedAsync 
             *  to process that message. */
            totalScore = 0;
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
                groupName = await result;

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
                context.Call(new Questionary(totalScore, _questions[0], groupName), Question2);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question1(context, result);
            }
        }

        private async Task Question2(IDialogContext context, IAwaitable<int> result)
        {
            totalScore = await result;
            try
            {
                context.Call(new Questionary(totalScore, _questions[1], groupName), Question3);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question2(context, result);
            }
        }

        private async Task Question3(IDialogContext context, IAwaitable<int> result)
        {
            totalScore = await result;
            try
            {
                context.Call(new Questionary(totalScore, _questions[2], groupName), Question4);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question3(context, result);
            }
        }

        private async Task Question4(IDialogContext context, IAwaitable<int> result)
        {
            totalScore = await result;
            try
            {
                context.Call(new Questionary(totalScore, _questions[3], groupName), Question5);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question4(context, result);
            }
        }

        private async Task Question5(IDialogContext context, IAwaitable<int> result)
        {
            totalScore = await result;
            try
            {
                context.Call(new Questionary(totalScore, _questions[4], groupName), Question6);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question5(context, result);
            }
        }

        private async Task Question6(IDialogContext context, IAwaitable<int> result)
        {
            totalScore = await result;
            try
            {
                context.Call(new Questionary(totalScore, _questions[5], groupName), Question7);
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
                context.Call(new Questionary(totalScore, _questions[6], groupName), Question8);
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
                context.Call(new Questionary(totalScore, _questions[7], groupName), Question9);
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
                context.Call(new Questionary(totalScore, _questions[8], groupName), QuestionFinal);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question9(context, result);
            }
        }

        private async Task QuestionFinal(IDialogContext context, IAwaitable<int> result)
        {
            totalScore = await result;
            try
            {
                context.Call(new Questionary(totalScore, _questions[9], groupName), Farewell);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await QuestionFinal(context, result);
            }
        }

        private async Task Farewell(IDialogContext context, IAwaitable<int> result)
        {
            totalScore = await result;
            try
            {
                await context.PostAsync($"Tu resultado final es { totalScore }.");
                await context.PostAsync($"{ groupName }, gracias por participar!");

                await SaveGroup();
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

        private async Task SaveGroup()
        {
            var groupentity = new TableEntity
            {
                PartitionKey = "Name",
                RowKey = groupName
            };
            var sm = new StorageManager();
            await sm.StoreEntity(groupentity, "Group");
        }
        
    }
}