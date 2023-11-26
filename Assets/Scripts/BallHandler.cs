using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private Rigidbody2D ballRigidbody;
    [SerializeField] private SpringJoint2D ballSpringJoint;
    [SerializeField] private float delayDuration;
    private bool isDragging;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (ballRigidbody == null) {return;}

        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchLoc = Touchscreen.current.primaryTouch.position.ReadValue();
            
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchLoc);

            ballRigidbody.position = worldPosition;
            isDragging = true;
            ballRigidbody.isKinematic = true;
        }
        else {
            if (isDragging)
            {
                isDragging = false;
                
                LaunchBall();
            }
        }
    }

    private void LaunchBall() 
    {
        ballRigidbody.isKinematic = false;
        ballRigidbody = null;
        Invoke(nameof(DetatchBall), delayDuration);
    }
    private void DetatchBall() 
    {
        ballSpringJoint.enabled = false;
        ballSpringJoint = null;
    }
}
