using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tracking.Core.Config;
using Tracking.Core.Maths;
using Tracking.Model;

namespace Tracking.Core.Processing
{
    internal class BallClassifier
    {
        public IList<Ball> Balls { get; set; } = new List<Ball>();

        private readonly ClassifierConfig config;

        public BallClassifier(ClassifierConfig config)
        {
            this.config = config;
            Reset();
        }

        public void Reset()
        {
            Balls.Clear();
            foreach (BallColor color in Enum.GetValues(typeof(BallColor)))
            {
                foreach (BallTeam team in Enum.GetValues(typeof(BallTeam)))
                {
                    if ((color == BallColor.White || color == BallColor.Black) && team == BallTeam.Half)
                        continue;

                    Balls.Add(new Ball
                    {
                        Type = new BallType(color, team),
                        Velocity = PointF.Empty,
                        Position = PointF.Empty,
                        OnTable = false
                    });
                }
            }
        }

        public void Update(IEnumerable<TrackedCircle> circles, SizeF frameSize)
        {
            var unassignedBalls = new HashSet<BallType>();

            // Reset balls
            foreach (var ball in Balls)
            {
                ball.Velocity = PointF.Empty;
                ball.OnTable = false;
                unassignedBalls.Add(ball.Type);
            }

            // Assign circles to balls
            foreach (var circle in circles)
            {
                var circleColor = circle.ColorBuffer.Average().ToHsv();
                var colorPercentage = circle.ColorPercentageBuffer.Average();
                var ballType = FindBestBallType(circleColor, colorPercentage);
                if (ballType != null && unassignedBalls.Contains(ballType.Value))
                {
                    var ball = FindBallByType(ballType.Value);
                    AssignCircleToBall(ball, circle, frameSize);
                    unassignedBalls.Remove(ballType.Value);
                }
                else
                {
                    circle.used = false;
                }
            }

            // Do nearest-neighbor assignment for those that were not assigned,
            // if there are circles still remaining
            foreach (var ball in Balls)
            {
                if (!ball.OnTable && unassignedBalls.Contains(ball.Type))
                {
                    var closestCircle = circles.Where(c => !c.used).OrderBy(c => c.CurrentCenter.DistanceTo(ball.RawPosition)).FirstOrDefault();
                    if (closestCircle != null)
                    {
                        AssignCircleToBall(ball, closestCircle, frameSize);
                        unassignedBalls.Remove(ball.Type);
                    }
                }
            }
        }

        private void AssignCircleToBall(Ball ball, TrackedCircle circle, SizeF frameSize)
        {
            ball.OnTable = true;
            ball.RawPosition = circle.PositionBuffer.Average();
            ball.Position = NormalizeBallCoordinates(ball.RawPosition, frameSize);
            ball.Velocity = NormalizeBallCoordinates(circle.VelocityBuffer.Average(), frameSize);
            circle.used = true;
        }

        private PointF NormalizeBallCoordinates(PointF point, SizeF frameSize)
        {
            return new PointF(point.X / frameSize.Width, point.Y / frameSize.Height);
        }

        private Ball FindBallByType(BallType type)
        {
            return Balls.First(b => b.Type == type);
        }

        private BallType? FindBestBallType(Hsv color, float colorPercentage)
        {
            if (colorPercentage <= 0.01)
                return new BallType(BallColor.White, BallTeam.Full);
            else if (color.Value < 25)
                return new BallType(BallColor.Black, BallTeam.Full);
            else
            {
                var ballColor = FindClosestBallColor(color);
                if (ballColor == null)
                    return null;
                else if (colorPercentage < 0.5)
                    return new BallType(ballColor.Value, BallTeam.Half);
                else
                    return new BallType(ballColor.Value, BallTeam.Full);
            }
        }

        private BallColor? FindClosestBallColor(Hsv color)
        {
            var bestMatch = config.ColorRanges.Select(range =>
            {
                // Distance between circle and range in value
                var valDist = range.Value.ValueCenter == null ? 0.0 : Math.Abs(color.Value - range.Value.ValueCenter.Value);

                // Distance between circle and range in hue
                var hueDistAbs = Math.Abs(color.Hue - range.Value.HueCenter);
                var hueDist = Math.Min(hueDistAbs, 180 - hueDistAbs);

                return (range, hueDist, valDist);
            })
            .Where(o => o.hueDist < o.range.Value.HueRange) // All options that are within the hue range
            .Where(o => o.range.Value.ValueRange == null || o.valDist < o.range.Value.ValueRange) // All options that are within the value range, if existing
            .Select(o => (color: o.range.Key, dist: Math.Sqrt(o.hueDist * o.hueDist + o.valDist * o.valDist))) // Extract requried properties
            .OrderBy(o => o.dist) // Order by match quality (distance)
            .FirstOrDefault(); // And get the best one

            if (bestMatch == default)
                return null;
            else
                return bestMatch.color;
        }
    }
}

