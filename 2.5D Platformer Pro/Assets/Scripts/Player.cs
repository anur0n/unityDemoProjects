using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    
    [SerializeField]
    private float _speed = 4f;

    [SerializeField]
    private float _gravity = 1f;

    [SerializeField]
    private float _jumpHeight = 15f;

    private float _yVelocity;

    private bool _canDoubleJump = false;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horiontalMove = Input.GetAxis("Horizontal");
        Debug.Log("Horizontal: " + horiontalMove);
        Vector3 direction = new Vector3(horiontalMove, 0, 0);
        Vector3 velocity = direction * _speed;
        if (_controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canDoubleJump)
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = false;
            }
            _yVelocity -= _gravity;
        }
        velocity.y = _yVelocity;
        //transform.Translate(direction * _speed * Time.deltaTime);
        _controller.Move(velocity * Time.deltaTime);
    }
}
