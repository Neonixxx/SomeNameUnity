using SomeName.Core.Items.Impl;
using UnityEngine;
using UnityEngine.UI;

namespace Farm
{
    public class UseSoulShotToggle : MonoBehaviour
    {
        private SoulShot _soulShot;
        private Toggle useSoulShotToggle;

        void Start()
        {
            _soulShot = FindObjectOfType<GameState>().Player.InventoryService.Inventory.SoulShot;
            useSoulShotToggle = GetComponent<Toggle>();
            if (_soulShot != null)
            {
                useSoulShotToggle.isOn = _soulShot.IsActivated;
                useSoulShotToggle.onValueChanged.AddListener(isOn => _soulShot.IsActivated = isOn);
            }
            else
                useSoulShotToggle.gameObject.SetActive(false);
        }
    }
}