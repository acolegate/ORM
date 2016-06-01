using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;

namespace ORM.Library.HelperFunctions.PropertyGrid
{
   public static class CsvFromStringList
   {
      /// <summary>Shows the dialog.</summary>
      /// <param name="availableItems">The available items.</param>
      /// <param name="selectedItems">The selected items.</param>
      /// <returns>A list of strings</returns>
      public static List<string> ShowDialog(IEnumerable<string> availableItems, List<string> selectedItems)
      {
         using (StringListForm stringListForm = new StringListForm(availableItems, selectedItems))
         {
            return stringListForm.ShowDialog() == DialogResult.OK ? stringListForm.SelectedItems : selectedItems;
         }
      }
   }

   public class CsvFromStringListUiTypeEditor : UITypeEditor
   {
      /// <summary>Edits the value.</summary>
      /// <param name="context">The context.</param>
      /// <param name="provider">The provider.</param>
      /// <param name="selectedItems">The selected items.</param>
      /// <returns>A populated Modeller object</returns>
      public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object selectedItems)
      {
         List<string> availableItems = new List<string>();

         MemberDescriptor memberDescriptor = context.PropertyDescriptor;
         if (memberDescriptor != null)
         {
            switch (memberDescriptor.Name)
            {
               case "EnumTables":
               case "IncludedTables":
                  {
                     availableItems = Modeller.RetrieveTableNames();
                     break;
                  }

               case "IncludedStoredProcedures":
                  {
                     availableItems = Modeller.RetrieveStoredProcedureNames();
                     break;
                  }

               case "IncludedViews":
                  {
                     availableItems = Modeller.RetrieveViewNames();
                     break;
                  }
            }
         }

         return EditValue(availableItems, selectedItems as List<string>);
      }

      /// <summary>
      /// Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" /> method.
      /// </summary>
      /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
      /// <returns>
      /// A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> value that indicates the style of editor used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" /> method. If the <see cref="T:System.Drawing.Design.UITypeEditor" /> does not support this method, then <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> will return <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" />.
      /// </returns>
      public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
      {
         return UITypeEditorEditStyle.Modal;
      }

      /// <summary>Edits the value.</summary>
      /// <param name="availableItems">The available items.</param>
      /// <param name="selectedItems">The selected items.</param>
      /// <returns>A list of strings</returns>
      private static List<string> EditValue(IEnumerable<string> availableItems, List<string> selectedItems)
      {
         return CsvFromStringList.ShowDialog(availableItems, selectedItems);
      }
   }

   public class CsvConverter : TypeConverter
   {
      /// <summary>
      /// Converts the given value object to the specified type, using the specified context and culture information.
      /// </summary>
      /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
      /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed.</param>
      /// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
      /// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to.</param>
      /// <returns>
      /// An <see cref="T:System.Object" /> that represents the converted value.
      /// </returns>
      public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
      {
         List<string> values = value as List<string>;

         if (destinationType == typeof(string) && values != null)
         {
            return string.Join(",", values.ToArray());
         }

         return base.ConvertTo(context, culture, value, destinationType);
      }
   }
}
