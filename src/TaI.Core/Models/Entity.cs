using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TaI.Core.Models {
    public abstract class Entity {
        protected Entity(Vector2 location, Texture2D texture) {
            Location = location;
            Texture = texture;
        }

        public virtual Vector2 Location {
            get; set;
        }

        public virtual Texture2D Texture {
            get; set;
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}