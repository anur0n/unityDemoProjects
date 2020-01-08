 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private GameObject _leftEngineFirePrefab;

    [SerializeField]
    private GameObject _rightEngineFirePrefab;

    [SerializeField]
    private float _fireRate = 0.5f;

    [SerializeField]
    private AudioClip _laserShotClip;
    
    [SerializeField]
    private AudioSource _audioSource;

    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnMgr;
    private UIManager _uiMgr;

    [SerializeField]
    private int _score = 0;

    [SerializeField]
    private GameObject _explosionPrefab;

    private bool _isTripleShotActive = false;

    private bool _isSpeedActive = false;

    private bool _isShieldActive = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnMgr = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if(_spawnMgr == null)
        {
            Debug.LogError("Spawn Mgr is NULL");
        }

        _uiMgr = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiMgr == null)
        {
            Debug.LogError("UI Mgr is NULL");
        }
        else
        {
            _uiMgr.onScoreUpdate(_score);
            _uiMgr.onLivesUpdate(_lives);
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio Source is null");
        }
        else
        {
            _audioSource.clip = _laserShotClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if(Input.GetKeyDown(KeyCode.Space) && Time.time>_canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        if (_isSpeedActive)
        {
            Debug.Log("Speed boost active");
            transform.Translate(direction * _speed * _speedMultiplier * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 4f), 0);

        if (transform.position.x >= 11.2)
        {
            transform.position = new Vector3(-11.1f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -11.1)
        {
            transform.position = new Vector3(11.2f, transform.position.y, transform.position.z);
        }
    }
    
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(-0.185f, .24f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, .98f, 0), Quaternion.identity);
        }

        _audioSource.Play();

    }

    public void Damage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            updateShield(_isShieldActive);
            return;
        }
        _lives--;
        _uiMgr.onLivesUpdate(_lives);

        if (_lives == 2)
        {
            _rightEngineFirePrefab.SetActive(true);
        }

        if (_lives == 1)
        {
            _leftEngineFirePrefab.SetActive(true);
        }

        if (_lives < 1)
        {
            _spawnMgr.onPlayerDeath();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.5f);
        }
    }

    public void ActivateTripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }

    public void ActivateSpeed()
    {
        _isSpeedActive = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isSpeedActive = false;
    }

    public void ActivateShield()
    {
        _isShieldActive = true;
        updateShield(_isShieldActive);
        //StartCoroutine(ShieldPowerDownRoutine());
    }

    private void updateShield(bool active)
    {
        _shieldVisualizer.SetActive(active);
    }
    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isShieldActive = false;
        updateShield(_isShieldActive);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiMgr.onScoreUpdate(_score);
    }

}
