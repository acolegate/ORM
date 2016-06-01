namespace ORM
{
	partial class ProgressForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.progressGroupBox = new System.Windows.Forms.GroupBox();
			this.codeGenerationProgressBar = new System.Windows.Forms.ProgressBar();
			this.messagesGroupBox = new System.Windows.Forms.GroupBox();
			this.messagesTextBox = new System.Windows.Forms.TextBox();
			this.closeButton = new System.Windows.Forms.Button();
			this.copyButton = new System.Windows.Forms.Button();
			this.progressGroupBox.SuspendLayout();
			this.messagesGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// progressGroupBox
			// 
			this.progressGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressGroupBox.Controls.Add(this.codeGenerationProgressBar);
			this.progressGroupBox.Location = new System.Drawing.Point(12, 12);
			this.progressGroupBox.Name = "progressGroupBox";
			this.progressGroupBox.Size = new System.Drawing.Size(568, 55);
			this.progressGroupBox.TabIndex = 0;
			this.progressGroupBox.TabStop = false;
			this.progressGroupBox.Text = "Progress";
			// 
			// codeGenerationProgressBar
			// 
			this.codeGenerationProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.codeGenerationProgressBar.Location = new System.Drawing.Point(13, 19);
			this.codeGenerationProgressBar.Name = "codeGenerationProgressBar";
			this.codeGenerationProgressBar.Size = new System.Drawing.Size(544, 23);
			this.codeGenerationProgressBar.TabIndex = 1;
			// 
			// messagesGroupBox
			// 
			this.messagesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.messagesGroupBox.Controls.Add(this.messagesTextBox);
			this.messagesGroupBox.Location = new System.Drawing.Point(12, 73);
			this.messagesGroupBox.Name = "messagesGroupBox";
			this.messagesGroupBox.Size = new System.Drawing.Size(568, 249);
			this.messagesGroupBox.TabIndex = 1;
			this.messagesGroupBox.TabStop = false;
			this.messagesGroupBox.Text = "Messages";
			// 
			// messagesTextBox
			// 
			this.messagesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.messagesTextBox.BackColor = System.Drawing.SystemColors.Window;
			this.messagesTextBox.Location = new System.Drawing.Point(13, 19);
			this.messagesTextBox.Multiline = true;
			this.messagesTextBox.Name = "messagesTextBox";
			this.messagesTextBox.ReadOnly = true;
			this.messagesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.messagesTextBox.Size = new System.Drawing.Size(544, 216);
			this.messagesTextBox.TabIndex = 1;
			// 
			// closeButton
			// 
			this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.closeButton.Location = new System.Drawing.Point(505, 338);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(75, 23);
			this.closeButton.TabIndex = 2;
			this.closeButton.Text = "&Close";
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// copyButton
			// 
			this.copyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.copyButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.copyButton.Location = new System.Drawing.Point(424, 338);
			this.copyButton.Name = "copyButton";
			this.copyButton.Size = new System.Drawing.Size(75, 23);
			this.copyButton.TabIndex = 3;
			this.copyButton.Text = "C&opy";
			this.copyButton.UseVisualStyleBackColor = true;
			this.copyButton.Click += new System.EventHandler(this.CopyButton_Click);
			// 
			// ProgressForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.closeButton;
			this.ClientSize = new System.Drawing.Size(592, 373);
			this.ControlBox = false;
			this.Controls.Add(this.copyButton);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.messagesGroupBox);
			this.Controls.Add(this.progressGroupBox);
			this.Name = "ProgressForm";
			this.ShowInTaskbar = false;
			this.Text = "ORM - Code Generation";
			this.progressGroupBox.ResumeLayout(false);
			this.messagesGroupBox.ResumeLayout(false);
			this.messagesGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox progressGroupBox;
		private System.Windows.Forms.ProgressBar codeGenerationProgressBar;
		private System.Windows.Forms.GroupBox messagesGroupBox;
		private System.Windows.Forms.TextBox messagesTextBox;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.Button copyButton;
	}
}