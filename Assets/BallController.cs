using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class BallController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _reflectionRandomization = .3f;
    

    private Vector2 lastRigidbodyVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        SendBallInRandomDirection();
    }

    private void SendBallInRandomDirection()
    {
        _rigidbody2D.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * _speed;
        lastRigidbodyVelocity = _rigidbody2D.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ResetBall();
        }

        if (_rigidbody2D.velocity.magnitude < .1f)
        {
            ResetBall();
        }
            
    }

    private void ResetBall()
    {
        _rigidbody2D.velocity = Vector3.zero;
        _rigidbody2D.simulated = false;
        _rigidbody2D.transform.position = Vector3.zero;
        _rigidbody2D.simulated = true;
        SendBallInRandomDirection();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var randomOffset = new Vector2(Random.Range(-_reflectionRandomization, _reflectionRandomization), Random.Range(-_reflectionRandomization, _reflectionRandomization));
        var reflectedVector = Vector2.Reflect(lastRigidbodyVelocity, (other.contacts[0].normal + randomOffset).normalized);
        _rigidbody2D.velocity = reflectedVector;
        lastRigidbodyVelocity = _rigidbody2D.velocity;
    }
}
