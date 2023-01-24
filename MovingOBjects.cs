using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingOBjects : MonoBehaviour
{

    public GameObject Target;
    public float rotation;

    public Transform[] Moveconstraints;
    int currentTarget=0;
    public float speed;
    float damage;

    float OnCollisionEnter(Collision col)
    {if ( col.gameObject.name== Target.name)
        {
            damage= 10;
            Debug.Log(damage);
        }
        return damage;
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, rotation, 0);
        transform.position = Vector3.MoveTowards(transform.position, Moveconstraints[currentTarget].position, speed * Time.deltaTime);

        if (transform.position == Moveconstraints[currentTarget].position)
        {
            currentTarget++;
            currentTarget = currentTarget % Moveconstraints.Length;
        }
    }
}