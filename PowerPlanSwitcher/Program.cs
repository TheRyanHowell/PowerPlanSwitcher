using PowerPlanSwitcher.Properties;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PowerManagerAPI;
using System.Timers;
using System.Runtime.InteropServices;

namespace PowerPlanSwitcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrayContext());
        }
    }

    public class TrayContext : ApplicationContext
    {
        private NotifyIcon trayIcon;
        private SettingsForm settingsForm;

        System.Timers.Timer aTimer;

        public static Guid powerSaverPlanGuid = new Guid("a1841308-3541-4fab-bc81-f71556f20b4a");
        public static Guid balancedPlanGuid = new Guid("381b4222-f694-41f0-9685-ff5bb260df2e");
        public static Guid highPerformancePlanGuid = new Guid("8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c");

        private bool sleep = false;
        private Guid oldPlan = balancedPlanGuid;

        // Import user32.dll, used to figure out how long it's been since user activity
        [DllImport("user32.dll")]
        public static extern Boolean GetLastInputInfo(ref tagLASTINPUTINFO plii);

        public struct tagLASTINPUTINFO
        {
            public uint cbSize;
            public Int32 dwTime;
        }

        public TrayContext()
        {
            // Create the settings form, hidden
            settingsForm = new SettingsForm();
            settingsForm.Hide();
            
            // Set the tray icon, with the power plan context menu
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenu = new ContextMenu(GetTrayList().ToArray()),
                Visible = true,
            };

            // Update tray hover text
            UpdateTrayText();

            // Setup a timer to check for events
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimerEvent);
            aTimer.Interval = settingsForm.getPollInterval();
            aTimer.Enabled = true;
        }

        // Get the list of power plans for the tray context menu
        private List<MenuItem> GetTrayList()
        {
            var activePlan = PowerManager.GetActivePlan();

            List<MenuItem> menuList = new List<MenuItem>();
            var planList = PowerManager.ListPlans();

            foreach (var plan in planList)
            {
                var name = PowerManager.GetPlanName(plan);
                if (plan == activePlan)
                {
                    name += " (active)";
                }

                menuList.Add(new MenuItem(name, (sender, e) => { ChangePlan(plan); }));
            }

            menuList.Add(new MenuItem("Settings", SettingsHandler));
            menuList.Add(new MenuItem("Exit", ExitHandler));

            return menuList;
        }

        // Change power plan, updating the context menu
        void ChangePlan(System.Guid plan)
        {
            PowerManager.SetActivePlan(plan);
            UpdateTrayMenu();
        }

        // Re-generate the context menu
        private void UpdateTrayMenu()
        {
            trayIcon.ContextMenu = new ContextMenu(GetTrayList().ToArray());
        }

        // Update tray hover text, with power plan and battery status
        private void UpdateTrayText()
        {
            trayIcon.Text = PowerManager.GetPlanName(PowerManager.GetActivePlan());

            if (SystemInformation.PowerStatus.BatteryChargeStatus != BatteryChargeStatus.NoSystemBattery)
            {
                trayIcon.Text += " | " + SystemInformation.PowerStatus.BatteryLifePercent.ToString("P0");
                if (SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.Charging)
                {
                    trayIcon.Text += " | Charging";
                }

                if (SystemInformation.PowerStatus.BatteryLifePercent <= 100)
                {
                    trayIcon.Icon = Resources.Battery100;
                }
                else if (SystemInformation.PowerStatus.BatteryLifePercent <= 90)
                {
                    trayIcon.Icon = Resources.Battery90;
                }
                else if (SystemInformation.PowerStatus.BatteryLifePercent <= 80)
                {
                    trayIcon.Icon = Resources.Battery80;
                }
                else if (SystemInformation.PowerStatus.BatteryLifePercent <= 70)
                {
                    trayIcon.Icon = Resources.Battery70;
                }
                else if (SystemInformation.PowerStatus.BatteryLifePercent <= 60)
                {
                    trayIcon.Icon = Resources.Battery60;
                }
                else if (SystemInformation.PowerStatus.BatteryLifePercent <= 50)
                {
                    trayIcon.Icon = Resources.Battery50;
                }
                else if (SystemInformation.PowerStatus.BatteryLifePercent <= 40)
                {
                    trayIcon.Icon = Resources.Battery40;
                }
                else if (SystemInformation.PowerStatus.BatteryLifePercent <= 30)
                {
                    trayIcon.Icon = Resources.Battery30;
                }
                else if (SystemInformation.PowerStatus.BatteryLifePercent <= 20)
                {
                    trayIcon.Icon = Resources.Battery20;
                }
                else if (SystemInformation.PowerStatus.BatteryLifePercent <= 10)
                {
                    trayIcon.Icon = Resources.Battery10;
                }
            }
        }

        // When the event timer fires
        private void OnTimerEvent(object source, ElapsedEventArgs e)
        {
            // Update timer the interval based on settings
            aTimer.Interval = settingsForm.getPollInterval();
           
            var madeChange = false;
            
            // If idle event is enabled
            if (settingsForm.getIdleEnabled())
            {
                // If we are idle beyond timeout
                if (GetIdleTime() > settingsForm.getIdleTimeout())
                {
                    // And not already sleeping
                    if (!sleep)
                    {
                        // Store the old plan, and switch to the defined sleep plan
                        oldPlan = PowerManager.GetActivePlan();
                        PowerManager.SetActivePlan(settingsForm.getIdlePlan());
                        sleep = true;
                        madeChange = true;
                        Console.WriteLine("Sleep: Idle");
                    }
                }
                // We are sleeping but no longer idle, change to previous plan
                else if (sleep == true)
                {
                    PowerManager.SetActivePlan(oldPlan);
                    sleep = false;
                    madeChange = true;
                    Console.WriteLine("Woken: Idle");
                }
            }

            // If battery event enabled
            if (settingsForm.getBatEnabled())
            {
                // If battery in use
                if (SystemInformation.PowerStatus.BatteryChargeStatus != BatteryChargeStatus.NoSystemBattery && SystemInformation.PowerStatus.BatteryChargeStatus != BatteryChargeStatus.Unknown)
                {
                    // If battery lower than defined level for sleep
                    var batPer = SystemInformation.PowerStatus.BatteryLifePercent;
                    if (batPer < settingsForm.getBatLevel())
                    {
                        // And not already sleeping
                        if (!sleep)
                        {
                            // Store the old plan, and switch to the defined sleep plan
                            oldPlan = PowerManager.GetActivePlan();
                            PowerManager.SetActivePlan(settingsForm.getBatPlan());
                            sleep = true;
                            madeChange = true;

                            Console.WriteLine("Sleep: Battery");
                        }
                    }
                    // We are sleeping but no longer below battery threshold, change to previous plan
                    else if (sleep == true)
                    {
                        PowerManager.SetActivePlan(oldPlan);
                        sleep = false;
                        madeChange = true;

                        Console.WriteLine("Woken: Battery");
                    }
                }
            }

            // If the state changed, update the tray with new power plan
            if (madeChange)
            {
                UpdateTrayMenu();
                UpdateTrayText();
            }
        }

        // Figure out how long we have been idle for
        private Int32 GetIdleTime()
        {
            tagLASTINPUTINFO LastInput = new tagLASTINPUTINFO();
            Int32 IdleTime = -1;
            LastInput.cbSize = (uint)Marshal.SizeOf(LastInput);
            LastInput.dwTime = 0;

            if (GetLastInputInfo(ref LastInput))
            {
                IdleTime = System.Environment.TickCount - LastInput.dwTime;
            }
            return IdleTime;
        }

        // Handler for settings button in context menu
        void SettingsHandler(object sender, EventArgs e)
        {
            settingsForm.Show();
        }

        // Handle for exit button in context menu
        void ExitHandler(object sender, EventArgs e)
        {
            trayIcon.Visible = false;

            Application.Exit();
        }
    }
}
