using FolderBrowserEx;
using System;
using System.Windows;

namespace WindowsDirLinker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDirectoryConnector directoryConnector = new DirectoryConnector();
        
        private string lastDirectory = Environment.CurrentDirectory;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void RunConnect()
        {
            directoryConnector.PathToCurrentDir = PathToCurrentDir.Text;
            directoryConnector.PathToRealDir = PathToRealDir.Text;
            directoryConnector.FakeDirName = FakeDirName.Text;

            try
            {
                directoryConnector.Connect();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }


        private void PathToCurrentDirButton_Click(object sender, RoutedEventArgs e)
            => PathToCurrentDir.Text = GetDirNameViaDialoger() ?? PathToCurrentDir.Text;


        private void PathToRealDirButton_Click(object sender, RoutedEventArgs e)
            => PathToRealDir.Text = GetDirNameViaDialoger() ?? PathToRealDir.Text;


        private void FakeDirNameButton_Click(object sender, RoutedEventArgs e)
            => FakeDirName.Text = null;

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
            => RunConnect();

        private void ShowError(string message)
            => MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        private string GetDirNameViaDialoger()
        {
            var dialog = new FolderBrowserDialog()
            {
                DefaultFolder = lastDirectory
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lastDirectory = dialog.SelectedFolder;
                return lastDirectory;
            }

            return null;
        }
    }
}
