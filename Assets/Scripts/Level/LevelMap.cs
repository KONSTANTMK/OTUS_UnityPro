using JetBrains.Annotations;

namespace Homework.EventBus
{
    [UsedImplicitly]
    public sealed class LevelMap
    {
        public EntityMap Entities { get; }
        
        public TileMap Tiles { get; }

        public LevelMap(EntityMap entities, TileMap tiles)
        {
            Entities = entities;
            Tiles = tiles;
        }
    }
}