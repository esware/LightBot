using Dev.Scripts.Managers;
using Game.UI;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BaseOperation))]
public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private GameObject _cloneObject;
    private bool _isDragging = false;
    private bool _isAdded = false;
    public bool IsAdded
    {
        get => _isAdded;
        set => _isAdded = value;
    }

    private void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
    }
    public BaseOperation Operation => this.GetComponent<BaseOperation>();

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (UIHandler.Instance.targetProc != null)
        {
            _cloneObject = Instantiate(gameObject, _canvas.transform);
            _rectTransform = _cloneObject.GetComponent<RectTransform>();
            _isDragging = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDragging && _rectTransform != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPos
            );
            _cloneObject.transform.localPosition=localPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isDragging)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(UIHandler.Instance.targetProc , eventData.position))
            {
                _rectTransform.SetParent(UIHandler.Instance.targetProc , false);
                _rectTransform.anchoredPosition = Vector2.zero;
                
                UIHandler.Instance.AddOperation(this);
            }
            else
            {
                UIHandler.Instance.RemoveOperation(this);
                Destroy(_cloneObject);
            }

            _isDragging = false;
        }
    }
}
