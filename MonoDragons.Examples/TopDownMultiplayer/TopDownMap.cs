using Microsoft.Xna.Framework;
using MonoDragons.Core;
using MonoDragons.Core.Engine;

namespace MonoDragons.Examples.TopDownMultiplayer
{
    public sealed class TopDownMap : IVisual
    {
        private readonly TopDownTile[,] _tiles;

        public TopDownMap(int width, int height)
        {
            _tiles = new TopDownTile[width, height];
            for(var x = 0; x < _tiles.GetLength(0); x++)
                for(var y = 0; y < _tiles.GetLength(1); y++)
                    _tiles[x, y] = new TopDownTile { IsBlocking = false, Layer = 0, Sprite = "TopDownMultiplayer/g1", 
                        Transform = new Transform2(TopDownWorld.RawPositionOf(new Point(x, y)), TopDownWorld.TileSize)};
        }
        
        public void Draw(Transform2 parentTransform)
        {
            for(var x = 0; x < _tiles.GetLength(0); x++)
                for(var y = 0; y < _tiles.GetLength(1); y++)
                    _tiles[x,y].Draw(parentTransform);
        }
    }
}
