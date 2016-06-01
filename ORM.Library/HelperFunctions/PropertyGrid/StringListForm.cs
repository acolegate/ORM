using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ORM.Library.HelperFunctions.PropertyGrid
{
   public partial class StringListForm : Form
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="StringListForm" /> class.
      /// </summary>
      /// <param name="availableItems">The available items.</param>
      /// <param name="selectedItems">The selected items.</param>
      public StringListForm(IEnumerable<string> availableItems, ICollection<string> selectedItems)
      {
         InitializeComponent();

         if (selectedItems == null)
         {
            selectedItems = new List<string>();
         }

         foreach (string availableItem in availableItems)
         {
            checkedListBox.Items.Add(availableItem, selectedItems.Contains(availableItem));
         }
      }

      /// <summary>Gets the dialog result for the form.</summary>
      /// <value>The selected items.</value>
      /// <returns>A <see cref="T:System.Windows.Forms.DialogResult" /> that represents the result of the form when used as a dialog box.</returns>
      public List<string> SelectedItems
      {
         get
         {
            return checkedListBox.CheckedItems.Cast<string>().ToList();
         }
      }

      /// <summary>Handles the Click event of the AllButton control.</summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void AllButton_Click(object sender, EventArgs e)
      {
         for (int i = 0; i < checkedListBox.Items.Count; i++)
         {
            checkedListBox.SetItemChecked(i, true);
         }
      }

      /// <summary>Handles the Click event of the CancelButton control.</summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void CancelButton_Click(object sender, EventArgs e)
      {
         DialogResult = DialogResult.Cancel;
      }

      /// <summary>Handles the Click event of the NoneButton control.</summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void NoneButton_Click(object sender, EventArgs e)
      {
         for (int i = 0; i < checkedListBox.Items.Count; i++)
         {
            checkedListBox.SetItemChecked(i, false);
         }
      }

      /// <summary>Handles the Click event of the OkButton control.</summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void OkButton_Click(object sender, EventArgs e)
      {
         DialogResult = DialogResult.OK;
      }
   }
}
