using System.Collections.Generic;
using System.Linq;
using SomeName.Core.Domain;
using SomeName.Core.Effects;
using SomeName.Core.Monsters.Interfaces;
using SomeName.Core.Services;
using SomeName.Core.Skills;
using UnityEngine;
using UnityEngine.UI;

public class FarmContoller : MonoBehaviour
{
    public SimpleHealthBar MonsterHealthBar;
    public Text MonsterDescription;
    public SimpleHealthBar PlayerHealthBar;
    public SimpleHealthBar ExpBar;

    public Button FightBossButton;
    public SimpleHealthBar FightBossValueProgressBar;

    public Text LevelText;
    public Text GoldText;

    public InventorySlot DefaultSkillSlot;
    public InventorySlot[] ActiveSkillSlots;

    public GameObject EffectPrefab;
    private Dictionary<Effect, GameObject> _effectSlots = new Dictionary<Effect, GameObject>();
    private Transform _canvasTransform;

    private SimpleHealthBar _defaultSkillCooldownBar;
    private SimpleHealthBar[] _activeSkillCooldownBars;

    public InventorySlot MonsterCastingSkillSlot;
    public SimpleHealthBar MonsterCastingSkillCooldownBar;

    private Player _player;
    private SkillService _skillService;
    private EffectService _effectService;
    private ResourceManager _resourceManager;
    private SomeName.Core.Services.LocationService _locationService;

    private Monster _monster;
    private SkillService _monsterSkillService;

    // Use this for initialization
    void Start ()
    {
        MonsterHealthBar = GameObject.Find("MonsterStatus").GetComponentInChildren<SimpleHealthBar>();
        var gameState = GameObject.Find("GameState").GetComponent<GameState>();
        _player = gameState.Player;
        _skillService = _player.SkillService;
        _effectService = _player.EffectService;
        _resourceManager = FindObjectOfType<ResourceManager>();
        _locationService = _player.LocationService;

        _canvasTransform = GameObject.Find("Canvas").transform;

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

        EffectsUpdate();
        SkillsUpdate();

        if (_monster.IsDead)
        {
            var drop = _monster.GetDrop();
            _player.TakeDrop(drop);
            _locationService.MonsterKilled();
            // FarmForm.UpdateDropInfo(new DropInfo(_monster, drop));
            NewMonster();
        }

        // Обновление кастуемого скилла монстра.
        _monsterSkillService.Update(_player, Time.deltaTime);
        if (_monsterSkillService.IsCasting)
        {
            var castingSkill = _monsterSkillService.GetCastingSkill();
            MonsterCastingSkillSlot.gameObject.SetActive(true);
            UpdateSkillSlot(castingSkill, MonsterCastingSkillSlot, MonsterCastingSkillCooldownBar);
        }
        else
        {
            MonsterCastingSkillSlot.gameObject.SetActive(false);
        }

        MonsterHealthBar.UpdateBar(_monster.Health, _monster.MaxHealth);
        MonsterDescription.text = _monster.ToString();
        PlayerHealthBar.UpdateBar(_player.Health, _player.GetMaxHealth());
        ExpBar.UpdateBar(_player.Exp, _player.ExpForNextLevel);

        LevelText.text = $"Level: {_player.Level.ToString()}";
        GoldText.text = _player.Inventory.Gold.ToString();

        UpdateFightBoss();
    }

    private void EffectsUpdate()
    {
        _effectService.Update(Time.deltaTime);

        foreach (var effect in _effectService.Effects.All)
            if (!_effectSlots.ContainsKey(effect))
                _effectSlots.Add(effect, Instantiate(EffectPrefab, _canvasTransform));

        var x = -270f;
        var y = -670f;
        var xStep = 95f;

        foreach (var item in _effectSlots)
        {
            var effect = item.Key;
            var gameObject = item.Value;
            if (!_effectService.Effects.All.Contains(effect))
            {
                Destroy(gameObject);
                continue;
            }

            var slot = gameObject.GetComponent<InventorySlot>();
            var durationBar = gameObject.GetComponentInChildren<SimpleHealthBar>();

            gameObject.transform.localPosition = new Vector3(x, y);
            x += xStep;
            var sprite = _resourceManager.GetSprite(effect.ImageId);
            slot.SetMainSprite(sprite);
            durationBar.StringFormatValue = 0;
            durationBar.UpdateBar((float)effect.DurationLeft, (float)effect.Duration);
        }
    }

    /// <summary>
    /// Обновить скиллы игрока.
    /// </summary>
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

    private void UpdateFightBoss()
    {
        FightBossValueProgressBar.UpdateBar(_locationService.GetCurrentFightBossValue(), _locationService.GetRequiredFightBossValue());
        if (_locationService.CanFightBoss())
        {
            FightBossButton.gameObject.SetActive(true);
        }
        else
        {
            FightBossButton.gameObject.SetActive(false);
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
    {
        _monster = _locationService.GetMonster();
        _monsterSkillService = _monster.MonsterSkillController;
        _monsterSkillService.StartBattle();
    }

    public void FightBoss()
    {
        _monster = _locationService.GetBoss();
        _monsterSkillService = _monster.MonsterSkillController;
        _monsterSkillService.StartBattle();
    }
}
