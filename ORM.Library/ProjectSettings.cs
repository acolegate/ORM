using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Windows.Forms.Design;
using System.Xml;

using ORM.Library.HelperFunctions;
using ORM.Library.HelperFunctions.PropertyGrid;

namespace ORM.Library
{
   public class ProjectSettings
   {
      private const string DefaultCommandsFileTemplate = "Templates/Commands_File.txt";
      private const string DefaultModelClassTemplate = "Templates/Model_Class.txt";
      private const string DefaultModelEnumTemplate = "Templates/Model_Enum.txt";
      private const string DefaultModelPropertyTemplate = "Templates/Model_Property.txt";
      private const string DefaultModelSortExtensionMethodTemplate = "Templates/Model_Sort_ExtensionMethod.txt";
      private const string DefaultModelTemplate = "Templates/Models_File.txt";
      private const string DefaultServiceClassTemplate = "Templates/Service_Class.txt";
      private const string DefaultServiceCreateTemplate = "Templates/Service_Create.txt";
      private const string DefaultServiceDeleteTemplate = "Templates/Service_Delete.txt";
      private const string DefaultServicePopulateModelPropertyEnumTemplate = "Templates/Service_Populate_Model_Property_Enum.txt";
      private const string DefaultServicePopulateModelPropertyNullableTemplate = "Templates/Service_Populate_Model_Property_Nullable.txt";
      private const string DefaultServicePopulateModelPropertyTemplate = "Templates/Service_Populate_Model_Property.txt";
      private const string DefaultServicePopulateModelTemplate = "Templates/Service_Populate_Model.txt";
      private const string DefaultServicePopulateSqlParamTemplate = "Templates/Service_Populate_Sql_Param.txt";
      private const string DefaultServicePopulateSqlParamTemplateEnum = "Templates/Service_Populate_Sql_Param_Enum.txt";
      private const string DefaultServicePopulateSqlStoredprocedureParamTemplate = "Templates/Service_Populate_Sql_StoredProcedure_Param.txt";
      private const string DefaultServiceRetrieveAllTemplate = "Templates/Service_Retrieve_All.txt";
      private const string DefaultServiceRetrieveTemplate = "Templates/Service_Retrieve.txt";
      private const string DefaultServiceStoredprocedureTemplate = "Templates/Service_StoredProcedure.txt";
      private const string DefaultServiceStoredprocedureVoidTemplate = "Templates/Service_StoredProcedure_Void.txt";
      private const string DefaultServiceStoredproceduresClassTemplate = "Templates/Service_StoredProcedures_Class.txt";
      private const string DefaultServiceUpdateTemplate = "Templates/Service_Update.txt";
      private const string DefaultServiceViewsClassTemplate = "Templates/Service_Views_Class.txt";
      private const string DefaultServicesFileTemplate = "Templates/Services_File.txt";

      /// <summary>
      /// Initializes a new instance of the <see cref="ProjectSettings" /> class.
      /// </summary>
      public ProjectSettings()
      {
         PopulateWithDefaults();
      }

      /// <summary>Initializes a new instance of the <see cref="ProjectSettings"/> class.</summary>
      /// <param name="pathAndFilename">The path and filename.</param>
      public ProjectSettings(string pathAndFilename)
      {
         SettingsPathAndFilename = pathAndFilename;
         ReadSettingsFromFile(pathAndFilename);
      }

      /// <summary>Gets or sets the name of the commands class.</summary>
      /// <value>The name of the commands class.</value>
      [Category("Naming")]
      [Description("The class name used for database commands i.e. ExecuteNonReader() etc.")]
      [DefaultValue("Commands")]
      [DisplayName("Commands Class")]
      public string CommandsClassName { get; set; }

      /// <summary>Gets or sets the connection string.</summary>
      /// <value>The connection string.</value>
      [Category("Database")]
      [Description("The string used to connect to the source database. This should be in the same form as that used in app/web.config ConnectionStrings attributes")]
      [Editor(typeof(ConnectionStringUiTypeEditor), typeof(UITypeEditor))]
      [DisplayName("Source")]
      public string ConnectionString { get; set; }

      /// <summary>Gets or sets the name of the connection string.</summary>
      /// <value>The name of the connection string.</value>
      [Category("Database")]
      [Description("The name used to identify to the connection string in the generated code i.e. \"main\"")]
      [DefaultValue("main")]
      [DisplayName("Connection String Name")]
      public string ConnectionStringName { get; set; }

