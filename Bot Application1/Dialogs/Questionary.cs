using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot_Application1.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_Application1.Dialogs
{
    [Serializable]
    public class Questionary : IDialog<int>
    {
        private const int MaxPoints = 5;
        private const int MinPoints = 3;
        private readonly Question _question;
        private int _totalScore;

        public Questionary(int totalScore, Question question)
        {
            _totalScore = totalScore;
            _question = question;
        }

        public async Task StartAsync(IDialogContext context)
        {
            //Send question
            await context.PostAsync(_question.Text);

            //Send options
            foreach (var o in _question.Options)
                await context.PostAsync(o.OptionLetter +". "+ o.Text);

            context.Wait(this.MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (!IsValidOption(message.Text))
            {
                await context.PostAsync("Oops! Intenta escribiendo la letra de una opción válida");
                context.Wait(this.MessageReceivedAsync);
            }

            //Correct in First try
            if (IsCorrect(message.Text))
            {
                _totalScore += MaxPoints;
                context.Done(_totalScore);
            }
            else
            {
                await context.PostAsync("Incorrecto! Intentalo de nuevo:");

                foreach(var o in RemoveSelected(message.Text))
                    await context.PostAsync(o.OptionLetter + ". " + o.Text);

                context.Wait(this.AskAgain);
            }
        }

        private async Task AskAgain(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            //Correct in Second try
            if (IsCorrect(message.Text))
            {
                _totalScore += MinPoints;
                context.Done(_totalScore);
            }
            else
            {
                //Incorrect!
                _totalScore += 0;
                context.Done(_totalScore);
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