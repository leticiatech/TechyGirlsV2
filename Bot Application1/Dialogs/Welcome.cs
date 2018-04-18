using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            await context.PostAsync("Hola, Soy Ada. Como es tu nombre?");

            context.Wait(this.MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            /* If the message returned is a valid name, return it to the calling dialog. */
            if (message.Text.ToLower().Contains("llamo") || message.Text.ToLower().Contains("nombre es"))
            {
                
                    var indexSplit = message.Text.LastIndexOf(" ");
                    var name = message.Text.Remove(0, indexSplit);
                if (name != null && name.Length > 0)
                {
                    await context.PostAsync($"Hola {name}.");
                }
                context.Done(name);
            }
            /* Else, try again by re-prompting the user. */
            else
            {
                --attempts;
                if (attempts > 0)
                {
                    await context.PostAsync("Lo siento. No te entiendo. Como es tu nombre?");

                    context.Wait(this.MessageReceivedAsync);
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