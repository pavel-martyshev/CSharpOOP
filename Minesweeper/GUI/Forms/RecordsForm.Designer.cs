namespace Minesweeper.GUI
{
    partial class RecordsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordsForm));
            recordsPanel = new Panel();
            recordsLabel = new Label();
            recordsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // recordsPanel
            // 
            recordsPanel.AutoScroll = true;
            recordsPanel.BorderStyle = BorderStyle.Fixed3D;
            recordsPanel.Controls.Add(recordsLabel);
            recordsPanel.Dock = DockStyle.Fill;
            recordsPanel.Location = new Point(0, 0);
            recordsPanel.Name = "recordsPanel";
            recordsPanel.Size = new Size(289, 450);
            recordsPanel.TabIndex = 0;
            // 
            // recordsLabel
            // 
            recordsLabel.AutoSize = true;
            recordsLabel.Font = new Font("Consolas", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            recordsLabel.Location = new Point(3, 0);
            recordsLabel.Name = "recordsLabel";
            recordsLabel.Size = new Size(130, 22);
            recordsLabel.TabIndex = 0;
            recordsLabel.Text = "recordsLabel";
            // 
            // RecordsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(289, 450);
            Controls.Add(recordsPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "RecordsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "High Scores";
            recordsPanel.ResumeLayout(false);
            recordsPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel recordsPanel;
        private Label recordsLabel;
    }
}