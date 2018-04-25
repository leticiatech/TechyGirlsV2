using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot_Application1.Models;
using Bot_Application1.Storage;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_Application1.Dialogs
{
    [Serializable]
    public class Questionary : IDialog<int>
    {
        private readonly Question _question;

        private const int MaxPoints = 5;
        private const int MinPoints = 3;

        public Questionary(Question question)
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
                context.Done(MaxPoints);
            }
            else
            {
                await context.PostAsync("Incorrecto! Intentalo de nuevo:");

                var line = new StringBuilder();

                //Send question
                foreach (var o in RemoveSelected(message.Text))
                {
                    line.AppendLine("");
                    line.AppendLine(o.OptionLetter + ". " + o.Text);
                }
                await context.PostAsync(line.ToString());

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
                context.Done(MinPoints);
            }
            else
            {
                await context.PostAsync("Incorrecto :(");
                context.Done(0);
            }
        }

        private bool IsValidOption(string received)
        {
            received = received.ToLower().Trim();
            return received.Equals("a") || received.Equals("b") || received.Equals("c");
        }

        private bool IsCorrect(string received)
        {
            return _question.GetCorrectAnswer().OptionLetter.Equals(received.ToLower().Trim());
        }

        private IEnumerable<Option> RemoveSelected(string selected)
        {
            return _question.Options.Where(o => !o.OptionLetter.Equals(selected.ToLower().Trim())).ToList();
        }
    }
}