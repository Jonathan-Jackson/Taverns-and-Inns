using Microsoft.Xna.Framework;

namespace TaI.Core.Common {

    public class Camera {
        public Matrix Transform { get; private set; }

        public void Follow(Vector2 location, int targetWidth, int targetHeight, int screenWidth, int screenHeight) {
            var position = Matrix.CreateTranslation(
                -location.X - (targetWidth / 2),
                -location.Y - (targetHeight / 2),
            0);

            var offset = Matrix.CreateTranslation(screenWidth / 2, screenHeight / 2, 0);
            var zoom = Matrix.CreateScale(1);

            Transform = position * offset * zoom;
        }
    }
}