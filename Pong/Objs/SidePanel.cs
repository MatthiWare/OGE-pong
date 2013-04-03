using System;
using System.Collections.Generic;
using System.Text;
using OpenGameEngine.GameObjects;
using SharpDX.Direct2D1;
using SharpDX;
using OpenGameEngine.Input;

namespace Pong.Objs
{
    public enum OpponentType
    {
        Player,
        Computer,
    }

    public enum Side
    {
        Right,
        Left,
    }

    public class SidePanel
        : GameObject
    {
        
        public OpponentType OpponentType { get; set; }
        public Side Side {get;set;}
        public Boolean Computer = false;
        public Int32 Score { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float CompDifficulty { get; set; }

        public Vector2 MoveDir { get; set; }

        public SidePanel(OpponentType t,Side s)
        {
            Score = 0;
           this.OpponentType = t;
            this.Side=s;
            CompDifficulty = 2.2f;
        }

        public override void Update(OpenGameEngine.Base.GameCoreEventArgs e)
        {
            base.Update(e);

            if (OpponentType == Objs.OpponentType.Player)
            {
                if (Side == Objs.Side.Left)
                {
                    float _y = 0;

                    KeyboardStateInfo kb = e.Core.CurrentKeyboardStateInfo;

                    if (kb[System.Windows.Forms.Keys.Z])
                        _y -= 4.1f;
                    if (kb[System.Windows.Forms.Keys.S])
                        _y += 4.1f;

                    MoveDir = new Vector2(0, _y);
                }
                else
                {
                    float _y = 0;

                    KeyboardStateInfo kb = e.Core.CurrentKeyboardStateInfo;

                    if (kb[System.Windows.Forms.Keys.Up])
                        _y -= 4.1f;
                    if (kb[System.Windows.Forms.Keys.Down])
                        _y += 4.1f;

                    MoveDir = new Vector2(0, _y);
                }
            }
            else
            {
                Level _cLevel = (Level)e.Core.Services.GetService(typeof(Level));
                Ball _b = _cLevel._ball;
                float _bY = _b.Position.Y + (_b.Height / 2);
                float _y = 0;
                float _thisY = this.Position.Y + (this.Height / 2);

                if (_thisY > _bY)
                    _y -= (1.1f * CompDifficulty);
                else if (_thisY < _bY)
                    _y += (1.1f * CompDifficulty);

                    MoveDir=new Vector2(0,_y);
            }
                
            

            this.Position += MoveDir;
        }

        public override void Load(OpenGameEngine.Base.GameCoreEventArgs e)
        {
            base.Load(e);

            Assets asset = (Assets)e.Core.Services.GetService(typeof(Assets));
            Bitmap bmp = asset["Content\\Stick.png"];

            this.Width = bmp.Size.Width;
            this.Height = bmp.Size.Height;

            MoveDir = new Vector2(0, 0);
        }

        public System.Drawing.RectangleF GetRectangularCollider()
        {
            return new System.Drawing.RectangleF(Position.X, Position.Y, Width, Height);
        }

        public override void Draw(OpenGameEngine.Base.DrawEventArgs e)
        {
            Assets asset = (Assets)e.Core.Services.GetService(typeof(Assets));

            Bitmap bmp = asset["Content\\Stick.png"];

            base.Draw(e);

            e.DrawImage(bmp, this.Position);
        }

    }
}
