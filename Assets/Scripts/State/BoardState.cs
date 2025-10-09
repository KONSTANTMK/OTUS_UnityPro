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

            // Пример: R0=Paladin (с щитом), R1=Huntress, R2=IceMage, R3=Devourer
            Red.Add(new Hero { Index = 0, Team = TeamId.Red,  Name = "R0 Paladin",  Kind = HeroKind.Paladin,  DivineShield = true });
            Red.Add(new Hero { Index = 2, Team = TeamId.Red,  Name = "R1 Huntress", Kind = HeroKind.Huntress });
            Red.Add(new Hero { Index = 4, Team = TeamId.Red,  Name = "R2 IceMage",  Kind = HeroKind.IceMage });
            Red.Add(new Hero { Index = 6, Team = TeamId.Red,  Name = "R3 Devourer", Kind = HeroKind.Devourer });

            // Пример: B0=DumbOrc, B1=LordVamp, B2=Electro, B3=Meditator
            Blue.Add(new Hero { Index = 1, Team = TeamId.Blue, Name = "B0 Orc",      Kind = HeroKind.DumbOrc });
            Blue.Add(new Hero { Index = 3, Team = TeamId.Blue, Name = "B1 Vamp",     Kind = HeroKind.LordVamp });
            Blue.Add(new Hero { Index = 5, Team = TeamId.Blue, Name = "B2 Electro",  Kind = HeroKind.Electro });
            Blue.Add(new Hero { Index = 7, Team = TeamId.Blue, Name = "B3 Medit",    Kind = HeroKind.Meditator });

            // порядок ходов как раньше
            for (int i = 0; i < 4; i++) { _order.Add(Red[i]); _order.Add(Blue[i]); }
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