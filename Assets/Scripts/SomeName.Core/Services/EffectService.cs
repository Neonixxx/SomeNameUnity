using System;
using System.Linq;
using SomeName.Core.Effects;

namespace SomeName.Core.Services
{
    public class EffectService
    {
        public EffectService(IBattleUnit battleUnit, Effects.Effects effects)
        {
            Effects = effects;
            BattleUnit = battleUnit;
        }

        public IBattleUnit BattleUnit { get; set; }

        public Effects.Effects Effects { get; set; }

        public long GetDamageBonus()
            => Effects.All.Sum(e => e.DamageBonus);

        public double GetDamageKoef()
            => Effects.All.Multiply(e => e.DamageKoef);

        public long GetDefenceBonus()
            => Effects.All.Sum(e => e.DefenceBonus);

        public double GetDefenceKoef()
            => Effects.All.Multiply(e => e.DefenceKoef);

        public void Add(Effect effect)
        {
            effect.Start(BattleUnit);
            Effects.All.Add(effect);
        }

        public void Remove(Effect effect)
        {
            if (!Effects.All.Contains(effect))
                throw new InvalidOperationException($"{nameof(effect)} не содержится в эффектах.");
            Effects.All.Remove(effect);
            effect.End();
        }

        public void RemoveAll()
            => Effects.All.Clear();

        public void Update(double timeDelta)
        {
            for (int i = 0; i < Effects.All.Count; i++)
            {
                var effect = Effects.All[i];
                effect.Update(timeDelta);
                if (effect.DurationLeft <= 0)
                {
                    Effects.All.Remove(effect);
                    i--;
                }
            }
        }
    }
}
