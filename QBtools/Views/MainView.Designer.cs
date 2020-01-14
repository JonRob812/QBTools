namespace QBtools
{
    partial class MainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.cmdOK = new System.Windows.Forms.Button();
            this.SetupId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AddToolsButton = new System.Windows.Forms.Button();
            this.responsebox = new System.Windows.Forms.RichTextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Image = global::QBtools.Properties.Resources.ok_24;
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // SetupId
            // 
            this.SetupId.BackColor = System.Drawing.Color.Black;
            this.SetupId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.SetupId, "SetupId");
            this.SetupId.ForeColor = System.Drawing.Color.Silver;
            this.SetupId.Name = "SetupId";            
            this.SetupId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SetupId_KeyDown);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Name = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // AddToolsButton
            // 
            this.AddToolsButton.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.AddToolsButton, "AddToolsButton");
            this.AddToolsButton.ForeColor = System.Drawing.Color.Silver;
            this.AddToolsButton.Name = "AddToolsButton";
            this.AddToolsButton.UseVisualStyleBackColor = false;
            this.AddToolsButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // responsebox
            // 
            this.responsebox.BackColor = System.Drawing.Color.Black;
            this.responsebox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.responsebox, "responsebox");
            this.responsebox.ForeColor = System.Drawing.Color.Silver;
            this.responsebox.Name = "responsebox";
            // 
            // MainView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.Controls.Add(this.responsebox);
            this.Controls.Add(this.AddToolsButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SetupId);
            this.Controls.Add(this.cmdOK);
            this.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainView";
            this.Load += new System.EventHandler(this.MainView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.TextBox SetupId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddToolsButton;
        private System.Windows.Forms.RichTextBox responsebox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}