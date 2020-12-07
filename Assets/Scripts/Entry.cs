using FieldGameplay;
using UnityEngine;
using Views;

public class Entry : MonoBehaviour
{
    [Header("Models")]
    
    [SerializeField] private Field _fieldEntity = default;

    [Header("Views")]
    
    [SerializeField] private UIController _uiController = default;

    [SerializeField] private MoveView _moveView = default;

    private void Awake()
    {
        var firstPlayer = new Player("Nic");
        var secondPlayer = new Player("Alicia");
        
        var gameSession = new GameSession();
        
        _uiController.Init(gameSession);

        var moveManager = new MoveManager(gameSession, _fieldEntity, firstPlayer, secondPlayer);
        _moveView.Init(moveManager, firstPlayer, secondPlayer);
    }
}