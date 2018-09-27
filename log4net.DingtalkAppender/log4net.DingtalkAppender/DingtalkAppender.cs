using log4net.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace log4net.Appender
{
    public class DingtalkAppender : AppenderSkeleton
    {
        public DingtalkAppender()
        {
            this.Emoticon = new Emoticon();
        }

        public Emoticon Emoticon { get; set; }

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

        private string GetEmoticon(Level level)
        {
            switch (level.DisplayName.ToLowerInvariant())
            {
                case "warn":
                    return this.Emoticon.Warn;
                case "error":
                case "fatal":
                    return this.Emoticon.Error;
                default:
                    return this.Emoticon.Notice;
            }
        }

        private Message GenerateMessage(LoggingEvent loggingEvent)
        {
            var emoticon = this.GetEmoticon(loggingEvent.Level);
            var hostName = GlobalContext.Properties["log4net:HostName"];
            var renderedLog = this.RenderLoggingEvent(loggingEvent);

            var content = $"{emoticon} [{loggingEvent.Level.DisplayName}] on {hostName}\r\n{renderedLog}";

            content = content.Replace("\r", string.Empty);

            return new Message { Msgtype = "text", Text = new MessageText { Content = content } };
        }
    }
}