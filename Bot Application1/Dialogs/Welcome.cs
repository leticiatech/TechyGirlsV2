using System;
using System.Linq;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading.Tasks;

namespace Bot_Application1.Dialogs
{
    [Serializable]
    public class Welcome : IDialog<string>
    {
        private int attempts = 3;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            /* If the message returned is a valid name, return it to the calling dialog. */
            if (message.Text != null && message.Text.Contains(" "))
            {
                await context.PostAsync("Prueba escribiendo un nombre sin espacios! Vamos de nuevo:");

                context.Wait(this.MessageReceivedAsync);
            }
            else if (message.Text != null && message.Text.Trim().Length > 0)
            {
                var name = message.Text.Trim();

                await context.PostAsync($"Bienvenidas {name}!");
                context.Done(name);
            }
            /* Else, try again by re-prompting the user. */
            else
            {
                --attempts;
                if (attempts > 0)
                {
                    await context.PostAsync("No entendí! :( Vuelve a escribir el nombre:");

                    context.Wait(this.MessageReceivedAsync);
                }
                else
                {
                    /* Fails the current dialog, removes it from the dialog stack, and returns the exception to the 
                        parent/calling dialog. */
                    context.Fail(new TooManyAttemptsException("Disculpa, pero no entendí! :("));
                }
            }
        }
    }
}