using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    [SerializeField]
    private AudioClip _explosionClip;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private GameObject _laserPrefab;

    private float _fireRate = 1;
    private float _canFire = -1;
    private Player _player;

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is null");
        }

        transform.position = new Vector3(Random.Range(-11.1f, 11.2f), 8f, 0);
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator is null");
        }

        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("Enemy Audio source is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;

            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);

            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for(int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemy();
            }
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -6)
        {
            transform.position = new Vector3(Random.Range(-11.1f, 11.2f), 8f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.transform.name);

        if(other.gameObject.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            _animator.SetTrigger("onEnemyDeath");
            _speed = 0;
            Destroy(GetComponent<Collider2D>());
            _audioSource.Play(); Destroy(this.gameObject, 2.8f);
        } else if(other.gameObject.tag == "Laser")
        {
            if (_player != null)
            {
                _player.AddScore(10);
            }
            _animator.SetTrigger("onEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(other.gameObject);
            Destroy(this.gameObject, 2.8f);
            
        }
    }
}
