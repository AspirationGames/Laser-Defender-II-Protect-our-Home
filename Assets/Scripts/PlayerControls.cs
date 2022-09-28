using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float movementMaximumX = 16f;
    [SerializeField] float movementMaximumY = 9f;
    [SerializeField] float movementMinimumX = -16f;
    [SerializeField] float movementMinimumY = -4f;

    [Header("Rotation")]
    [Tooltip("Rotation Intensity")]
    [SerializeField] float positionPitchFactor = 2f;
    [SerializeField] float controlPitchFactor = 2f;

    [SerializeField] float positionYawFactor = 2f;

    [SerializeField] float controlRollFactor = 2f;

    [Header("Shooting")]

    [SerializeField] ParticleSystem[] playerLasers;

    Vector2 playerInput;

    bool isFiring;

    
    
    void Awake() 
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        ProcessMovement();
        ProcessRotation();
        ProcessFiring();
        
    }

    void OnMove(InputValue value)
    {
        playerInput = value.Get<Vector2>();
    }

    void ProcessMovement()
    {
        float offSetX = playerInput.x * moveSpeed * Time.deltaTime;
        float offSetY = playerInput.y * moveSpeed * Time.deltaTime;
        float newPositionX = transform.localPosition.x + offSetX;
        float newPositionY = transform.localPosition.y + offSetY;

        transform.localPosition = new Vector3 
        (Mathf.Clamp(newPositionX,movementMinimumX,movementMaximumX),
        Mathf.Clamp(newPositionY,movementMinimumY,movementMaximumY), 
        transform.localPosition.z);

    }

    void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + playerInput.y*controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = playerInput.x * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

    }

    void OnFire(InputValue value)
    {
        isFiring = value.isPressed;

    }

    void ProcessFiring()
    {
    
        foreach(ParticleSystem laser in playerLasers)
        {
            
            var laserEmission = laser.emission;
            laserEmission.enabled = isFiring; 
            //When you use ParticleSystem.Stop() it waits for all of the emitted particles to disappear before stopping the particle system; because of this, ParticleSystem.Play() won't work until all of your particles have disappeared.
            //if your laser particles had a lifetime of 10 seconds, so after releasing the fire button, you would have to wait 10 seconds before I could fire again
        }
             

    }
}
