namespace ORM.Library
{
   public enum TemplateTypeEnum
   {
      ModelsFile, 
      ModelClass, 
      ModelProperty, 
      ServicesFile, 
      ServiceClass, 
      ServiceCreate, 
      ServiceRetrieve, 
      ServiceRetrieveAll, 
      ServiceUpdate, 
      ServiceDelete, 
      ServicePopulateModel, 
      ServicePopulateModelProperty, 
      ServicePopulateModelPropertyNullable, 
      ServicePopulateModelPropertyEnum, 
      CommandsFile, 
      ServicePopulateSqlParam, 
      ServicePopulateSqlParamEnum, 
      ServiceStoredProcedure, 
      ServiceStoredProcedureVoid, 
      ServiceStoredProceduresClass, 
      ServicePopulateSqlStoredProcedureParam, 
      ServiceViewsClass, 
      ModelEnum, 
      ModelSortExtensionMethod
   }

   public sealed class Settings
   {
      private static readonly Settings InternalInstance = new Settings();

      /// <summary>
      /// Prevents a default instance of the <see cref="Settings" /> class from being created.
      /// </summary>
      private Settings()
      {
         // REQUIRED BY THE SINGLETON - DO NOT REMOVE THIS EMPTY CONSTRUCTOR
      }

      /// <summary>Gets the instance.</summary>
      /// <value>The instance.</value>
      public static Settings Instance
      {
         get
         {
            return InternalInstance;
         }
      }

      /// <summary>Gets or sets the project settings.</summary>
      /// <value>The project settings.</value>
      public ProjectSettings ProjectSettings { get; set; }
   }
}
