using System;
using System.Reflection;
using System.Windows.Forms;

namespace ORM.HelperFunctions
{
	internal static class PropertyGrid
	{
		/// <summary>Gets the property grid splitter position.</summary>
		/// <param name="propertyGrid">The property grid.</param>
		/// <returns>The splitter position</returns>
		public static int GetPropertyGridSplitterPosition(System.Windows.Forms.PropertyGrid propertyGrid)
		{
			int width = -1;

			Type realType = propertyGrid.GetType();
			while (realType != null
			       && realType != typeof(System.Windows.Forms.PropertyGrid))
			{
				realType = realType.BaseType;
			}

			if (realType != null)
			{
				FieldInfo fieldInfo = realType.GetField(@"gridView", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);

				if (fieldInfo != null)
				{
					object gridView = fieldInfo.GetValue(propertyGrid);

					PropertyInfo propertyInfo = gridView.GetType().GetProperty("InternalLabelWidth", BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance);
					width = (int)propertyInfo.GetValue(gridView, null);
				}
			}

			return width;
		}

		/// <summary>Resizes the property grid splitter by pixels.</summary>
		/// <param name="propertyGrid">The property grid.</param>
		/// <param name="widthInPixels">The width in pixels.</param>
		public static void ResizePropertyGridSplitterByPixels(System.Windows.Forms.PropertyGrid propertyGrid, int widthInPixels)
		{
			// Go up in hierarchy until found real property grid type.
			Type realType = propertyGrid.GetType();
			while (realType != null
			       && realType != typeof(System.Windows.Forms.PropertyGrid))
			{
				realType = realType.BaseType;
			}

			if (realType != null)
			{
				FieldInfo gridViewField = realType.GetField(@"gridView", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);

				if (gridViewField != null)
				{
					object gridView = gridViewField.GetValue(propertyGrid);

					MethodInfo moveSplitterToMethod = gridView.GetType().GetMethod(@"MoveSplitterTo", BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance);
					moveSplitterToMethod.Invoke(gridView, new object[] { widthInPixels });
				}
			}
		}

		/// <summary>Resizes the description area.</summary>
		/// <param name="propertyGrid">The grid.</param>
		/// <param name="lines">The lines.</param>
		public static void ResizeDescriptionArea(System.Windows.Forms.PropertyGrid propertyGrid, int lines)
		{
			const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic;

			PropertyInfo info = propertyGrid.GetType().GetProperty("Controls");
			Control.ControlCollection collection = (Control.ControlCollection)info.GetValue(propertyGrid, null);

			foreach (Control control in collection)
			{
				Type type = control.GetType();

				if ("DocComment" == type.Name)
				{
					if (type.BaseType != null)
					{
						FieldInfo field = type.BaseType.GetField("userSized", Flags);
						if (field != null)
						{
							field.SetValue(control, true);
						}
					}

					info = type.GetProperty("Lines");
					info.SetValue(control, lines, null);

					//propertyGrid.HelpVisible = true;
					break;
				}
			}
		}
	}
}