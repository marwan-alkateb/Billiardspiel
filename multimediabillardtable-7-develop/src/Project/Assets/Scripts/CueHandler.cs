using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Class which handles the two Cue states(visible and invisible) and its animation in order to charge the cue.
/// </summary>
public class CueHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _aimLayer;
    [SerializeField] [Range(0.1f, 1f)] private float _chargeAmount;
    [SerializeField] private Rigidbody rigidbodyWhiteBall;

    private List<Ball> _balls = new List<Ball>();
    public GameObject Cue;
    private TouchControl _touchControl;
    private float _charge;
    private Animator _animator;

    public AudioSource cueshotsound;
    public Vector3 CurrentPosition;
    public bool ClickInProgress = false;
    public bool SaveCursorPosition = true;
    public bool BallsAreMoving => _balls.Any(x => x.IsMoving);



    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _touchControl = rigidbodyWhiteBall.GetComponent<TouchControl>();
    }

    private void Start()
    {
        var balls = FindObjectsOfType<Ball>();
        foreach (var ball in balls)
            _balls.Add(ball);

    }

    /// <summary>
    /// Function which handles visibility and transformation of the cue.
    /// Checks if the balls are moving. If balls are moving then user cannot use cue to make another shot until they stop.
    /// </summary>
    public void Update()
    {
        var ballsAreMoving = BallsAreMoving;
		if (ballsAreMoving)
		{
			return;
		}

        // checks if left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            //Avoid shooting of the whiteball when UI buttons are clicked.
            //Detects the UI click

            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (EventSystem.current.currentSelectedGameObject != null
                    && EventSystem.current.currentSelectedGameObject.CompareTag("ball") != true)
                {
                    return;
                }
            }

            ClickInProgress = true;
            if (SaveCursorPosition)
            {
                CurrentPosition = Input.mousePosition;
                SaveCursorPosition = false;

            }

            UnityEngine.Debug.Log("Mouse button down");

            StartCoroutine(ChargeShot());

        }

        if (Input.GetMouseButtonUp(0))
        {
            UnityEngine.Debug.Log("Mouse button up");
            SaveCursorPosition = true;
        }

        if (ClickInProgress)
        {
            // draws a line from the camera to the current position of the mouse cursor
            var ray = Camera.main.ScreenPointToRay(CurrentPosition);
            RaycastHit hit;

            // if the ray hit a collider after casting, save it to variable hit
            if (!Physics.Raycast(ray, out hit, _aimLayer))
            {
                return;
            }

            // get normalized vector based off distance and direction from white ball to cursor-based hit
            var hitPoint = new Vector3(hit.point.x, 0.9f, hit.point.z);
            var output = (hitPoint - transform.position).normalized;

            // set cue rotation
            transform.rotation = Quaternion.LookRotation(new Vector3(output.x, 0, output.z));
        } 
        else  
        {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (!Physics.Raycast(ray, out hit, _aimLayer))
                {
                    return;
                }

                // set cue position to position of white ball
                transform.position = rigidbodyWhiteBall.transform.position;
                var hitPoint = new Vector3(hit.point.x, 0.9f, hit.point.z);
                var output = (hitPoint - transform.position).normalized;
            
            // set cue rotation
            transform.rotation = Quaternion.LookRotation(new Vector3(output.x, 0, output.z));
        }
    }

    /// <summary>
    /// Handles the animation of cue.
    /// When LMB is pressed, cue is animated in such a way that it moves away from white ball
    /// When when the LMB is released, the cue moves forward in the direction of white ball to charge it.
    /// </summary>

    private IEnumerator ChargeShot()
    {
        while (!Input.GetMouseButtonUp(0))
        {
            _charge += _chargeAmount * Time.deltaTime * 3;
            if (_charge > 10f)
                _charge = 10f;

            _animator.SetFloat("Charge", _charge);
            yield return null;
        }

        cueshotsound.volume = 1f;
        cueshotsound.Play();

        var force = _charge;
        _animator.SetTrigger("Shoot");

        // Charge the cue on the position of mouse-click
        _touchControl.Shoot(force, CurrentPosition);
        _charge = 0;
        _animator.SetFloat("Charge", _charge);

        ClickInProgress = false;
    }
}
