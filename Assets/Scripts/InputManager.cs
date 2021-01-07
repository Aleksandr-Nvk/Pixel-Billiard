using System.Collections;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera = default;
    
    public Action<Vector3> OnTouchDown;
    public Action<Vector3> OnTouchDrag;
    public Action<Vector3> OnTouchUp;

    private Vector3 _touchDownPosition;
    private Vector3 _touchDragPosition;
    private Vector3 _touchUpPosition;
    
    private Coroutine _inputChecker;

    private bool _isChecking;

    /// <summary>
    /// Starts checking input
    /// </summary>
    public void StartChecking()
    {
        if (!_isChecking)
            _inputChecker = StartCoroutine(CheckInput());
        _isChecking = true;
    }

    /// <summary>
    /// Stops checking input
    /// </summary>
    public void StopChecking()
    {
        if (_isChecking)
            StopCoroutine(_inputChecker);
        _isChecking = false;
    }

    private IEnumerator CheckInput()
    {
        yield return new WaitForEndOfFrame();
        
        while (true)
        {

    #if  UNITY_EDITOR
        
            if (Input.GetMouseButtonDown(0))
            {
                _touchDownPosition = GetClickPosition();
                OnTouchDown?.Invoke(_touchDownPosition);
            }
            if (Input.GetMouseButton(0))
            {
                _touchDragPosition = GetClickPosition();
                if (_touchDragPosition != _touchDownPosition) OnTouchDrag?.Invoke(_touchDragPosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                _touchUpPosition = GetClickPosition();
                if (_touchUpPosition != _touchDownPosition) OnTouchUp?.Invoke(_touchUpPosition);
            }
        
    #endif

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _touchDownPosition = GetTouchPosition();
                OnTouchDown?.Invoke(_touchDownPosition);
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                _touchDragPosition = GetTouchPosition();
                OnTouchDrag?.Invoke(_touchDragPosition);
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _touchUpPosition = GetTouchPosition();
                if (_touchUpPosition != _touchDownPosition) OnTouchUp?.Invoke(_touchUpPosition);
            }

            yield return null;
        }
    }

    /// <summary>
    /// Converts Vector2 mouse position into Vector3 world position
    /// </summary>
    /// <returns> Mouse click world position </returns>
    private Vector3 GetClickPosition() => _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    
    /// <summary>
    /// Converts Vector2 touch position into Vector3 world position
    /// </summary>
    /// <returns> Touch world position </returns>
    private Vector3 GetTouchPosition() => _mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
}