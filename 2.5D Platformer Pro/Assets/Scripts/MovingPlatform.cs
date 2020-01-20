using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private GameObject _pointA, _pointB;
    private GameObject target;

    [SerializeField]
    private float _speed = 2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == _pointA.transform.position)
        {
            target = _pointB;
        } else if (transform.position == _pointB.transform.position)
        {
            target = _pointA;
        }
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * _speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Break();
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //other.transform.parent = null;
        }
    }
}
