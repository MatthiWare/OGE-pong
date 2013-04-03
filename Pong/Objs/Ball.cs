using System;
using System.Collections.Generic;
using System.Text;
using OpenGameEngine.GameObjects;
using SharpDX;
using OpenGameEngine.Base;
using SharpDX.Direct2D1;

namespace Pong.Objs
{
    public class Ball :
        GameObject
    {
        public Vector2 MoveDirection { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Speed { get; set; }
        private Int32 totalScore;

        public Ball()
        {
            MoveDirection = new Vector2(0, 0);
            this.Name = "Ball";
            this.AcceptsInput = false;
            
            Reset();
        }

        public void Reset()
        {
            MoveDirection = new Vector2(0, 0);
            Random rnd = new Random();
            this.Position = new Vector2(373, 273);

            while (MoveDirection.X == 0 || MoveDirection.Y == 0)
            {
                float x = 0.2f * rnd.Next(-1, 1);
                float y = 0.2f * rnd.Next(-1, 1);
                if (y > 1.8f) y = 1.587f;
                if (y < -1.8f) y = -1.587f;

                this.MoveDirection = new Vector2(x, y);
            }

            Speed = (float)(23 + Math.Round((float)(totalScore / 5), 0));
        }

        public override void Load(OpenGameEngine.Base.GameCoreEventArgs e)
        {
            base.Load(e);

            Assets assets = (Assets)e.Core.Services.GetService(typeof(Assets));
            assets.LoadAsset("Content\\Ball.png", e);

            Bitmap bmp = assets["Content\\Ball.png"];
            this.Width = bmp.Size.Width;
            this.Height = bmp.Size.Height;
        }

        public override void Update(OpenGameEngine.Base.GameCoreEventArgs e)
        {
            base.Update(e);

            Move(e);
        }

        public void Move(GameCoreEventArgs e)
        {
            Vector2 _md = new Vector2(MoveDirection.X * Speed, MoveDirection.Y * Speed);
            this.Position += _md;

            CheckWallCollision(e);
            CheckPanelCollision(e);
        }

        public void CheckWallCollision(GameCoreEventArgs e)
        {
            float dirY = MoveDirection.Y;
            float dirX = MoveDirection.X;

            DrawingRectangleF _screen = new DrawingRectangleF(0, 0, 800, 600);
            float _x = this.Position.X;
            float _y = this.Position.Y;
            float _xw = _x + Width;
            float _yh = _y + Height;

            if (_y <= 0 || _yh >= _screen.Height)
                dirY = -(dirY);

            Level _lvlCurrent = (Level)e.Core.Services.GetService(typeof(Level));
            if (_x <= 0) 
            {
                _lvlCurrent._compPanel.Score += 1;
                totalScore += 1;
                Reset();
            }
            else if (_xw >= _screen.Width)
            {
                _lvlCurrent._playerPanel.Score += 1;
                totalScore += 1;

                if (_lvlCurrent._playerPanel.Score % 5 == 0)
                    _lvlCurrent._compPanel.CompDifficulty += 1;
                Reset();
            }

            MoveDirection = new Vector2(dirX, dirY);

        }

        public void CheckPanelCollision(GameCoreEventArgs e)
        {
            float dirY = MoveDirection.Y;
            float dirX = MoveDirection.X;

            Level _lvlCurrent = (Level)e.Core.Services.GetService(typeof(Level));

            SidePanel[] ps = new SidePanel[] { _lvlCurrent._compPanel, _lvlCurrent._playerPanel };

            System.Drawing.RectangleF recBal = this.GetRectangularCollider();

            foreach (SidePanel p in ps)
            {
                System.Drawing.RectangleF recPanel = p.GetRectangularCollider();
                
                float pPanel = (recPanel.Height/3);

                if (recBal.IntersectsWith(recPanel))
                {
                    dirX = -dirX;

                    if (Position.Y > recPanel.Y && Position.Y < recPanel.Y + pPanel)
                        dirY -= 0.01f;
                    else if (Position.Y < recPanel.Y + recPanel.Height && p.Position.Y > recPanel.Y + pPanel)
                        dirY += 0.01f;
                    }

            }

            MoveDirection = new Vector2(dirX, dirY);

        }

        public System.Drawing.RectangleF GetRectangularCollider()
        {
            return new System.Drawing.RectangleF(Position.X, Position.Y, Width, Height);
        }

        public override void Draw(OpenGameEngine.Base.DrawEventArgs e)
        {
            Assets asset = (Assets)e.Core.Services.GetService(typeof(Assets));

            Bitmap bmp = asset["Content\\Ball.png"];

            base.Draw(e);

            e.DrawImage(bmp, this.Position);
        }

    }
}
