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
            await context.PostAsync("¿Como están?");

            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            /* If the message returned is a valid name, return it to the calling dialog. */
            if (!message.Text.ToLower().Contains("no") && (message.Text.ToLower().Contains("bien") || message.Text.ToLower().Contains("excelente") || message.Text.ToLower().Contains("feliz") || message.Text.ToLower().Contains("contenta")))
            {
                await context.PostAsync("Que bueno! Me alegro. Ahora arrancamos con la prueba. ¿Estás pronta para la primera pregunta?  Solo son 10 preguntas");
                context.Done(message.Text);
            }
            else if (message.Text.ToLower().Contains("no") || message.Text.ToLower().Contains("mal") || message.Text.ToLower().Contains("deprimida") || message.Text.ToLower().Contains("triste") || message.Text.ToLower().Contains("enojada"))
            {
                await context.PostAsync("Bueno, espero que estés mejor mañana! Ahora arrancamos con la prueba. ¿Estás pronta para la primera pregunta?  Solo son 10 preguntas");

                context.Done(message.Text);
            }
            /* Else, try again by re-prompting the user. */
            else
            {
                --attempts;
                if (attempts > 0)
                {
                    await context.PostAsync("No entiendo. ¿Como están?");

                    context.Wait(MessageReceivedAsync);
                }
                else
                {
                    /* Fails the current dialog, removes it from the dialog stack, and returns the exception to the 
                        parent/calling dialog. */
                    context.Fail(new TooManyAttemptsException("Disculpa, pero no entendi!"));
                }
            }
        }
    }
}