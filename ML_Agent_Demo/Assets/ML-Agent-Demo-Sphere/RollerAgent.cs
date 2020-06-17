#define UNITY_EDITOR
#define UNITY_EDITOR_OSX

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using UnityEditor;



public class RollerAgent : Agent
{
    Rigidbody rigidBody;

    public Transform target;

    public Transform danger;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }


    public override void AgentReset()
    {
        if (this.transform.position.y < 0)
        {
            rigidBody.angularVelocity = Vector3.zero;
            rigidBody.velocity = Vector3.zero;
            rigidBody.transform.position = new Vector3(0, 1.5f, 0);
        }
        target.position = new Vector3(Random.value * 70 - 35, 1.5f, Random.value * 70 - 35);
    }

    public override void CollectObservations()
    {
        AddVectorObs(target.position);
        AddVectorObs(this.transform.position);
        AddVectorObs(danger.position);

        AddVectorObs(rigidBody.velocity.x);
        AddVectorObs(rigidBody.velocity.z);

    }


    public float speed = 10;
    public override void AgentAction(float[] vectorAction)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        rigidBody.AddForce(controlSignal * speed);
        AddReward(-.01f);

        float distanceToTarget = Vector3.Distance(this.transform.position, target.position);

        float distanceToDanger = Vector3.Distance(this.transform.position, danger.position);

        if (distanceToDanger < 4.5f)
        {
            SetReward(-1.0f);
            //Done();
        }


        if (distanceToTarget < 4f)
        {
            SetReward(20.0f);
            Done();
        }

        if (this.transform.position.y < 0)
        {
            SetReward(-1.0f);
            Done();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "wall")
        {
            Light halo = (other.gameObject.GetComponent<Light>());
            halo.intensity = 1.5f;
            AddReward(-1f);
        }

        if (other.gameObject.tag == "danger" || other.gameObject.tag == "target")
        {
            Light halo = (other.gameObject.GetComponent<Light>());
            halo.intensity = 1.5f;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "danger" || other.gameObject.tag == "target" || other.gameObject.tag == "wall")
        {
            Light halo = (other.gameObject.GetComponent<Light>());
            halo.intensity = 0f;
        }
    }

    //IEnumerator DisableHalo(float time, ref Collision other)
    //{
    //    yield return new WaitForSeconds(time);

    //    SerializedObject halo = new SerializedObject(other.gameObject.GetComponent("Halo"));
    //    halo.FindProperty("m_Size").floatValue = 0f;
    //    halo.ApplyModifiedProperties();
    //}

    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }
}
