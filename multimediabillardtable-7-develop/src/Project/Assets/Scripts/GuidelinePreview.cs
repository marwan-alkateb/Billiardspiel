using System.Collections;
using System.Collections.Generic;
using Logic.GameLogic.Util;
using UnityEngine;

public class GuidelinePreview : MonoBehaviour
{
    private LineRenderer m_lineRenderer;
    public GameObject shotPoint;
    public GameObject whiteBall;
    public GameObject queue;
    public float sphereRadius;

    private CueHandler m_cueHandlerObj;

    void Start()
    {
        whiteBall = GameObject.Find("fullWhite");
        queue = GameObject.Find("Cube.004_Cube.005");
        m_lineRenderer = GetComponent<LineRenderer>();
        m_cueHandlerObj = GetComponent<CueHandler>();

        sphereRadius = whiteBall.GetComponent<SphereCollider>().radius;
        m_lineRenderer.startWidth = sphereRadius * 2;
        m_lineRenderer.endWidth = sphereRadius * 2;
    }

  
    void Update()
    {
        int gameMode = PlayerPrefs.GetInt("game-mode", (int)GameMode.MainMenu);
        if (gameMode == (int)GameMode.MainMenu) return;
        /// <summary>
        /// as long as the balls are not moving colliding objects are detected using SphereCast and the direction vector of the shot point
        /// when a collision is detected the distance between the position of the shot point and the colliding object is calculated
        /// the end position of the LineRenderer is set to the position of the colliding object
        /// </summary>
        if (!m_cueHandlerObj.BallsAreMoving)
        {
            m_lineRenderer.positionCount = 2;

            Vector3 direction = shotPoint.transform.forward;
            Vector3 startingPosition = shotPoint.transform.position;

            m_lineRenderer.SetPosition(0, startingPosition);
            m_lineRenderer.SetPosition(1, direction * 20 + startingPosition);

            RaycastHit hit;

            float distanceLength = 0f;
            if (Physics.SphereCast(startingPosition, sphereRadius, direction, out hit, 100))
            {
                if (hit.collider.CompareTag("ball"))
                {
                    distanceLength = Vector3.Distance(hit.collider.transform.position, startingPosition);
                    m_lineRenderer.SetPosition(1, direction * distanceLength + startingPosition);
                }

                if (hit.collider.CompareTag("border"))
                {
                    distanceLength = Vector3.Distance(startingPosition, hit.point);
                    m_lineRenderer.SetPosition(1, direction * distanceLength + startingPosition);
                }
            }
            m_lineRenderer.material.mainTextureScale = new Vector2(18.5f, 1);
        }
        else
        {
            m_lineRenderer.positionCount = 0;
        }

    }
}
