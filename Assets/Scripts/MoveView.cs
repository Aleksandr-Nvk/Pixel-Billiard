using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MoveView : MonoBehaviour
{
    [SerializeField] private Image[] _firstPlayerBallIcons = default;
    [SerializeField] private Image[] _secondPlayerBallIcons = default;
    
    [Space]
    
    [SerializeField] private TextMeshProUGUI _firstPlayerName = default;
    [SerializeField] private TextMeshProUGUI _secondPlayerName = default;
    
    [Space]
    
    [SerializeField] private GameObject _firstPlayerPointer = default;
    [SerializeField] private GameObject _secondPlayerPointer = default;

    private int _firstPlayerBallIndex;
    private int _secondPlayerBallIndex;

    public void Init(Player firstPlayer, Player secondPlayer)
    {
        _firstPlayerName.text = firstPlayer.Name;
        _secondPlayerName.text = secondPlayer.Name;
    }

    /// <summary>
    /// Switches the pointer
    /// </summary>
    public void SwitchPointer()
    {
        _firstPlayerPointer.SetActive(!_firstPlayerPointer.activeSelf);
        _secondPlayerPointer.SetActive(!_secondPlayerPointer.activeSelf);
    }

    /// <summary>
    /// Adds a rolled ball icon to the first player's view
    /// </summary>
    /// <param name="ballSprite"> Ball icon </param>
    public void AddBallToFirstPlayer(Sprite ballSprite)
    {
        var image = _firstPlayerBallIcons[_firstPlayerBallIndex];
        image.sprite = ballSprite;
        image.color = Color.white;
        _firstPlayerBallIndex++;
    }
    
    /// <summary>
    /// Adds a rolled ball icon to the second player's view
    /// </summary>
    /// <param name="ballSprite"> Ball icon </param>
    public void AddBallToSecondPlayer(Sprite ballSprite)
    {
        var image = _secondPlayerBallIcons[_secondPlayerBallIndex];
        image.sprite = ballSprite;
        image.color = Color.white;
        _secondPlayerBallIndex++;
    }
}