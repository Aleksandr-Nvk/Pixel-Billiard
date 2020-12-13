using FieldGameplay;
using UnityEngine;
using Models;
using Views;

public class Entry : MonoBehaviour
{
    [Header("Models")]
    
    [SerializeField] private GameplayRootSpawner _gameplayRootSpawner = default;

    [Header("Views")]
    
    [SerializeField] private UIController _uiController = default;

    [SerializeField] private MoveView _moveView = default;

    private void Awake()
    {
        var firstPlayer = new Player("Nic");
        var secondPlayer = new Player("Alicia");
        
        var gameSession = new GameSession();
        var settings = new Settings();
        _uiController.Init(gameSession, settings);

        _gameplayRootSpawner.Init(out var field);

        var moveManager = new MoveManager(gameSession, field, firstPlayer, secondPlayer);
        _moveView.Init(moveManager, firstPlayer, secondPlayer);
    }
}