﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SomeName.Core.Items.Impl;
using SomeName.Core.Items.Interfaces;
using SomeName.Core.Locations;
using SomeName.Core.Managers;
using SomeName.Core.Services;

namespace SomeName.Core.Domain
{
    // TODO : Сделать конвертер Player -> StatsInfo.
    public class Player : IAttacker, IAttackTarget
    {
        [JsonIgnore]
        public PlayerStatsCalculator PlayerStatsCalculator { get; set; }

        [JsonIgnore]
        public InventoryService InventoryService { get; set; }

        [JsonIgnore]
        public CubeService CubeService { get; set; }

        [JsonIgnore]
        public SkillService SkillService { get; set; }

        [JsonIgnore]
        public ExperienceManager ExperienceManager { get; set; }

        [JsonIgnore]
        public LocationService LocationService { get; set; }

        [JsonIgnore]
        public EffectService EffectService { get; set; }

        public Player()
        {
            PlayerStatsCalculator = PlayerStatsCalculator.Standard;
        }

        public event EventHandler OnEvade;

        public void OnEvadeActivate(object obj, EventArgs e)
            => OnEvade?.Invoke(obj, e);

        public void Initialize()
        {
            LocationService = new LocationService(LocationsInfo);
            ExperienceManager = new ExperienceManager(this);
            InventoryService = new InventoryService(Inventory);
            CubeService = new CubeService(this);
            SkillService = new SkillService(this, Skills);
            EffectService = new EffectService(Effects);
        }

        public Level Level { get; set; } = new Level();

        public long ExpForNextLevel { get; set; }

        public long Exp { get; set; }

        public long Health { get; set; }

        public bool IsDead { get; set; }

        public Inventory Inventory { get; set; } = new Inventory();

        public Skills.Skills Skills { get; set; } = new Skills.Skills();

        public LocationsInfo LocationsInfo { get; set; } = new LocationsInfo();

        public Effects.Effects Effects { get; set; } = new Effects.Effects();

        public long GetDamage()
        {
            var result = PlayerStatsCalculator.CalculateDamage(this);
            result = Convert.ToInt64(result * EffectService.GetDamageKoef());
            result += EffectService.GetDamageBonus();
            return result;
        }

        public long GetDefence()
            => PlayerStatsCalculator.CalculateDefence(this);

        public double GetDefenceKoef()
            => PlayerStatsCalculator.CalculateDefenceKoef(this);

        public long GetMaxHealth()
            => PlayerStatsCalculator.CalculateMaxHealth(this);

        public int GetPower()
            => PlayerStatsCalculator.CalculatePower(this);

        public int GetVitality()
            => PlayerStatsCalculator.CalculateVitality(this);

        public int GetAccuracy()
            => PlayerStatsCalculator.CalculateAccuracy(this);

        public int GetEvasion()
            => PlayerStatsCalculator.CalculateEvasion(this);

        public double GetCritChance()
            => PlayerStatsCalculator.CalculateCritChance(this);

        public double GetCritDamage()
            => PlayerStatsCalculator.CalculateCritDamage(this);

        public long GetHealthPerHit()
            => PlayerStatsCalculator.CalculateHealthPerHit(this);


        public void OnHit()
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

        public void TakeItem(IItem item)
            => InventoryService.Add(item);

        public void TakeItems(List<IItem> items)
            => InventoryService.AddRange(items);

        public SoulShot GetSoulShot()
            => Inventory.SoulShot;
    }
}
