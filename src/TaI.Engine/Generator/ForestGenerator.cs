using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using TaI.Core.Models;
using TaI.Engine.Content;

namespace TaI.Engine.Generator {

    public class ForestGenerator {
        // Chance by %, this is done in priority
        // order, so a tree is highest priority
        // and rolled for first.

        private const int TREE_CHANCE = 80;
        private const int ROCK_CHANCE = 35;
        private const int GRASS_CHANCE = 25;
        private const int POND_CHANCE = 1;

        // This effects 'space' on the X & Y axis that
        // we can move our asset to freely. This space
        // HAS to be empty for movement, but will create
        // a less 'patterned' distribution.

        private const int MAX_WIGGLE_X = 10;
        private const int MAX_WIGGLE_Y = 20;

        private readonly IReadOnlyDictionary<ForestTexture, Texture2D> _textures;
        private static readonly Random _random = new Random();

        public ForestGenerator(IReadOnlyDictionary<ForestTexture, Texture2D> textures) {
            _textures = textures;
        }

        public List<Asset> Generate(Vector2 start, Vector2 end) {
            var output = new List<Asset>();

            output.Add(CreateBackground(start, end));

            // We may still need this to make sure we aren't intersecting
            // with the asset just placed to the left.
            //Asset lastAsset = null;
            var rows = new List<Asset>();
            var currentRow = new List<Asset>();
            float xPos = start.X;
            float yPos = start.Y;
            bool isRendering = true;

            // Render the very top row!
            while (xPos < end.X) {
                // determine the object.
                var asset = GetRolledAsset();
                asset.Location = new Vector2(xPos, yPos);

                if (xPos + asset.Width < end.X) {
                    // add random wiggle if it'd fit
                    if (xPos + asset.Width + MAX_WIGGLE_X < end.X)
                        asset.Location += new Vector2(_random.Next(0, MAX_WIGGLE_X), 0);
                    if (yPos + asset.Width + MAX_WIGGLE_Y < end.Y)
                        asset.Location += new Vector2(0, _random.Next(0, MAX_WIGGLE_Y));

                    output.Add(asset);
                    rows.Add(asset);
                }

                // add some randomness with this
                xPos += asset.Width;
            }

            // Render rows underneath.
            while (isRendering) {
                xPos = start.X;
                currentRow = new List<Asset>();
                // Render the very top row!
                while (xPos < end.X) {
                    var asset = GetRolledAsset();
                    // get the y position of our above nodes end
                    var aboveNode = rows.Where(asset => asset.Location.X >= xPos || asset.Location.X <= xPos + asset.Width || asset.Location.X + asset.Width >= xPos)
                                        .OrderByDescending(asset => asset.Location.Y + asset.Height)
                                        .First();

                    yPos = aboveNode.Location.Y + aboveNode.Height;
                    // determine the object.
                    asset.Location = new Vector2(xPos, yPos);
                    // would this fit?
                    if (yPos + asset.Height < end.Y && xPos + asset.Width < end.X) {
                        // add random wiggle if it'd fit
                        if (xPos + asset.Width + MAX_WIGGLE_X < end.X)
                            asset.Location += new Vector2(_random.Next(0, MAX_WIGGLE_X), 0);
                        if (yPos + asset.Height + MAX_WIGGLE_Y < end.Y)
                            asset.Location += new Vector2(0, _random.Next(0, MAX_WIGGLE_Y));

                        currentRow.Add(asset);
                    }

                    // add some randomness with this
                    xPos += asset.Width;
                }

                isRendering = currentRow.Any();
                rows.AddRange(currentRow);
            }

            output.AddRange(rows.OrderBy(x => x.Location.Y));

            return output;
        }

        private Asset GetRolledAsset() {
            if (_random.Next(0, 100) < GRASS_CHANCE)
                return new BackgroundAsset(new Vector2(0, 0), _textures[ForestTexture.GrassOne]);
            if (_random.Next(0, 100) < TREE_CHANCE)
                return new Asset(new Vector2(0, 0), _textures[ForestTexture.TreeOne]);
            if (_random.Next(0, 100) < ROCK_CHANCE)
                return new BackgroundAsset(new Vector2(0, 0), _textures[ForestTexture.RockOne]);
            if (_random.Next(0, 100) < POND_CHANCE)
                return new Asset(new Vector2(0, 0), _textures[ForestTexture.PondOne]);

            return new BackgroundAsset(new Vector2(0, 0), _textures[ForestTexture.PlantOne]);
        }

        private Asset CreateBackground(Vector2 start, Vector2 end) {
            // We use a single texture
            // and stretch it to fill the rectangle coords.
            return new Asset(
                location: new Vector2(start.X, start.Y),
                texture: _textures[ForestTexture.GrassOne],
                width: (int)Math.Abs(start.X - end.X),
                height: (int)Math.Abs(start.Y - end.Y)
            );
        }
    }
}