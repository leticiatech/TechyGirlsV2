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
        private readonly IDataAccess _dataAccess;

        private int totalScore;
        private string groupName;
        private readonly List<Question> _questions = QuestionsFactory.GetQuestions();

        public RootDialog(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task StartAsync(IDialogContext context)
        {
            /* Wait until the first message is received from the conversation and call MessageReceviedAsync 
             *  to process that message. */
            totalScore = 0;
            context.Wait(this.MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
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
                context.Call(new Questionary(_questions[0]), Question2);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question1(context, result);
            }
        }

        private async Task Question2(IDialogContext context, IAwaitable<int> result)
        {
            //Save Previous Score
            await SaveScore(await result, 1);

            //Send next Question
            try
            {
                context.Call(new Questionary(_questions[1]), Question3);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question2(context, result);
            }
        }

        private async Task Question3(IDialogContext context, IAwaitable<int> result)
        {
            //Save Previous Score
            await SaveScore(await result, 2);

            //Send next Question
            try
            {
                context.Call(new Questionary(_questions[2]), Question4);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question3(context, result);
            }
        }

        private async Task Question4(IDialogContext context, IAwaitable<int> result)
        {
            //Save Previous Score
            await SaveScore(await result, 3);

            //Send next Question
            try
            {
                context.Call(new Questionary(_questions[3]), Question5);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question4(context, result);
            }
        }

        private async Task Question5(IDialogContext context, IAwaitable<int> result)
        {
            //Save Previous Score
            await SaveScore(await result, 4);

            //Send next Question
            try
            {
                context.Call(new Questionary(_questions[4]), Question6);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question5(context, result);
            }
        }

        private async Task Question6(IDialogContext context, IAwaitable<int> result)
        {
            //Save Previous Score
            await SaveScore(await result, 5);

            //Send next Question
            try
            {
                context.Call(new Questionary(_questions[5]), Question7);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question6(context, result);
            }
        }

        private async Task Question7(IDialogContext context, IAwaitable<int> result)
        {
            //Save Previous Score
            await SaveScore(await result, 6);

            //Send next Question
            try
            {
                context.Call(new Questionary(_questions[6]), Question8);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question7(context, result);
            }
        }

        private async Task Question8(IDialogContext context, IAwaitable<int> result)
        {
            //Save Previous Score
            await SaveScore(await result, 7);

            //Send next Question
            try
            {
                context.Call(new Questionary(_questions[7]), Question9);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question8(context, result);
            }
        }

        private async Task Question9(IDialogContext context, IAwaitable<int> result)
        {
            //Save Previous Score
            await SaveScore(await result, 8);

            //Send next Question
            try
            {
                context.Call(new Questionary(_questions[8]), QuestionFinal);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await Question9(context, result);
            }
        }

        private async Task QuestionFinal(IDialogContext context, IAwaitable<int> result)
        {
            //Save Previous Score
            await SaveScore(await result, 9);

            //Send next Question
            try
            {
                context.Call(new Questionary(_questions[9]), Farewell);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await QuestionFinal(context, result);
            }
        }

        private async Task Farewell(IDialogContext context, IAwaitable<int> result)
        {
            //Save Previous Score
            await SaveScore(await result, 10);

            //Send next Question
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

        //FAIL DATA
        private async Task SaveScore(int score, int question)
        {
            totalScore += score;
            var groupentity = new GroupTableEntity(groupName, question.ToString()) { Score = score };
            await _dataAccess.StoreEntity(groupentity, "GroupScore");
        }

        private async Task SaveGroup()
        {
            var groupentity = new TableEntity
            {
                PartitionKey = "Name",
                RowKey = groupName
            };
            await _dataAccess.StoreEntity(groupentity, "Group");
        }
        
    }
}