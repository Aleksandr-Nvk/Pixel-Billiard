using UnityEngine;

public class MoveView : MonoBehaviour
{
    [SerializeField] private GameObject _firstPlayerPointer = default;
    [SerializeField] private GameObject _secondPlayerPointer = default;

    private MoveManager _moveManager;

    public void Init(MoveManager moveManager)
    {
        _moveManager = moveManager;
        _moveManager.OnPlayerSwitched += SwitchPointer;
    }

    private void OnDestroy()
    {
        _moveManager.OnPlayerSwitched -= SwitchPointer;
    }

    /// <summary>
    /// Switches the pointer
    /// </summary>
    private void SwitchPointer()
    {
        _firstPlayerPointer.SetActive(!_firstPlayerPointer.activeSelf);
        _secondPlayerPointer.SetActive(!_secondPlayerPointer.activeSelf);
    }
}