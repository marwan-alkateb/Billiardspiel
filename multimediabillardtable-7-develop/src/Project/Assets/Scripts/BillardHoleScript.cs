using System.Collections;
using UnityEngine;

/// <summary> This Class handles the collisions of the billard pool table pockets with other objects.</summary>
public class BillardHoleScript : MonoBehaviour
{
    /// <summary> world coordinates of the box aside. </summary>
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
        Debug.Log("Collision Detected! - Ball dropped into a pocket!");
        StartCoroutine(TeleportBallToBox(ballCollider));
    }



    /// <summary>
    /// Coroutine waits one second and then teleports the ball to the box.
    /// Purpose: a better realistic effect.
    /// </summary>
    /// <param name="ballCollider"> Collider - the collider of an object which goes through the pocket.</param>
    /// <returns>IEnumerator - wait time in seconds</returns>
    private IEnumerator TeleportBallToBox(Collider ballCollider)
    {
        yield return new WaitForSeconds(1);
        // If ball goes into a pocket, then teleport it to the box.
        if (ballCollider.gameObject.CompareTag("ball"))
        {
            // following coorindates settings are necessary so balls don't accumulate on each other in box
            boxCoordinates.y += 1;
            boxCoordinates.z += Random.Range(-0.1f, 0.1f);
            ballCollider.gameObject.transform.position = boxCoordinates;
        }
        Debug.Log("Ball removed after dropping into a pocket!");
    }
}
