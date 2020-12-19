using System.Collections;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera = default;
    
    public static Action<Vector3> OnTouchDown;
    public static Action<Vector3> OnTouchDrag;
    public static Action<Vector3> OnTouchUp;

    private static Vector3 _touchDownPosition;
    private static Vector3 _touchDragPosition;
    private static Vector3 _touchUpPosition;

    private static Camera _camera;

    private static InputManager _instance;

    private static Coroutine _inputChecker;

    private void Start()
    {
        _instance = this;
        _camera = _mainCamera;
        
        StartTracking();
    }

    /// <summary>
    /// Starts checking input
    /// </summary>
    public static void StartTracking()
    {
        _inputChecker = _instance.StartCoroutine(CheckInput());
    }

    /// <summary>
    /// Stops checking input
    /// </summary>
    public static void StopTracking()
    {
        _instance.StopCoroutine(_inputChecker);
    }

    private static IEnumerator CheckInput()
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
                if (_touchDragPosition != _touchDownPosition)
                    OnTouchDrag?.Invoke(_touchDragPosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                _touchUpPosition = GetClickPosition();
                OnTouchUp?.Invoke(_touchUpPosition);
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
                OnTouchUp?.Invoke(_touchUpPosition);
            }

            yield return null;
        }
    }

    /// <summary>
    /// Converts Vector2 mouse position into Vector3 world position
    /// </summary>
    /// <returns> Mouse click world position </returns>
    private static Vector3 GetClickPosition()
    {
        return _camera.ScreenToWorldPoint(Input.mousePosition);
    }
    
    /// <summary>
    /// Converts Vector2 touch position into Vector3 world position
    /// </summary>
    /// <returns> Touch world position </returns>
    private static Vector3 GetTouchPosition()
    {
        return _camera.ScreenToWorldPoint(Input.GetTouch(0).position);
    }
}