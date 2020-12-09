using FieldGameplay;
using Models;
using UnityEngine;
using Views;

public class Entry : MonoBehaviour
{
    [Header("Objects")]
    
    [SerializeField] private AudioSource _musicAudioSource = default;
    [SerializeField] private AudioSource _soundAudioSource = default;

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
        var settings = new Settings(_musicAudioSource, _soundAudioSource);
        _uiController.Init(gameSession, settings);

        var moveManager = new MoveManager(gameSession, _fieldEntity, firstPlayer, secondPlayer);
        _moveView.Init(moveManager, firstPlayer, secondPlayer);
    }
}