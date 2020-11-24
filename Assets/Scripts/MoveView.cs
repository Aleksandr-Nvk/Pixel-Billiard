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

    private Player _firstPlayer;
    private Player _secondPlayer;

    public void Init(Player firstPlayer, Player secondPlayer)
    {
        _firstPlayer = firstPlayer;
        _secondPlayer = secondPlayer;
        
        _firstPlayerName.text = firstPlayer.Name;
        _secondPlayerName.text = secondPlayer.Name;

        _firstPlayer.OnBallRolled += AddBallForPlayer;
        _secondPlayer.OnBallRolled += AddBallForPlayer;

        _firstPlayer.OnPlayerSwitched += SwitchPointer;
        _secondPlayer.OnPlayerSwitched += SwitchPointer;
    }

    private void OnDestroy()
    {
        _firstPlayer.OnBallRolled -= AddBallForPlayer;
        _secondPlayer.OnBallRolled -= AddBallForPlayer;

        _firstPlayer.OnPlayerSwitched -= SwitchPointer;
        _secondPlayer.OnPlayerSwitched -= SwitchPointer;
    }

    /// <summary>
    /// Switches the pointer
    /// </summary>
    private void SwitchPointer()
    {
        _firstPlayerPointer.SetActive(!_firstPlayerPointer.activeSelf);
        _secondPlayerPointer.SetActive(!_secondPlayerPointer.activeSelf);
    }

    /// <summary>
    /// Adds an icon to rolled balls view
    /// </summary>
    /// <param name="icon"> Icon to add </param>
    /// <param name="player"> Player to add for </param>
    private void AddBallForPlayer(Sprite icon, Player player)
    {
        if (player == _firstPlayer)
            AddBallToView(_firstPlayerBallIcons, ref _firstPlayerBallIndex, icon);
        else
            AddBallToView(_secondPlayerBallIcons, ref _secondPlayerBallIndex, icon);
    }

    /// <summary>
    /// Adds an icon to the icons list
    /// </summary>
    /// <param name="icons"> Icons list </param>
    /// <param name="index"> Current index </param>
    /// <param name="icon"> Icon to add </param>
    private void AddBallToView(Image[] icons, ref int index, Sprite icon)
    {
        var image = icons[index];
        image.sprite = icon;
        image.color = Color.white;
        index++;
    }
}