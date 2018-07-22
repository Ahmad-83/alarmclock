#define SYSTEM_TRAY
#undef PROGRESSBAR_COLOR_DLL
/* Program.cs
 * inClass2 - AlarmClock
 * 
 * Revision History
 *      v0.01
 *          2018.05.31 - Jaehyun Park : Created Project
 *      v0.10
 *          2018.05.31 - Jaehyun Park : Form Design
 *          2018.05.31 - Jaehyun Park : Main feature implementation
 *          2018.05.31 - Jaehyun Park : Support progress bar
 *          2018.05.31 - Jaehyun Park : Support system tray mode
 *          2018.05.31 - Jaehyun Park : Support embedded wav resource with sound player
 *          2018.05.31 - Jaehyun Park : Support progress bar color change
 *      v0.20
 *          2018.05.31 - Jaehyun Park : End time formatter
 */
using System;
using System.Drawing;
using System.Windows.Forms;

using System.Reflection;
using System.IO;
using System.Media;
using System.Text.RegularExpressions;

#if PROGRESSBAR_COLOR_DLL
using System.Runtime.InteropServices;   // custom progress bar
#endif

namespace AlarmClock
{
    public enum AlarmState
    {
        Stopped,
        Running,
        Paused,
        Expired
    }

    public partial class FormAlarmClock : Form
    {
        /// <summary>
        /// private variables
        /// </summary>
        private AlarmState state;
        private TimeSpan? timeLeft;
        private TimeSpan? timeElapsed;
        private TimeSpan? totalTime;

        /// <summary>
        /// get/set properties
        /// </summary>
        public TimeSpan? TimeLeft
        {
            get { return this.timeLeft; }
            set { this.timeLeft = value; }
        }
        public TimeSpan? TimeElapsed
        {
            get { return this.timeElapsed; }
            set { this.timeElapsed = value; }
        }
        public TimeSpan? TotalTime
        {
            get { return this.totalTime; }
            set { this.totalTime = value; }
        }

        /// <summary>
        /// static variables;
        /// </summary>
        static int vibration = 0;   // the range of panel vibration when alram is expired

        #region SoundPlayer with embedded resource
        private Assembly assembly;
        private Stream soundStream;
        private SoundPlayer sp;
        #endregion

        public FormAlarmClock()
        {
            InitializeComponent();
        }

        #region System Tray Implementation
        /// <summary>
        /// [Support System Tray Icon]
        /// mouse double click event of tray icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
#if SYSTEM_TRAY
            this.Visible = true;    // Display Form
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;  // Stop minimizing
            this.Activate();        // Activate Form
#endif
        }

        /// <summary>
        /// [Support System Tray Icon]
        /// Close event of main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAlarmClock_FormClosing(object sender, FormClosingEventArgs e)
        {
#if SYSTEM_TRAY
            e.Cancel = true;        // Cancel closing event
            this.Visible = false;   // Hide Form
#endif
        }

