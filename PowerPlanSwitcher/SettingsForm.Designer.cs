namespace PowerPlanSwitcher
{
    partial class SettingsForm
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.idleSelect = new System.Windows.Forms.ComboBox();
            this.batteryInput = new System.Windows.Forms.NumericUpDown();
            this.idleInput = new System.Windows.Forms.NumericUpDown();
            this.batterySelect = new System.Windows.Forms.ComboBox();
            this.idleCheckBox = new System.Windows.Forms.CheckBox();
            this.batteryCheckBox = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pollInput = new System.Windows.Forms.NumericUpDown();
            this.pollIntervalLabel = new System.Windows.Forms.Label();
            this.autoStartCheckBox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.batteryInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idleInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pollInput)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.idleSelect, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.batteryInput, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.idleInput, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.batterySelect, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.idleCheckBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.batteryCheckBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pollInput, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.autoStartCheckBox, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.pollIntervalLabel, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(776, 119);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // idleSelect
            // 
            this.idleSelect.FormattingEnabled = true;
            this.idleSelect.Location = new System.Drawing.Point(291, 30);
            this.idleSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.idleSelect.Name = "idleSelect";
            this.idleSelect.Size = new System.Drawing.Size(485, 24);
            this.idleSelect.TabIndex = 4;
            this.toolTip1.SetToolTip(this.idleSelect, "On idle, which plan should be used?");
            this.idleSelect.SelectedIndexChanged += new System.EventHandler(this.idleSelect_SelectedIndexChanged);
            // 
            // batteryInput
            // 
            this.batteryInput.Location = new System.Drawing.Point(178, 2);
            this.batteryInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.batteryInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.batteryInput.Name = "batteryInput";
            this.batteryInput.Size = new System.Drawing.Size(107, 22);
            this.batteryInput.TabIndex = 3;
            this.toolTip1.SetToolTip(this.batteryInput, "At what battery threshold should the power plan switch?");
            this.batteryInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.batteryInput.ValueChanged += new System.EventHandler(this.batteryInput_ValueChanged);
            // 
            // idleInput
            // 
            this.idleInput.Location = new System.Drawing.Point(178, 30);
            this.idleInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.idleInput.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.idleInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.idleInput.Name = "idleInput";
            this.idleInput.Size = new System.Drawing.Size(107, 22);
            this.idleInput.TabIndex = 2;
            this.toolTip1.SetToolTip(this.idleInput, "After how long should the computer be considered idle?");
            this.idleInput.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.idleInput.ValueChanged += new System.EventHandler(this.idleInput_ValueChanged);
            // 
            // batterySelect
            // 
            this.batterySelect.FormattingEnabled = true;
            this.batterySelect.Location = new System.Drawing.Point(291, 2);
            this.batterySelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.batterySelect.Name = "batterySelect";
            this.batterySelect.Size = new System.Drawing.Size(485, 24);
            this.batterySelect.TabIndex = 5;
            this.toolTip1.SetToolTip(this.batterySelect, "When battery threshold reached, what plan should be used?");
            this.batterySelect.SelectedIndexChanged += new System.EventHandler(this.batterySelect_SelectedIndexChanged);
            // 
            // idleCheckBox
            // 
            this.idleCheckBox.AutoSize = true;
            this.idleCheckBox.Location = new System.Drawing.Point(3, 30);
            this.idleCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.idleCheckBox.Name = "idleCheckBox";
            this.idleCheckBox.Size = new System.Drawing.Size(152, 21);
            this.idleCheckBox.TabIndex = 0;
            this.idleCheckBox.Text = "Idle Threshold (ms)";
            this.toolTip1.SetToolTip(this.idleCheckBox, "Switching to a different power plan when computer is idle.");
            this.idleCheckBox.UseVisualStyleBackColor = true;
            this.idleCheckBox.CheckedChanged += new System.EventHandler(this.idleCheckBox_CheckedChanged);
            // 
            // batteryCheckBox
            // 
            this.batteryCheckBox.AutoSize = true;
            this.batteryCheckBox.Location = new System.Drawing.Point(3, 2);
            this.batteryCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.batteryCheckBox.Name = "batteryCheckBox";
            this.batteryCheckBox.Size = new System.Drawing.Size(169, 21);
            this.batteryCheckBox.TabIndex = 1;
            this.batteryCheckBox.Text = "Battery Threshold (%)";
            this.toolTip1.SetToolTip(this.batteryCheckBox, "Switching to a different power plan when battery reaches a certain threshold.");
            this.batteryCheckBox.UseVisualStyleBackColor = true;
            this.batteryCheckBox.CheckedChanged += new System.EventHandler(this.batteryCheckBox_CheckedChanged);
            // 
            // pollInput
            // 
            this.pollInput.Location = new System.Drawing.Point(178, 58);
            this.pollInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pollInput.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.pollInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pollInput.Name = "pollInput";
            this.pollInput.Size = new System.Drawing.Size(107, 22);
            this.pollInput.TabIndex = 7;
            this.toolTip1.SetToolTip(this.pollInput, "How often should the application check for changes?");
            this.pollInput.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.pollInput.ValueChanged += new System.EventHandler(this.pollInput_ValueChanged);
            // 
            // pollIntervalLabel
            // 
            this.pollIntervalLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pollIntervalLabel.AutoSize = true;
            this.pollIntervalLabel.Location = new System.Drawing.Point(4, 60);
            this.pollIntervalLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.pollIntervalLabel.Name = "pollIntervalLabel";
            this.pollIntervalLabel.Size = new System.Drawing.Size(113, 17);
            this.pollIntervalLabel.TabIndex = 8;
            this.pollIntervalLabel.Text = "Poll Interval (ms)";
            this.pollIntervalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.pollIntervalLabel, "The application checks for battery state and idle time on a timer.");
            // 
            // autoStartCheckBox
            // 
            this.autoStartCheckBox.AutoSize = true;
            this.autoStartCheckBox.Location = new System.Drawing.Point(3, 84);
            this.autoStartCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.autoStartCheckBox.Name = "autoStartCheckBox";
            this.autoStartCheckBox.Size = new System.Drawing.Size(87, 21);
            this.autoStartCheckBox.TabIndex = 9;
            this.autoStartCheckBox.Text = "Autostart";
            this.toolTip1.SetToolTip(this.autoStartCheckBox, "Should the application autostart?");
            this.autoStartCheckBox.UseVisualStyleBackColor = true;
            this.autoStartCheckBox.CheckedChanged += new System.EventHandler(this.autoStartCheckBox_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 134);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.batteryInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idleInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pollInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox idleSelect;
        private System.Windows.Forms.NumericUpDown batteryInput;
        private System.Windows.Forms.NumericUpDown idleInput;
        private System.Windows.Forms.ComboBox batterySelect;
        private System.Windows.Forms.CheckBox idleCheckBox;
        private System.Windows.Forms.CheckBox batteryCheckBox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label pollIntervalLabel;
        private System.Windows.Forms.NumericUpDown pollInput;
        private System.Windows.Forms.CheckBox autoStartCheckBox;
    }
}