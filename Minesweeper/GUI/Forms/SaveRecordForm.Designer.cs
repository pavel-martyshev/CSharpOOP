namespace Minesweeper.GUI
{
    partial class SaveRecordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveRecordForm));
            saveRecordLayoutPanel = new FlowLayoutPanel();
            congratulationsLabel = new Label();
            nameTextBox = new TextBox();
            saveButton = new Button();
            saveRecordLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // saveRecordLayoutPanel
            // 
            saveRecordLayoutPanel.BorderStyle = BorderStyle.Fixed3D;
            saveRecordLayoutPanel.Controls.Add(congratulationsLabel);
            saveRecordLayoutPanel.Controls.Add(nameTextBox);
            saveRecordLayoutPanel.Controls.Add(saveButton);
            saveRecordLayoutPanel.Location = new Point(12, 12);
            saveRecordLayoutPanel.Name = "saveRecordLayoutPanel";
            saveRecordLayoutPanel.Size = new Size(470, 100);
            saveRecordLayoutPanel.TabIndex = 0;
            // 
            // congratulationsLabel
            // 
            congratulationsLabel.Dock = DockStyle.Top;
            congratulationsLabel.Font = new Font("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            congratulationsLabel.Location = new Point(3, 0);
            congratulationsLabel.Name = "congratulationsLabel";
            congratulationsLabel.Size = new Size(459, 64);
            congratulationsLabel.TabIndex = 0;
            congratulationsLabel.Text = "Congratulations! Enter your name and click \"Save\" to save your result";
            congratulationsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // nameTextBox
            // 
            nameTextBox.BorderStyle = BorderStyle.FixedSingle;
            nameTextBox.Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            nameTextBox.Location = new Point(3, 67);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(149, 22);
            nameTextBox.TabIndex = 1;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(158, 67);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 23);
            saveButton.TabIndex = 2;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += SaveButton_Click;
            // 
            // SaveRecordForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(494, 124);
            Controls.Add(saveRecordLayoutPanel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "SaveRecordForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Save new record";
            saveRecordLayoutPanel.ResumeLayout(false);
            saveRecordLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel saveRecordLayoutPanel;
        private Label congratulationsLabel;
        private TextBox nameTextBox;
        private Button saveButton;
    }
}