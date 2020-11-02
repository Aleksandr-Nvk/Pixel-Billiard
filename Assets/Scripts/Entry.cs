using UnityEngine;

public class Entry : MonoBehaviour
{
#pragma warning disable 0649

    [SerializeField] private MoveManager _moveManager;
    
#pragma warning restore
    
    private void Awake()
    {
        var firstPlayer = new Player("Nic");
        var secondPlayer = new Player("Alicia");

        _moveManager.Init(firstPlayer, secondPlayer);
    }
}