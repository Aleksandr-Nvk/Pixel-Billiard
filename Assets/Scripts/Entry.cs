using Behaviours;
using UnityEngine;
using Models;
using Views;

public class Entry : MonoBehaviour
{
    [Header("Models")]
    
    [SerializeField] private Field _fieldEntity = default;

    [Header("Views")]
    
    [SerializeField] private UIController _uiController = default;

    [SerializeField] private MoveView _moveView = default;

    [SerializeField] private PlayerView _firstPlayerView = default;
    [SerializeField] private PlayerView _secondPlayerView = default;
    
    private void Awake()
    {
        var firstPlayer = new Player("Nic");
        var secondPlayer = new Player("Alicia");
        
        var gameSession = new GameSession(firstPlayer, secondPlayer, _fieldEntity);
        
        _uiController.Init(gameSession);

        _firstPlayerView.Init(firstPlayer);
        _secondPlayerView.Init(secondPlayer);

        var moveManager = new MoveManager(gameSession, _fieldEntity, firstPlayer, secondPlayer);
        _moveView.Init(moveManager);
    }
}