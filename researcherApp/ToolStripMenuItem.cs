using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;


namespace researcherApp
{
     

    [System.ComponentModel.DesignerCategory("code")]
    [ToolStripItemDesignerAvailability (ToolStripItemDesignerAvailability.ContextMenuStrip | ToolStripItemDesignerAvailability.MenuStrip)]
    public partial class ToolStripMenuItemTrack : ToolStripControlHost
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        public ToolStripMenuItemTrack()
            : base(CreateControlInstance())
        {
        }
      
        public TrackBar TrackBar
        {
            get
            {
                return Control as TrackBar;
            }
        }
        
        private static Control CreateControlInstance()
        {
            TrackBar t = new TrackBar();
            t.AutoSize = false;
            t.Width = 200;
            t.Height = 25;
            t.TickStyle = TickStyle.None;
            t.Minimum = 1;
            return t;
        }


        [DefaultValue(0)]
        public int Value
        {
            get { return TrackBar.Value; }
            set { TrackBar.Value = value; }
        }

      
        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);
            TrackBar trackBar = control as TrackBar;
            trackBar.ValueChanged += new EventHandler(trackBar_ValueChanged);
        }
        
        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnUnsubscribeControlEvents(control);
            TrackBar trackBar = control as TrackBar;
            trackBar.ValueChanged -= new EventHandler(trackBar_ValueChanged);
        }
       
        void trackBar_ValueChanged(object sender, EventArgs e)
        {
        
            if (this.ValueChanged != null)
            {
                ValueChanged(sender, e);
            }
        }
       
        public event EventHandler ValueChanged;

        private static int MakeParam(int loWord, int hiWord)
        {
            return (hiWord << 16) | (loWord & 0xffff);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            //TrackBar trackBar = control as TrackBar;
            
            SendMessage(TrackBar.Handle, 0x0128, MakeParam(1, 0x1), 0);
        }


    }
}
