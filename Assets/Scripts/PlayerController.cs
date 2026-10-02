using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue;
    public float speed; // This is your max engine force multiplier
    private int count;
    private int numPickups = 3;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    
    // New UI text elements to display movement data
    public TextMeshProUGUI positionText;
    public TextMeshProUGUI velocityText;

    // Variable to cache the previous frame's location
    private Vector3 lastPosition;

    void Start()
    {
        count = 0;
        winText.gameObject.SetActive(false);
        SetCountText();

        // Initialize the old position at the very beginning
        lastPosition = transform.position;
    }

    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);
        GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime);
    }

    // Update runs once per frame (ideal for frame-dependent position changes)
    void Update()
    {
        // 1. Update the position UI
        positionText.text = "Position: " + transform.position.ToString("F2");

        // 2. Calculate the velocity vector: (Current Position - Last Position) / Time Elapsed
        Vector3 displacement = transform.position - lastPosition;
        Vector3 calculatedVelocity = displacement / Time.deltaTime;

        // 3. Convert the velocity vector into a human-readable scalar speed
        float scalarSpeed = calculatedVelocity.magnitude;

        // 4. Update the UI text strings
        velocityText.text = "Velocity: " + calculatedVelocity.ToString("F2") + 
                             "\nSpeed: " + scalarSpeed.ToString("F2") + " units/s";

        // 5. CRITICAL STEP: Save the current position as the 'old' position for the next frame
        lastPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    private void SetCountText() 
    {
        scoreText.text = "Score: " + count.ToString();
        if (count >= numPickups) 
        {
            winText.gameObject.SetActive(true);
        }
    }  
}
