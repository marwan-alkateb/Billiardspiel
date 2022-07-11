using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// checks whether a ball collision or movement is taking place.
/// </summary>
public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private bool sleeping;

    /// <summary>
    /// indicates if a collision or movement is taking place.
    /// </summary>
    public bool IsMoving => !sleeping;

    /// <summary>
    /// initialize the rigidbody of the ball.
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// updates the sleeping status the ball's rigidbody
    /// </summary>
    void FixedUpdate()
    {
        sleeping = rb.IsSleeping();
    }
}
