using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missiles
{
    public class PlayerData
    {
        public Vector2 Position { get; set; }
        public bool IsAlive { get; set; }
        public Color Color { get; set; }
        public float Angle { get; set; }
        public float Power { get; set; }
    }
}
