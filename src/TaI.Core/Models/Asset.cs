using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TaI.Core.Models {

    public class Asset {

        public Asset(Vector2 location, Texture2D texture, bool isUnderCharacters = false)
            : this(location, texture, texture.Width, texture.Height, isUnderCharacters) {
        }

        public Asset(Vector2 location, Texture2D texture, int width, int height, bool isUnderCharacters = false) {
            Location = location;
            Texture = texture;
            _rectangle = new Rectangle(0, 0, width, height);
            IsUnderCharacters = isUnderCharacters;
        }

        public Vector2 Location { get; set; }

        public Texture2D Texture { get; set; }

        public bool IsUnderCharacters { get; set; }

        public int Width { get => _rectangle.Width; }

        public int Height { get => _rectangle.Height; }

        private Rectangle _rectangle;

        public void Draw(SpriteBatch sBatch) {
            sBatch.Draw(Texture,
                new Rectangle((int)Location.X, (int)Location.Y, Width, Height),
                _rectangle,
                Color.White);
        }
    }
}