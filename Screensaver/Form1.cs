using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Screensaver
{
    /// <summary>
    /// Main form. It shows the screensaver, either in preview mode or screensaver mode.
    /// </summary>
    public partial class Form1 : Form
    {
        bool preview;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        public Form1(Rectangle rect)
        {
            InitializeComponent();
            this.Bounds = rect;
            Cursor.Hide();

            this.preview = false;
        }
        // Win32API required for displaying the form inside the handle passed in preview mode.
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        /// <summary>
        /// Constructor for the preview mode. Resizes the form according to the window pointed by the handle.
        /// </summary>
        /// <param name="handle">Handle to a window in which the form is displayed.</param>
        public Form1(IntPtr handle)
        {
            InitializeComponent();
            SetParent(this.Handle, handle);
            SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));
            Rectangle parent;
            GetClientRect(handle, out parent);
            this.Size = parent.Size;
            this.Location = new Point(0, 0);

            this.preview = true;
        }

        // Mouse position (initialized to an undefined value).
        int x = -1;
        int y = -1;

        /// <summary>
        /// Mouse events handler. Since they are fired also at the form's creation, the handler has to keep track of the mouse position and close the application only if it has moved and if it's not in preview mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Input(object sender, MouseEventArgs e)
        {
            if (x == -1 && y == -1)
            {
                x = e.X;
                y = e.Y;
            }

            if (Math.Abs(e.X - x) > 0 || Math.Abs(e.Y - y) > 0)
                if (!this.preview)
                    Application.Exit();
        }

        /// <summary>
        /// Keyboard event handler. Closes the application if it's not in preview mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Input(object sender, KeyEventArgs e)
        {
            if (!this.preview)
                Application.Exit();
        }

        /// <summary>
        /// Timer tick event handler. The main logic of the screensaver should be updated from here, before invalidating the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            // TO DO: Main logic here (possibly decoupling it from the form, e.g. with a MVC architecture)...


            this.Invalidate();
        }

        /// <summary>
        /// Paint event handler. It should draw the screensaver.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Example drawing: a random line.
            Random r = new Random();
            Point a = new Point(r.Next(this.Bounds.Width), r.Next(this.Bounds.Height));
            Point b = new Point(r.Next(this.Bounds.Width), r.Next(this.Bounds.Height));

            e.Graphics.DrawLine(Pens.Black, a, b);
        }
    }
}
