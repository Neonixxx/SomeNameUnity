using System;
using SomeName.Core.Balance;
using SomeName.Core.Domain;
using SomeName.Core.Exceptions;

namespace SomeName.Core.Monsters.Interfaces
{
    public abstract class Monster : IAttacker, IAttackTarget
    {
        public MonsterAttackController Attacker { get; set; }

        public DropService DropFactory { get; set; }

        public MonsterStatsBalance MonsterStatsBalance { get; set; }

        public int Level { get; private set; }

        public long Damage { get; set; }

        public double AttackSpeed { get; set; }

        public int Accuracy { get; set; }

        public int Evasion { get; set; }

        public long MaxHealth { get; private set; }

        public long Health { get; set; }

        public Drop DroppedItems { get; private set; }

        public bool IsDead { get; set; }

        public bool IsDropTaken { get; private set; }

        public string Description { get; protected set; } = "Not implemented";

        public event EventHandler OnEvade;

        // TODO : Сделать разную скорость атаки монстров.
        public virtual void Respawn(int level)
        {
            Level = level;
            Damage = MonsterStatsBalance.GetDefaultDPS(level);
            AttackSpeed = 1.0;
            Accuracy = MonsterStatsBalance.GetDefaultAccuracy(level);
            Evasion = MonsterStatsBalance.GetDefaultEvasion(level);
            MaxHealth = MonsterStatsBalance.GetDefaultHealth(level);
            Health = MaxHealth;
            DroppedItems = DropFactory.Build(level, MonsterStatsBalance.GetDefaultDropValue(level));
            IsDead = false;
            IsDropTaken = false;
            Attacker = new MonsterAttackController(this);
        }

        public void Update(IAttackTarget target, double timeDelta)
            => Attacker.Update(target, timeDelta);

        public Drop GetDrop()
        {
            if (!IsDead)
                throw new MonsterNotDeadException();

            return DroppedItems;
        }

        public override string ToString()
            => $"Level {Level} {Description}";

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
