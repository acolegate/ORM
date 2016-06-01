using System;
using System.Windows.Forms;

namespace ORM
{
   public partial class ProgressForm : Form
   {
      public ProgressForm()
      {
         InitializeComponent();
      }

      /// <summary>Sets a value indicating whether the buttons are enabled.</summary>
      /// <value>The buttons are enabled.</value>
      public bool ButtonsAreEnabled
      {
         set
         {
            copyButton.Enabled = value;
            closeButton.Enabled = value;
         }
      }

      /// <summary>Sets a value indicating the progress maximum.</summary>
      /// <value>The progress maximum.</value>
      public int ProgressMaximum
      {
         set
         {
            codeGenerationProgressBar.Maximum = value;
         }
      }

      /// <summary>Adds the message.</summary>
      /// <param name="message">The message.</param>
      /// <value>The messages.</value>
      public void AddMessage(string message)
      {
         if (string.IsNullOrEmpty(messagesTextBox.Text) == false)
         {
            messagesTextBox.Text += "\r\n";
         }

         messagesTextBox.Text += message;

         messagesTextBox.SelectionStart = messagesTextBox.Text.Length;
         messagesTextBox.ScrollToCaret();
      }

      /// <summary>Clears the messages.</summary>
      public void ClearMessages()
      {
         messagesTextBox.Clear();
      }

      /// <summary>Increments the progress.</summary>
      public void IncrementProgressBar()
      {
         // HACK to make sure the progress bar never overflows - This needs to be fixed
         if (codeGenerationProgressBar.Value == codeGenerationProgressBar.Maximum)
         {
            codeGenerationProgressBar.Maximum++;
         }

         codeGenerationProgressBar.Value++;
      }

      /// <summary>Resets the progress bar.</summary>
      public void ResetProgressBar()
      {
         codeGenerationProgressBar.Value = 0;
      }

      /// <summary>Handles the Click event of the CloseButton control.</summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void CloseButton_Click(object sender, EventArgs e)
      {
         Hide();
      }

      /// <summary>Handles the Click event of the CopyButton control.</summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void CopyButton_Click(object sender, EventArgs e)
      {
         Clipboard.SetText(messagesTextBox.Text);
         MessageBox.Show(this, "Copied to clipboard", "Caption", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
   }
}
