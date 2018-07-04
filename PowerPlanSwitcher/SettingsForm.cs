using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;
using PowerManagerAPI;

namespace PowerPlanSwitcher
{
    public partial class SettingsForm : Form
    {
        // Define default settings
        private bool batEnabled = false;
        private int batLevel = 50;
        private Guid batPlan = TrayContext.powerSaverPlanGuid;


        private bool idleEnabled = false;
        private int idleTimeout = 60000;
        private Guid idlePlan = TrayContext.powerSaverPlanGuid;

        private int pollInterval = 5000;
        private bool autoStart = false;

        RegistryKey registryAppKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // Get existing settings from registry
            if (registryAppKey.GetValue("PowerPlanSwitcher") == null)
            {
                autoStart = false;
            }
            else
            {
                autoStart = true;
            }

            try
            {
                if (Properties.Settings.Default["batEnabled"] != null)
                {
                    this.batEnabled = (bool)Properties.Settings.Default["batEnabled"];
                }
            }
            catch (Exception) { }

            try
            {
                if (Properties.Settings.Default["idleEnabled"] != null)
                {
                    this.idleEnabled = (bool)Properties.Settings.Default["idleEnabled"];
                }
            }
            catch (Exception) { }

            try
            {
                if (Properties.Settings.Default["batLevel"] != null)
                {
                    this.batLevel = (int)Properties.Settings.Default["batLevel"];
                }
            }
            catch (Exception) { }

            try
            {
                if (Properties.Settings.Default["idleTimeout"] != null)
                {
                    this.idleTimeout = (int)Properties.Settings.Default["idleTimeout"];
                }
            }
            catch (Exception) { }

            try
            {
                if (Properties.Settings.Default["idlePlan"] != null)
                {
                    this.idlePlan = new Guid((string)Properties.Settings.Default["idlePlan"]);
                }
            }
            catch (Exception) { }

            try
            {
                if (Properties.Settings.Default["batPlan"] != null)
                {
                    this.batPlan = new Guid((string)Properties.Settings.Default["batPlan"]);
                }
            }
            catch (Exception) { }

            try
            {
                if (Properties.Settings.Default["pollInterval"] != null)
                {
                    this.pollInterval = (int)Properties.Settings.Default["pollInterval"];
                }
            }
            catch (Exception) { }

            // Define dropdown style
            idleSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            batterySelect.DropDownStyle = ComboBoxStyle.DropDownList;

            // Create dropdown options of power plans, bound by index
            Dictionary<string, string> planSource = new Dictionary<string, string>();
            var planList = PowerManager.ListPlans();
            var idleSelectedIndex = 0;
            var batterySelectedIndex = 0;

            for (var i = 0; i < planList.Count; i++)
            {
                if (planList[i] == this.batPlan)
                {
                    batterySelectedIndex = i;
                }

                if (planList[i] == this.idlePlan)
                {
                    idleSelectedIndex = i;
                }

                planSource.Add(planList[i].ToString(), PowerManager.GetPlanName(planList[i]));
            }
            
            idleSelect.DataSource = new BindingSource(planSource, null);
            idleSelect.DisplayMember = "Value";
            idleSelect.ValueMember = "Key";
            idleSelect.SelectedIndex = idleSelectedIndex;

            batterySelect.DataSource = new BindingSource(planSource, null);
            batterySelect.DisplayMember = "Value";
            batterySelect.ValueMember = "Key";
            batterySelect.SelectedIndex = batterySelectedIndex;

            // Load all other settings into form
            batteryInput.Value = this.batLevel;
            idleInput.Value = this.idleTimeout;
            batteryCheckBox.Checked = this.batEnabled;
            idleCheckBox.Checked = this.idleEnabled;
        }

        private void batteryCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.batEnabled = batteryCheckBox.Checked;
            Properties.Settings.Default["batEnabled"] = this.batEnabled;
        }

        private void idleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.idleEnabled = idleCheckBox.Checked;
            Properties.Settings.Default["idleEnabled"] = this.idleEnabled;
        }

        private void idleInput_ValueChanged(object sender, EventArgs e)
        {
            this.idleTimeout = (int)idleInput.Value;
            Properties.Settings.Default["idleTimeout"] = this.idleTimeout;
        }

        private void batteryInput_ValueChanged(object sender, EventArgs e)
        {
            this.batLevel = (int)batteryInput.Value;
            Properties.Settings.Default["batLevel"] = this.batLevel;
        }

        private void batterySelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedPlan = ((KeyValuePair<string, string>)batterySelect.SelectedItem).Key;
            this.batPlan = new Guid(selectedPlan);
            Properties.Settings.Default["batPlan"] = selectedPlan;
        }

        private void idleSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedPlan = ((KeyValuePair<string, string>)idleSelect.SelectedItem).Key;
            this.idlePlan = new Guid(selectedPlan);
            Properties.Settings.Default["idlePlan"] = selectedPlan;
        }

        private void pollInput_ValueChanged(object sender, EventArgs e)
        {
            this.pollInterval = (int)pollInput.Value;
            Properties.Settings.Default["pollInterval"] = this.pollInterval;
        }

        private void autoStartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.autoStart = autoStartCheckBox.Checked;
            if (this.autoStart)
            {
                registryAppKey.SetValue("PowerPlanSwitcher", Application.ExecutablePath);
            }
            else
            {
                registryAppKey.DeleteValue("PowerPlanSwitcher", false);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
            if (e.CloseReason != CloseReason.WindowsShutDown)
            {
                this.Visible = false;
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }

        public bool getBatEnabled()
        {
            return this.batEnabled;
        }

        public int getBatLevel()
        {
            return this.batLevel;
        }

        public Guid getBatPlan()
        {
            return this.batPlan;
        }

        public bool getIdleEnabled()
        {
            return this.idleEnabled;
        }

        public int getIdleTimeout()
        {
            return this.idleTimeout;
        }

        public Guid getIdlePlan()
        {
            return this.idlePlan;
        }

        public int getPollInterval()
        {
            return this.pollInterval;
        }
    }
}
