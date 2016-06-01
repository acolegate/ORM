using System;
using System.Windows.Forms;

using ORM.Library;
using ORM.Library.HelperFunctions;

using PropertyGrid = ORM.HelperFunctions.PropertyGrid;

namespace ORM
{
   public partial class MainForm : Form
   {
      private const string NewProjectFilename = "NewProject.xml";
      private const int PropertyGridDescriptionLines = 3;
      private const int PropertyGridLabelColumnWidth = 150;
      private const string TitleBarPrefix = "ORM";

      private readonly ProgressForm _progressForm;
      private Modeller _modeller;
      private ProjectFileState _projectFileState;
      private ProjectSettings _projectSettings;

      /// <summary>
      /// Initializes a new instance of the <see cref="MainForm" /> class.
      /// </summary>
      public MainForm()
      {
         InitializeComponent();

         _projectFileState = ProjectFileState.NoProjectLoaded;
         _progressForm = new ProgressForm();

         UpdateStatusBar("No project loaded");
      }

      private enum ProjectFileState
      {
         NoProjectLoaded, 
         NewUnsavedProject, 
         ChangedUnsavedProject, 
         SavedUnchangedProject
      }

      /// <summary>Closes the app.</summary>
      private static void CloseApp()
      {
         Application.Exit();
      }

      /// <summary>Closes the project.</summary>
      private void CloseProject()
      {
         _projectFileState = ProjectFileState.NoProjectLoaded;

         propertyGrid.SelectedObject = null;
         propertyGrid.Refresh();

         UpdateFileMenu();

         UpdateTitleBar();

         UpdateStatusBar("No project loaded");
      }

      /// <summary>
      /// Handles the Click event of the closeToolStripMenuItem control.
      /// </summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
      {
         CloseProject();
      }

