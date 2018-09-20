using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    class NextPageButton : MonoBehaviour, IPointerClickHandler
    {
        private Inventory _inventory;

        public void Start()
            => _inventory = FindObjectOfType<Inventory>();

        public void OnPointerClick(PointerEventData eventData)
            => _inventory.NextPage();
    }
}
