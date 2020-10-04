using Fantome.Libraries.League.Helpers.BIN;
using Fantome.Libraries.League.IO.BIN;
using Railgun.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace Railgun
{
    public partial class MainWindow : Window
    {
        public Dictionary<uint, string> hashlist(string file)
        {
            Dictionary<uint, string> filenames = new Dictionary<uint, string>();
            List<string> fileraws = File.ReadAllLines(file).ToList();
            foreach (string fileraw in fileraws)
            {
                string[] filefield = fileraw.Split(' ');
                uint key = Convert.ToUInt32(filefield[0], 16);
                if (filenames.ContainsKey(key))
                    filenames[key] = filefield[1];
                else
                    filenames.Add(key, filefield[1]);
            }
            return filenames;
        }

        public MainWindow()
        {
            InitializeComponent();
            Dictionary<uint, string> classNames = hashlist("hashes.bintypes.txt");
            Dictionary<uint, string> fieldNames = hashlist("hashes.binfields.txt");
            Dictionary<uint, string> entryNames = hashlist("hashes.binentries.txt");
            BINGlobal.SetHashmap(entryNames, classNames, fieldNames);
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog()
            {
                Title = "Select the BIN file you want to open",
                Multiselect = false,
                Filter = "BIN Files (*.bin)|*.bin"
            };

            if(dialog.ShowDialog() == true)
            {
                this.MainTreeView.Items.Add(BINProcessor.GenerateTreeView(new BINFile(dialog.FileName), dialog.FileName));
            }
        }
    }
}
