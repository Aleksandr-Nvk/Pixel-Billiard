using UnityEngine;
using System;

public class Controls : MonoBehaviour
{
#pragma warning disable 0649

    [SerializeField] private Camera _mainCamera;
    
#pragma warning restore
    
    public Action<Vector3> OnTouchDown;
    public Action<Vector3> OnTouchDrag;
    public Action<Vector3> OnTouchUp;

    private Vector3 _startTouchPosition;
    private Vector3 _currentTouchPosition;
    private Vector3 _endTouchPosition;
    
    private void Update()
    {
#if  UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            _startTouchPosition = GetClickPosition();
            OnTouchDown?.Invoke(_startTouchPosition);
        }
        if (Input.GetMouseButton(0))
        {
            _currentTouchPosition = GetClickPosition();
            OnTouchDrag?.Invoke(_currentTouchPosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            _endTouchPosition = GetClickPosition();
            OnTouchUp?.Invoke(_endTouchPosition);
        }
        
#endif

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _startTouchPosition = GetTouchPosition();
            OnTouchDown?.Invoke(_startTouchPosition);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _currentTouchPosition = GetTouchPosition();
            OnTouchDrag?.Invoke(_currentTouchPosition);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            _endTouchPosition = GetTouchPosition();
            OnTouchUp?.Invoke(_endTouchPosition);
        }
    }

    /// <summary>
    /// Converts Vector2 mouse position into Vector3 world position
    /// </summary>
    /// <returns> Mouse click world position </returns>
    private Vector3 GetClickPosition()
    {
        return _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    
    /// <summary>
    /// Converts Vector2 touch position into Vector3 world position
    /// </summary>
    /// <returns> Touch world position </returns>
    private Vector3 GetTouchPosition()
    {
        return _mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
    }
}