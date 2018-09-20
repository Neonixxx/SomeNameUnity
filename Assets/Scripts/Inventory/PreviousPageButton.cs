using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    class PreviousPageButton : MonoBehaviour, IPointerClickHandler
    {
        private Inventory _inventory;

        public void Start()
            => _inventory = FindObjectOfType<Inventory>();

        public void OnPointerClick(PointerEventData eventData)
            => _inventory.PreviousPage();
    }
}
