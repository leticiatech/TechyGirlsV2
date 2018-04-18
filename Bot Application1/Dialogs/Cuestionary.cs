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
    public class Cuestionary : IDialog<int>
    {
        private int finalResult;
        private int attempts = 3;

        public Cuestionary(int fR)
        {
            this.finalResult = fR;
        }

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync($"Voy a hacerte unas preguntas y tu debes elegir la opcion correcta.");
            await context.PostAsync($"Empecemos");
            await context.PostAsync($"Pregunta numero 1: Quien es la chica de HR?");
            await context.PostAsync($"a. Leticia");
            await context.PostAsync($"b. Mariasol");
            await context.PostAsync($"c. Silvana");

            context.Wait(this.MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (message.Text.ToLower().Equals("b"))
            {
                finalResult += 5;
                context.Done(finalResult);
            }
            else
            {
                //ARREGLAR LOGICA SI RESPONDE MAL
                if (message.Text.ToLower().Equals("a"))
                {
                    await context.PostAsync("Lo siento. Esa no es la opcion correcta. Intentalo de nuevo:");
                    await context.PostAsync($"b. Mariasol");
                    await context.PostAsync($"c. Silvana");

                    context.Wait(MessageReceivedAsync);

                    if (message.Text.ToLower().Equals("b"))
                    {
                        finalResult += 3;
                        context.Done(finalResult);
                    }
                    else
                    {
                        context.Done(finalResult);
                    }
                }
                else if (message.Text.ToLower().Equals("c"))
                {
                    await context.PostAsync("Lo siento. Esa no es la opcion correcta. Intentalo de nuevo:");
                    await context.PostAsync($"a. Leticia");
                    await context.PostAsync($"b. Mariasol");

                    context.Wait(MessageReceivedAsync);

                    if (message.Text.ToLower().Equals("b"))
                    {
                        finalResult += 3;
                        context.Done(finalResult);
                    }
                    else
                    {
                        context.Done(finalResult);
                    }
                }
            }
        }
    }
}