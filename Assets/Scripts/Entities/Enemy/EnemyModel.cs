using Declarative;

namespace Homework.EventBus
{
    public sealed class EnemyModel : DeclarativeModel
    {
        [Section]
        public Position position;

        [Section]
        public Life life;
    }
}