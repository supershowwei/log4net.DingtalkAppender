using log4net.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace log4net.Appender
{
    public class DingtalkAppender : AppenderSkeleton
    {
        public string WebhookUrl { get; set; }

        protected override void Append(LoggingEvent loggingEvent)
        {
            var client = new RestClient(this.WebhookUrl);

            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");

            var message = this.GenerateMessage(loggingEvent);

            request.AddParameter(
                "application/json",
                JsonConvert.SerializeObject(
                    message,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                ParameterType.RequestBody);

            client.Execute(request);
        }

        private static string GetEmoticon(Level level)
        {
            switch (level.DisplayName.ToLowerInvariant())
            {
                case "warn":
                    return "[流汗]";
                case "error":
                case "fatal":
                    return "[大哭]";
                default:
                    return "[广播]";
            }
        }

        private Message GenerateMessage(LoggingEvent loggingEvent)
        {
            return new Message
                       {
                           Msgtype = "text",
                           Text =
                               new MessageText
                                   {
                                       Content =
                                           string.Format(
                                               "{0} [{1}] on {2}\r\n{3}",
                                               GetEmoticon(loggingEvent.Level),
                                               loggingEvent.Level.DisplayName,
                                               GlobalContext.Properties["log4net:HostName"],
                                               this.RenderLoggingEvent(loggingEvent))
                                   }
                       };
        }
    }
}