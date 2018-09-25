using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image _image;
    private Image _backgroundImage;

    public Sprite MainSprite { get { return _image.sprite; } }

    public Sprite ActiveBackgroudSprite;
    public Sprite UnactiveBackgroundSprite;

    public bool IsUnderPointer;
    public bool IsWithItem;
	// Use this for initialization
	void Start ()
    {
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsWithItem)
            DragStarted?.Invoke(this, eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsWithItem)
            Drag?.Invoke(this, eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsWithItem)
            DragEnded?.Invoke(this, eventData.position);
    }

    public void OnPointerEnter(PointerEventData eventData)
        => IsUnderPointer = true;

    public void OnPointerExit(PointerEventData eventData)
        => IsUnderPointer = false;

    public event EventHandler FirstClick;

    public event EventHandler DoubleClick;

    public event EventHandler<Vector2> DragStarted;

    public event EventHandler<Vector2> Drag;

    public event EventHandler<Vector2> DragEnded;
}
