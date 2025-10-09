using System.Collections.Generic;

namespace State
{
    public class BoardState
    {
        public readonly List<Hero> Red  = new();
        public readonly List<Hero> Blue = new();

        private readonly List<Hero> _order = new(); // Красн0, Син0, Красн1, Син1, ...
        private int _cursor = 0;

        public void SetupDefaultTeams()
        {
            Red.Clear(); Blue.Clear(); _order.Clear(); _cursor = 0;

            // Временная расстановка: 4 на 4, индексы 0..7
            for (int i = 0; i < 4; i++)
            {
                Red.Add(new Hero { Index = i * 2,     Team = TeamId.Red,  Name = $"R{i}" });
                Blue.Add(new Hero{ Index = i * 2 + 1, Team = TeamId.Blue, Name = $"B{i}" });
            }

            // Фиксированный порядок: R0, B0, R1, B1, R2, B2, R3, B3
            for (int i = 0; i < 4; i++)
            {
                _order.Add(Red[i]);
                _order.Add(Blue[i]);
            }
        }

        public Hero NextActive()
        {
            // крутимся, пока не найдём живого, и если он Frozen — снимаем флаг и пропускаем
            for (int k = 0; k < _order.Count; k++)
            {
                var h = _order[_cursor];
                _cursor = (_cursor + 1) % _order.Count;

                if (!h.Alive) continue;
                if (h.Frozen) { h.Frozen = false; continue; } // пропуск одного хода

                return h;
            }
            return null; // теоретически, если все мертвы — игру завершим раньше
        }
    }
}