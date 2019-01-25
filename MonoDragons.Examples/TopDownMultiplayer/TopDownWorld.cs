using Microsoft.Xna.Framework;
using MonoDragons.Core;

namespace MonoDragons.Examples.TopDownMultiplayer
{
    public static class TopDownWorld
    {
        public const int XTileSize = 64;
        public const int YTileSize = 64;

        public static readonly Size2 TileSize = new Size2(XTileSize, YTileSize);
        public static Vector2 RawPositionOf(Point worldPos) => (worldPos * new Point(XTileSize, YTileSize)).ToVector2();
        public static Transform2 Tile(int xPos, int yPos) => new Transform2(RawPositionOf(new Point(xPos, yPos)), TileSize);
    }
}
