using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tracking.Core.Maths;

namespace Tracking.Core.Processing
{
    internal class TrackedCircleManager
    {
        public IEnumerable<TrackedCircle> Circles => circlesDict.Values;

        public int CircleCount => circlesDict.Count;

        private int idCounter = 0;
        private readonly IDictionary<int, TrackedCircle> circlesDict = new Dictionary<int, TrackedCircle>();

        public void Register(PointF center)
        {
            var id = idCounter++;
            circlesDict[id] = new TrackedCircle() { Id = id, Active = true, CurrentCenter = center };
        }

        public TrackedCircle FindById(int id)
        {
            return circlesDict[id];
        }

        public TrackedCircle FindClosestTo(PointF point)
        {
            TrackedCircle closest = null;
            float closestDistance = 9999.9f;

            foreach (var circle in Circles)
            {
                var dist = circle.CurrentCenter.DistanceTo(point);
                if (dist < closestDistance)
                {
                    closest = circle;
                    closestDistance = dist;
                }
            }

            return closest;
        }

        public bool ContainsAt(PointF point)
        {
            return Circles.Any(c => c.CurrentCenter.DistanceTo(point) <= 4);
        }

        public void DiscardInactive()
        {
            var inactiveCircleIds = circlesDict.Where(entry => !entry.Value.Active).Select(entry => entry.Key).ToList();
            foreach (var id in inactiveCircleIds)
                circlesDict.Remove(id);
        }

        public void Clear()
        {
            idCounter = 0;
            circlesDict.Clear();
        }
    }
}
