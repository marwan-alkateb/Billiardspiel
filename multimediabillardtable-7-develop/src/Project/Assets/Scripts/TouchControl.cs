using UnityEngine;
/// <summary>
/// Class which controls the movement of the white ball, the shot strength and speed.
/// </summary>
public class TouchControl : MonoBehaviour
{

    Rigidbody rb;
    public float speed = 1200f;
    public float speedMax = 2000f;
    float touchStartTime = 0f;
    public LayerMask Layer;
    private int shootCount = 0;

    /// <summary>
    /// Initializes the rigidbody of the white ball. 
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    /// <summary>
    /// The update looks if the left mouse button is clicked and starts to count how long
    /// the button is clicked. Then it multiplies that time with the speed. 
    /// When the ball falls of the table it is teleported to it's start position.
    /// </summary>
    void Update()
    {
        // if its out of the table -> teleport
        if (rb.transform.position.y < 0.6f)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            rb.transform.position = new Vector3(-0.762f, 1.096f, -0.027f);
        }
    }


	public void Shoot(float force, Vector3 currentPosition)
    {
		// draws a line from the camera to the current position of the mouse cursor
		var ray = Camera.main.ScreenPointToRay(currentPosition);

		if (!Physics.Raycast(ray, out var hitInfo, 100, Layer))
				return;
        
        rb.AddForce((hitInfo.point - rb.position).normalized * (speedMax * force * 1.2f));
       
        incrementShootCount();
    }

    /// <summary>
    ///  Increments shootCount, this infomation essentially needed for the trickshot mode 
    /// </summary>
    public void incrementShootCount()
    {
        shootCount++;
        UnityEngine.Debug.Log("Shoot Count: " + shootCount);

    }

    public int getShootCount()
    {
        return shootCount;
    }

    public void resetShootCount()
    {
        shootCount = 0;
    }
}
