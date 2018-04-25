using System;
using System.Text;
using System.Threading.Tasks;
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
            System.Threading.Thread.Sleep(1000);
            await context.PostAsync("¿Como están?");

            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            /* If the message returned is a valid name, return it to the calling dialog. */
            if (!message.Text.ToLower().Contains("no") && (message.Text.ToLower().Contains("bien") || message.Text.ToLower().Contains("excelente") || message.Text.ToLower().Contains("feliz") || message.Text.ToLower().Contains("contenta")))
            {
                await context.PostAsync("Qué bueno equipo! Me alegro.");
                System.Threading.Thread.Sleep(2000);

                var line = new StringBuilder();
                line.AppendLine("Ahora arrancamos con la prueba, debes contestar las siguientes 10 preguntas.");
                line.AppendLine("");
                line.AppendLine("Comencemos con la primera:");
                await context.PostAsync(line.ToString());
                System.Threading.Thread.Sleep(2000);

                context.Done(message.Text);
            }
            else if (message.Text.ToLower().Contains("no") || message.Text.ToLower().Contains("mal") || message.Text.ToLower().Contains("deprimida") || message.Text.ToLower().Contains("triste") || message.Text.ToLower().Contains("enojada"))
            {
                await context.PostAsync("Bueno, espero alegrarles un poco el día!");
                System.Threading.Thread.Sleep(2000);

                var line = new StringBuilder();
                line.AppendLine("Ahora arrancamos con la prueba, debes contestar las siguientes 10 preguntas.");
                line.AppendLine("");
                line.AppendLine("Comencemos con la primera:");
                await context.PostAsync(line.ToString());
                System.Threading.Thread.Sleep(2000);

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