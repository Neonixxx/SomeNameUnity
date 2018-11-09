using System;

namespace SomeName.Core.Effects
{
    public class Effect
    {
        public string ImageId { get; set; }

        public string Description { get; set; }

        public double Duration { get; set; }

        public double DurationLeft { get; set; }

        public long DamageBonus { get; set; } = 0;

        public double DamageKoef { get; set; } = 1.0;

        public void Start()
        {
            DurationLeft = Duration;
        }

        public void End()
        {

        }

        public void Update(double timeDelta)
        {
            DurationLeft = Math.Max(DurationLeft - timeDelta, 0);
        }
    }
}
