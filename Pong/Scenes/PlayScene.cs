using System;
using System.Collections.Generic;
using System.Text;
using OpenGameEngine.Scenes;
using Pong.Objs;
using System.Windows.Forms;

namespace Pong.Scenes
{
    public class PlayScene : GameScene
    {
        Level _level;

        public PlayScene()
        {
            this.Name = "Play";
        }

        public override void Load(OpenGameEngine.Base.GameCoreEventArgs e)
        {
            base.Load(e);

            Assets assets = (Assets)e.Core.Services.GetService(typeof(Assets));
            assets.LoadAsset("Content\\Stick.png", e);

            _level = new Level();

            e.Core.Services.AddService<Level>(_level);

            _level.Load(e);

        }

        public override void Update(OpenGameEngine.Base.GameCoreEventArgs e)
        {
            base.Update(e);

            _level.Update(e);

            if (e.Core.CurrentKeyboardStateInfo[Keys.Escape])
                e.Core.SwitchScene("Menu");
        }

        public override void Draw(OpenGameEngine.Base.DrawEventArgs e)
        {
            base.Draw(e);

            _level.Draw(e);
        }
    }
}
