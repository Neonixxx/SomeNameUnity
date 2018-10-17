using System;
using SomeName.Core.Balance;
using SomeName.Core.Domain;
using SomeName.Core.Exceptions;

namespace SomeName.Core.Monsters.Interfaces
{
    public abstract class Monster : IAttacker, IAttackTarget
    {
        public MonsterAttackController Attacker { get; set; }

        public int Level { get; set; }

        public long Damage { get; set; }

        public double AttackSpeed { get; set; }

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


        public void Update(IAttackTarget target, double timeDelta)
            => Attacker.Update(target, timeDelta);

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
            => Damage;

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
        public double GetDefenceKoef()
            => 0.0;

        public void OnHit() { }

        public void OnEvadeActivate(object obj, EventArgs e)
            => OnEvade?.Invoke(obj, e);
    }
}
