using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;

namespace ORM.Library.HelperFunctions.PropertyGrid
{
   public static class ConnectionString
   {
      /// <summary>Shows the dialog.</summary>
      /// <param name="connectionString">The connection string.</param>
      /// <returns>The connection string</returns>
      public static string ShowDialog(string connectionString)
      {
         string retVal = string.Empty;

         Type msdascType = Type.GetTypeFromProgID("DataLinks");
         object adoConn;
         object msdascLink = Activator.CreateInstance(msdascType);

         if (string.IsNullOrEmpty(connectionString))
         {
            adoConn = msdascType.InvokeMember("PromptNew", BindingFlags.Instance | BindingFlags.InvokeMethod, null, msdascLink, null);

            if (adoConn != null)
            {
               retVal = TypeDescriptor.GetProperties(adoConn)["ConnectionString"].GetValue(adoConn) as string;
            }
         }
         else
         {
            adoConn = Activator.CreateInstance(Type.GetTypeFromProgID("ADODB.Connection"));
            TypeDescriptor.GetProperties(adoConn)["ConnectionString"].SetValue(adoConn, connectionString);

            retVal = (bool)msdascType.InvokeMember("PromptEdit", BindingFlags.Instance | BindingFlags.InvokeMethod, null, msdascLink, new[] { adoConn }) ? TypeDescriptor.GetProperties(adoConn)["ConnectionString"].GetValue(adoConn) as string : connectionString;
         }

         // remove the "provider=" key value pair
         if (retVal != null)
         {
            retVal = retVal.Split(';').Where(keyvaluepair => keyvaluepair.StartsWith("provider=", StringComparison.OrdinalIgnoreCase) == false).Aggregate(string.Empty, (current, keyvaluepair) => current + keyvaluepair + ';');
         }

         return retVal;
      }
   }

   public class ConnectionStringUiTypeEditor : UITypeEditor
   {
      /// <summary>
      /// Edits the specified object's value using the editor style indicated by the <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> method.
      /// </summary>
      /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
      /// <param name="provider">An <see cref="T:System.IServiceProvider" /> that this editor can use to obtain services.</param>
      /// <param name="value">The object to edit.</param>
      /// <returns>
      /// The new value of the object. If the value of the object has not changed, this should return the same object it was passed.
      /// </returns>
      public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
      {
         return EditValue(value as string);
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
      /// <param name="value">The value.</param>
      /// <returns>The edited value</returns>
      private static string EditValue(string value)
      {
         return ConnectionString.ShowDialog(value);
      }
   }
}
