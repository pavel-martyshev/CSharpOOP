using System.Windows.Forms;

namespace TemperatureConverterTask;

partial class TemperatureConverterView
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
        inputPanel = new Panel();
        inputScalesComboBox = new ComboBox();
        inputTemperatureTextBox = new TextBox();
        convertedTemperaturePanel = new Panel();
        convertedTemperatureLabel = new Label();
        conversionScalesComboBox = new ComboBox();
        inputPanel.SuspendLayout();
        convertedTemperaturePanel.SuspendLayout();
        SuspendLayout();
        // 
        // inputPanel
        // 
        inputPanel.Controls.Add(inputScalesComboBox);
        inputPanel.Controls.Add(inputTemperatureTextBox);
        inputPanel.Location = new Point(12, 12);
        inputPanel.Name = "inputPanel";
        inputPanel.Size = new Size(330, 86);
        inputPanel.TabIndex = 4;
        // 
        // inputScalesComboBox
        // 
        inputScalesComboBox.BackColor = Color.FromArgb(27, 26, 32);
        inputScalesComboBox.DisplayMember = "Value";
        inputScalesComboBox.FlatStyle = FlatStyle.Flat;
        inputScalesComboBox.Font = new Font("Arial", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
        inputScalesComboBox.ForeColor = Color.FromArgb(230, 230, 230);
        inputScalesComboBox.FormattingEnabled = true;
        inputScalesComboBox.Location = new Point(0, 56);
        inputScalesComboBox.MaxDropDownItems = 3;
        inputScalesComboBox.Name = "inputScalesComboBox";
        inputScalesComboBox.Size = new Size(330, 30);
        inputScalesComboBox.TabIndex = 4;
        inputScalesComboBox.ValueMember = "Key";
        inputScalesComboBox.SelectedIndexChanged += InputScalesComboBox_SelectedIndexChanged;
        // 
        // inputTemperatureTextBox
        // 
        inputTemperatureTextBox.BackColor = Color.FromArgb(27, 26, 32);
        inputTemperatureTextBox.BorderStyle = BorderStyle.FixedSingle;
        inputTemperatureTextBox.Font = new Font("Arial", 36F, FontStyle.Regular, GraphicsUnit.Point, 204);
        inputTemperatureTextBox.ForeColor = Color.FromArgb(230, 230, 230);
        inputTemperatureTextBox.Location = new Point(0, 0);
        inputTemperatureTextBox.Name = "inputTemperatureTextBox";
        inputTemperatureTextBox.RightToLeft = RightToLeft.No;
        inputTemperatureTextBox.Size = new Size(330, 63);
        inputTemperatureTextBox.TabIndex = 5;
        inputTemperatureTextBox.TextAlign = HorizontalAlignment.Center;
        inputTemperatureTextBox.TextChanged += InputTemperatureTextBox_TextChanged;
        // 
        // convertedTemperaturePanel
        // 
        convertedTemperaturePanel.Controls.Add(convertedTemperatureLabel);
        convertedTemperaturePanel.Controls.Add(conversionScalesComboBox);
        convertedTemperaturePanel.Location = new Point(348, 12);
        convertedTemperaturePanel.Name = "convertedTemperaturePanel";
        convertedTemperaturePanel.Size = new Size(330, 86);
        convertedTemperaturePanel.TabIndex = 6;
        // 
        // convertedTemperatureLabel
        // 
        convertedTemperatureLabel.BorderStyle = BorderStyle.FixedSingle;
        convertedTemperatureLabel.Font = new Font("Arial", 36F, FontStyle.Regular, GraphicsUnit.Point, 204);
        convertedTemperatureLabel.ForeColor = Color.FromArgb(230, 230, 230);
        convertedTemperatureLabel.Location = new Point(0, 0);
        convertedTemperatureLabel.Name = "convertedTemperatureLabel";
        convertedTemperatureLabel.Size = new Size(330, 55);
        convertedTemperatureLabel.TabIndex = 5;
        convertedTemperatureLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // conversionScalesComboBox
        // 
        conversionScalesComboBox.BackColor = Color.FromArgb(27, 26, 32);
        conversionScalesComboBox.DisplayMember = "Value";
        conversionScalesComboBox.FlatStyle = FlatStyle.Flat;
        conversionScalesComboBox.Font = new Font("Arial", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
        conversionScalesComboBox.ForeColor = Color.FromArgb(230, 230, 230);
        conversionScalesComboBox.FormattingEnabled = true;
        conversionScalesComboBox.Location = new Point(0, 56);
        conversionScalesComboBox.MaxDropDownItems = 3;
        conversionScalesComboBox.Name = "conversionScalesComboBox";
        conversionScalesComboBox.Size = new Size(330, 30);
        conversionScalesComboBox.TabIndex = 4;
        conversionScalesComboBox.ValueMember = "Key";
        conversionScalesComboBox.SelectedIndexChanged += ConversionScalesComboBox_SelectedIndexChanged;
        // 
        // TemperatureConverterView
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(27, 26, 32);
        ClientSize = new Size(690, 110);
        Controls.Add(convertedTemperaturePanel);
        Controls.Add(inputPanel);
        Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Name = "TemperatureConverterView";
        Text = "Конвертер температуры";
        inputPanel.ResumeLayout(false);
        inputPanel.PerformLayout();
        convertedTemperaturePanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion
    public Panel inputPanel;
    public ComboBox inputScalesComboBox;
    public TextBox inputTemperatureTextBox;
    public Panel convertedTemperaturePanel;
    public ComboBox conversionScalesComboBox;
    public Label convertedTemperatureLabel;
}
