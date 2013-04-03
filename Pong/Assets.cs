using System;
using System.Collections.Generic;
using System.Text;
using SharpDX.Direct2D1;
using OpenGameEngine.Base;
using OpenGameEngine.Helper;

namespace Pong
{
    public class Assets :
        Dictionary<String, Bitmap>
    {
        public void LoadAsset(String name, GameCoreEventArgs e)
        {
            Zip content = (Zip)e.Core.Services.GetService(typeof(Zip));
            Bitmap bmp = BitmapHelper.LoadFromStream(e.Core.RenderTarget2D, content.Extract(name), System.Drawing.Color.Fuchsia);

            if (this.ContainsKey(name))
                this[name] = bmp;
            else
                this.Add(name, bmp);

        }

    }
}
