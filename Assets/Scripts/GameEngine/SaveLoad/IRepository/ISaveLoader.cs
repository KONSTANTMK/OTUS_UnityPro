namespace GameEngine
{
    public interface ISaveLoader
    {
        void SaveGame(GameContext gameContext,IGameRepository gameRepository);
        void LoadGame(GameContext gameContext, IGameRepository gameRepository);
    }
}