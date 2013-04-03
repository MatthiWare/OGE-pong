using System;
using OpenGameEngine;
using OpenGameEngine.Base;

namespace Pong
{
    public class Entry
    {
        public static void Main(string[] args)
        {
            GameConfiguration config = new GameConfiguration("Pong", 800,600, 60);
            PongEngine engine = new PongEngine();
            engine.Run(config);
        }
    }
}
