using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class which restarts the scene ( original position of the balls ) .
/// </summary>
public class ResetBalls : MonoBehaviour
{
    /// <summary>
    /// Spawn position of the balls.
    /// </summary>
    Dictionary<string, Vector3> _ballsDefaultPosition = new Dictionary<string, Vector3>();
    Dictionary<string, Vector3> _9ballsDefaultPosition = new Dictionary<string, Vector3>();


    private void Start()
    {
        // Save the spawn position of the ball before the game starts.
        foreach (Transform ball in transform)
        {
            _ballsDefaultPosition.Add(ball.name, ball.transform.position);
        }


        _9ballsDefaultPosition.Add("fullWhite", new Vector3(-0.395f, -0.03140962f, -0.024f));

        _9ballsDefaultPosition.Add("fullYellow", new Vector3(0.597f, 0.94f, -0.015f));

        _9ballsDefaultPosition.Add("fullBlue", new Vector3(0.6655f, 0.94f, -0.0505f));

        _9ballsDefaultPosition.Add("fullRed", new Vector3(0.6655f, 0.94f, 0.0205f));

        _9ballsDefaultPosition.Add("fullPurple", new Vector3(0.728f, 0.94f, 0.056f));

        _9ballsDefaultPosition.Add("halfYellow", new Vector3(0.728f, 0.94f, -0.015f));

        _9ballsDefaultPosition.Add("fullOrange", new Vector3(0.728f, 0.94f, -0.086f));

        _9ballsDefaultPosition.Add("fullGreen", new Vector3(0.791f, 0.94f, -0.0505f));

        _9ballsDefaultPosition.Add("fullBrown", new Vector3(0.791f, 0.94f, 0.0205f));

        _9ballsDefaultPosition.Add("fullBlack", new Vector3(0.859f, 0.94f, -0.015f));


        // Ball not needed
        _9ballsDefaultPosition.Add("halfBlue", new Vector3(-1.75f, 0.75f, -0.5f));

        _9ballsDefaultPosition.Add("halfRed", new Vector3(-1.75f, 0.75f, -0.4f));

        _9ballsDefaultPosition.Add("halfPurple", new Vector3(-1.75f, 0.75f, -0.3f));

        _9ballsDefaultPosition.Add("halfOrange", new Vector3(-1.75f, 0.75f, -0.2f));

        _9ballsDefaultPosition.Add("halfGreen", new Vector3(-1.75f, 0.75f, -0.1f));

        _9ballsDefaultPosition.Add("halfBrown", new Vector3(-1.75f, 0.75f, 0.0f));


    }
    /// <summary>
    /// Restarts the scene when you click spacebar.
    /// </summary>
    void Update()
    {
        //old verison of resetting the ball
        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }

    /// <summary>
    /// Resets the current position of the balls to the spawn position.
    /// </summary>
    public void Reset(string logicname)
    {
        if(logicname == "EightBallLogic" || logicname == "TrickshotLogic")
        {
            GameModePosition(_ballsDefaultPosition);
        }else if(logicname == "NineBallLogic")
        {
            GameModePosition(_9ballsDefaultPosition);
        }



    }

    public void GameModePosition(Dictionary<string, Vector3>  _ballsDefaultPosition)
    {
        foreach (var ball in _ballsDefaultPosition)
        {
            GameObject.Find(ball.Key).GetComponent<Rigidbody>().velocity = Vector3.zero;
            GameObject.Find(ball.Key).GetComponent<Rigidbody>().angularVelocity = Vector3.zero;


            GameObject.Find(ball.Key).transform.position = ball.Value;
            GameObject.Find(ball.Key).transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));


        }
    }
}
