using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class BoulderPickup : MonoBehaviour
{ 
    HingeJoint hinge;
    public Rigidbody tether;
    public Rigidbody Deposit;
    bool Grabbed;
    float tethertime = 0;

     void Update()
    {
        tethertime += Time.deltaTime;
    }
    void OnCollisionEnter(Collision col)
    {   
       
        if (Grabbed == false && tethertime> 2f)
        {
         
            if (col.gameObject.name == tether.name)
            {
                Grabbed = false;
                hinge = this.gameObject.AddComponent<HingeJoint>();
                hinge.connectedBody = tether;
                Debug.Log("Gotcha");
            }
            else if (col.gameObject.name == Deposit.name)
            {
                tethertime = 0;
                Grabbed = true;
            }
            
        }
      
        
    }
}