using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    private Camera mainCamera;
    
    [SerializeField] private GameObject ball;
    [SerializeField] private Rigidbody2D pivot;

    private Rigidbody2D currentBallRigidbody;
    private SpringJoint2D currentBallSpringJoint;
    
    [SerializeField] private float delayDuration;
    [SerializeField] private float delayReSpawn;
    private bool isDragging;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        SpawnBall();
    }

    // Update is called once per frame
    void Update() 
    {
        if (currentBallRigidbody == null) {return;}

        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchLoc = Touchscreen.current.primaryTouch.position.ReadValue();
            
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchLoc);

            currentBallRigidbody.position = worldPosition;
            isDragging = true;
            currentBallRigidbody.isKinematic = true;
        }
        else {
            if (isDragging)
            {
                isDragging = false;
                
                LaunchBall();
            }
        }
    }

    private void SpawnBall() 
    {
        GameObject newBall = Instantiate(ball, pivot.position, Quaternion.identity);
        
        currentBallRigidbody = newBall.GetComponent<Rigidbody2D>();
        currentBallSpringJoint = newBall.GetComponent<SpringJoint2D>();
        
        currentBallSpringJoint.connectedBody = pivot;
    }
    private void LaunchBall() 
    {
        currentBallRigidbody.isKinematic = false;
        currentBallRigidbody = null;
        Invoke(nameof(DetatchBall), delayDuration);
        Invoke(nameof(SpawnBall), delayReSpawn);
    }
    private void DetatchBall() 
    {
        currentBallSpringJoint.enabled = false;
        currentBallSpringJoint = null;
    }
}
