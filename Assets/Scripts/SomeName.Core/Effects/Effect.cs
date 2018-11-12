using System;
using Newtonsoft.Json;

namespace SomeName.Core.Effects
{
    public class Effect
    {
        public Effect() { }

        public string ImageId { get; set; }

        public string Description { get; set; }

        public double Duration { get; set; }

        public double DurationLeft { get; set; }

        public long DamageBonus { get; set; } = 0;
        public double DamageKoef { get; set; } = 1.0;

        public long DefenceBonus { get; set; } = 0;
        public double DefenceKoef { get; set; } = 1.0;

        [JsonIgnore]
        public IBattleUnit EffectOwner { get; set; }

        public virtual void Start(IBattleUnit effectOwner)
        {
            DurationLeft = Duration;
            EffectOwner = effectOwner;
            InternalStart(effectOwner);
        }

        protected virtual void InternalStart(IBattleUnit effectOwner) { }

        public virtual void End()
        {
            EffectOwner = null;
        }

        public virtual void Update(double timeDelta)
        {
            DurationLeft = Math.Max(DurationLeft - timeDelta, 0);
            InternalUpdate(timeDelta);
        }

        protected virtual void InternalUpdate(double timeDelta) { }
    }
}
