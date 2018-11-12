using Newtonsoft.Json;
using SomeName.Core.Items.Impl;
using SomeName.Core.Locations;
using SomeName.Core.Managers;
using SomeName.Core.Services;

namespace SomeName.Core.Domain
{
    // TODO : Сделать конвертер Player -> StatsInfo.
    public class Player : BattleUnit
    {
        [JsonIgnore]
        public PlayerStatsCalculator PlayerStatsCalculator { get; set; }

        [JsonIgnore]
        public InventoryService InventoryService { get; set; }

        [JsonIgnore]
        public CubeService CubeService { get; set; }

        [JsonIgnore]
        public ExperienceManager ExperienceManager { get; set; }

        [JsonIgnore]
        public LocationService LocationService { get; set; }

        public Player()
        {
            PlayerStatsCalculator = PlayerStatsCalculator.Standard;
        }

        public void Initialize()
        {
            LocationService = new LocationService(LocationsInfo);
            ExperienceManager = new ExperienceManager(this);
            InventoryService = new InventoryService(Inventory);
            CubeService = new CubeService(this);
            SkillService = new SkillService(this, Skills);
            EffectService = new EffectService(this, Effects);
        }

        public Level Level { get; set; } = new Level();

        public long ExpForNextLevel { get; set; }

        public long Exp { get; set; }

        public Inventory Inventory { get; set; } = new Inventory();

        public LocationsInfo LocationsInfo { get; set; } = new LocationsInfo();
        

        public override double GetDefenceKoef()
            => PlayerStatsCalculator.CalculateDefenceKoef(this);

        public long GetMaxHealth()
            => PlayerStatsCalculator.CalculateMaxHealth(this);

        public int GetPower()
            => PlayerStatsCalculator.CalculatePower(this);

        public int GetVitality()
            => PlayerStatsCalculator.CalculateVitality(this);

        public long GetHealthPerHit()
            => PlayerStatsCalculator.CalculateHealthPerHit(this);

        protected override long GetDamageInternal()
            => PlayerStatsCalculator.CalculateDamage(this);

        protected override long GetDefenceInternal()
            => PlayerStatsCalculator.CalculateDefence(this);

        protected override int GetAccuracyInternal()
            => PlayerStatsCalculator.CalculateAccuracy(this);

        protected override int GetEvasionInternal()
            => PlayerStatsCalculator.CalculateEvasion(this);

        protected override double GetCritChanceInternal()
            => PlayerStatsCalculator.CalculateCritChance(this);

        protected override double GetCritDamageInternal()
            => PlayerStatsCalculator.CalculateCritDamage(this);


        public override void OnHitActivate()
        {
            var healthResult = Health + GetHealthPerHit();
            var maxHealth = GetMaxHealth();
            Health = healthResult > maxHealth
                ? maxHealth
                : healthResult;
        }


        public void Respawn()
        {
            Health = GetMaxHealth();
            IsDead = false;
        }

        public void TakeDrop(Drop drop)
        {
            TakeExp(drop.Exp);
            InventoryService.Add(drop);
        }

        public void TakeExp(long exp)
            => ExperienceManager.TakeExp(exp);

        public override SoulShot GetSoulShot()
            => Inventory.SoulShot;
    }
}
