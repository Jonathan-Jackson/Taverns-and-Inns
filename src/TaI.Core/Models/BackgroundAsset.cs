using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TaI.Core.Models {

    public class BackgroundAsset : Asset {

        public BackgroundAsset(Vector2 location, Texture2D texture)
            : base(location, texture, texture.Width, texture.Height, true) {
        }
    }
}