using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMovement : MonoBehaviour
{

    public float Max;
    
    public float Acceleration;
    public float Gravity;
    public static float time=0;
    float Speed;
    void Start()
    {
    }

    void Update()
    {
       this.transform.position += new Vector3  (0f,-Gravity, 0f) * Time.deltaTime;
        transform.position += transform.up * Time.deltaTime * Speed;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-(Time.deltaTime * Speed), 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(Time.deltaTime * Speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-(Time.deltaTime * Speed), 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(Time.deltaTime * Speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
            {
                time += Time.deltaTime;
                if (Speed < Max)
                {
                    Speed += Acceleration;
                    time += Time.deltaTime;
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                time += Time.deltaTime;
                if (Speed < Max)
                {
                    Speed += Acceleration;
                }
            }
            else
            {
                if (Speed > 0)
                {
                    Speed -= Acceleration;
                }
                if (Speed < 0)
                {
                    Speed = 0;
                }
            }



       


        
    }

}