using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TaI.Core.Common;
using TaI.Core.Enums;

namespace TaI.Engine.Content {

    public static class TextureHelper {

        public static IReadOnlyDictionary<PlayerAnimation, Animation> GetPlayerAnimations(ContentManager content)
            => new Dictionary<PlayerAnimation, Animation>() {
                { PlayerAnimation.Right, new Animation(content.Load<Texture2D>("Characters/HumanMale/right_large"), 7) },
                { PlayerAnimation.Left, new Animation(content.Load<Texture2D>("Characters/HumanMale/left_large"), 7) },
                { PlayerAnimation.Up, new Animation(content.Load<Texture2D>("Characters/HumanMale/left_large"), 7) },
                { PlayerAnimation.Down, new Animation(content.Load<Texture2D>("Characters/HumanMale/left_large"), 7) }
            };

        public static IReadOnlyDictionary<ForestTexture, Texture2D> GetForestTextures(ContentManager content)
            => new Dictionary<ForestTexture, Texture2D>() {
                { ForestTexture.TreeOne, content.Load<Texture2D>("Trees/tree_1_large") },
                { ForestTexture.TreeTwo, content.Load<Texture2D>("Trees/tree_2_large") },
                { ForestTexture.PlantOne, content.Load<Texture2D>("Plants/plant_1_large") },
                { ForestTexture.PlantTwo, content.Load<Texture2D>("Plants/plant_2_large") },
                { ForestTexture.PondOne, content.Load<Texture2D>("Misc/pond_1_large") },
                { ForestTexture.RockOne, content.Load<Texture2D>("Rock/rock_1") },
                { ForestTexture.RockTwo, content.Load<Texture2D>("Rock/rock_2") },
                { ForestTexture.GrassOne, content.Load<Texture2D>("Grass/grass_1") },
                { ForestTexture.GrassTwo, content.Load<Texture2D>("Grass/grass_2") },
            };
    }

    public enum ForestTexture {
        TreeOne = 100,
        TreeTwo = 101,
        GrassOne = 200,
        GrassTwo = 201,
        PlantOne = 300,
        PlantTwo = 301,
        PondOne = 400,
        RockOne = 500,
        RockTwo = 501
    }
}