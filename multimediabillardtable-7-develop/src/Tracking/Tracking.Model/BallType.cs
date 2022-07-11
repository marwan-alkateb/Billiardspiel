using System;

namespace Tracking.Model
{
    /// <summary>
    /// Holds information about the color and team of a single ball
    /// </summary>
    public struct BallType : IEquatable<BallType>, IComparable<BallType>
    {
        public BallColor Color { get; private set; }

        public BallTeam Team { get; private set; }

        public BallType(BallColor color, BallTeam team)
        {
            Color = color;
            Team = team;

            if ((color == BallColor.White || color == BallColor.Black) && team != BallTeam.Full)
                throw new ArgumentException($"Invalid ball combination {team}{color}");
        }

        public static BallType FromInt(int i)
        {
            if (i < 8)
                return new BallType(BallColor.White + i, BallTeam.Full);
            else if (i == 8)
                return new BallType(BallColor.Black, BallTeam.Full);
            else
                return new BallType(BallColor.White + i - 8, BallTeam.Half);
        }

        public override bool Equals(object obj)
        {
            return obj is BallType id && Equals(id);
        }

        public bool Equals(BallType other)
        {
            return Color == other.Color &&
                   Team == other.Team;
        }

        public override int GetHashCode()
        {
            int hashCode = -1983255515;
            hashCode = hashCode * -1521134295 + Color.GetHashCode();
            hashCode = hashCode * -1521134295 + Team.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Creates a new ball type which has the team set
        /// to the new team, if possible.
        /// </summary>
        /// <param name="team">The new team to be set</param>
        /// <returns>A new ball type instance</returns>
        public BallType ToTeam(BallTeam team)
        {
            // There are no half-white or half-black balls
            if (Color == BallColor.White || Color == BallColor.Black)
                team = BallTeam.Full;

            return new BallType { Color = Color, Team = team };
        }

        public static bool operator ==(BallType a, BallType b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(BallType a, BallType b)
        {
            return !a.Equals(b);
        }

        public static bool operator >(BallType a, BallType b)
        {
            return a.ToInt() > b.ToInt();
        }

        public static bool operator <(BallType a, BallType b)
        {
            return a.ToInt() < b.ToInt();
        }

        /// <summary>
        /// Convert this ball type into its string representation
        /// </summary>
        /// <returns>String representation in the format {team}{Color}, e.g. fullGreen</returns>
        public override string ToString()
        {
            return Team.ToString().ToLower() + Color.ToString();
        }

        /// <summary>
        /// Convert this ball type into its integer representation, used
        /// for ball value comparison
        /// </summary>
        /// <returns>White = 0, Full Colors = 1..7, Black = 8, Half Colors = 9..15</returns>
        public int ToInt()
        {
            return (int)Color + (int)Team * 8;
        }

        public int CompareTo(BallType other)
        {
            return ToInt().CompareTo(other.ToInt());
        }
    }
}
