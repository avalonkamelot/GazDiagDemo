using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using TubeDemo.Data.Contexts;
using TubeDemo.Data.Repository;

namespace TubeDemo.Forms
{
    /// <summary>
    /// Interaction logic for DefectsViewForm.xaml
    /// </summary>
    /// 
    public partial class DefectsViewForm : UserControl
    {
        public DefectsViewForm()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Data files (*.csv;*.xls;*.xlsx)|*.csv;*.xls;*.xlsx",
                InitialDirectory = AssemblyDirectory
            };
            if(openFileDialog.ShowDialog()==true)
            {
                var dataStream = new FileStream(openFileDialog.FileName,FileMode.Open);

                if (System.IO.Path.GetExtension(openFileDialog.FileName).ToLower() == ".csv")
                {
                    mGrid.ViewModel.Repository = new TubeSegmentsRepository(new CsvContext(dataStream));
                }
                else if (System.IO.Path.GetExtension(openFileDialog.FileName).ToLower() == ".xls" || System.IO.Path.GetExtension(openFileDialog.FileName).ToLower() == ".xlsx")
                {
                    mGrid.ViewModel.Repository = new TubeSegmentsRepository(new XlsContext(dataStream));
                }
            }
        }

        public static string? AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly()?.EscapedCodeBase ?? String.Empty;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return System.IO.Path.GetDirectoryName(path);
            }
        }
    }
}
