namespace ReadmeGenerator_Desktop
{
    /// <summary>
    /// Controller for the main form
    /// </summary>
    public partial class frmMain : Form
    {
        string filePath = "";
        string readmeString = "";
        
        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates an open file dialog to allow user to select XML file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_LoadXML_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files | *.xml";
            openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }

            lbl_XMLPath.Text = filePath;
        }

        /// <summary>
        /// Calls the CreateDoc() method to read the XML file and generate the readme.md string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Generate_Click(object sender, EventArgs e)
        {
            if (filePath != "")
            {
                bool excludeProgram = chkbox_Program.Checked;
                bool excludeAppConfig = chkbox_AppConfig.Checked;
                
                readmeString = Generator.CreateDoc(filePath, excludeProgram, excludeAppConfig);
                txtbox_Readme.Text = readmeString;
            }
        }

        /// <summary>
        /// Creates a save file dialog to allow user to save the readme.md file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Markdown Files | *.md";
            saveFileDialog.Title = "Save Readme File";
            saveFileDialog.FileName = "readme.md";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, readmeString);
            }
        }

        /// <summary>
        /// Closes the app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}