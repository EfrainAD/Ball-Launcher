using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private Rigidbody2D ballRigidbody;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchLoc = Touchscreen.current.primaryTouch.position.ReadValue();
            
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchLoc);

            ballRigidbody.isKinematic = true;
            ballRigidbody.position = worldPosition;
        }
        else {
            ballRigidbody.isKinematic = false;
        }
    }
}
