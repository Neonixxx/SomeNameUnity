using System;
using System.Linq;
using SomeName.Core.Effects;

namespace SomeName.Core.Services
{
    public class EffectService
    {
        public EffectService(Effects.Effects effects)
        {
            Effects = effects;
        }

        public Effects.Effects Effects { get; set; }

        public long GetDamageBonus()
            => Effects.All.Sum(e => e.DamageBonus);

        public double GetDamageKoef()
            => Effects.All.Multiply(e => e.DamageKoef);

        public void Add(Effect effect)
        {
            Effects.All.Add(effect);
            effect.Start();
        }

        public void Remove(Effect effect)
        {
            if (!Effects.All.Contains(effect))
                throw new InvalidOperationException($"{nameof(effect)} не содержится в эффектах.");
            Effects.All.Remove(effect);
            effect.End();
        }

        public void Update(double timeDelta)
        {
            Effects.All.ForEach(e =>
            {
                e.Update(timeDelta);
                if (e.DurationLeft <= 0)
                    Effects.All.Remove(e);
            });
        }
    }
}
