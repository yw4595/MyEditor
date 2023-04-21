using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
 * Author: Yanzhi Wang
 * 
 * Purpose: This class represents the main form of MyEditor application. It handles user interface events, such as opening, saving, and editing text files, as well as font and color selection. 
 * 
 * Restrictions: None
 */

namespace MyEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Register event handlers for menu items and toolbar buttons
            this.newToolStripMenuItem.Click += new EventHandler(NewToolStripMenuItem_Click);
            this.openToolStripMenuItem.Click += new EventHandler(OpenToolStripMenuItem_Click);
            this.saveToolStripMenuItem.Click += new EventHandler(SaveToolStripMenuItem_Click);
            this.exitToolStripMenuItem.Click += new EventHandler(ExitToolStripMenuItem_Click);
            this.copyToolStripMenuItem.Click += new EventHandler(CopyToolStripMenuItem_Click);
            this.cutToolStripMenuItem.Click += new EventHandler(CutToolStripMenuItem_Click);
            this.pasteToolStripMenuItem.Click += new EventHandler(PasteToolStripMenuItem_Click);

            this.toolStrip.ItemClicked += new ToolStripItemClickedEventHandler(ToolStrip_ItemClicked);

            // Set initial window title
            this.Text = "MyEditor";
        }

        // Method to handle "New" menu item click
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Clear the rich text box and reset window title
            richTextBox.Clear();
            this.Text = "MyEditor";
        }

        // Method to handle "Open" menu item click
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Display the Open File dialog
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Determine the file type based on extension
                RichTextBoxStreamType richTextBoxStreamType = RichTextBoxStreamType.RichText;
                if (openFileDialog.FileName.ToLower().Contains(".txt"))
                {
                    richTextBoxStreamType = RichTextBoxStreamType.PlainText;
                }

                // Load the file into the rich text box and update the window title
                richTextBox.LoadFile(openFileDialog.FileName, richTextBoxStreamType);
                this.Text = "MyEditor (" + openFileDialog.FileName + ")";
            }
        }

        // Method to handle "Save" menu item click
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Use the filename from the Open File dialog if available
            saveFileDialog.FileName = openFileDialog.FileName;

            // Display the Save File dialog
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Determine the file type based on extension
                RichTextBoxStreamType richTextBoxStreamType = RichTextBoxStreamType.RichText;
                if (saveFileDialog.FileName.ToLower().Contains(".txt"))
                {
                    richTextBoxStreamType = RichTextBoxStreamType.PlainText;
                }

                // Save the contents of the rich text box to the file and update the window title
                richTextBox.SaveFile(saveFileDialog.FileName, richTextBoxStreamType);
                this.Text = "MyEditor (" + openFileDialog.FileName + ")";
            }
        }

        // Method to handle "Exit" menu item click
        private void ExitToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            // Close the application
            Application.Exit();
        }

        // Method to handle "Copy" menu item click
        private void CopyToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            // Copy the selected text to the clipboard
            richTextBox.Copy();
        }

        // Method to handle "Cut" menu item click
        private void CutToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            // Cut the selected text to the clipboard
            richTextBox.Cut();
        }

        // Method to handle "Paste" menu item click
        private void PasteToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            // Paste the contents of the clipboard into the rich text box
            richTextBox.Paste();
        }

        // Method to handle toolbar item clicks
        private void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            FontStyle fontStyle = FontStyle.Regular;
            ToolStripButton toolStripButton = null;

            if (e.ClickedItem == this.boldToolStripButton)
            {
                fontStyle = FontStyle.Bold;
                toolStripButton = this.boldToolStripButton;
            }
            else if (e.ClickedItem == this.italicsToolStripButton)
            {
                fontStyle = FontStyle.Italic;
                toolStripButton = this.italicsToolStripButton;

            }
            else if (e.ClickedItem == this.underlineToolStripButton)
            {
                fontStyle = FontStyle.Underline;
                toolStripButton = this.underlineToolStripButton;
            }


            else if (e.ClickedItem == this.colorToolStripButton)
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    richTextBox.SelectionColor = colorDialog.Color;
                    colorToolStripButton.BackColor = colorDialog.Color;
                }
            }


            if (fontStyle != FontStyle.Regular)
            {
                toolStripButton.Checked = !toolStripButton.Checked;
                SetSelectionFont(fontStyle, toolStripButton.Checked);
            }
        }


        // Method to set the font style for the selected text
        private void SetSelectionFont(FontStyle fontStyle, bool bSet)
        {
            Font newFont = null;
            Font selectionFont = null;
            selectionFont = richTextBox.SelectionFont;
            if (selectionFont == null)
            {
                selectionFont = richTextBox.Font;
            }
            if (bSet)
            {
                newFont = new Font(selectionFont, selectionFont.Style | fontStyle);
            }
            else
            {
                newFont = new Font(selectionFont, selectionFont.Style & ~fontStyle);
            }
            this.richTextBox.SelectionFont = newFont;
        }
    }
}
