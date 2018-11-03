using Microsoft.Xna.Framework;
using System;

namespace FollowEffect
{
    internal static class Helper
    {
        public static float RadianBetween(Vector2 from, Vector2 to)
        {
            var direction = to - from;
            direction.Normalize();

            return (float)Math.Atan2(direction.Y, direction.X);
        }

        public static float DistanceBetween(Vector2 from, Vector2 to)
        {
            var direction = to - from;
            return direction.Length();
        }
    }
}