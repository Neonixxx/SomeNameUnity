using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    private Inventory.Inventory _inventory;
    private Image _image;
    private Image _backgroundImage;

    public Sprite ActiveBackgroudSprite;
    public Sprite UnactiveBackgroundSprite;

    public bool IsWithItem;
	// Use this for initialization
	void Start ()
    {
        _inventory = GetComponentInParent<Inventory.Inventory>();
        var background = transform.Find("Background");
        _image = background.Find("Front").GetComponent<Image>();
        _backgroundImage = background.GetComponent<Image>();
    }

    public void SetMainSprite(Sprite sprite)
        => _image.sprite = sprite;

    public void BackgroundSpriteIsActive(bool isActive)
    {
        _backgroundImage.sprite = isActive
            ? ActiveBackgroudSprite
            : UnactiveBackgroundSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FirstClick?.Invoke(this, null);
        if (eventData.clickCount == 2 
            || (Input.touchCount > 0 && Input.GetTouch(0).tapCount == 2))
            DoubleClick?.Invoke(this, null);
    }

    public event EventHandler FirstClick;

    public event EventHandler DoubleClick;
}
