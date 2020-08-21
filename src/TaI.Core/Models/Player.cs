using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using TaI.Core.Common;
using TaI.Core.Enums;

namespace TaI.Core.Models {

    public class Player : Entity {
        private const float MAX_SPEED = 6;
        private float _speedX = 0;
        private float _speedY = 0;
        public Vector2 Velocity;

        private AnimationManager _animationManager;
        private IReadOnlyDictionary<PlayerAnimation, Animation> _animations;

        public Player(Vector2 location, IReadOnlyDictionary<PlayerAnimation, Animation> animations)
            : base(location, animations.Values.First().Texture) { // NEED TO DIVIDE WIDTH BY 7!
            _animations = animations;
            _animationManager = new AnimationManager(animations.Values.First());
        }

        public void IncreaseSpeed(float speedIncrementX, float speedIncrementY) {
            if (_speedX + speedIncrementX < MAX_SPEED
                && _speedX + speedIncrementX > -MAX_SPEED)
                _speedX += speedIncrementX;

            if (_speedY + speedIncrementY < MAX_SPEED
                && _speedY + speedIncrementY > -MAX_SPEED)
                _speedY += speedIncrementY;
        }

        public override void Update(GameTime gameTime) {
            bool isActiveMovementX = Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.D);
            bool isActiveMovementY = Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.S);

            float speedX = 0, speedY = 0;

            // Whammy speed the other way when not actively moving.
            // this creates a lazy 'slowdown' effect on updates.
            // (update this to not be.. lazy)
            if (!isActiveMovementX && _speedX != 0)
                speedX += _speedX > 0 ? -0.5f : 0.5f;
            if (!isActiveMovementY && _speedY != 0)
                speedY += _speedY > 0 ? -0.5f : 0.5f;

            IncreaseSpeed(speedX, speedY);
            speedX = speedY = 0;

            if (Keyboard.GetState().IsKeyDown(Keys.W)) {
                speedY = -0.5f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S)) {
                speedY = 0.5f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A)) {
                speedX = -0.5f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                speedX = 0.5f;
            }

            IncreaseSpeed(speedX, speedY);
            UpdateLocationWithSpeed();

            SetAnimations();
            _animationManager.Update(gameTime);
        }

        private void SetAnimations() {
            if (_speedX > 0)
                _animationManager.Play(_animations[PlayerAnimation.Right]);
            else if (_speedX < 0)
                _animationManager.Play(_animations[PlayerAnimation.Left]);
            else if (_speedY > 0)
                _animationManager.Play(_animations[PlayerAnimation.Down]);
            else if (_speedY < 0)
                _animationManager.Play(_animations[PlayerAnimation.Up]);
            else _animationManager.Stop();
        }

        private void UpdateLocationWithSpeed() {
            Location += new Vector2(_speedX, _speedY);
            _animationManager.Position += new Vector2(_speedX, _speedY);
        }

        public override void Draw(SpriteBatch sBatch) {
            if (_animationManager != null)
                _animationManager.Draw(sBatch);
            else
                sBatch.Draw(Texture, Location, Color.White);
        }
    }
}