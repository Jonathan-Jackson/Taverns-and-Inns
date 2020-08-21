using Microsoft.Xna.Framework.Graphics;

namespace TaI.Core.Common {

    public class Animation {
        public int CurrentFrame { get; set; }

        public int FrameCount { get; private set; }

        public int FrameHeight { get { return Texture.Height; } }

        public float FrameSpeed { get; set; }

        public int FrameWidth { get { return Texture.Width / FrameCount; } }

        public bool IsLooping { get; set; }

        public Texture2D Texture { get; private set; }

        public Animation(Texture2D texture, int frameCount, float frameSpeed = 0.15f) {
            Texture = texture;
            FrameCount = frameCount;
            FrameSpeed = frameSpeed;
            IsLooping = true;
        }
    }
}