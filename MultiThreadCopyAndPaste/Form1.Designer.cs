namespace MultiThreadCopyAndPaste
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Button submitButton;
        private FolderBrowserDialog folderBrowserDialog1;
        private ToolStripMenuItem folderMenuItem;
        private Button selectButton;
        private Label selectPath;
        private Button targetButton;
        private Label targetPath;
        private FolderBrowserDialog folderBrowserDialog2;
        private TreeView selectView;
        private ProgressBar progressBar1;
        private TextBox selectBox;
        private TextBox targetBox;
        private ToolTip toolTip1;
        private Label loadLabel;
    }
}