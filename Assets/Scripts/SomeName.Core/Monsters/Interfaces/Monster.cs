using System;
using SomeName.Core.Domain;
using SomeName.Core.Exceptions;
using SomeName.Core.Items.Impl;

namespace SomeName.Core.Monsters.Interfaces
{
    public abstract class Monster : BattleUnit
    {
        public int Level { get; set; }

        public long Damage { get; set; }

        public int Accuracy { get; set; }

        public int Evasion { get; set; }

        public long MaxHealth { get; set; }

        public Drop DroppedItems { get; set; }

        public bool IsDropTaken { get; set; }

        public string Description { get; set; } = "Not implemented";

        public MonsterType MonsterType { get; set; }

        public Drop GetDrop()
        {
            if (!IsDead)
                throw new MonsterNotDeadException();

            return DroppedItems;
        }

        public override string ToString()
            => $"{MonsterType.GetDescription()}" +
            $"{Environment.NewLine}Level {Level} {Description}";

        protected override long GetDamageInternal()
            => Damage;

        // TODO : Сделать защиту монстра.
        protected override long GetDefenceInternal()
            => 0;

        protected override int GetAccuracyInternal()
            => Accuracy;

        // TODO : Сделать шанс крита и силу крита монстрам.
        protected override double GetCritChanceInternal()
            => 0.0;

        protected override double GetCritDamageInternal()
            => 0.0;

        protected override int GetEvasionInternal()
            => Evasion;

        public override double GetDefenceKoef()
            => PlayerStatsCalculator.Standard.CalculateDefenceKoef(Level, GetDefence());

        public override SoulShot GetSoulShot()
            => null;
    }
}
