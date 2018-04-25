using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Bot_Application1.Storage;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_Application1.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private readonly IDataAccess _dataAccess;

        public MessagesController(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new Dialogs.RootDialog(_dataAccess));
            }
            else
            {
                await HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private async Task<Activity> HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed

                if (message.MembersAdded.Any(o => o.Id == message.Recipient.Id))
                {
                    var connector = new ConnectorClient(new System.Uri(message.ServiceUrl));
                    var reply1 = message.CreateReply("Hola, mi nombre es Ada!");
                    System.Threading.Thread.Sleep(1000);
                    var reply2 = message.CreateReply("Escribe un nombre para tu equipo:");
                    await connector.Conversations.ReplyToActivityAsync(reply1);
                    await connector.Conversations.ReplyToActivityAsync(reply2);
                    message.Type = ActivityTypes.Message;
                }
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}