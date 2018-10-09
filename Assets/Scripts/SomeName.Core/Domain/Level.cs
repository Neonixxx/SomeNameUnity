namespace SomeName.Core.Domain
{
    public class Level
    {
        public int Normal { get; set; }

        public int Paragon { get; set; } = -1;

        public override string ToString()
        {
            return Paragon <= 0
                ? Normal.ToString()
                : $"{Normal} ({Paragon})";

        }
    }
}
