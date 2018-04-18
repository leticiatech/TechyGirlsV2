using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_Application1.Dialogs
{
    [Serializable]
    public class Formalities : IDialog<string>
    {
        private int attempts = 3;

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Como estas?");

            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            /* If the message returned is a valid name, return it to the calling dialog. */
            if (!message.Text.ToLower().Contains("no") && message.Text.ToLower().Contains("bien") || message.Text.ToLower().Contains("genial"))
            {
                await context.PostAsync("Que bueno! Me alegro. Ahora vamos a jugar un juego. Si?");
                context.Done(message.Text);
            }
            else if (message.Text.ToLower().Contains("mal") || message.Text.ToLower().Contains("no muy bien"))
            {
                await context.PostAsync("Espero poder alegrar un poco tu dia. Ahora vamos a jugar un juego. Si?");
                context.Done(message.Text);
            }
            /* Else, try again by re-prompting the user. */
            else
            {
                --attempts;
                if (attempts > 0)
                {
                    await context.PostAsync("No te entiendo. Como estas?");

                    context.Wait(MessageReceivedAsync);
                }
                else
                {
                    /* Fails the current dialog, removes it from the dialog stack, and returns the exception to the 
                        parent/calling dialog. */
                    context.Fail(new TooManyAttemptsException("Disculpa, pero no entendi tu nombre."));
                }
            }
        }
    }
}