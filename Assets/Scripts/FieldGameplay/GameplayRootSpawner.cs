using UnityEngine;
using Balls;

namespace FieldGameplay
{
    public class GameplayRootSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _trianglePrefab = default;
        [SerializeField] private GameObject _fieldPrefab = default;
        [SerializeField] private GameObject _cuePrefab = default;

        public void Init(out Field field)
        {
            field = Instantiate(_fieldPrefab, transform).GetComponent<Field>();
            var cue = Instantiate(_cuePrefab, transform).GetComponentInChildren<Cue>();
            
            var triangle = Instantiate(_trianglePrefab, transform);
            var balls = triangle.GetComponentsInChildren<Ball>();

            foreach (var ball in balls)
            {
                if (ball is WhiteBall whiteBall)
                {
                    cue.Init(whiteBall, field, cue.gameObject.transform.parent.gameObject);
                    whiteBall.Init(field);
                }
                
                field.AddBall(ball);
            }
            gameObject.SetActive(false);
        }
    }
}