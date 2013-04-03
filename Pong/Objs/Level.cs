using System;
using System.Collections.Generic;
using System.Text;
using OpenGameEngine.Base;
using SharpDX;
using SharpDX.Direct2D1;

namespace Pong.Objs
{
    public class Level
    {
        public Ball _ball;
        public SidePanel _playerPanel;
        public SidePanel _compPanel;

        public Level()
        {
            _ball = new Ball();

            _playerPanel = new SidePanel(OpponentType.Player, Side.Left);

            _compPanel = new SidePanel(OpponentType.Computer, Side.Right);
            //_compPanel.Computer = true;
        }

        public void Load(GameCoreEventArgs e)
        {
            _ball.Load(e);

            _playerPanel.Load(e);
            _playerPanel.Position = new SharpDX.Vector2(21, (300 - 65));

            _compPanel.Load(e);
            _compPanel.Position = new SharpDX.Vector2((800 - 42), (300 - 65));
        }

        public void Draw(DrawEventArgs e)
        {
            e.DrawString(_playerPanel.Score.ToString(), "Comic Sans MS", 24, new SolidColorBrush(e.Target, Color.CornflowerBlue), new Vector2(180, 15));
            e.DrawString(_compPanel.Score.ToString(), "Comic Sans MS", 24, new SolidColorBrush(e.Target, Color.CornflowerBlue), new Vector2(580, 15));
            
            _playerPanel.Draw(e);
            _compPanel.Draw(e);
            _ball.Draw(e);
        }

        public void Update(GameCoreEventArgs e)
        {
            _ball.Update(e);

            _playerPanel.Update(e);
            _compPanel.Update(e);
        }

    }
}
