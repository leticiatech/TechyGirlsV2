using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Bot_Application1.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_Application1.Dialogs
{
    [Serializable]
    public class TieBreak : IDialog<string>
    {
        private readonly List<Question> _questions = QuestionsFactory.GetQuestions();
        public async Task StartAsync(IDialogContext context)
        {
            System.Threading.Thread.Sleep(1000);

            var line = new StringBuilder();
            line.AppendLine("Hola de nuevo! Ahora vamos a jugar nuevamente para desempatar.");
            line.AppendLine("");
            line.AppendLine("Deben ordenar correctamente los pasos del proceso de desarrollo de software.");
            line.AppendLine("");
            line.AppendLine("Comencemos.");
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
            //await SaveScoreTieBreak(await result, 1);

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
            //Save Previous Score
           // await SaveScoreTieBreak(await result, 2);

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
            //Save Previous Score
            //await SaveScoreTieBreak(await result, 3);

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
            //Save Previous Score
            //await SaveScoreTieBreak(await result, 4);

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
            //await SaveScoreTieBreak(await result, 5);

            await context.PostAsync("Suerte!");
           
            context.Done("");
            
        }

        private async Task FailMessage(IDialogContext context)
        {
            await context.PostAsync("Lo lamento, no te entendi. Intentemos de nuevo!");
        }
    }
}