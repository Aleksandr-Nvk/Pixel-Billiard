using System.Collections.Generic;
using UnityEngine;
using System;

public class WhiteBall : MonoBehaviour, IBall
{
    [SerializeField] private Field _field = default;
    
    [SerializeField] private Rigidbody2D _ball = default;

    [SerializeField] private Vector3 _respawnPosition = default;

    private bool _isRolled;

    public Action OnRespawned;
    
    private Action<List<IBall>> _onBallsStopped;

    private void Start()
    {
        _onBallsStopped = _field.OnBallsStopped += _ => { if (_isRolled) Respawn(); };
    }

    private void OnDestroy()
    {
        _field.OnBallsStopped -= _onBallsStopped;
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
        _isRolled = false;
        
        OnRespawned?.Invoke();
    }
}