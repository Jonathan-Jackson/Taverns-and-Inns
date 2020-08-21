using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using TaI.Core.Common;
using TaI.Core.Models;
using TaI.Engine.Content;
using TaI.Engine.Generator;

namespace Taverns_and_Inns {

    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private readonly List<Asset> _assets;
        private readonly List<Effect> _effects;

        private readonly Camera _camera;
        private Player _player;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _assets = new List<Asset>();
            _effects = new List<Effect>();
            _camera = new Camera();
        }

        protected override void Initialize() {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _assets.Add(new Asset(
                new Vector2(0, 0),
                Content.Load<Texture2D>("Characters/Testingno")
            ));

            var forest = new ForestGenerator(TextureHelper.GetForestTextures(Content))
                    .Generate(new Vector2(-1000, -1000), new Vector2(1000, 1000));

            _assets.AddRange(forest);
            _effects.Add(Content.Load<Effect>("Effects/TestShader"));
            _player = new Player(new Vector2(0, 0), TextureHelper.GetPlayerAnimations(Content));
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player.Update(gameTime);
            _camera.Follow(_player.Location,
                _player.Texture.Width / 7,
                _player.Texture.Height,
                _graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Transparent);
            _spriteBatch?.Begin(transformMatrix: _camera.Transform, sortMode:
                SpriteSortMode.Immediate,
                blendState: BlendState.AlphaBlend);

            foreach (var effect in _effects)
                effect.CurrentTechnique.Passes[0].Apply();

            foreach (var asset in _assets)
                asset.Draw(_spriteBatch);

            _player.Draw(_spriteBatch);

            _spriteBatch?.End();
            base.Draw(gameTime);
        }
    }
}