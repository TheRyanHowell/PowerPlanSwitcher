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

        [DllImport("user32.dll")]
        public static extern Boolean GetLastInputInfo(ref tagLASTINPUTINFO plii);

        public struct tagLASTINPUTINFO
        {
            public uint cbSize;
            public Int32 dwTime;
        }

        public TrayContext()
        {
            settingsForm = new SettingsForm();
            settingsForm.Hide();
            
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenu = new ContextMenu(GetTrayList().ToArray()),
                Visible = true,
                
            };

            UpdateTrayText();

            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimerEvent);
            aTimer.Interval = settingsForm.getPollInterval();
            aTimer.Enabled = true;
        }

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

        void ChangePlan(System.Guid plan)
        {
            PowerManager.SetActivePlan(plan);
            UpdateTrayMenu();
        }

        private void UpdateTrayMenu()
        {
            trayIcon.ContextMenu = new ContextMenu(GetTrayList().ToArray());
        }

        private void UpdateTrayText()
        {
            trayIcon.Text = PowerManager.GetPlanName(PowerManager.GetActivePlan());

            if (SystemInformation.PowerStatus.BatteryChargeStatus != BatteryChargeStatus.NoSystemBattery)
            {
                trayIcon.Text += " | " + SystemInformation.PowerStatus.BatteryLifePercent.ToString("P0");
                if(SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.Charging)
                {
                    trayIcon.Text += " | Charging";
                }
            }
        }

        private void OnTimerEvent(object source, ElapsedEventArgs e)
        {
            aTimer.Interval = settingsForm.getPollInterval();
            var madeChange = false;
            if (settingsForm.getIdleEnabled())
            {
                if (GetIdleTime() > settingsForm.getIdleTimeout())
                {
                    if (!sleep)
                    {
                        oldPlan = PowerManager.GetActivePlan();
                        PowerManager.SetActivePlan(settingsForm.getIdlePlan());
                        sleep = true;
                        madeChange = true;
                        Console.WriteLine("Sleep: Idle");
                    }
                }
                else if (sleep == true)
                {
                    PowerManager.SetActivePlan(oldPlan);
                    sleep = false;
                    madeChange = true;
                    Console.WriteLine("Woken: Idle");
                }
            }

            if (settingsForm.getBatEnabled())
            {
                if (SystemInformation.PowerStatus.BatteryChargeStatus != BatteryChargeStatus.NoSystemBattery)
                {
                    var batPer = SystemInformation.PowerStatus.BatteryLifePercent;
                    if (batPer < settingsForm.getBatLevel())
                    {
                        if (!sleep)
                        {
                            oldPlan = PowerManager.GetActivePlan();
                            PowerManager.SetActivePlan(settingsForm.getBatPlan());
                            sleep = true;
                            madeChange = true;

                            Console.WriteLine("Sleep: Battery");
                        }
                    }
                    else if (sleep == true)
                    {
                        PowerManager.SetActivePlan(oldPlan);
                        sleep = false;
                        madeChange = true;

                        Console.WriteLine("Woken: Battery");
                    }
                }
            }

            if (madeChange)
            {
                UpdateTrayMenu();
            }
        }

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

        void SettingsHandler(object sender, EventArgs e)
        {
            settingsForm.Show();
        }

        void ExitHandler(object sender, EventArgs e)
        {
            trayIcon.Visible = false;

            Application.Exit();
        }
    }
}
