using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private UIManager _UIManager;
    // Start is called before the first frame update
    void Start()
    {
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_UIManager == null)
        {
            Debug.LogError("UIManager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered with other: "+other);
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.addCoin();
            }

            Destroy(this.gameObject);
        }
        
    }
}
