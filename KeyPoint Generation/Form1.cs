//Author: Colin Pollard 2/7/2020
using PointGen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyPoint_Generation
{
    /// <summary>
    /// GUI for creating vector paths from csv of key points.
    /// </summary>
    public partial class Form1 : Form
    {
        //String to hold name of csv file.
        string filePath;
        string outputPath;
        PointGen.PointGen gen;

        public Form1()
        {
            filePath = null;
            InitializeComponent();
        }

        /// <summary>
        /// Event fired by pressing the select file button. Opens a file dialog to specify a csv file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "csv files (*.csv)|*.csv";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
            }
        }

        /// <summary>
        /// Generates points from selected csv. Throws dialog error if results in too many points, invalid file, or no selected file name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunButton_Click(object sender, EventArgs e)
        {
            //Make sure file is selected first.
            if(filePath == null || outputPath == null)
            {
                MessageBox.Show("Please select a valid input and output first.", "No File Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            gen = new PointGen.PointGen(filePath, outputPath);
            gen.success += CompleteMessage;
            //Do work.
            gen.Run(); 
        }

        /// <summary>
        /// Selects a folder to save files to.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutputSelectButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowser.ShowDialog();
                if (result == DialogResult.OK)
                {
                    outputPath = folderBrowser.SelectedPath;
                }
            }
        }

        /// <summary>
        /// Displays completion message.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="steps"></param>
        public void CompleteMessage(double time, int steps)
        {
            MessageBox.Show("Successfully Generated. Estimated time: " + time + " Machine path steps: " + steps + " .", "Completed",
    MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
