using UnityEngine;

namespace Balls
{
    public class ColorBall : Ball
    {
        [SerializeField] private bool _isStriped = default;
    
        [SerializeField] private Sprite _icon = default;

        public bool IsStriped => _isStriped;
    
        public Sprite Icon => _icon;
    }
}