namespace Tracking.Model
{
    public static class BallTypes
    {
        public static BallType FullWhite { get; } = new BallType(BallColor.White, BallTeam.Full);
        public static BallType FullBlack { get; } = new BallType(BallColor.Black, BallTeam.Full);

        public static BallType FullYellow { get; } = new BallType(BallColor.Yellow, BallTeam.Full);
        public static BallType FullBlue { get; } = new BallType(BallColor.Blue, BallTeam.Full);
        public static BallType FullRed { get; } = new BallType(BallColor.Red, BallTeam.Full);
        public static BallType FullPurple { get; } = new BallType(BallColor.Purple, BallTeam.Full);
        public static BallType FullOrange { get; } = new BallType(BallColor.Orange, BallTeam.Full);
        public static BallType FullGreen { get; } = new BallType(BallColor.Green, BallTeam.Full);
        public static BallType FullMaroon { get; } = new BallType(BallColor.Maroon, BallTeam.Full);

        public static BallType HalfYellow { get; } = new BallType(BallColor.Yellow, BallTeam.Half);
        public static BallType HalfBlue { get; } = new BallType(BallColor.Blue, BallTeam.Half);
        public static BallType HalfRed { get; } = new BallType(BallColor.Red, BallTeam.Half);
        public static BallType HalfPurple { get; } = new BallType(BallColor.Purple, BallTeam.Half);
        public static BallType HalfOrange { get; } = new BallType(BallColor.Orange, BallTeam.Half);
        public static BallType HalfGreen { get; } = new BallType(BallColor.Green, BallTeam.Half);
        public static BallType HalfMaroon { get; } = new BallType(BallColor.Maroon, BallTeam.Half);
    }
}

