namespace GarterBelt.GUI
{
    partial class GarterGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GarterGUI));
            this.button_findHandle = new System.Windows.Forms.Button();
            this.button_selectHandle = new System.Windows.Forms.Button();
            this.button_Show = new System.Windows.Forms.Button();
            this.button_Hide = new System.Windows.Forms.Button();
            this.button_Opacity = new System.Windows.Forms.Button();
            this.group_handle = new System.Windows.Forms.GroupBox();
            this.group_window = new System.Windows.Forms.GroupBox();
            this.button_Topmost = new System.Windows.Forms.Button();
            this.group_program = new System.Windows.Forms.GroupBox();
            this.button_Shortcuts = new System.Windows.Forms.Button();
            this.button_About = new System.Windows.Forms.Button();
            this.group_handle.SuspendLayout();
            this.group_window.SuspendLayout();
            this.group_program.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_findHandle
            // 
            this.button_findHandle.Location = new System.Drawing.Point(6, 20);
            this.button_findHandle.Name = "button_findHandle";
            this.button_findHandle.Size = new System.Drawing.Size(107, 43);
            this.button_findHandle.TabIndex = 0;
            this.button_findHandle.Text = "Find";
            this.button_findHandle.UseVisualStyleBackColor = true;
            this.button_findHandle.Click += new System.EventHandler(this.button_findHandle_Click);
            // 
            // button_selectHandle
            // 
            this.button_selectHandle.Location = new System.Drawing.Point(119, 20);
            this.button_selectHandle.Name = "button_selectHandle";
            this.button_selectHandle.Size = new System.Drawing.Size(107, 43);
            this.button_selectHandle.TabIndex = 0;
            this.button_selectHandle.Text = "Select By History";
            this.button_selectHandle.UseVisualStyleBackColor = true;
            this.button_selectHandle.Click += new System.EventHandler(this.button_selectHandle_Click);
            // 
            // button_Show
            // 
            this.button_Show.Location = new System.Drawing.Point(6, 20);
            this.button_Show.Name = "button_Show";
            this.button_Show.Size = new System.Drawing.Size(107, 43);
            this.button_Show.TabIndex = 0;
            this.button_Show.Text = "Show";
            this.button_Show.UseVisualStyleBackColor = true;
            this.button_Show.Click += new System.EventHandler(this.button_Show_Click);
            // 
            // button_Hide
            // 
            this.button_Hide.Location = new System.Drawing.Point(119, 20);
            this.button_Hide.Name = "button_Hide";
            this.button_Hide.Size = new System.Drawing.Size(107, 43);
            this.button_Hide.TabIndex = 0;
            this.button_Hide.Text = "Hide";
            this.button_Hide.UseVisualStyleBackColor = true;
            this.button_Hide.Click += new System.EventHandler(this.button_Hide_Click);
            // 
            // button_Opacity
            // 
            this.button_Opacity.Location = new System.Drawing.Point(232, 20);
            this.button_Opacity.Name = "button_Opacity";
            this.button_Opacity.Size = new System.Drawing.Size(107, 43);
            this.button_Opacity.TabIndex = 0;
            this.button_Opacity.Text = "Opacity";
            this.button_Opacity.UseVisualStyleBackColor = true;
            this.button_Opacity.Click += new System.EventHandler(this.button_Opacity_Click);
            // 
            // group_handle
            // 
            this.group_handle.Controls.Add(this.button_findHandle);
            this.group_handle.Controls.Add(this.button_selectHandle);
            this.group_handle.Location = new System.Drawing.Point(12, 12);
            this.group_handle.Name = "group_handle";
            this.group_handle.Size = new System.Drawing.Size(458, 69);
            this.group_handle.TabIndex = 2;
            this.group_handle.TabStop = false;
            this.group_handle.Text = "Handle";
            // 
            // group_window
            // 
            this.group_window.Controls.Add(this.button_Show);
            this.group_window.Controls.Add(this.button_Opacity);
            this.group_window.Controls.Add(this.button_Hide);
            this.group_window.Controls.Add(this.button_Topmost);
            this.group_window.Location = new System.Drawing.Point(12, 87);
            this.group_window.Name = "group_window";
            this.group_window.Size = new System.Drawing.Size(458, 69);
            this.group_window.TabIndex = 3;
            this.group_window.TabStop = false;
            this.group_window.Text = "Window";
            // 
            // button_Topmost
            // 
            this.button_Topmost.Location = new System.Drawing.Point(345, 20);
            this.button_Topmost.Name = "button_Topmost";
            this.button_Topmost.Size = new System.Drawing.Size(107, 43);
            this.button_Topmost.TabIndex = 0;
            this.button_Topmost.Text = "Topmost";
            this.button_Topmost.UseVisualStyleBackColor = true;
            this.button_Topmost.Click += new System.EventHandler(this.button_Topmost_Click);
            // 
            // group_program
            // 
            this.group_program.Controls.Add(this.button_Shortcuts);
            this.group_program.Controls.Add(this.button_About);
            this.group_program.Location = new System.Drawing.Point(12, 162);
            this.group_program.Name = "group_program";
            this.group_program.Size = new System.Drawing.Size(458, 69);
            this.group_program.TabIndex = 2;
            this.group_program.TabStop = false;
            this.group_program.Text = "Program";
            // 
            // button_Shortcuts
            // 
            this.button_Shortcuts.Location = new System.Drawing.Point(6, 20);
            this.button_Shortcuts.Name = "button_Shortcuts";
            this.button_Shortcuts.Size = new System.Drawing.Size(107, 43);
            this.button_Shortcuts.TabIndex = 0;
            this.button_Shortcuts.Text = "Shortcuts";
            this.button_Shortcuts.UseVisualStyleBackColor = true;
            this.button_Shortcuts.Click += new System.EventHandler(this.button_Shortcuts_Click);
            // 
            // button_About
            // 
            this.button_About.Location = new System.Drawing.Point(119, 20);
            this.button_About.Name = "button_About";
            this.button_About.Size = new System.Drawing.Size(107, 43);
            this.button_About.TabIndex = 0;
            this.button_About.Text = "About Garterbelt";
            this.button_About.UseVisualStyleBackColor = true;
            // 
            // GarterGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 243);
            this.Controls.Add(this.group_window);
            this.Controls.Add(this.group_program);
            this.Controls.Add(this.group_handle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GarterGUI";
            this.Text = "Garterbelt";
            this.group_handle.ResumeLayout(false);
            this.group_window.ResumeLayout(false);
            this.group_program.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_findHandle;
        private System.Windows.Forms.Button button_selectHandle;
        private System.Windows.Forms.Button button_Show;
        private System.Windows.Forms.Button button_Hide;
        private System.Windows.Forms.Button button_Opacity;
        private System.Windows.Forms.GroupBox group_handle;
        private System.Windows.Forms.GroupBox group_window;
        private System.Windows.Forms.Button button_Topmost;
        private System.Windows.Forms.GroupBox group_program;
        private System.Windows.Forms.Button button_Shortcuts;
        private System.Windows.Forms.Button button_About;
    }
}