      /// <summary>Gets or sets the custom dictionary file path and filename.</summary>
      /// <value>The custom dictionary file path and filename.</value>
      [Category("Dictionaries")]
      [Description("The path and filename of the custom dictionary file")]
      [DefaultValue("Dictionaries/CustomDictionary.txt")]
      [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
      [DisplayName("Custom")]
      public string CustomDictionaryFilePathAndFilename { get; set; }

      [Category("Naming")]
      [Description("The suffix to remove from a primary key column when referenced by an Enum i.e. CustomerTypeId becomes CustomerType when this is 'id'")]
      [DefaultValue("Id")]
      [DisplayName("Enum primary key column name to remove")]
      public string EnumPrimaryKeyColumnSuffixToRemove { get; set; }

      /// <summary>Gets or sets the enum suffix.</summary>
      /// <value>The enum suffix.</value>
      [Category("Naming")]
      [Description("The suffix used when refering to enums i.e. CustomerTypeEnum")]
      [DefaultValue("Enum")]
      [DisplayName("Enum Suffix")]
      public string EnumSuffix { get; set; }

      /// <summary>Gets or sets the enums.</summary>
      /// <value>The enums.</value>
      [Category("Entities")]
      [Description("Which tables should be used as enums")]
      [DisplayName("Enum tables")]
      [Editor(typeof(CsvFromStringListUiTypeEditor), typeof(UITypeEditor))]
      [TypeConverter(typeof(CsvConverter))]
      public List<string> EnumTables { get; set; }

      [Category("Naming")]
      [Description("Forces all Entities to be converted to Pascal Case i.e. customertypeid becomes CustomerTypeId")]
      [DefaultValue(false)]
      [DisplayName("Force Pascal Case")]
      public bool ForcePascalCase { get; set; }

      /// <summary>Gets or sets the included stored procedures.</summary>
      /// <value>The included stored procedures.</value>
      [Category("Entities")]
      [Description("The stored procedures to include in generated code")]
      [DisplayName("Stored procedures")]
      [Editor(typeof(CsvFromStringListUiTypeEditor), typeof(UITypeEditor))]
      [TypeConverter(typeof(CsvConverter))]
      public List<string> IncludedStoredProcedures { get; set; }

      /// <summary>Gets or sets the included tables.</summary>
      /// <value>The included tables.</value>
      [Category("Entities")]
      [Description("The tables to include in generated code")]
      [DisplayName("Tables")]
      [Editor(typeof(CsvFromStringListUiTypeEditor), typeof(UITypeEditor))]
      [TypeConverter(typeof(CsvConverter))]
      public List<string> IncludedTables { get; set; }

      /// <summary>Gets or sets the included views.</summary>
      /// <value>The included views.</value>
      [Category("Entities")]
      [Description("The views to include in generated code")]
      [DisplayName("Views")]
      [Editor(typeof(CsvFromStringListUiTypeEditor), typeof(UITypeEditor))]
      [TypeConverter(typeof(CsvConverter))]
      public List<string> IncludedViews { get; set; }

      /// <summary>Gets or sets the main dictionary file path and filename.</summary>
      /// <value>The main dictionary file path and filename.</value>
      [Category("Dictionaries")]
      [Description("The path and filename of the main dictionary file")]
      [DefaultValue("Dictionaries/Dictionary.txt")]
      [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
      [DisplayName("Main")]
      public string MainDictionaryFilePathAndFilename { get; set; }

      /// <summary>Gets or sets the name of the models class.</summary>
      /// <value>The name of the models class.</value>
      [Category("Naming")]
      [Description("The class name used for models")]
      [DefaultValue("Models")]
      [DisplayName("Models Class Name")]
      public string ModelClassName { get; set; }

      /// <summary>Gets or sets the models class suffix.</summary>
      /// <value>The models class suffix.</value>
      [Category("Naming")]
      [Description("The suffix used for methods when refering to models i.e. PopulateCustomerModel")]
      [DefaultValue("Model")]
      [DisplayName("Models Class Suffix")]
      public string ModelClassSuffix { get; set; }

      /// <summary>Gets or sets the namespace.</summary>
      /// <value>The namespace.</value>
      [Category("Naming")]
      [Description("The namespace used in the generated code files i.e. GeneratedCode")]
      [DefaultValue("GeneratedCode")]
      [DisplayName("Namespace")]
      public string Namespace { get; set; }

      /// <summary>Gets or sets the output commands class path and filename.</summary>
      /// <value>The output commands class path and filename.</value>
      [Category("Output")]
      [Description("The path and filename of the generated Commands class")]
      [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
      [DisplayName("Commands Class")]
      public string OutputCommandsClassPathAndFilename { get; set; }

      /// <summary>Gets or sets the output models class path and filename.</summary>
      /// <value>The output models class path and filename.</value>
      [Category("Output")]
      [Description("The path and filename of the generated Models class")]
      [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
      [DisplayName("Models Class")]
      public string OutputModelsClassPathAndFilename { get; set; }

      /// <summary>Gets or sets the output services class path and filename.</summary>
      /// <value>The output services class path and filename.</value>
      [Category("Output")]
      [Description("The path and filename of the generated Services class")]
      [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
      [DisplayName("Services Class")]
      public string OutputServicesClassPathAndFilename { get; set; }

      /// <summary>Gets or sets the reserved words file path and filename.</summary>
      /// <value>The reserved words file path and filename.</value>
      [Category("Dictionaries")]
      [Description("The path and filename of reserved words dictionary")]
      [DefaultValue("Dictionaries/ReservedWords.txt")]
      [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
      [DisplayName("Reserved Words")]
      public string ReservedWordsFilePathAndFilename { get; set; }

      /// <summary>Gets or sets the service class suffix.</summary>
      /// <value>The service class suffix.</value>
      [Category("Naming")]
      [Description("The Suffix used for methods when refering to services i.e. CustomerService")]
      [DefaultValue("Service")]
      [DisplayName("Service Class Suffix")]
      public string ServiceClassSuffix { get; set; }

      /// <summary>Gets or sets the settings path and filename.</summary>
      /// <value>The settings path and filename.</value>
      [Browsable(false)]
      public string SettingsPathAndFilename { get; set; }

      /// <summary>Gets or sets the name of the stored procedures class.</summary>
      /// <value>The name of the stored procedures class.</value>
      [Category("Naming")]
      [Description("The class name used for stored procedures i.e. StoredProcedures")]
      [DefaultValue("StoredProcedures")]
      [DisplayName("Stored Procedures Class")]
      public string StoredProceduresClassName { get; set; }

      /// <summary>Gets or sets the templates.</summary>
      /// <value>The templates.</value>
      [Category("Templates")]
      [Description("The templates used during code generation")]
      [DisplayName("Templates")]
      public Dictionary<TemplateTypeEnum, Template> Templates { get; set; }

      /// <summary>Gets or sets the name of the views class.</summary>
      /// <value>The name of the views class.</value>
      [Category("Naming")]
      [Description("The class name used for views i.e. Views")]
      [DefaultValue("Views")]
      [DisplayName("View Class Name")]
      public string ViewsClassName { get; set; }

      /// <summary>Saves the settings to file.</summary>
      public void SaveSettingsToFile()
      {
         SaveSettingsToFile(SettingsPathAndFilename);
      }

      /// <summary>Saves the settings to file.</summary>
      /// <param name="filename">The filename.</param>
      public void SaveSettingsToFile(string filename)
      {
         const string DocTemplate = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<settings>\r\n{0}\r\n\t<output>\r\n{1}\t</output>\r\n\r\n\t<wordfiles>\r\n{2}\t</wordfiles>\r\n\r\n\t<naming>\r\n{3}\t</naming>\r\n\r\n\t<templates>\r\n{4}\t</templates>\r\n\r\n\t<entities>\r\n{5}\t</entities>\r\n\r\n</settings>";
         const string ConnectionStringTemplate = "\t<connectionstring name=\"{0}\" value=\"{1}\"/>\r\n";
         const string AttributeTemplate = "\t\t<{0} value=\"{1}\" />\r\n";
         const string TemplateTemplate = "\t\t<template type=\"{0}\" filename=\"{1}\" />\r\n";

         const string IncludedStoredProcedureRowTemplate = "\t\t\t<storedprocedure sqlname=\"{0}\"/>\r\n";
         const string IncludedTableRowTemplate = "\t\t\t<table sqlname=\"{0}\" enum=\"{1}\"/>\r\n";
         const string IncludedViewRowTemplate = "\t\t\t<view sqlname=\"{0}\"/>\r\n";

         const string EntitiesTemplate = "\t\t<tables>\r\n{0}\t\t</tables>\r\n\t\t<storedprocedures>\r\n{1}\t\t</storedprocedures>\r\n\t\t<views>\r\n{2}\t\t</views>\r\n";

         string connectionString = string.Format(ConnectionStringTemplate, ConnectionStringName, ConnectionString);

         string output = string.Empty;
         output += string.Format(AttributeTemplate, "commandsclasspathandfilename", OutputCommandsClassPathAndFilename);
         output += string.Format(AttributeTemplate, "modelsclasspathandfilename", OutputModelsClassPathAndFilename);
         output += string.Format(AttributeTemplate, "servicesclasspathandfilename", OutputServicesClassPathAndFilename);

         string wordfiles = string.Empty;
         wordfiles += string.Format(AttributeTemplate, "maindictionaryfilename", MainDictionaryFilePathAndFilename);
         wordfiles += string.Format(AttributeTemplate, "reservedwordsfilename", ReservedWordsFilePathAndFilename);
         wordfiles += string.Format(AttributeTemplate, "customdictionaryfilename", CustomDictionaryFilePathAndFilename);

         string naming = string.Empty;
         naming += string.Format(AttributeTemplate, "namespace", Namespace);
         naming += string.Format(AttributeTemplate, "modelclassname", ModelClassName);
         naming += string.Format(AttributeTemplate, "commandsclassname", CommandsClassName);
         naming += string.Format(AttributeTemplate, "serviceclasssuffix", ServiceClassSuffix);
         naming += string.Format(AttributeTemplate, "modelclasssuffix", ModelClassSuffix);
         naming += string.Format(AttributeTemplate, "enumsuffix", EnumSuffix);
         naming += string.Format(AttributeTemplate, "enumprimarykeycolumnsuffixtoremove", EnumPrimaryKeyColumnSuffixToRemove);
         naming += string.Format(AttributeTemplate, "viewsclassname", ViewsClassName);
         naming += string.Format(AttributeTemplate, "storedproceduresclassname", StoredProceduresClassName);
         naming += string.Format(AttributeTemplate, "forcepascalcase", ForcePascalCase);

         string templates = string.Empty;
         templates += string.Format(TemplateTemplate, "commandsfile", Templates[TemplateTypeEnum.CommandsFile].Filename);
         templates += string.Format(TemplateTemplate, "modelenum", Templates[TemplateTypeEnum.ModelEnum].Filename);
         templates += string.Format(TemplateTemplate, "modelsfile", Templates[TemplateTypeEnum.ModelsFile].Filename);
         templates += string.Format(TemplateTemplate, "modelproperty", Templates[TemplateTypeEnum.ModelProperty].Filename);
         templates += string.Format(TemplateTemplate, "modelclass", Templates[TemplateTypeEnum.ModelClass].Filename);
         templates += string.Format(TemplateTemplate, "serviceclass", Templates[TemplateTypeEnum.ServiceClass].Filename);
         templates += string.Format(TemplateTemplate, "servicecreate", Templates[TemplateTypeEnum.ServiceCreate].Filename);
         templates += string.Format(TemplateTemplate, "servicedelete", Templates[TemplateTypeEnum.ServiceDelete].Filename);
         templates += string.Format(TemplateTemplate, "servicesfile", Templates[TemplateTypeEnum.ServicesFile].Filename);
         templates += string.Format(TemplateTemplate, "serviceretrieve", Templates[TemplateTypeEnum.ServiceRetrieve].Filename);
         templates += string.Format(TemplateTemplate, "serviceretrieveall", Templates[TemplateTypeEnum.ServiceRetrieveAll].Filename);
         templates += string.Format(TemplateTemplate, "serviceupdate", Templates[TemplateTypeEnum.ServiceUpdate].Filename);
         templates += string.Format(TemplateTemplate, "servicepopulatemodel", Templates[TemplateTypeEnum.ServicePopulateModel].Filename);
         templates += string.Format(TemplateTemplate, "servicepopulatemodelproperty", Templates[TemplateTypeEnum.ServicePopulateModelProperty].Filename);
         templates += string.Format(TemplateTemplate, "servicepopulatemodelpropertynullable", Templates[TemplateTypeEnum.ServicePopulateModelPropertyNullable].Filename);
         templates += string.Format(TemplateTemplate, "servicepopulatemodelpropertyenum", Templates[TemplateTypeEnum.ServicePopulateModelPropertyEnum].Filename);
         templates += string.Format(TemplateTemplate, "servicepopulatesqlparam", Templates[TemplateTypeEnum.ServicePopulateSqlParam].Filename);
         templates += string.Format(TemplateTemplate, "servicepopulatesqlparamenum", Templates[TemplateTypeEnum.ServicePopulateSqlParamEnum].Filename);
         templates += string.Format(TemplateTemplate, "servicestoredprocedure", Templates[TemplateTypeEnum.ServiceStoredProcedure].Filename);
         templates += string.Format(TemplateTemplate, "servicestoredprocedurevoid", Templates[TemplateTypeEnum.ServiceStoredProcedureVoid].Filename);
         templates += string.Format(TemplateTemplate, "servicestoredproceduresclass", Templates[TemplateTypeEnum.ServiceStoredProceduresClass].Filename);
         templates += string.Format(TemplateTemplate, "servicepopulatesqlstoredProcedureparam", Templates[TemplateTypeEnum.ServicePopulateSqlStoredProcedureParam].Filename);
         templates += string.Format(TemplateTemplate, "serviceviewsclass", Templates[TemplateTypeEnum.ServiceViewsClass].Filename);
         templates += string.Format(TemplateTemplate, "modelsortextensionmethod", Templates[TemplateTypeEnum.ModelSortExtensionMethod].Filename);

         // write included entries
         string includedTablesXml = string.Empty;
         string includedStoredProceduresXml = string.Empty;
         string includedViewsXml = string.Empty;

         includedTablesXml = IncludedTables.Aggregate(includedTablesXml, (current, table) => current + string.Format(IncludedTableRowTemplate, table, EnumTables.Contains(table)));
         includedStoredProceduresXml = IncludedStoredProcedures.Aggregate(includedStoredProceduresXml, (current, procedure) => current + string.Format(IncludedStoredProcedureRowTemplate, procedure));
         includedViewsXml = IncludedViews.Aggregate(includedViewsXml, (current, view) => current + string.Format(IncludedViewRowTemplate, view));

         string entities = string.Format(EntitiesTemplate, includedTablesXml, includedStoredProceduresXml, includedViewsXml);

         Io.WriteTextFile(filename, string.Format(DocTemplate, connectionString, output, wordfiles, naming, templates, entities));
      }

      /// <summary>Gets the attribute string value.</summary>
      /// <param name="xmlNode">The XML node.</param>
      /// <param name="attributeName">Name of the attribute.</param>
      /// <returns>The Attribute value as a string</returns>
      private static string GetAttributeValue(XmlNode xmlNode, string attributeName)
      {
         string returnValue = null;

         if (xmlNode != null)
         {
            if (xmlNode.Attributes != null)
            {
               returnValue = xmlNode.Attributes[attributeName].Value;
            }
         }

         return returnValue;
      }

      /// <summary>Parses the type of the template.</summary>
      /// <param name="templateType">Type of the template.</param>
      /// <returns>A template type</returns>
      private static TemplateTypeEnum ParseTemplateType(string templateType)
      {
         return (TemplateTypeEnum)Enum.Parse(typeof(TemplateTypeEnum), templateType, true);
      }

      /// <summary>Reads a text file.</summary>
      /// <param name="filename">The filename.</param>
      /// <returns>The contents of the file a a string</returns>
      private static string ReadTextFile(string filename)
      {
         return new StreamReader(filename).ReadToEnd();
      }

      /// <summary>Populates this instance with defaults.</summary>
      private void PopulateWithDefaults()
      {
         IncludedStoredProcedures = new List<string>();
         IncludedTables = new List<string>();
         IncludedViews = new List<string>();

         CommandsClassName = "Commands";
         ConnectionStringName = "main";
         CustomDictionaryFilePathAndFilename = "Dictionaries/CustomDictionary.txt";
         MainDictionaryFilePathAndFilename = "Dictionaries/Dictionary.txt";
         ReservedWordsFilePathAndFilename = "Dictionaries/ReservedWords.txt";
         ModelClassName = "Models";
         ModelClassSuffix = "Model";
         EnumSuffix = "Enum";
         EnumPrimaryKeyColumnSuffixToRemove = "Id";
         Namespace = "GeneratedCode";
         ServiceClassSuffix = "Service";
         StoredProceduresClassName = "StoredProcedures";
         ViewsClassName = "Views";
         ForcePascalCase = false;

         Templates = new Dictionary<TemplateTypeEnum, Template> {
                                                                   { TemplateTypeEnum.CommandsFile, new Template {
                                                                                                                    Filename = DefaultCommandsFileTemplate, 
                                                                                                                    FileContents = ReadTextFile(DefaultCommandsFileTemplate)
                                                                                                                 } }, 
                                                                   { TemplateTypeEnum.ModelsFile, new Template {
                                                                                                                  Filename = DefaultModelTemplate, 
                                                                                                                  FileContents = ReadTextFile(DefaultModelTemplate)
                                                                                                               } }, 
                                                                   { TemplateTypeEnum.ModelProperty, new Template {
                                                                                                                     Filename = DefaultModelPropertyTemplate, 
                                                                                                                     FileContents = ReadTextFile(DefaultModelPropertyTemplate)
                                                                                                                  } }, 
                                                                   { TemplateTypeEnum.ModelClass, new Template {
                                                                                                                  Filename = DefaultModelClassTemplate, 
                                                                                                                  FileContents = ReadTextFile(DefaultModelClassTemplate)
                                                                                                               } }, 
                                                                   { TemplateTypeEnum.ServiceClass, new Template {
                                                                                                                    Filename = DefaultServiceClassTemplate, 
                                                                                                                    FileContents = ReadTextFile(DefaultServiceClassTemplate)
                                                                                                                 } }, 
                                                                   { TemplateTypeEnum.ServiceCreate, new Template {
                                                                                                                     Filename = DefaultServiceCreateTemplate, 
                                                                                                                     FileContents = ReadTextFile(DefaultServiceCreateTemplate)
                                                                                                                  } }, 
                                                                   { TemplateTypeEnum.ServiceDelete, new Template {
                                                                                                                     Filename = DefaultServiceDeleteTemplate, 
                                                                                                                     FileContents = ReadTextFile(DefaultServiceDeleteTemplate)
                                                                                                                  } }, 
                                                                   { TemplateTypeEnum.ServicesFile, new Template {
                                                                                                                    Filename = DefaultServicesFileTemplate, 
                                                                                                                    FileContents = ReadTextFile(DefaultServicesFileTemplate)
                                                                                                                 } }, 
                                                                   { TemplateTypeEnum.ServicePopulateModel, new Template {
                                                                                                                            Filename = DefaultServicePopulateModelTemplate, 
                                                                                                                            FileContents = ReadTextFile(DefaultServicePopulateModelTemplate)
                                                                                                                         } }, 
                                                                   { TemplateTypeEnum.ServicePopulateModelProperty, new Template {
                                                                                                                                    Filename = DefaultServicePopulateModelPropertyTemplate, 
                                                                                                                                    FileContents = ReadTextFile(DefaultServicePopulateModelPropertyTemplate)
                                                                                                                                 } }, 
                                                                   { TemplateTypeEnum.ServicePopulateModelPropertyNullable, new Template {
                                                                                                                                            Filename = DefaultServicePopulateModelPropertyNullableTemplate, 
                                                                                                                                            FileContents = ReadTextFile(DefaultServicePopulateModelPropertyNullableTemplate)
                                                                                                                                         } }, 
                                                                   { TemplateTypeEnum.ServicePopulateModelPropertyEnum, new Template {
                                                                                                                                        Filename = DefaultServicePopulateModelPropertyEnumTemplate, 
                                                                                                                                        FileContents = ReadTextFile(DefaultServicePopulateModelPropertyEnumTemplate)
                                                                                                                                     } }, 
                                                                   { TemplateTypeEnum.ServicePopulateSqlParam, new Template {
                                                                                                                               Filename = DefaultServicePopulateSqlParamTemplate, 
                                                                                                                               FileContents = ReadTextFile(DefaultServicePopulateSqlParamTemplate)
                                                                                                                            } }, 
                                                                   { TemplateTypeEnum.ServicePopulateSqlParamEnum, new Template {
                                                                                                                                   Filename = DefaultServicePopulateSqlParamTemplateEnum, 
                                                                                                                                   FileContents = ReadTextFile(DefaultServicePopulateSqlParamTemplateEnum)
                                                                                                                                } }, 
                                                                   { TemplateTypeEnum.ServicePopulateSqlStoredProcedureParam, new Template {
                                                                                                                                              Filename = DefaultServicePopulateSqlStoredprocedureParamTemplate, 
                                                                                                                                              FileContents = ReadTextFile(DefaultServicePopulateSqlStoredprocedureParamTemplate)
                                                                                                                                           } }, 
                                                                   { TemplateTypeEnum.ServiceRetrieve, new Template {
                                                                                                                       Filename = DefaultServiceRetrieveTemplate, 
                                                                                                                       FileContents = ReadTextFile(DefaultServiceRetrieveTemplate)
                                                                                                                    } }, 
                                                                   { TemplateTypeEnum.ServiceRetrieveAll, new Template {
                                                                                                                          Filename = DefaultServiceRetrieveAllTemplate, 
                                                                                                                          FileContents = ReadTextFile(DefaultServiceRetrieveAllTemplate)
                                                                                                                       } }, 
                                                                   { TemplateTypeEnum.ServiceStoredProcedure, new Template {
                                                                                                                              Filename = DefaultServiceStoredprocedureTemplate, 
                                                                                                                              FileContents = ReadTextFile(DefaultServiceStoredprocedureTemplate)
                                                                                                                           } }, 
                                                                   { TemplateTypeEnum.ServiceStoredProceduresClass, new Template {
                                                                                                                                    Filename = DefaultServiceStoredproceduresClassTemplate, 
                                                                                                                                    FileContents = ReadTextFile(DefaultServiceStoredproceduresClassTemplate)
                                                                                                                                 } }, 
                                                                   { TemplateTypeEnum.ServiceStoredProcedureVoid, new Template {
                                                                                                                                  Filename = DefaultServiceStoredprocedureVoidTemplate, 
                                                                                                                                  FileContents = ReadTextFile(DefaultServiceStoredprocedureVoidTemplate)
                                                                                                                               } }, 
                                                                   { TemplateTypeEnum.ServiceUpdate, new Template {
                                                                                                                     Filename = DefaultServiceUpdateTemplate, 
                                                                                                                     FileContents = ReadTextFile(DefaultServiceUpdateTemplate)
                                                                                                                  } }, 
                                                                   { TemplateTypeEnum.ServiceViewsClass, new Template {
                                                                                                                         Filename = DefaultServiceViewsClassTemplate, 
                                                                                                                         FileContents = ReadTextFile(DefaultServiceViewsClassTemplate)
                                                                                                                      } }, 
                                                                   { TemplateTypeEnum.ModelEnum, new Template {
                                                                                                                 Filename = DefaultModelEnumTemplate, 
                                                                                                                 FileContents = ReadTextFile(DefaultModelEnumTemplate)
                                                                                                              } }, 
                                                                   { TemplateTypeEnum.ModelSortExtensionMethod, new Template {
                                                                                                                                Filename = DefaultModelSortExtensionMethodTemplate, 
                                                                                                                                FileContents = ReadTextFile(DefaultModelSortExtensionMethodTemplate)
                                                                                                                             } }
                                                                };
      }

      /// <summary>Reads the settings from file.</summary>
      /// <param name="filename">The filename.</param>
      private void ReadSettingsFromFile(string filename)
      {
         Templates = new Dictionary<TemplateTypeEnum, Template>();

         IncludedViews = new List<string>();
         IncludedTables = new List<string>();
         IncludedStoredProcedures = new List<string>();
         EnumTables = new List<string>();

         XmlDocument xmlDocument = new XmlDocument();
         xmlDocument.Load(filename);

         XmlNode xmlRootNode = xmlDocument.SelectSingleNode("settings");

         if (xmlRootNode != null)
         {
            ConnectionStringName = GetAttributeValue(xmlRootNode.SelectSingleNode("connectionstring"), "name");
            ConnectionString = GetAttributeValue(xmlRootNode.SelectSingleNode("connectionstring"), "value");

            XmlNode outputNode = xmlRootNode.SelectSingleNode("output");
            if (outputNode != null)
            {
               OutputCommandsClassPathAndFilename = GetAttributeValue(outputNode.SelectSingleNode("commandsclasspathandfilename"), "value");
               OutputModelsClassPathAndFilename = GetAttributeValue(outputNode.SelectSingleNode("modelsclasspathandfilename"), "value");
               OutputServicesClassPathAndFilename = GetAttributeValue(outputNode.SelectSingleNode("servicesclasspathandfilename"), "value");
            }

            XmlNode wordFilesNode = xmlRootNode.SelectSingleNode("wordfiles");
            if (wordFilesNode != null)
            {
               MainDictionaryFilePathAndFilename = GetAttributeValue(wordFilesNode.SelectSingleNode("maindictionaryfilename"), "value");
               ReservedWordsFilePathAndFilename = GetAttributeValue(wordFilesNode.SelectSingleNode("reservedwordsfilename"), "value");
               CustomDictionaryFilePathAndFilename = GetAttributeValue(wordFilesNode.SelectSingleNode("customdictionaryfilename"), "value");
            }

            XmlNode namingRootNode = xmlRootNode.SelectSingleNode("naming");
            if (namingRootNode != null)
            {
               Namespace = GetAttributeValue(namingRootNode.SelectSingleNode("namespace"), "value");
               ModelClassName = GetAttributeValue(namingRootNode.SelectSingleNode("modelclassname"), "value");
               CommandsClassName = GetAttributeValue(namingRootNode.SelectSingleNode("commandsclassname"), "value");
               ServiceClassSuffix = GetAttributeValue(namingRootNode.SelectSingleNode("serviceclasssuffix"), "value");
               ModelClassSuffix = GetAttributeValue(namingRootNode.SelectSingleNode("modelclasssuffix"), "value");
               EnumSuffix = GetAttributeValue(namingRootNode.SelectSingleNode("enumsuffix"), "value");
               EnumPrimaryKeyColumnSuffixToRemove = GetAttributeValue(namingRootNode.SelectSingleNode("enumprimarykeycolumnsuffixtoremove"), "value");
               ViewsClassName = GetAttributeValue(namingRootNode.SelectSingleNode("viewsclassname"), "value");
               StoredProceduresClassName = GetAttributeValue(namingRootNode.SelectSingleNode("storedproceduresclassname"), "value");
               ForcePascalCase = bool.Parse(GetAttributeValue(namingRootNode.SelectSingleNode("forcepascalcase"), "value"));
            }

            XmlNode templatesNode = xmlRootNode.SelectSingleNode("templates");
            if (templatesNode != null)
            {
               string templateFilename;

               foreach (XmlNode templateNode in templatesNode.ChildNodes)
               {
                  templateFilename = GetAttributeValue(templateNode, "filename");
                  Templates.Add(ParseTemplateType(GetAttributeValue(templateNode, "type")), new Template {
                                                                                                            Filename = templateFilename, 
                                                                                                            FileContents = ReadTextFile(templateFilename)
                                                                                                         });
               }
            }

            XmlNode entitiesNode = xmlRootNode.SelectSingleNode("entities");

            if (entitiesNode != null)
            {
               string sqlName;

               XmlNode tablesNode = entitiesNode.SelectSingleNode("tables");
               if (tablesNode != null)
               {
                  foreach (XmlNode tableNode in tablesNode.ChildNodes)
                  {
                     sqlName = GetAttributeValue(tableNode, "sqlname");

                     IncludedTables.Add(sqlName);

                     if (bool.Parse(GetAttributeValue(tableNode, "enum")))
                     {
                        EnumTables.Add(sqlName);
                     }
                  }
               }

               XmlNode storedProceduresNode = entitiesNode.SelectSingleNode("storedprocedures");
               if (storedProceduresNode != null)
               {
                  foreach (XmlNode storedProcedureNode in storedProceduresNode.ChildNodes)
                  {
                     IncludedStoredProcedures.Add(GetAttributeValue(storedProcedureNode, "sqlname"));
                  }
               }

               XmlNode viewsNode = entitiesNode.SelectSingleNode("views");
               if (viewsNode != null)
               {
                  foreach (XmlNode viewNode in viewsNode.ChildNodes)
                  {
                     IncludedStoredProcedures.Add(GetAttributeValue(viewNode, "sqlname"));
                  }
               }
            }
         }
      }

      public struct Template
      {
         public string FileContents { get; set; }
         public string Filename { get; set; }
      }
   }
}
