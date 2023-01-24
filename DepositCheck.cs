using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositCheck : MonoBehaviour
{
   public static  bool isWon;
    public Rigidbody LightObject;
    public Rigidbody MediumObject;
    public Rigidbody Bigobject;
    public bool Lightdeposit;
    public bool Mediumeposit;
    public bool BigDeposit;
    void OnCollisionEnter(Collision col)
    {
        
        if (col.gameObject.name == LightObject.name)
        {
            Destroy(LightObject.GetComponent<HingeJoint>());
            Lightdeposit = true;
        }
        if (col.gameObject.name == MediumObject.name)
        {
            Destroy(MediumObject.GetComponent<HingeJoint>());
            Mediumeposit = true;
        }
        if (col.gameObject.name == Bigobject.name)
        {
            Destroy(Bigobject.GetComponent<HingeJoint>());
            BigDeposit = true;
        }
    }

    private void Update()
    {
        if (Lightdeposit ==true && Mediumeposit ==true && BigDeposit== true)
        {
            isWon = true; 
            Debug.Log("You Win !!!!");
        }
    }
}

