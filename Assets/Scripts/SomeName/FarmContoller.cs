using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SomeName.Core.Domain;
using SomeName.Core.Monsters.Impl;
using SomeName.Core.Monsters.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FarmContoller : MonoBehaviour {

    public SimpleHealthBar MonsterHealthBar;
    public SimpleHealthBar PlayerHealthBar;
    public SimpleHealthBar ExpBar;

    public Text LevelText;
    public Text GoldText;

    private Player _player;

    private Monster _monster;

    // Use this for initialization
    void Start ()
    {
        MonsterHealthBar = GameObject.Find("MonsterStatus").GetComponentInChildren<SimpleHealthBar>();
        var gameState = GameObject.Find("GameState").GetComponent<GameState>();
        _player = gameState.Player;

        _player.Respawn();
        NewMonster();
    }

    private void Update()
    {
        if (Input.touches.Any(s => s.phase == TouchPhase.Began)  || Input.GetMouseButtonDown(0))
        {
            _player.Attack(_monster);
            if (_monster.IsDead)
            {
                var drop = _monster.GetDrop();
                _player.TakeDrop(drop);
                _player.Health = _player.GetMaxHealth();
                // FarmForm.UpdateDropInfo(new DropInfo(_monster, drop));
                NewMonster();
            }
        }

        _monster.Update(_player, Time.deltaTime);

        MonsterHealthBar.UpdateBar(_monster.Health, _monster.MaxHealth);
        PlayerHealthBar.UpdateBar(_player.Health, _player.GetMaxHealth());
        ExpBar.UpdateBar(_player.Exp, _player.ExpForNextLevel);

        LevelText.text = $"Level: {_player.Level.ToString()}";
        GoldText.text = _player.Inventory.Gold.ToString();

        if (_player.IsDead)
            SceneManager.LoadScene("Game");
    }

    private void NewMonster()
        => _monster = MonsterFactory.GetRandomMonster(_player.Level);
}
