using System;
using SomeName.Core.Domain;
using SomeName.Core.Exceptions;
using SomeName.Core.Items.Impl;
using SomeName.Core.Services;

namespace SomeName.Core.Monsters.Interfaces
{
    public abstract class Monster : IBattleUnit
    {
        public SkillService SkillService { get; set; }

        public EffectService EffectService { get; set; }

        public Skills.Skills Skills { get; set; } = new Skills.Skills();

        public Effects.Effects Effects { get; set; } = new Effects.Effects();

        public int Level { get; set; }

        public long Damage { get; set; }

        public int Accuracy { get; set; }

        public int Evasion { get; set; }

        public long MaxHealth { get; set; }

        public long Health { get; set; }

        public Drop DroppedItems { get; set; }

        public bool IsDead { get; set; }

        public bool IsDropTaken { get; set; }

        public string Description { get; set; } = "Not implemented";

        public MonsterType MonsterType { get; set; }

        public event EventHandler OnEvade;

        public Drop GetDrop()
        {
            if (!IsDead)
                throw new MonsterNotDeadException();

            return DroppedItems;
        }

        public override string ToString()
            => $"{MonsterType.GetDescription()}" +
            $"{Environment.NewLine}Level {Level} {Description}";

        public long GetDamage()
        {
            var result = Damage;
            result = Convert.ToInt64(result * EffectService.GetDamageKoef());
            result += EffectService.GetDamageBonus();
            return result;
        }

        public int GetAccuracy()
            => Accuracy;

        // TODO : Сделать шанс крита и силу крита монстрам.
        public double GetCritChance()
            => 0.0;

        public double GetCritDamage()
            => 0.0;

        public int GetEvasion()
            => Evasion;

        // TODO : Сделать защиту монстра.
        // TODO : Доделать изменение защиты эффектами.
        public double GetDefenceKoef()
            => 0.0;

        public void OnHit() { }

        public void OnEvadeActivate(object obj, EventArgs e)
            => OnEvade?.Invoke(obj, e);

        public void OnDeathActivate(object attacker, EventArgs e)
        {
            EffectService.RemoveAll();
        }

        public SoulShot GetSoulShot()
            => null;
    }
}
