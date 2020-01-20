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

    [SerializeField]
    private int _coins = 0;

    private UIManager _UIManager;
    // Start is called before the first frame update
    void Start()
    {
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_UIManager == null)
        {
            Debug.LogError("UIManager is null");
        }
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horiontalMove = Input.GetAxis("Horizontal");
        //Debug.Log("Horizontal: " + horiontalMove);
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
        if (horiontalMove != 0)
        {
            _controller.Move(velocity * Time.deltaTime);
        }
        //Debug.Log(velocity);
    }

    public void addCoin()
    {
        _coins++;
        _UIManager.updateCoin(_coins);
    }
}
