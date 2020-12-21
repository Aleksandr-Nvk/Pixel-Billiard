using Balls;
using CueData;
using FieldData;
using UnityEngine;
using Models;
using Views;

public class Entry : MonoBehaviour
{
    [Header("Field objects")]
    
    [SerializeField] private Triangle _trianglePrefab = default;
    
    [SerializeField] private Cue _cuePrefab = default;
    
    [SerializeField] private Field _fieldPrefab = default;
    
    [Header("Models")]
    
    [SerializeField] private AudioManager _audioManager = default;

    [Header("Views")]
    
    [SerializeField] private UIController _uiController = default;
    
    private void Awake()
    {
        var ballsFactory = new BallsFactory(_trianglePrefab, _audioManager);
        var cueFactory = new CueFactory(_cuePrefab);
        var fieldFactory = new FieldFactory(_fieldPrefab);

        var settings = new Settings();
        _uiController.Init(settings);
    }
}