using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionSound : MonoBehaviour
{
    public AudioSource collisionsound;
    public AudioSource ballcollisionwall;
    public AudioSource ballcollisionpocket;

    private const float VelocityDivider = 2.0f;       // 50% of velocity

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "pocket")
        {
            ballcollisionpocket.volume = Mathf.Clamp01(collision.relativeVelocity.magnitude / VelocityDivider*2);
            ballcollisionpocket.Play();
        }

        else if (collision.collider.tag == "cushion")
        {
            ballcollisionwall.volume = Mathf.Clamp01(collision.relativeVelocity.magnitude / VelocityDivider);
            ballcollisionwall.Play();

            //UnityEngine.Debug.Log("collision.relativeVelocity.magnitude : " + collision.relativeVelocity.magnitude);
            //UnityEngine.Debug.Log("ballcollisionwall.volume : " + ballcollisionwall.volume);
        }

        else
        {
            collisionsound.volume = Mathf.Clamp01(collision.relativeVelocity.magnitude / VelocityDivider);
            collisionsound.Play();
        }
    }
}
