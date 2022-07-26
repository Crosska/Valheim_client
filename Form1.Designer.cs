namespace Client
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.directoryPathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chooseGameDirectoryButton = new System.Windows.Forms.Button();
            this.startGameButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.loadingUpdateProgressBar = new System.Windows.Forms.ProgressBar();
            this.percentIconLabel = new System.Windows.Forms.Label();
            this.autoDetectGamePathRadioButton = new System.Windows.Forms.RadioButton();
            this.manualDetectGamePathRadioButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.currentProcessibleFile = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // directoryPathTextBox
            // 
            resources.ApplyResources(this.directoryPathTextBox, "directoryPathTextBox");
            this.directoryPathTextBox.Name = "directoryPathTextBox";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // chooseGameDirectoryButton
            // 
            resources.ApplyResources(this.chooseGameDirectoryButton, "chooseGameDirectoryButton");
            this.chooseGameDirectoryButton.Name = "chooseGameDirectoryButton";
            this.chooseGameDirectoryButton.UseVisualStyleBackColor = true;
            this.chooseGameDirectoryButton.Click += new System.EventHandler(this.chooseGameDirectoryButton_Click_1);
            // 
            // startGameButton
            // 
            resources.ApplyResources(this.startGameButton, "startGameButton");
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.Click += new System.EventHandler(this.startGameButton_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // loadingUpdateProgressBar
            // 
            resources.ApplyResources(this.loadingUpdateProgressBar, "loadingUpdateProgressBar");
            this.loadingUpdateProgressBar.Maximum = 1000;
            this.loadingUpdateProgressBar.Name = "loadingUpdateProgressBar";
            // 
            // percentIconLabel
            // 
            resources.ApplyResources(this.percentIconLabel, "percentIconLabel");
            this.percentIconLabel.Name = "percentIconLabel";
            // 
            // autoDetectGamePathRadioButton
            // 
            resources.ApplyResources(this.autoDetectGamePathRadioButton, "autoDetectGamePathRadioButton");
            this.autoDetectGamePathRadioButton.Checked = true;
            this.autoDetectGamePathRadioButton.Name = "autoDetectGamePathRadioButton";
            this.autoDetectGamePathRadioButton.TabStop = true;
            this.autoDetectGamePathRadioButton.UseVisualStyleBackColor = true;
            this.autoDetectGamePathRadioButton.CheckedChanged += new System.EventHandler(this.autoDetectGamePathRadioButton_CheckedChanged);
            // 
            // manualDetectGamePathRadioButton
            // 
            resources.ApplyResources(this.manualDetectGamePathRadioButton, "manualDetectGamePathRadioButton");
            this.manualDetectGamePathRadioButton.Name = "manualDetectGamePathRadioButton";
            this.manualDetectGamePathRadioButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.directoryPathTextBox);
            this.panel1.Controls.Add(this.chooseGameDirectoryButton);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Client.Properties.Resources._1653474759_8;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.autoDetectGamePathRadioButton);
            this.panel2.Controls.Add(this.manualDetectGamePathRadioButton);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // currentProcessibleFile
            // 
            resources.ApplyResources(this.currentProcessibleFile, "currentProcessibleFile");
            this.currentProcessibleFile.BackColor = System.Drawing.Color.Azure;
            this.currentProcessibleFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.currentProcessibleFile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.currentProcessibleFile.Name = "currentProcessibleFile";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.Controls.Add(this.currentProcessibleFile);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.percentIconLabel);
            this.Controls.Add(this.loadingUpdateProgressBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.startGameButton);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox directoryPathTextBox;
        private Label label1;
        private Button chooseGameDirectoryButton;
        private Button startGameButton;
        private Label label2;
        private ProgressBar loadingUpdateProgressBar;
        private Label percentIconLabel;
        private RadioButton autoDetectGamePathRadioButton;
        private RadioButton manualDetectGamePathRadioButton;
        private Panel panel1;
        private PictureBox pictureBox1;
        private Panel panel2;
        private Label currentProcessibleFile;
    }
}