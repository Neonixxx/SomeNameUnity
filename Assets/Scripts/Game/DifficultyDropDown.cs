using System.Collections;
using System.Collections.Generic;
using SomeName.Core.Services;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyDropDown : MonoBehaviour
{
    private DifficultyService _difficultyService = DifficultyService.Standard;
    private Dropdown _dropdown;

    // Use this for initialization
    void Start ()
    {
        _dropdown = gameObject.GetComponent<Dropdown>();
        var battleDifficulties = _difficultyService.BattleDifficulties;
        var optionDatas = new List<Dropdown.OptionData>();
        for (int i = 0; i < battleDifficulties.Length; i++)
            optionDatas.Add(new Dropdown.OptionData(battleDifficulties[i]));

        _dropdown.options = optionDatas;
        _dropdown.value = _difficultyService.GetCurrentDifficultyIndex();
        _dropdown.onValueChanged.AddListener(delegate { ValueChanged(); });
    }

    private void ValueChanged()
        => _difficultyService.SetBattleDifficulty(_dropdown.value);

    // Update is called once per frame
    void Update ()
    {
		
	}
}
