using System.Text.RegularExpressions;

namespace MultiThreadCopyAndPaste
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.submitButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectButton = new System.Windows.Forms.Button();
            this.selectPath = new System.Windows.Forms.Label();
            this.targetButton = new System.Windows.Forms.Button();
            this.targetPath = new System.Windows.Forms.Label();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.selectView = new System.Windows.Forms.TreeView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.selectBox = new System.Windows.Forms.TextBox();
            this.targetBox = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(602, 378);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(122, 23);
            this.submitButton.TabIndex = 0;
            this.submitButton.Text = "Copy Directories";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submit_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyDocuments;
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // folderMenuItem
            // 
            this.folderMenuItem.Name = "folderMenuItem";
            this.folderMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // selectButton
            // 
            this.selectButton.Location = new System.Drawing.Point(696, 31);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(28, 23);
            this.selectButton.TabIndex = 1;
            this.selectButton.Text = "...";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.select_Click);
            // 
            // selectPath
            // 
            this.selectPath.AutoSize = true;
            this.selectPath.Location = new System.Drawing.Point(12, 35);
            this.selectPath.Name = "selectPath";
            this.selectPath.Size = new System.Drawing.Size(89, 15);
            this.selectPath.TabIndex = 2;
            this.selectPath.Text = "Select Directory";
            // 
            // targetButton
            // 
            this.targetButton.Location = new System.Drawing.Point(696, 340);
            this.targetButton.Name = "targetButton";
            this.targetButton.Size = new System.Drawing.Size(28, 23);
            this.targetButton.TabIndex = 3;
            this.targetButton.Text = "...";
            this.targetButton.UseVisualStyleBackColor = true;
            this.targetButton.Click += new System.EventHandler(this.target_Click);
            // 
            // targetPath
            // 
            this.targetPath.AutoSize = true;
            this.targetPath.Location = new System.Drawing.Point(10, 344);
            this.targetPath.Name = "targetPath";
            this.targetPath.Size = new System.Drawing.Size(118, 15);
            this.targetPath.TabIndex = 4;
            this.targetPath.Text = "Destination Directory";
            // 
            // folderBrowserDialog2
            // 
            this.folderBrowserDialog2.RootFolder = System.Environment.SpecialFolder.MyDocuments;
            this.folderBrowserDialog2.ShowNewFolderButton = false;
            // 
            // selectView
            // 
            this.selectView.CheckBoxes = true;
            this.selectView.Location = new System.Drawing.Point(12, 74);
            this.selectView.Name = "selectView";
            this.selectView.Size = new System.Drawing.Size(702, 260);
            this.selectView.TabIndex = 5;
            this.selectView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.selectView_AfterCheck);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 378);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(536, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // selectBox
            // 
            this.selectBox.Location = new System.Drawing.Point(134, 31);
            this.selectBox.Name = "selectBox";
            this.selectBox.Size = new System.Drawing.Size(556, 23);
            this.selectBox.TabIndex = 7;
            // 
            // targetBox
            // 
            this.targetBox.Location = new System.Drawing.Point(134, 340);
            this.targetBox.Name = "targetBox";
            this.targetBox.Size = new System.Drawing.Size(556, 23);
            this.targetBox.TabIndex = 8;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(726, 413);
            this.Controls.Add(this.selectBox);
            this.Controls.Add(this.targetBox);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.selectView);
            this.Controls.Add(this.targetPath);
            this.Controls.Add(this.targetButton);
            this.Controls.Add(this.selectPath);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.submitButton);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void select_Click(object sender, EventArgs e)
        {
            DialogResult drResult = folderBrowserDialog1.ShowDialog();
            if (drResult == DialogResult.OK)
            {
                this.selectBox.Text = folderBrowserDialog1.SelectedPath;
            }
            selectView.Nodes.Clear();
            toolTip1.ShowAlways = true;
            if (!String.IsNullOrEmpty(this.selectBox.Text) && Directory.Exists(this.selectBox.Text))
            {
                LoadDirectory(this.selectBox.Text);
            }
            else
            {
                MessageBox.Show("Select Directory");
            }
        }
        private void target_Click(object sender, EventArgs e)
        {
            DialogResult drResult = folderBrowserDialog2.ShowDialog();
            if (drResult == DialogResult.OK)
            {
                this.targetBox.Text = folderBrowserDialog2.SelectedPath;
            }

        }

        private void LoadDirectory(string list)
        {
            DirectoryInfo di = new DirectoryInfo(list);
            TreeNode tds = selectView.Nodes.Add(di.Name);
            tds.Checked = true;
            tds.Tag = di.FullName;
            tds.StateImageIndex = 0;

            //LoadFiles(list, tds);
            LoadSubDirectories(list, tds);
        }
        private void LoadSubDirectories(string dir, TreeNode td)
        {
            Dictionary<string, Tuple<string, TreeNode>> tmp = new Dictionary<string, Tuple<string, TreeNode>>();

            List<string> subdirectoryEntries = Directory.EnumerateDirectories(dir, "*", SearchOption.TopDirectoryOnly).ToList();
            if (subdirectoryEntries.Count > 0)
            {
                //Parallel.ForEach(subdirectoryEntries, new ParallelOptions() { MaxDegreeOfParallelism = 20 }, (subdirectory) =>
                //{
                //    Console.WriteLine($"thread = {Thread.CurrentThread.ManagedThreadId}");
                //    DirectoryInfo di = new DirectoryInfo(subdirectory);
                //    //TreeNode tds = td.Nodes.Add(di.Name);
                //    //tds.StateImageIndex = 0;
                //    //tds.Tag = di.FullName;
                //    //LoadFiles(subdirectory, tds);
                //    //LoadSubDirectories(subdirectory, tds);
                //    tmp.Add(di.Name, new Tuple<string, TreeNode>(subdirectory, new TreeNode()));
                //    tmp[di.Name].Item2.StateImageIndex = 0;
                //    tmp[di.Name].Item2.Tag = di.FullName;
                //});

                //foreach (var key in tmp.Keys)
                //{
                //    td.nodes
                //    LoadFiles(tmp[key].Item1, tmp[key].Item2);
                //    LoadSubDirectories(subdirectory, tds);
                //}
                foreach (string subdirectory in subdirectoryEntries)
                {
                    DirectoryInfo di = new DirectoryInfo(subdirectory);
                    TreeNode tds = td.Nodes.Add(di.Name);
                    tds.StateImageIndex = 0;
                    tds.Tag = di.FullName;
                    if (td.Checked)
                    {
                        tds.Checked = true;
                    }
                    //LoadFiles(subdirectory, tds);
                }
            }
        }

        private void selectView_MouseMove(object sender, MouseEventArgs e)
        {

            // Get the node at the current mouse pointer location.  
            TreeNode theNode = this.selectView.GetNodeAt(e.X, e.Y);

            // Set a ToolTip only if the mouse pointer is actually paused on a node.  
            if (theNode != null && theNode.Tag != null)
            {
                // Change the ToolTip only if the pointer moved to a new node.  
                if (theNode.Tag.ToString() != this.toolTip1.GetToolTip(this.selectView))
                    this.toolTip1.SetToolTip(this.selectView, theNode.Tag.ToString());

            }
            else     // Pointer is not over a node so clear the ToolTip.  
            {
                this.toolTip1.SetToolTip(this.selectView, "");
            }
        }

        private void submit_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            List<string> clickedList = new List<string>();
            if (!String.IsNullOrEmpty(this.selectBox.Text) && !String.IsNullOrEmpty(this.targetBox.Text))
            {
                if (this.selectView.Nodes[0].Checked)
                {
                    clickedList = getDirectory(this.selectBox.Text, "all");
                }
                else
                {
                    foreach (TreeNode node in this.selectView.Nodes[0].Nodes)
                    {
                        if (node.Checked)
                        {
                            clickedList.Add(node.Name);
                        }
                    }
                }
                this.CopyDirectory(this.selectBox.Text, clickedList, this.targetBox.Text);
            }
        }

        //private void LoadDirectory(string dir)
        //{
        //    DirectoryInfo di = new DirectoryInfo(dir);
        //    progressBar1.Maximum = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories).Length + Directory.GetDirectories(dir, "**", SearchOption.AllDirectories).Length;

        //}

        private void CopyDirectory(string selectPath, List<string> sourceList, string targetPath)
        {
            List<string> folderList = new List<string>();
            Parallel.ForEach(sourceList, new ParallelOptions() { MaxDegreeOfParallelism = 10 }, (folder) =>
            {
                var folders = Directory.GetDirectories(folder, "*", SearchOption.AllDirectories).ToList();
                folderList.AddRange(folders);
            });
            Parallel.ForEach(folderList, new ParallelOptions() { MaxDegreeOfParallelism = 10 }, (d) =>
            {
                Directory.CreateDirectory(d.Replace(selectPath, targetPath));
                Console.WriteLine($"thread = {Thread.CurrentThread.ManagedThreadId}");
            });
            List<string> fileList = new List<string>();
            Parallel.ForEach(folderList, new ParallelOptions() { MaxDegreeOfParallelism = 10 }, (folder) =>
            {
                var files = Directory.GetFiles(folder, "*", SearchOption.AllDirectories).ToList();
                fileList.AddRange(files);
            });

            Parallel.ForEach(fileList, new ParallelOptions() { MaxDegreeOfParallelism = 10 }, (f) =>
            {
                File.Copy(f, f.Replace(selectPath, targetPath), true);
                Console.WriteLine($"thread = {Thread.CurrentThread.ManagedThreadId}");
            });

        }
        private List<string> getDirectory(string path, string option)
        {
            if (option == "all")
            {
                return Directory.GetDirectories(path, "*", SearchOption.AllDirectories).ToList();
            }
            else if (option == "top")
            {
                return Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly).ToList();
            }
            else
            {
                return new List<string>();
            }

        }

        private void LoadFiles(string dir, TreeNode td)
        {
            List<string> Files = Directory.GetFiles(dir, "*.*").ToList();

            // Loop through them to see files  
            Parallel.ForEach(Files, new ParallelOptions() { MaxDegreeOfParallelism = 20 }, (file) =>
            {
                FileInfo fi = new FileInfo(file);
                TreeNode tds = td.Nodes.Add(fi.Name);
                tds.Tag = fi.FullName;
                tds.StateImageIndex = 1;
            });
        }

        private void selectView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode n in e.Node.Nodes)
            {
                n.Checked = e.Node.Checked;
            }
        }
    }
}