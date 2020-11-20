using UnityEngine;

public class Entry : MonoBehaviour
{
    [SerializeField] private MoveManager _moveManager = default;

    [SerializeField] private MoveView _moveView = default;
    
    private void Awake()
    {
        var firstPlayer = new Player("Nic");
        var secondPlayer = new Player("Alicia");

        _moveManager.Init(firstPlayer, secondPlayer);
        _moveView.Init(firstPlayer, secondPlayer);
    }
}