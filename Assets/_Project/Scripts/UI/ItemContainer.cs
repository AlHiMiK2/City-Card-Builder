using System.Collections.Generic;
using _Project.Scripts.Handlers;
using _Project.Scripts.UI;
using BezierSolution;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ItemContainer : MonoBehaviour, IDropHandler
{
    [SerializeField] private BezierSpline _spline;
    [SerializeField] private float _spacing;
    [SerializeField] private float _sortSmoothness;
    
    private int _capacity;
    private List<Draggable> _draggables = new List<Draggable>();
    
    public BezierSpline Spline => _spline;
    public bool IsFull => _draggables.Count >= _capacity;

    private void Start()
    {
        _capacity = GameHandler.Instance.GameConfig.MaxCardCount;
    }

    private void Update()
    {
        SortItems(Time.deltaTime);
        Remap();
    }

    private void SortItems(float dt)
    {
        for (var i = 0; i < _draggables.Count; i++)
        {
            var draggable = _draggables[i];
            if (draggable.IsDragging) continue;
            float t = 0.5f + (i - (_draggables.Count - 1) / 2f) * _spacing;
            Vector3 targetPosition = _spline.GetPoint(t);
            
            draggable.transform.position = Vector3.Lerp(draggable.transform.position, targetPosition, dt * _sortSmoothness);
            draggable.transform.right = _spline.GetTangent(t);
        }
    }
    
    public void AddItem(Draggable draggable)
    {
        _draggables.Add(draggable);
        draggable.SetID(_draggables.Count - 1);
    }

    public void RemoveItem(int id)
    {
        _draggables.RemoveAt(id);
        ResetCardIds();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag == null) return;
        if (eventData.pointerDrag.TryGetComponent(out Draggable draggable))
        {
            if(draggable.Container == this) return;
            draggable.SetContainer(this);
        }
    }

    private void Remap()
    {
        _draggables.Sort((a, b) => a.transform.position.x.CompareTo(b.transform.position.x));
        ResetCardIds();
    }

    private void ResetCardIds()
    {
        for (int i = 0; i < _draggables.Count; i++)
        {
            _draggables[i].SetID(i);
        }
    }
}
