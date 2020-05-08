using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using log4net.Core;

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
            var emoticon = this.GetEmoticon(loggingEvent.Level);
            var hostName = GlobalContext.Properties["log4net:HostName"];
            var renderedLog = this.RenderLoggingEvent(loggingEvent);

            var contentBuilder = new StringBuilder();

            contentBuilder.AppendLine($"{emoticon} [{loggingEvent.Level.DisplayName}] on {hostName}");
            contentBuilder.AppendLine();
            contentBuilder.Append(renderedLog);

            var message = JsonSerializer.Serialize(
                new { Msgtype = "text", Text = new { Content = contentBuilder.Replace("\r", string.Empty).ToString() } },
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var webhook = new Uri(this.WebhookUrl);

            var request = new HttpRequestMessage(HttpMethod.Post, webhook)
                          {
                              Content = new StringContent(message, Encoding.UTF8, "application/json")
                          };

            var httpClient = HttpClientFactory.Instance.CreateClient(new Uri($"{webhook.Scheme}{Uri.SchemeDelimiter}{webhook.Host}"));

            httpClient.SendAsync(request).GetAwaiter().GetResult();
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
    }
}