namespace log4net.DingtalkAppender.WinForms
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.DebugLogButton = new System.Windows.Forms.Button();
            this.ErrorLogButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DebugLogButton
            // 
            this.DebugLogButton.Location = new System.Drawing.Point(12, 12);
            this.DebugLogButton.Name = "DebugLogButton";
            this.DebugLogButton.Size = new System.Drawing.Size(75, 23);
            this.DebugLogButton.TabIndex = 0;
            this.DebugLogButton.Text = "Debug Log";
            this.DebugLogButton.UseVisualStyleBackColor = true;
            this.DebugLogButton.Click += new System.EventHandler(this.OnDebugLogButtonClick);
            // 
            // ErrorLogButton
            // 
            this.ErrorLogButton.Location = new System.Drawing.Point(12, 41);
            this.ErrorLogButton.Name = "ErrorLogButton";
            this.ErrorLogButton.Size = new System.Drawing.Size(75, 23);
            this.ErrorLogButton.TabIndex = 1;
            this.ErrorLogButton.Text = "Error Log";
            this.ErrorLogButton.UseVisualStyleBackColor = true;
            this.ErrorLogButton.Click += new System.EventHandler(this.OnErrorLogButtonClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ErrorLogButton);
            this.Controls.Add(this.DebugLogButton);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DebugLogButton;
        private System.Windows.Forms.Button ErrorLogButton;
    }
}

