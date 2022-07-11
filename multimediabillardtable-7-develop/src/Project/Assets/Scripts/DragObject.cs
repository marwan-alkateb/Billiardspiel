using Logic.GameLogic;
using System.Collections.Generic;
using Logic.GameLogic.EightBallPool;
using Logic.GameLogic.NineBall;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Class which enables the drag and drop of the balls in the box aside of billiard table.
/// </summary>
public class DragObject : MonoBehaviour
{
    private bool _mouseState;
    private GameObject target;
    public Vector3 screenSpace;
    public Vector3 offset;
    public LayerMask layer;
    public GameObject box;
    private List<GameObject> listOfBalls;

    
    /// <summary>
    /// Function which checks if the right mouse button is clicked. If it is clicked the ball follows the mouse cursor
    /// until the button is let go. 
    /// </summary>
    void Update()
    {
        // checks if right mouse button was clicked
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hitInfo;

            listOfBalls = new List<GameObject>();
            foreach (var rootGameObject in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if (rootGameObject.gameObject.name == "Balls")
                {
                    GetChildRecursive(rootGameObject);

                }
            }

            target = ReturnClickedObject(out hitInfo);
            // target position
            if (target != null)
            {
                _mouseState = true;
                screenSpace = Camera.main.WorldToScreenPoint(target.transform.position);
                offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
            }




        }
        if (Input.GetMouseButtonUp(1))
        {
            _mouseState = false;
            if (target != null)
                UnfreezeBalls(listOfBalls);
        
        }
        if (_mouseState)
        {
            //GameObject of the Table that the ball cant be placed outsite 
            GameObject display = GameObject.Find("SlatePMMA8");
            var rend = display.GetComponent<Renderer>();
            var cornerPositionA = rend.bounds.max;
            var cornerPositionB = rend.bounds.min;
            const float TABLE_OFFSET = 0.1f;
            var maxX = cornerPositionA.x - TABLE_OFFSET;
            var maxY = cornerPositionA.z - TABLE_OFFSET;
            var minX = cornerPositionB.x + TABLE_OFFSET;
            var minY = cornerPositionB.z + TABLE_OFFSET;

            //convert mouse position to usable vector
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
            var postiton = Camera.main.ScreenToWorldPoint(mousePosition);

            //Ball cant be placed outsite of the table
            if (postiton.x >= minX && postiton.z >= minY && postiton.x <= maxX && postiton.z <= maxY)
            {
                //convert the screen mouse position to world point and adjust with offset
                var curPosition = postiton + offset;
                //update the position of the object in the world
                target.transform.position = curPosition;
                target.transform.Translate(Vector3.up * 0.03f, Space.World);
                target.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                target.GetComponent<Rigidbody>().velocity = Vector3.zero;
                target.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }
    }

    /// <summary>
    /// Function which finds the clicked game object and returns it so that we can drag and drop it.
    /// </summary>
    /// <param name="hit">RaycastHit - we check which object is clicked</param>
    /// <returns>The clicked Game Object</returns>
    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {

            if ((GameObject.Find("EightBallLogic") && GameObject.Find("EightBallLogic").activeSelf))
            {
                EightBallPool game = (EightBallPool)GameObject.Find("EightBallLogic").GetComponent(typeof(EightBallPool));

                if (game.GetGameState().GetFreeWhiteBall() && hit.transform.tag == "whiteBall")
                {
                    target = hit.collider.gameObject;

                }
            }

            if (GameObject.Find("NineBallLogic") && GameObject.Find("NineBallLogic").activeSelf)
            {
                NineBall game = (NineBall)GameObject.Find("NineBallLogic").GetComponent(typeof(NineBall)); 

                if (game.GetGameState().GetFreeWhiteBall() && hit.transform.tag == "whiteBall")
                {
                    target = hit.collider.gameObject;

                }
            }

            if (GameObject.Find("TrickshotLogic") && GameObject.Find("TrickshotLogic").activeSelf)
            {
                if (hit.transform.tag == "whiteBall" || hit.transform.tag == "ball")
                {
                    target = hit.collider.gameObject;

                    foreach (GameObject game in listOfBalls)
                    {
                        if (game.name == hit.collider.gameObject.name)
                        {
                            listOfBalls.Remove(game);

                            break;
                        }
                    }
                    FreezeBalls(listOfBalls);

                }





            }
           
        }



        return target;
    }

    /// <summary>
    /// Function which freezes balls from a given list
    /// </summary>
    /// <param name="balls">List<GameObjects> -Contains all existing balls</param>
    private void FreezeBalls(List<GameObject> balls)
    {
        foreach (var ball in balls)
        {
            ball.GetComponent<Rigidbody>().constraints =
                ball.name == "fullWhite" ? RigidbodyConstraints.FreezeRotation : RigidbodyConstraints.FreezeAll;
        }
    }

    /// <summary>
    /// Function which unfreezes balls from a given list
    /// </summary>
    /// <param name="balls">List<GameObjects> -Contains all existing balls</param>
    private void UnfreezeBalls(List<GameObject> balls)
    {
        foreach (var ball in balls)
        {
            ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }

    /// <summary>
    /// Recursive function which generates a list of all childObjects from the given GameObject
    /// </summary>
    /// <param name="obj">GameObjects -ParentObject containing wanted children</param>
    private void GetChildRecursive(GameObject obj)
    {


        foreach (Transform child in obj.transform)
        {
            if (null == child)
                continue;
            listOfBalls.Add(child.gameObject);
            GetChildRecursive(child.gameObject);

        }
    }
}
