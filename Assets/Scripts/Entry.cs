using FieldGameplay;
using UnityEngine;
using Models;
using Views;

public class Entry : MonoBehaviour
{
    [Header("Models")]
    
    [SerializeField] private FieldItemsFactory _fieldItemsFactory = default;

    [Header("Views")]
    
    [SerializeField] private UIController _uiController = default;
    
    private void Awake()
    {
        var settings = new Settings();
        _uiController.Init(settings);

        _fieldItemsFactory.Create();
    }
}