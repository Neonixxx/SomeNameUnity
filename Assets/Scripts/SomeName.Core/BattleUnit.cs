using System;
using Newtonsoft.Json;
using SomeName.Core.Items.Impl;
using SomeName.Core.Services;

namespace SomeName.Core
{
    public abstract class BattleUnit : IBattleUnit
    {
        [JsonIgnore]
        public SkillService SkillService { get; set; }

        [JsonIgnore]
        public EffectService EffectService { get; set; }

        public long Health { get; set; }
        public bool IsDead { get; set; }

        public Skills.Skills Skills { get; set; } = new Skills.Skills();

        public Effects.Effects Effects { get; set; } = new Effects.Effects();

        public event EventHandler OnEvade;

        public int GetAccuracy()
            => GetAccuracyInternal();

        protected abstract int GetAccuracyInternal();

        public double GetCritChance()
            => GetCritChanceInternal();

        protected abstract double GetCritChanceInternal();

        public double GetCritDamage()
            => GetCritDamageInternal();

        protected abstract double GetCritDamageInternal();

        public long GetDamage()
        {
            var result = GetDamageInternal();
            result = Convert.ToInt64(result * EffectService.GetDamageKoef());
            result += EffectService.GetDamageBonus();
            return result;
        }

        protected abstract long GetDamageInternal();

        public long GetDefence()
        {
            var result = GetDefenceInternal();
            result = Convert.ToInt64(result * EffectService.GetDefenceKoef());
            result += EffectService.GetDefenceBonus();
            return result;
        }

        protected abstract long GetDefenceInternal();

        public abstract double GetDefenceKoef();

        public int GetEvasion()
            => GetEvasionInternal();

        protected abstract int GetEvasionInternal();

        public abstract SoulShot GetSoulShot();

        public void OnDeathActivate(object obj, EventArgs e)
        {
            EffectService.RemoveAll();
        }

        public void OnEvadeActivate(object obj, EventArgs e)
            => OnEvade?.Invoke(obj, e);

        public virtual void OnHitActivate() { }
    }
}
