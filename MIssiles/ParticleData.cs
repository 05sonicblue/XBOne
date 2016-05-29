using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missiles
{
    public class ParticleData
    {
        public float BirthTime { get; set; }
        public float MaxAge { get; set; }
        public Vector2 OriginalPosition { get; set; }
        public Vector2 Acceleration { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Position { get; set; }
        public float Scaling { get; set; }
        public Color ModColor { get; set; }
    }
}
