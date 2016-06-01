namespace ORM.Library.HelperFunctions.PropertyGrid
{
	partial class StringListForm
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
			this.availableItemsGroupBox = new System.Windows.Forms.GroupBox();
			this.noneButton = new System.Windows.Forms.Button();
			this.allButton = new System.Windows.Forms.Button();
			this.checkedListBox = new System.Windows.Forms.CheckedListBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.availableItemsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// availableItemsGroupBox
			// 
			this.availableItemsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.availableItemsGroupBox.Controls.Add(this.noneButton);
			this.availableItemsGroupBox.Controls.Add(this.allButton);
			this.availableItemsGroupBox.Controls.Add(this.checkedListBox);
			this.availableItemsGroupBox.Location = new System.Drawing.Point(12, 12);
			this.availableItemsGroupBox.Name = "availableItemsGroupBox";
			this.availableItemsGroupBox.Size = new System.Drawing.Size(305, 275);
			this.availableItemsGroupBox.TabIndex = 0;
			this.availableItemsGroupBox.TabStop = false;
			this.availableItemsGroupBox.Text = "Available items";
			// 
			// noneButton
			// 
			this.noneButton.Location = new System.Drawing.Point(255, 19);
			this.noneButton.Name = "noneButton";
			this.noneButton.Size = new System.Drawing.Size(44, 23);
			this.noneButton.TabIndex = 5;
			this.noneButton.Text = "None";
			this.noneButton.UseVisualStyleBackColor = true;
			this.noneButton.Click += new System.EventHandler(this.NoneButton_Click);
			// 
			// allButton
			// 
			this.allButton.Location = new System.Drawing.Point(205, 19);
			this.allButton.Name = "allButton";
			this.allButton.Size = new System.Drawing.Size(44, 23);
			this.allButton.TabIndex = 4;
			this.allButton.Text = "All";
			this.allButton.UseVisualStyleBackColor = true;
			this.allButton.Click += new System.EventHandler(this.AllButton_Click);
			// 
			// checkedListBox
			// 
			this.checkedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkedListBox.CheckOnClick = true;
			this.checkedListBox.FormattingEnabled = true;
			this.checkedListBox.IntegralHeight = false;
			this.checkedListBox.Location = new System.Drawing.Point(6, 48);
			this.checkedListBox.Name = "checkedListBox";
			this.checkedListBox.ScrollAlwaysVisible = true;
			this.checkedListBox.Size = new System.Drawing.Size(293, 221);
			this.checkedListBox.Sorted = true;
			this.checkedListBox.TabIndex = 0;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(242, 311);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(161, 311);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// StringListForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(329, 346);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.availableItemsGroupBox);
			this.Name = "StringListForm";
			this.Text = "Select Items";
			this.availableItemsGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox availableItemsGroupBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.CheckedListBox checkedListBox;
		private System.Windows.Forms.Button noneButton;
		private System.Windows.Forms.Button allButton;
	}
}