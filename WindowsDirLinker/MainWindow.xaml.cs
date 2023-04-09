using FolderBrowserEx;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
