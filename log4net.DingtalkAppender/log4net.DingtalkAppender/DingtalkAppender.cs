using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using log4net.Core;

namespace log4net.Appender
{
    public class DingtalkAppender : AppenderSkeleton
    {
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public DingtalkAppender()
        {
            this.Emoticon = new Emoticon();
        }

        public Emoticon Emoticon { get; set; }

        public string WebhookUrl { get; set; }

        public string Secret { get; set; }

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

            var signature = string.IsNullOrEmpty(this.Secret) ? string.Empty : GenerateSignature(this.Secret);

            var webhook = new Uri(string.Concat(this.WebhookUrl, signature));

            var request = new HttpRequestMessage(HttpMethod.Post, webhook)
                          {
                              Content = new StringContent(message, Encoding.UTF8, "application/json")
                          };

            var httpClient = HttpClientFactory.Instance.CreateClient(new Uri($"{webhook.Scheme}{Uri.SchemeDelimiter}{webhook.Host}"));

            httpClient.SendAsync(request).GetAwaiter().GetResult();
        }

        private static string GenerateSignature(string secret)
        {
            var signatureBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(secret))
            {
                var timestamp = (long)DateTime.UtcNow.Subtract(Jan1st1970).TotalMilliseconds;

                signatureBuilder.AppendFormat("&timestamp={0}", timestamp);

                using (var hmacSHA256 = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
                {
                    var signature = string.Concat(timestamp, "\n", secret);

                    signature = Convert.ToBase64String(hmacSHA256.ComputeHash(Encoding.UTF8.GetBytes(signature)));

                    signatureBuilder.AppendFormat("&sign={0}", Uri.EscapeDataString(signature));
                }
            }

            return signatureBuilder.ToString();
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