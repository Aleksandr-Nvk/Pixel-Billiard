using UnityEngine;

public class WhiteBall : MonoBehaviour, IBall
{
#pragma warning disable 0649
    
    [SerializeField] private BallType _ballType;

    [SerializeField] private Field _field;

    [SerializeField] private Vector3 _respawnPosition;
    
#pragma warning restore
    
    public BallType BallType => _ballType;
    
    private Rigidbody2D _ball;

    private bool _isRolled;

    private void Start()
    {
        _field.OnBallsStopped += _ => { if (_isRolled) Respawn(); };
    }
    
    private void OnValidate()
    {
        _ball = GetComponent<Rigidbody2D>();
    }

    public void Roll()
    {
        _isRolled = true;

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Hits the white ball within physics
    /// </summary>
    /// <param name="force"> Hit force </param>
    /// <param name="forceMode"> Force mode </param>
    public void Hit(Vector3 force, ForceMode2D forceMode)
    {
        _ball.AddForce(force, forceMode);
        _field.CheckBallsMovement();
    }

    /// <summary>
    /// Respawns the white ball if it was rolled
    /// </summary>
    private void Respawn()
    {
        gameObject.SetActive(true);
        transform.position = _respawnPosition;
    }
}