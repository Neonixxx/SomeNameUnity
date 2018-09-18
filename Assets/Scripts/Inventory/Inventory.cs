using System.Collections;
using System.Collections.Generic;
using SomeName.Core.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        public List<GameObject> InventorySlots = new List<GameObject>();

        private ResourceManager _resourceManager;

        private InventoryService InventoryService;

        // Use this for initialization
        void Start()
        {
            InventoryService = FindObjectOfType<GameState>().Player.InventoryService;
            _resourceManager = FindObjectOfType<ResourceManager>();
        }

        // Update is called once per frame
        void Update()
        {
            for (var i = 0; i < InventorySlots.Count && i < InventoryService.Count; i++)
            {
                InventorySlots[i].GetComponent<Image>().sprite = _resourceManager.GetSprite(InventoryService.Get(i).ImageId);
            }
        }
    }
}