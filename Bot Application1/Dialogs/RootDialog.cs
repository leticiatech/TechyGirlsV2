using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_Application1.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private string name;
        private int finalResult;

        public async Task StartAsync(IDialogContext context)
        {
            /* Wait until the first message is received from the conversation and call MessageReceviedAsync 
             *  to process that message. */
            context.Wait(this.MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;

            await SendWelcomeMessageAsync(context);
        }

        private async Task SendWelcomeMessageAsync(IDialogContext context)
        {

            context.Call(new Welcome(), NameDialogResumeAfter);
        }

        private async Task SendFormalitiesMessageAsync(IDialogContext context)
        {

            context.Call(new Formalities(), FormalitiesDialogResumeAfter);
        }

        private async Task SendCuestion1MessageAsync(IDialogContext context)
        {

            context.Call(new Cuestionary(finalResult), CuestionsDialogResumeAfter);
        }

        private async Task NameDialogResumeAfter(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                this.name = await result;

                context.Call(new Formalities(), FormalitiesDialogResumeAfter);
            }
            catch (TooManyAttemptsException)
            {
                await context.PostAsync("Lo lamento, no te entendi. Tratemos de nuevo.");

                await SendWelcomeMessageAsync(context);
            }
        }

        private async Task FormalitiesDialogResumeAfter(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
               context.Call(new Cuestionary(finalResult), CuestionsDialogResumeAfter);
            }
            catch (TooManyAttemptsException)
            {
                await context.PostAsync("Lo lamento, no te entendi. Tratemos de nuevo.");

                await SendFormalitiesMessageAsync(context);
            }
        }

        private async Task CuestionsDialogResumeAfter(IDialogContext context, IAwaitable<int> result)
        {
            try
            {
                this.finalResult = await result;

                await context.PostAsync($"{ name } su resultado final es { result }.");

            }
            catch (TooManyAttemptsException)
            {
                await context.PostAsync("Lo lamento, no te entendi. Tratemos de nuevo.");
            }
            finally
            {
                await this.SendWelcomeMessageAsync(context);
            }
        }
    }
}