      /// <summary>Handles the Click event of the ExitToolStripMenuItem control.</summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
      {
         if (_projectFileState == ProjectFileState.ChangedUnsavedProject || _projectFileState == ProjectFileState.NewUnsavedProject)
         {
            switch (MessageBox.Show(this, string.Format("Do you want to save changes you made to {0}?", _projectSettings.SettingsPathAndFilename), "Caption", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
               case DialogResult.No:
                  {
                     CloseApp();
                     break;
                  }

               case DialogResult.Cancel:
                  {
                     break;
                  }

               case DialogResult.Yes:
                  {
                     if (_projectFileState == ProjectFileState.NewUnsavedProject)
                     {
                        SaveProjectFileAs();
                     }
                     else
                     {
                        SaveProject();
                     }

                     break;
                  }
            }
         }
         else
         {
            CloseApp();
         }
      }

      /// <summary>
      /// Handles the Click event of the GenerateCodeToolStripMenuItem control.
      /// </summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void GenerateCodeToolStripMenuItem_Click(object sender, EventArgs e)
      {
         _progressForm.ClearMessages();
         _progressForm.ResetProgressBar();

         _modeller = new Modeller(_projectSettings);
         _modeller.ReadEntities();

         _progressForm.ProgressMaximum = _modeller.EntityCount;

         _modeller.OnDatabaseMessage += Modeller_OnDatabaseMessage;
         _modeller.OnProgressMessage += Modeller_OnProgressMessage;

         _progressForm.ButtonsAreEnabled = false;
         _progressForm.Show(this);

         _modeller.Process();

         Io.WriteTextFile(_projectSettings.OutputCommandsClassPathAndFilename, _modeller.CommandsFileContents);
         Io.WriteTextFile(_projectSettings.OutputModelsClassPathAndFilename, _modeller.ModelsFileContents);
         Io.WriteTextFile(_projectSettings.OutputServicesClassPathAndFilename, _modeller.ServicesFileContents);

         _modeller.OnDatabaseMessage -= Modeller_OnDatabaseMessage;
         _modeller.OnProgressMessage -= Modeller_OnProgressMessage;

         _progressForm.ButtonsAreEnabled = true;

         UpdateStatusBar("Code generation complete");
      }

      /// <summary>Initialises the new ORM.</summary>
      /// <param name="fileName">Name of the file.</param>
      private void InitialiseNewORM(string fileName)
      {
         _projectSettings = new ProjectSettings(fileName);

         _modeller = new Modeller(_projectSettings);

         PopulatePropertyGrid();
      }

      /// <summary>Handles the FormClosing event of the MainForm control.</summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="FormClosingEventArgs" /> instance containing the event data.</param>
      private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
      {
         CloseApp();
      }

      /// <summary>Handles the OnDatabaseMessage event of the Modeller control.</summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="DatabaseMessageEventArgs" /> instance containing the event data.</param>
      private void Modeller_OnDatabaseMessage(object sender, DatabaseMessageEventArgs e)
      {
         _progressForm.AddMessage(e.Message);
      }

      /// <summary>Handles the OnProgressMessage event of the Modeller control.</summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="ProgressMessageEventArgs" /> instance containing the event data.</param>
      private void Modeller_OnProgressMessage(object sender, ProgressMessageEventArgs e)
      {
         if (e.AdvanceProgressBar)
         {
            _progressForm.IncrementProgressBar();
         }

         _progressForm.AddMessage(e.Message);
      }

      /// <summary>
      /// Handles the Click event of the NewProjectToolStripMenuItem control.
      /// </summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void NewProjectToolStripMenuItem_Click(object sender, EventArgs e)
      {
         _projectSettings = new ProjectSettings {
                                                   SettingsPathAndFilename = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\" + NewProjectFilename
                                                };

         _projectFileState = ProjectFileState.NewUnsavedProject;

         PopulatePropertyGrid();

         UpdateTitleBar();
         UpdateFileMenu();

         UpdateStatusBar("New project created");
      }

      /// <summary>
      /// Handles the Click event of the openProjectToolStripMenuItem control.
      /// </summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
      private void OpenProjectToolStripMenuItem_Click(object sender, EventArgs e)
      {
         bool showOpenFileDialogue = true;

         if (_projectFileState == ProjectFileState.ChangedUnsavedProject || _projectFileState == ProjectFileState.NewUnsavedProject)
         {
            switch (MessageBox.Show(this, string.Format("Opening a new project will discard changes you made to {0}\r\n\r\nSave these changes?", _projectSettings.SettingsPathAndFilename), "Caption", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
               case DialogResult.No:
                  {
                     break;
                  }

               case DialogResult.Cancel:
                  {
                     showOpenFileDialogue = false;
                     break;
                  }

               case DialogResult.Yes:
                  {
                     SaveProjectFileAs();
                     showOpenFileDialogue = false;
                     break;
                  }
            }
         }

         if (showOpenFileDialogue)
         {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
               InitialiseNewORM(openFileDialog.FileName);
               _projectFileState = ProjectFileState.SavedUnchangedProject;

               UpdateFileMenu();

               UpdateStatusBar("Project opened");
            }
         }

         UpdateTitleBar();
      }

      /// <summary>Populates the property grid.</summary>
      private void PopulatePropertyGrid()
      {
         propertyGrid.SelectedObject = _projectSettings;
         PropertyGrid.ResizePropertyGridSplitterByPixels(propertyGrid, PropertyGridLabelColumnWidth);
         PropertyGrid.ResizeDescriptionArea(propertyGrid, PropertyGridDescriptionLines);

         propertyGrid.Refresh();
      }

      /// <summary>Handles the Resize event of the propertyGrid control.</summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void PropertyGrid_Resize(object sender, EventArgs e)
      {
         PropertyGrid.ResizePropertyGridSplitterByPixels(propertyGrid, PropertyGridLabelColumnWidth);
      }

      /// <summary>Saves the project.</summary>
      private void SaveProject()
      {
         _projectSettings.SaveSettingsToFile();
         _projectFileState = ProjectFileState.SavedUnchangedProject;
      }

      /// <summary>
      /// Handles the Click event of the SaveProjectAsToolStripMenuItem control.
      /// </summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void SaveProjectAsToolStripMenuItem_Click(object sender, EventArgs e)
      {
         SaveProjectFileAs();
      }

      /// <summary>Saves the project file as.</summary>
      private void SaveProjectFileAs()
      {
         DialogResult dialogResult = saveFileDialog.ShowDialog(this);

         if (dialogResult == DialogResult.Ignore || dialogResult == DialogResult.OK || dialogResult == DialogResult.Yes)
         {
            _projectSettings.SaveSettingsToFile(saveFileDialog.FileName);
            _projectFileState = ProjectFileState.SavedUnchangedProject;

            UpdateStatusBar("Project saved");
         }
      }

      /// <summary>
      /// Handles the Click event of the SaveProjectToolStripMenuItem control.
      /// </summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
      private void SaveProjectToolStripMenuItem_Click(object sender, EventArgs e)
      {
         if (_projectFileState == ProjectFileState.ChangedUnsavedProject || _projectFileState == ProjectFileState.NewUnsavedProject)
         {
            // SAVE AS
            SaveProjectFileAs();
         }
         else
         {
            // JUST SAVE
            _projectSettings.SaveSettingsToFile();

            UpdateStatusBar("Project saved");
         }

         _projectFileState = ProjectFileState.SavedUnchangedProject;
      }

      /// <summary>Updates the file menu.</summary>
      private void UpdateFileMenu()
      {
         bool saveOptionsEnabled = _projectFileState == ProjectFileState.ChangedUnsavedProject || _projectFileState == ProjectFileState.NewUnsavedProject || _projectFileState == ProjectFileState.SavedUnchangedProject;

         saveProjectToolStripMenuItem.Enabled = saveOptionsEnabled;
         saveProjectAsToolStripMenuItem.Enabled = saveOptionsEnabled;
         closeToolStripMenuItem.Enabled = saveOptionsEnabled;
      }

      /// <summary>Updates the status bar.</summary>
      /// <param name="text">The text.</param>
      private void UpdateStatusBar(string text)
      {
         toolStripStatusLabel.Text = text;
      }

      /// <summary>Updates the title bar.</summary>
      private void UpdateTitleBar()
      {
         Text = TitleBarPrefix + (_projectSettings != null ? " - " + _projectSettings.SettingsPathAndFilename : string.Empty);
      }
   }
}
