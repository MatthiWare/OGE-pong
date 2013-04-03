using System;
using OpenGameEngine.Hud;
using SharpDX;

namespace Pong.Scenes.Huds
{
    public class MenuLabel
        : HudLabel
    {
        float _newSize = 80;
        float _oldSize = 70;
        Color _oldColor = Color.CornflowerBlue;
        Color _newColor = Color.AdjustSaturation(Color.CornflowerBlue, 10.0f);
        ClickCallBack _callback;

        public delegate void ClickCallBack(MenuLabel m);

        public MenuLabel(String text, Vector2 location, ClickCallBack cb)
        {
            this.Text = text;
            this.Position = location;
            this.TextColor = _oldColor;
            this.FontFamily = "Comic Sans MS";
            this.FontSize = _oldSize;
            this.AcceptsInput = true;
            this.UseCenterPositioning = false;
            _callback = cb;
        }

        public override void MouseEnter(OpenGameEngine.Base.GameCoreEventArgs e)
        {
            base.MouseEnter(e);

            this.FontSize = _newSize;
            this.TextColor = _newColor;
        }

        public override void MouseLeave(OpenGameEngine.Base.GameCoreEventArgs e)
        {
            base.MouseLeave(e);

            this.FontSize = _oldSize;
            this.TextColor = _oldColor;
        }

        public override void MouseDown(OpenGameEngine.Base.GameCoreEventArgs e)
        {
            base.MouseDown(e);

            _callback.Invoke(this);
        }
    }
}
