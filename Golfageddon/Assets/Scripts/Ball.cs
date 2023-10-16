using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
    private float lastCollisionTime = 0.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HittableObject"))
        { 
            int multiplier = 1;
            if (lastCollisionTime == 0.0f)
            {
                lastCollisionTime = Time.time;
            }
            else
            {
                float timeSinceLastCollision = Time.time - lastCollisionTime;
                Debug.Log(timeSinceLastCollision);
                if (timeSinceLastCollision <= 1.0f)
                {
                    multiplier += 1;
                }
            }
            int points = multiplier * collision.gameObject.GetComponent<HittableObject>().PointValue;
            // GameManager.Instance.IncrementScore(points); 
    
        }
    }
}
