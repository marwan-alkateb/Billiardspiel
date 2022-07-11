using System;
using Tracking.Core;
using Tracking.Model;
using Tracking.API;

namespace Tracking.Server
{
    internal class TrackingServer
    {
        private TrackingEngine trackingEngine;
        private API.Server server;

        public void Open()
        {
            Logger.Info("Starting server on port 3200...");
            server = new API.Server(3200);
            server.LogMessageAvailable += Server_LogMessageAvailable;
            server.ErrorMessageAvailable += Server_ErrorMessageAvailable;            

            Logger.Info("Initializing tracking engine...");
            trackingEngine = new TrackingEngine();
            trackingEngine.FrameAvailable += TrackingEngine_FrameAvailable;
            trackingEngine.CalibrationCompleted += TrackingEngine_CalibrationCompleted;
            trackingEngine.Open();
            Logger.Info("Tracking engine initialized");

            if (!trackingEngine.IsCalibrated)
            {
                Logger.Warn("Tracker is not calibrated. Clear the table and run the calibrate command.");
            }

            Logger.Info("Tracking server initialized.");
            RunCommandParser();
        }

        private void TrackingEngine_CalibrationCompleted(object sender, EventArgs e)
        {
            Logger.Info("Calibration completed");
        }

        private void Server_LogMessageAvailable(object sender, string e)
        {
            Logger.Info(e);
        }

        private void Server_ErrorMessageAvailable(object sender, string e)
        {
            Logger.Error(e);
        }

        private void TrackingEngine_FrameAvailable(object sender, Frame e)
        {
            server.SendAsyncFrame(e);
        }

        private void Start()
        {
            trackingEngine.Open();
            if (!trackingEngine.IsCalibrated)
            {
                Logger.Warn("Tracker is not calibrated. Clear the table and run the calibrate command.");
            }

            Logger.Info("Tracking server initialized.");

            server.Start();
            Logger.Info("Server started");
        }

        public void RunCommandParser()
        {
            // TODO Better handler
            while (true)
            {
                var command = Console.ReadLine();
                if (command == "exit")
                {
                    Logger.Info("Shutting down...");
                    trackingEngine.Close();
                    server.Dispose();
                    return;
                }
                else if (command == "calibrate")
                {
                    Logger.Info("Calibrating...");
                    trackingEngine.Calibrate();
                }
                else if (command == "start")
                {
                    Logger.Info("Starting tracking");
                    Start();
                }
            }
        }
    }
}
