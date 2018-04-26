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
    public class QuestionaryTieBreak : IDialog<int>
    {
        private readonly Question _question;

        private int score = 5;

        public QuestionaryTieBreak(Question question)
        {
            _question = question;
        }

        public async Task StartAsync(IDialogContext context)
        {
            var line = new StringBuilder();
            line.AppendLine(_question.Text);

            //Send question
            foreach (var o in _question.Options)
            {
                line.AppendLine("");
                line.AppendLine(o.OptionLetter + ". " + o.Text);
            }

            await context.PostAsync(line.ToString());

            context.Wait(this.MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (!IsValidOption(message.Text))
            {
                await context.PostAsync("Oops! Intenta escribiendo la letra de una opción válida:");
                return;
            }

            //Correct in First try
            if (IsCorrect(message.Text))
            {
                await context.PostAsync("Correcto!");
                context.Done(score);
            }
            else
            {
                score --;
                await context.PostAsync("Incorrecto! Intentalo de nuevo:");
                
                context.Wait(this.AskAgain);
            }
        }

        private async Task AskAgain(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            //Correct in Second try, or incorrect
            if (IsCorrect(message.Text))
            {
                await context.PostAsync("Correcto!");
                context.Done(score);
            }
            else
            {
                score--;
                await context.PostAsync("Incorrecto! Prueba otra vez:");
                context.Wait(this.MessageReceivedAsync);
            }
        }

        private bool IsValidOption(string received)
        {
            received = received.ToLower().Trim();
            return received.Equals("a") || received.Equals("b") || received.Equals("c") || received.Equals("d") || received.Equals("e");
        }

        private bool IsCorrect(string received)
        {
            return _question.GetCorrectAnswer().OptionLetter.Equals(received.ToLower().Trim());
        }
    }
}