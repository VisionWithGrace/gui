namespace gui
{
    partial class AdminPanel
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.recentObjectsButton = new System.Windows.Forms.Button();
            this.allObjectsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(565, 290);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // recentObjectsButton
            // 
            this.recentObjectsButton.Location = new System.Drawing.Point(114, 308);
            this.recentObjectsButton.Name = "recentObjectsButton";
            this.recentObjectsButton.Size = new System.Drawing.Size(140, 58);
            this.recentObjectsButton.TabIndex = 1;
            this.recentObjectsButton.Text = "Get Recent Objects";
            this.recentObjectsButton.UseVisualStyleBackColor = true;
            // 
            // allObjectsButton
            // 
            this.allObjectsButton.Location = new System.Drawing.Point(329, 308);
            this.allObjectsButton.Name = "allObjectsButton";
            this.allObjectsButton.Size = new System.Drawing.Size(150, 58);
            this.allObjectsButton.TabIndex = 2;
            this.allObjectsButton.Text = "Get All Objects";
            this.allObjectsButton.UseVisualStyleBackColor = true;
            // 
            // AdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 378);
            this.Controls.Add(this.allObjectsButton);
            this.Controls.Add(this.recentObjectsButton);
            this.Controls.Add(this.listBox1);
            this.Name = "AdminPanel";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button recentObjectsButton;
        private System.Windows.Forms.Button allObjectsButton;


    }
}