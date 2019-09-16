namespace GarterBelt.GUI
{
    partial class ShortcutSelect
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
            this.label1 = new System.Windows.Forms.Label();
            this.text_scWindowShow = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.text_scWindowHide = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.text_scGarterShow = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.text_scGarterHide = new System.Windows.Forms.TextBox();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Window Show";
            // 
            // text_scWindowShow
            // 
            this.text_scWindowShow.Location = new System.Drawing.Point(114, 10);
            this.text_scWindowShow.Name = "text_scWindowShow";
            this.text_scWindowShow.Size = new System.Drawing.Size(292, 21);
            this.text_scWindowShow.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Window Hide";
            // 
            // text_scWindowHide
            // 
            this.text_scWindowHide.Location = new System.Drawing.Point(114, 37);
            this.text_scWindowHide.Name = "text_scWindowHide";
            this.text_scWindowHide.Size = new System.Drawing.Size(292, 21);
            this.text_scWindowHide.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Garterbelt Show";
            // 
            // text_scGarterShow
            // 
            this.text_scGarterShow.Location = new System.Drawing.Point(114, 64);
            this.text_scGarterShow.Name = "text_scGarterShow";
            this.text_scGarterShow.Size = new System.Drawing.Size(292, 21);
            this.text_scGarterShow.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Garterbelt Hide";
            // 
            // text_scGarterHide
            // 
            this.text_scGarterHide.Location = new System.Drawing.Point(114, 91);
            this.text_scGarterHide.Name = "text_scGarterHide";
            this.text_scGarterHide.Size = new System.Drawing.Size(292, 21);
            this.text_scGarterHide.TabIndex = 1;
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(306, 118);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(100, 37);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(200, 118);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(100, 37);
            this.button_ok.TabIndex = 2;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // ShortcutSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 167);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.text_scGarterHide);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.text_scGarterShow);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.text_scWindowHide);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.text_scWindowShow);
            this.Controls.Add(this.label1);
            this.Name = "ShortcutSelect";
            this.Text = "ShortcutSelect";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox text_scWindowShow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_scWindowHide;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox text_scGarterShow;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox text_scGarterHide;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_ok;
    }
}