using System.Collections;
using UnityEngine;


/// <summary> Class which handles the collisions of the billard pool table pockets with other objects.</summary>
public class BalltouchesGround : MonoBehaviour
{
    /// <summary> world coordinates of the box aside. </summary>
    private static Vector3 boxCoordinates;

    private void Start()
    {
        boxCoordinates = GameObject.FindGameObjectWithTag("box").transform.position;
    }


    /// <summary>
    /// Starts the couroutine removePocketedBall which teleports a pocketed ball into the box aside.
    /// </summary>
    /// <param name="ballCollider">Collider - the collider of an object which goes through the pocket.</param>
    private void OnTriggerEnter(Collider ballCollider)
    {
        Debug.Log("Collision Detected! - Ball dropped on the ground!");
        StartCoroutine(TeleportBallToBox(ballCollider));
    }


    /// <summary>
    /// Coroutine which waits one second and then teleports the ball to the box.
    /// It is being waited for a better realistic effect.
    /// </summary>
    /// <param name="groundColision"> Collider - the collider of an object which goes through the pocket.</param>
    /// <returns>IEnumerator - wait time in seconds</returns>
    public IEnumerator TeleportBallToBox(Collider groundCollision)
    {
        yield return new WaitForSeconds(1);
        // If ball drops on the ground, then teleport it to the box.
        if (groundCollision.gameObject.CompareTag("ball"))
        {
            // following coorindates settings are necessary so balls don't accumulate on each other in box
            boxCoordinates.y += 1;
            boxCoordinates.z += Random.Range(-0.1f, 0.1f);
            groundCollision.gameObject.transform.position = boxCoordinates;
        }
        Debug.Log("Ball removed after dropping on the ground!");
    }
}
