using System;
using System.Collections.Generic;
using System.Text;
using OpenGameEngine;
using Pong.Scenes;
using OpenGameEngine.Helper;

namespace Pong
{
    public class PongEngine : GameEngine
    {
        public PongEngine()
        {
            
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            Assets asset = new Assets();
            this.Services.AddService<Assets>(asset);

            Zip content = new Zip("Content.dat");
            this.Services.AddService<Zip>(content);

            this.Scenes.Add(new MenuScene());
            this.Scenes.Add(new PlayScene());

            this.SwitchScene("Menu");
        }
    }
}
