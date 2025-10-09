namespace Services
{
    public class RandomService
    {
        private readonly System.Random _rng = new System.Random();
        public bool Roll50() => _rng.NextDouble() < 0.5;
        public int Range(int min, int max) => _rng.Next(min, max); // max exclusive
    }
}