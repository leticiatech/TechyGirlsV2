using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bot_Application1.Models;
using Bot_Application1.Storage;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_Application1.Dialogs
{
    [Serializable]
    public class TieBreak : IDialog<string>
    {
        private readonly IDataAccess _dataAccess;

        private readonly List<Question> _questions = QuestionsTieBreakFactory.GetQuestions();
        private int totalScore;
        private string _groupName;

        public TieBreak(IDataAccess dataAccess, string groupName)
        {
            _dataAccess = dataAccess;
            _groupName = groupName;
        }

        public async Task StartAsync(IDialogContext context)
        {
            System.Threading.Thread.Sleep(1000);

            var line = new StringBuilder();
            line.AppendLine("Hola de nuevo! Ahora vamos a jugar nuevamente para desempatar.");
            line.AppendLine("");
            line.AppendLine("Deben ordenar correctamente los pasos del proceso de desarrollo de software.");
            line.AppendLine("");
            line.AppendLine("Para comenzar escribe ok.");
            await context.PostAsync(line.ToString());
            System.Threading.Thread.Sleep(2000);

            context.Wait(QuestionTieBreak1);
        }

        //TIEBREAK

        private async Task QuestionTieBreak1(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            //Send next Question
            try
            {
                context.Call(new QuestionaryTieBreak(_questions[0]), QuestionTieBreak2);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await QuestionTieBreak1(context, result);
            }
        }

        private async Task QuestionTieBreak2(IDialogContext context, IAwaitable<int> result)
        {
            //Save Previous Score
            int score = await result;
            totalScore += score;

            //Send next Question
            try
            {
                context.Call(new QuestionaryTieBreak(_questions[1]), QuestionTieBreak3);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await QuestionTieBreak2(context, result);
            }
        }

        private async Task QuestionTieBreak3(IDialogContext context, IAwaitable<int> result)
        {
            int score = await result;
            totalScore += score;

            //Send next Question
            try
            {
                context.Call(new QuestionaryTieBreak(_questions[2]), QuestionTieBreak4);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await QuestionTieBreak3(context, result);
            }
        }

        private async Task QuestionTieBreak4(IDialogContext context, IAwaitable<int> result)
        {
            int score = await result;
            totalScore += score;

            //Send next Question
            try
            {
                context.Call(new QuestionaryTieBreak(_questions[3]), QuestionTieBreak5);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await QuestionTieBreak4(context, result);
            }
        }

        private async Task QuestionTieBreak5(IDialogContext context, IAwaitable<int> result)
        {
            int score = await result;
            totalScore += score;

            //Send next Question
            try
            {
                context.Call(new QuestionaryTieBreak(_questions[4]), FarewellTieBreak);
            }
            catch (TooManyAttemptsException)
            {
                await FailMessage(context);
                await QuestionTieBreak5(context, result);
            }
        }

        private async Task FarewellTieBreak(IDialogContext context, IAwaitable<int> result)
        {
            //Save Previous Score
            int score = await result;
            totalScore += score;
            await SaveScoreTieBreak();

            await context.PostAsync("Suerte!");
            context.Done("");
            
        }

        //SAVE DATA
        private async Task SaveScoreTieBreak()
        {
            var groupentity = new TiebreakTableEntity(_groupName) { Score = totalScore };
            await _dataAccess.StoreEntity(groupentity, "Tiebreak");
        }

        //FAIL MESSAGE
        private async Task FailMessage(IDialogContext context)
        {
            await context.PostAsync("Lo lamento, no te entendi. Intentemos de nuevo!");
        }
    }
}