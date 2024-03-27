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
        StartCoroutine(SendBallInRandomDirection());
    }

    private IEnumerator SendBallInRandomDirection()
    {
        var randomX = Random.Range(-1f, 1f);
        while (randomX is -1 or 0 or 1)
        {
            randomX  = Random.Range(-1f, 1f);
            yield return null;
        }
        
        var randomY = Random.Range(-1f, 1f);
        while (randomY is -1 or 0 or 1)
        {
            randomY  = Random.Range(-1f, 1f);
            yield return null;
        }
        
        _rigidbody2D.velocity = new Vector2(randomX, randomY).normalized * _speed;
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
        StartCoroutine(SendBallInRandomDirection());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var randomOffset = new Vector2(Random.Range(_reflectionRandomization, 0), Random.Range(_reflectionRandomization, 0));
        var reflectedVector = Vector2.Reflect(lastRigidbodyVelocity, (other.contacts[0].normal + randomOffset).normalized);
        _rigidbody2D.velocity = reflectedVector;
        lastRigidbodyVelocity = _rigidbody2D.velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.position.x > 0)
            GameManager.LeftPlayerScore++;
        if (transform.position.x < 0)
            GameManager.RightPlayerScore++;
        GameManager.ScoreUpdated?.Invoke(null, EventArgs.Empty);
        ResetBall();
    }
}
