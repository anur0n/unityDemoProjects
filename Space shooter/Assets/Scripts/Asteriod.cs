using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteriod : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 19f;

    [SerializeField]
    private GameObject _explosionPrefab;

    private SpawnManager _spawnMgr;
    
    // Start is called before the first frame update
    void Start()
    {
        _spawnMgr = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnMgr == null)
        {
            Debug.LogError("Spawn Manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.transform.name);

        if (other.gameObject.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _rotationSpeed = 0;
            _spawnMgr.startSpawning();
            Destroy(this.gameObject, 0.5f);
        }
        else if (other.gameObject.tag == "Laser")
        {
            _rotationSpeed = 0;
            GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            //transform.localScale(new Vector3(0, 0, 0),);
            
            Destroy(other.gameObject);
            _spawnMgr.startSpawning();
            Destroy(this.gameObject, 0.5f);
        }
    }
}
