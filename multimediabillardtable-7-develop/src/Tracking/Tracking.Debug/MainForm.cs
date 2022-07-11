using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Windows.Forms;
using Tracking.Core;
using Tracking.Core.Debug;
using Tracking.Core.Maths;
using Tracking.Model;

namespace Tracking.Debug
{
    public partial class MainForm : Form
    {
        private readonly TrackingEngine trackingEngine = new TrackingEngine();
        private Frame latestFrame;

        public MainForm()
        {
            InitializeComponent();
            trackingEngine.FrameAvailable += trackingEngine_FrameAvailable;
            trackingEngine.CalibrationCompleted += trackingEngine_CalibrationCompleted;
            FrameTracer.IsEnabled = true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            trackingEngine.Open();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            trackingEngine.Close();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            trackingEngine.Calibrate();
            btnCalibrate.Enabled = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            trackingEngine.Reset();
        }

        private void trackingEngine_FrameAvailable(object sender, Frame e)
        {
            if (cbDebugFrame.Items.Count != FrameTracer.Frames.Count)
            {
                cbDebugFrame.Items.Clear();
                foreach (var frame in FrameTracer.Frames.Keys)
                    cbDebugFrame.Items.Add(frame);
            }

            if (cbDebugFrame.SelectedIndex != -1)
            {
                var debugFrame = cbDebugFrame.SelectedItem as string;
                imageBox.Image = FrameTracer.Frames[debugFrame];
            }

            latestFrame = e;
        }

        private void trackingEngine_CalibrationCompleted(object sender, EventArgs e)
        {
            btnCalibrate.Enabled = true;
        }

        private void imageBox_Click(object sender, EventArgs e)
        {
            using (var bmp = new Bitmap(imageBox.Width, imageBox.Height))
            {
                imageBox.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                var clickPosition = imageBox.PointToClient(MousePosition);
                var clickedPixel = bmp.GetPixel(clickPosition.X, clickPosition.Y);
                var bgra = new Bgra(clickedPixel.B, clickedPixel.G, clickedPixel.R, 255);
                var hsv = bgra.ToHsv();
                tbClickedColor.Text = $"{hsv.Hue:0.###}/{hsv.Satuation:0.###}/{hsv.Value:0.###}";
            }
        }

        private void imageBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (latestFrame == null)
                return;

            var mousePos = imageBox.PointToClient(MousePosition).ToFloat().Mul(1.0f / (float)imageBox.ZoomScale);
            mousePos = mousePos.Add(new PointF(imageBox.HorizontalScrollBar.Value, imageBox.VerticalScrollBar.Value));
            lbCoordinates.Text = mousePos.ToString();

            tbHoveringBall.Clear();
            foreach (var ball in latestFrame.Balls)
                if (ball.RawPosition.DistanceTo(mousePos) < 7)
                    tbHoveringBall.Text = $"{ball.Type}\r\n  RawPosition: {ball.RawPosition}\r\n  Position: {ball.Position}\r\n  Velocity: {ball.Velocity}\r\n  OnTable: {ball.OnTable}";
        }
    }
}
