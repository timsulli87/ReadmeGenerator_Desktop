namespace ReadmeGenerator_Desktop
{
    /// <summary>
    /// Desktop version of the Readme Generator app. The app allows the user to load an XML documentation file, transform it into a readme.md file, and save the output file.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new frmMain());
        }
    }
}