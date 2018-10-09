using System.Linq;
using SomeName.Core.Domain;
using SomeName.Core.Monsters.Impl;
using SomeName.Core.Monsters.Interfaces;
using SomeName.Core.Services;
using SomeName.Core.Skills;
using UnityEngine;
using UnityEngine.UI;

public class FarmContoller : MonoBehaviour {

    public SimpleHealthBar MonsterHealthBar;
    public SimpleHealthBar PlayerHealthBar;
    public SimpleHealthBar ExpBar;

    public Text LevelText;
    public Text GoldText;

    public InventorySlot DefaultSkillSlot;
    public InventorySlot[] ActiveSkillSlots;

    private SimpleHealthBar _defaultSkillCooldownBar;
    private SimpleHealthBar[] _activeSkillCooldownBars;

    private Player _player;
    private SkillService _skillService;
    private ResourceManager _resourceManager;

    private Monster _monster;

    // Use this for initialization
    void Start ()
    {
        MonsterHealthBar = GameObject.Find("MonsterStatus").GetComponentInChildren<SimpleHealthBar>();
        var gameState = GameObject.Find("GameState").GetComponent<GameState>();
        _player = gameState.Player;
        _skillService = _player.SkillService;
        _resourceManager = FindObjectOfType<ResourceManager>();

        _defaultSkillCooldownBar = DefaultSkillSlot.GetComponentInChildren<SimpleHealthBar>();
        _activeSkillCooldownBars = ActiveSkillSlots.Select(s => s.GetComponentInChildren<SimpleHealthBar>()).ToArray();

        EventsSubscribe();

        _player.Respawn();
        _skillService.StartBattle();
        NewMonster();
    }

    private void OnDestroy()
    {
        _skillService.EndBattle();
    }

    private void EventsSubscribe()
    {
        DefaultSkillSlot.FirstClick += (obj, e) => DefaultSkillActivate();
        for (int i = 0; i < ActiveSkillSlots.Length; i++)
        {
            var index = i;
            ActiveSkillSlots[i].FirstClick += (obj, e) => ActiveSkillActivate(index);
        }
    }

    private void Update()
    {
        if (_player.IsDead)
            return;

        SkillsUpdate();

        if (_monster.IsDead)
        {
            var drop = _monster.GetDrop();
            _player.TakeDrop(drop);
            _player.Health = _player.GetMaxHealth();
            // FarmForm.UpdateDropInfo(new DropInfo(_monster, drop));
            NewMonster();
        }

        _monster.Update(_player, Time.deltaTime);

        MonsterHealthBar.UpdateBar(_monster.Health, _monster.MaxHealth);
        PlayerHealthBar.UpdateBar(_player.Health, _player.GetMaxHealth());
        ExpBar.UpdateBar(_player.Exp, _player.ExpForNextLevel);

        LevelText.text = $"Level: {_player.Level.ToString()}";
        GoldText.text = _player.Inventory.Gold.ToString();
    }

    private void SkillsUpdate()
    {
        _skillService.Update(_monster, Time.deltaTime);

        UpdateSkillSlot(_skillService.Skills.DefaultSkill, DefaultSkillSlot, _defaultSkillCooldownBar);

        var min = Mathf.Min(ActiveSkillSlots.Length, _skillService.Skills.ActiveSkills.Count);
        for (int i = 0; i < min; i++)
        {
            var skill = _skillService.Skills.ActiveSkills[i];
            var skillSlot = ActiveSkillSlots[i];
            var cooldownBar = _activeSkillCooldownBars[i];
            UpdateSkillSlot(skill, skillSlot, cooldownBar);
        }
    }

    private void UpdateSkillSlot(ISkill skill, InventorySlot skillSlot, SimpleHealthBar cooldownBar)
    {
        var sprite = _resourceManager.GetSprite(skill.ImageId);
        skillSlot.SetMainSprite(sprite);
        if (skill.IsCasting)
        {
            cooldownBar.displayText = SimpleHealthBar.DisplayText.Percentage;
            cooldownBar.UpdateColor(Color.green);
            cooldownBar.UpdateBar((float)skill.CurrentCastingTime, (float)skill.CastingTime);
        }
        else
        {
            cooldownBar.displayText = SimpleHealthBar.DisplayText.CurrentValue;
            cooldownBar.UpdateColor(Color.white);
            cooldownBar.UpdateBar((float)skill.CurrentCooldown, (float)skill.Cooldown);
        }
    }

    private void DefaultSkillActivate()
        => _skillService.Skills.DefaultSkill.StartCasting();

    private void ActiveSkillActivate(int skillIndex)
    {
        if (skillIndex >= _skillService.Skills.ActiveSkills.Count)
            return;
        _skillService.Skills.ActiveSkills[skillIndex].StartCasting();
    }

    private void NewMonster()
        => _monster = MonsterFactory.GetRandomMonster(_player.Level);
}
