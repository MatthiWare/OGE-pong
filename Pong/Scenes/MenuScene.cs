using System;
using System.Collections.Generic;
using System.Text;
using OpenGameEngine.Scenes;
using Pong.Scenes.Huds;
using SharpDX.Direct2D1;

namespace Pong.Scenes
{
    public class MenuScene : GameScene
    {
        public MenuScene()
        {
            this.Name = "Menu";
        }

        public override void Load(OpenGameEngine.Base.GameCoreEventArgs e)
        {
            base.Load(e);

            Assets asset = (Assets)e.Core.Services.GetService(typeof(Assets));
            asset.LoadAsset("Content\\Background.png", e);

            MenuLabel playLabel = new MenuLabel("Play!", new SharpDX.Vector2(325, 250), (m) => 
                {
                    e.Core.SwitchScene("Play");
                    m.Text = "Resume!";
                    m.Position = new SharpDX.Vector2(280, 250);
                }
            );

            MenuLabel exitLabel = new MenuLabel("Exit", new SharpDX.Vector2(325, 350), (m) =>
                {
                    e.Core.Exit();
                }
            );

            OpenGameEngine.Hud.HudLayer layer = new OpenGameEngine.Hud.HudLayer();

            layer.Labels.Add(playLabel);
            layer.Labels.Add(exitLabel);

            this.HudLayer.Add(layer);
            

        }

        public override void Draw(OpenGameEngine.Base.DrawEventArgs e)
        {
            Assets asset = (Assets)e.Core.Services.GetService(typeof(Assets));
            Bitmap bmpBackground = asset["Content\\Background.png"];

            e.DrawImage(bmpBackground, 0, 0);

            base.Draw(e);
        }
    }
}
