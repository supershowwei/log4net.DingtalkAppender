namespace log4net.Appender
{
    public class Emoticon
    {
        private string error;
        private string notice;
        private string warn;

        public string Notice
        {
            get
            {
                return string.IsNullOrEmpty(this.notice) ? "[广播]" : this.notice;
            }

            set
            {
                this.notice = value;
            }
        }

        public string Warn
        {
            get
            {
                return string.IsNullOrEmpty(this.warn) ? "[流汗]" : this.warn;
            }

            set
            {
                this.warn = value;
            }
        }

        public string Error
        {
            get
            {
                return string.IsNullOrEmpty(this.error) ? "[大哭]" : this.error;
            }

            set
            {
                this.error = value;
            }
        }
    }
}