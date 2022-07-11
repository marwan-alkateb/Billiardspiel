using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Model;

namespace Assets.Scripts.Tracking
{
    public class TrackingInterface
    {
        public static TrackingInterface Instance { get; } = new TrackingInterface();

        public Frame LatestFrame { get; set; } = null;

        private TrackingInterface()
        {
        }

        public void Connect()
        {

        }

        public void Shutdown()
        {

        }

    }
}
