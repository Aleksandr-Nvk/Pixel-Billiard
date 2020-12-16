using UnityEngine;
using Balls;
using Views;

namespace FieldGameplay
{
    public class FieldItemsFactory : MonoBehaviour
    {
        [SerializeField] private GameObject _trianglePrefab = default;
        [SerializeField] private Field _fieldPrefab = default;
        [SerializeField] private Cue _cuePrefab = default;

        [SerializeField] private MoveView _moveView = default;

        private GameObject _triangle;
        private Field _field;
        private Cue _cue;

        public void Create(bool deletePrevious = false)
        {
            if (deletePrevious)
                Delete();
            
            _field =  Instantiate(_fieldPrefab.gameObject, transform).GetComponent<Field>();
            _triangle = Instantiate(_trianglePrefab, transform);
            
            var balls = _triangle.GetComponentsInChildren<Ball>();
            
            foreach (var ballComponent in balls)
            {
                if (ballComponent is WhiteBall whiteBall)
                {
                    _cue = Instantiate(_cuePrefab.gameObject, transform).GetComponent<Cue>();
                    _cue.Init(whiteBall, _field);
                    whiteBall.Init(_fieldPrefab);
                }
            }
            
            _field.Init(balls);
            
            var moveManager = new MoveManager(_field, new Player("Nic"), new Player("Alicia"));
            _moveView.Init(moveManager);
        }

        private void Delete()
        {
            Destroy(_triangle);
            Destroy(_field.gameObject);
            Destroy(_cue);
        }
    }
}