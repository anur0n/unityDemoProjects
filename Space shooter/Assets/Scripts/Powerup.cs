using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    // 0 = Triple Shot
    // 1 = Speed
    // 2 = Shield
    [SerializeField]
    private int powerupID;

    [SerializeField]
    private AudioClip _powerupClip;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-11.1f, 11.2f), 8f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -6)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.transform.name);

        if (other.gameObject.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(_powerupClip, transform.position);
                switch (powerupID)
                {
                    case 0:
                        player.ActivateTripleShot();
                        break;
                    case 1:
                        player.ActivateSpeed();
                        break;
                    case 2:
                        player.ActivateShield();
                        break;
                    default:
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
