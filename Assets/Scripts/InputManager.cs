using System.Collections;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera = default;
    
    public Action<Vector3> OnTouchDown;
    public Action<Vector3> OnTouchDrag;
    public Action<Vector3> OnTouchUp;

    public Action OnEscapeButtonDown;
    
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

            if (Input.GetKeyDown(KeyCode.Escape))
                OnEscapeButtonDown?.Invoke();

            yield return null;
        }
    }

    /// <summary>
    /// Converts Vector2 mouse position into Vector3 world position
    /// </summary>
    /// <returns> Mouse click world position </returns>
    private Vector3 GetClickPosition() => _mainCamera.ScreenToWorldPoint(Input.mousePosition);
}