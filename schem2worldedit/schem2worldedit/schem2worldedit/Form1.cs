using System;
using System.IO;
using System.Windows.Forms;

namespace schem2worldedit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            pictureBox1.AllowDrop = true;

            pictureBox1.DragEnter += PictureBox1_DragEnter;
            pictureBox1.DragDrop += PictureBox1_DragDrop;

            if (pictureBox1 == null)
            {
                throw new InvalidOperationException("pictureBox1 n'est pas initialisé.");
            }
        }

        private void PictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0 && (files[0].EndsWith(".schem") || files[0].EndsWith(".schematic")))
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void PictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                string sourceFile = files[0];
                string targetPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    @".minecraft\config\worldedit\schematics"
                );

                try
                {
                    if (!Directory.Exists(targetPath))
                    {
                        Directory.CreateDirectory(targetPath);
                    }

                    string targetFile = Path.Combine(targetPath, Path.GetFileName(sourceFile));
                    File.Copy(sourceFile, targetFile, true);

                    MessageBox.Show("Click OK to import the file", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error copying file: {ex.Message}", "Error ;(", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PictureBox1 clicked!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Schem Files Supported (*.schem;*.schematic)|*.schem;*.schematic";
                openFileDialog.Title = "Select schematic file";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string sourceFile = openFileDialog.FileName;
                    string targetPath = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        @".minecraft\config\worldedit\schematics"
                    );

                    try
                    {
                        if (!Directory.Exists(targetPath))
                        {
                            Directory.CreateDirectory(targetPath);
                        }

                        string targetFile = Path.Combine(targetPath, Path.GetFileName(sourceFile));
                        File.Copy(sourceFile, targetFile, true);

                        MessageBox.Show("Fichier importé avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de l'importation du fichier : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = "https://github.com/saysaa/schem2worldedit";
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(target) { UseShellExecute = true });
        }
    }
}