        /// <summary>
        /// [Support System Tray Icon]
        /// Click exit menu of context menu on tray icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if SYSTEM_TRAY
            this.sp.Stop();
            clockTimer.Stop();
            notifyIcon.Icon.Dispose();  // Dispose icon object
            notifyIcon.Dispose();       // Dispose notifyIcon object
            Application.Exit();         // Finally, Exit program
#endif
        }
        #endregion

        #region Timer Method Implementation
        /// <summary>
        /// Calculate time span between end time and current time
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        private bool SetTimeSpan(ref TimeSpan? ts)
        {
            DateTime endTime;

            if (!DateTime.TryParse(textBoxEndTime.Text, out endTime))
                return false;

            ts = endTime - DateTime.Now;

            if (ts.Value.Ticks < 0.0)
                ts += TimeSpan.FromHours(24);

            return true;
        }

        /// <summary>
        /// Get total time span
        /// </summary>
        /// <returns></returns>
        private bool InitClockTimer()
        {
            return SetTimeSpan(ref this.totalTime);
        }

        /// <summary>
        /// Update time span
        /// </summary>
        /// <returns></returns>
        private bool UpdateClockTimer()
        {
            return SetTimeSpan(ref this.timeLeft);
        }

        /// <summary>
        /// Calculate time elapsed as percentage for progress bar from timeLeft and totalTime
        /// </summary>
        /// <returns></returns>
        private double? GetTimeElapsedAsPercentage()
        {
            long tLeft = this.TimeLeft.Value.Ticks;
            long tTotal = this.TotalTime.Value.Ticks;
            long tElapsed = tTotal - tLeft;

            if (tTotal == 0 || tElapsed < 0 || tElapsed > tTotal)
            {
                return 1000.0;
            }

            return 1000.0 * tElapsed / tTotal;
        }

        /// <summary>
        /// Timer tick function
        /// It's going to be forked every 100 miliseconds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clockTimer_Tick(object sender, EventArgs e)
        {
            labelCurrentTime.Text = DateTime.Now.ToString("HH:mm:ss");

            if (this.state == AlarmState.Running)
            {
                UpdateClockTimer();

                // Timer in progress
                if (progressBar.Value < progressBar.Maximum)
                    progressBar.Value = (int)GetTimeElapsedAsPercentage();

                // Timer Expired!
                if (progressBar.Value >= progressBar.Maximum)
                {
#region System Tray Implementation
#if SYSTEM_TRAY
                    this.Visible = true;    // Display Form
                    if (this.WindowState == FormWindowState.Minimized)
                        this.WindowState = FormWindowState.Normal;  // Stop minimizing
                    this.Activate();        // Activate Form
#endif
#endregion
                    this.state = AlarmState.Expired;
                    this.sp.PlayLooping();  // play sound player
                    //clockTimer.Stop();
                }
            }
            if (this.state == AlarmState.Expired)
            {
                //15 13 299 81
                vibration = (vibration + 1) % 2;
                panelClock.Location = new Point(15 - vibration, 13 - vibration);
                labelCurrentTime.ForeColor = (vibration == 0) ? Color.DimGray : Color.Black;
            }
        }
        #endregion  // Timer Method Implementation

        #region Utilities
        /// <summary>
        /// Time formatter
        /// Convert input end time to HH:MM:SS format
        /// </summary>
        /// <returns></returns>
        private bool TimeFormatter()
        {
            string str = textBoxEndTime.Text;
            DateTime dt = new DateTime(DateTime.Now.Ticks);
            Regex reg = new Regex(@"([HMS][a-zA-Z]*)");
            string[] tokens;
            int hour = 0;
            int min = 0;
            int sec = 0;

            str.Replace(" ",  "");
            str = str.ToUpper();

            tokens = reg.Split(str);

            if (tokens.Length <= 2)
                return false;
            
            for (int i = tokens.Length-2; i >= 0; i--)
            {
                switch(tokens[i].Substring(0,1))
                {
                    case "H":
                        if (!int.TryParse(tokens[--i], out hour) || hour < 0 || hour > 24)
                            return false;
                        dt = dt.AddHours(hour);
                        break;
                    case "M":
                        if (!int.TryParse(tokens[--i], out min) || min < 0 || min > 60)
                            return false;
                        dt = dt.AddMinutes(min);
                        break;
                    case "S":
                        if (!int.TryParse(tokens[--i], out sec) || sec < 0 || sec > 60)
                            return false;
                        dt = dt.AddSeconds(sec);
                        break;
                    default:
                        break;
                }
            }
            textBoxEndTime.Text = dt.ToString("HH:mm:ss");
            
            return true;
        }
        #endregion  // Utilities

        #region Event Handler Implementation
        /// <summary>
        /// AlarmClock state machine
        /// </summary>
        void StateMachine()
        {
            switch (this.state)
            {
                case AlarmState.Stopped:
                case AlarmState.Paused:
                    // TODO: end time formatter
                    TimeFormatter();
                    if (InitClockTimer())
                    {
                        buttonStart.Text = "Stop";
                        this.state = AlarmState.Running;
                    }
                    break;
                case AlarmState.Expired:
                    this.sp.Stop();
                    labelCurrentTime.ForeColor = Color.DarkGray;
                    panelClock.Location = new Point(15, 13);
                    textBoxEndTime.Text = "Click to enter time";

                    progressBar.Value = 0;
                    buttonStart.Text = "Start";
                    this.state = AlarmState.Stopped;
                    break;
                case AlarmState.Running:
                    progressBar.Value = 0;
                    buttonStart.Text = "Start";
                    this.state = AlarmState.Stopped;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Click buttonStart
        /// AlarmClock State Machine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            StateMachine();
        }

        /// <summary>
        /// Mouse leave from buttonStart
        /// Text color is going to be changed when mouse comes in and out.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_MouseLeave(object sender, EventArgs e)
        {
            buttonStart.ForeColor = Color.RoyalBlue;
        }

        /// <summary>
        /// Mouse enter into buttonStart
        /// Text color is going to be changed when mouse comes in and out.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_MouseEnter(object sender, EventArgs e)
        {
            buttonStart.ForeColor = Color.Crimson;
        }

        /// <summary>
        /// Click textBoxEndTime
        /// Text is going to be cleaned whenever user click this control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxEndTime_Click(object sender, EventArgs e)
        {
            textBoxEndTime.Text = "";
        }

        /// <summary>
        /// Key handler of end time control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxEndTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBox tb = sender as TextBox;

                //SendKeys.Send("{TAB}");
                //tb.Enabled = false;
                StateMachine();
            }
            */
        }

        /// <summary>
        /// Load main form
        /// 1. Create sound player object
        /// 2. Set properties what it's needed
        /// 3. Start clock timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAlarmClock_Load(object sender, EventArgs e)
        {

#region SoundPlayer with embedded resource
            /*
             * Solution Explorer -> Project -> Add -> Existing Item
             * Added resource property -> Build Action -> Embedded Resource
             * GetManifestResourceStream("<AssemblyName>.<SubPath>.filename")   
             * cf. <AssemblyName> is your namespace.
             */
            assembly = Assembly.GetExecutingAssembly();
            sp = new SoundPlayer(assembly.GetManifestResourceStream
                ("AlarmClock.Resources.Alarm10.wav"));
#endregion

            buttonStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;   // Remove hover background color

            progressBar.Minimum = 0;
            progressBar.Maximum = 1000;
            state = AlarmState.Stopped;

            clockTimer.Interval = 100;
            clockTimer.Start();

#if PROGRESSBAR_COLOR_DLL
            ModifyProgressBarColor.SetState(progressBar, 2);
#endif
        }
        #endregion  // Event Handler Implementation
    }
#if PROGRESSBAR_COLOR_DLL
    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }
#endif
}
