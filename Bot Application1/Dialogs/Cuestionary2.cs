using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Connector;

namespace Bot_Application1.Dialogs
{
    public class Cuestionary2 : IDialog<int>
    {
        private int finalResult;
        private int attempts = 3;

        public Cuestionary2(int fR)
        {
            this.finalResult = fR;
        }

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync($"Hasta el momento tu puntaje es de: {finalResult}");
            await context.PostAsync($"Pregunta numero 2: Como se llama la empresa?");
            await context.PostAsync($"a. Onetree");
            await context.PostAsync($"b. UnArbol");
            await context.PostAsync($"c. TakeOff");

            context.Wait(this.MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (message.Text.ToLower().Equals("a"))
            {
                finalResult += 5;
                context.Done(finalResult);
            }
            else
            {
                //ARREGLAR LOGICA SI RESPONDE MAL
                if (message.Text.ToLower().Equals("b"))
                {
                    await context.PostAsync("Lo siento. Esa no es la opcion correcta. Intentalo de nuevo:");
                    await context.PostAsync($"a. Onetree");
                    await context.PostAsync($"c. TakeOff");

                    context.Wait(MessageReceivedAsync);

                    if (message.Text.ToLower().Equals("a"))
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
                    await context.PostAsync($"a. Onetree");
                    await context.PostAsync($"b. TakeOff");

                    context.Wait(MessageReceivedAsync);

                    if (message.Text.ToLower().Equals("a"))
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