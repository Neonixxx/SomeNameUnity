using System.Collections.Generic;
using SomeName.Core.Locations;
using UnityEngine;
using UnityEngine.UI;

public class LocationDropDown : MonoBehaviour
{
    private SomeName.Core.Services.LocationService _locationService;
    private List<Location> _locations;
    private Dropdown _dropdown;

    // Use this for initialization
    void Start ()
    {
        _locationService = FindObjectOfType<GameState>().Player.LocationService;
        _dropdown = gameObject.GetComponent<Dropdown>();
        _locations = _locationService.GetOpenedLocations();
        var optionDatas = new List<Dropdown.OptionData>();
        for (int i = 0; i < _locations.Count; i++)
            optionDatas.Add(new Dropdown.OptionData(_locations[i].ToString()));

        _dropdown.options = optionDatas;
        _dropdown.value = _locations.IndexOf(_locationService.GetCurrentLocation());
        _dropdown.onValueChanged.AddListener(delegate { ValueChanged(); });
    }

    private void ValueChanged()
        => _locationService.MoveTo(_locations[_dropdown.value].Id);
}
