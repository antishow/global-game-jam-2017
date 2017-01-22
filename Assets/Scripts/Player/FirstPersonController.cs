using UnityEngine;
using System.Collections;
 
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]
 
public class FirstPersonController : MonoBehaviour {
 
    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    private bool grounded = false;
 
    //Camera Look
    public Camera camera;
    public float verticalLook;
    public bool inverted = false;
    public float mouseSpeedX = 10;
    public float mouseSpeedY = 10;
    public float playerVerticalLookAngle = 60;
     
    void Awake () {
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().useGravity = false;
    }
 
    void FixedUpdate () {
        if (grounded) {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;
 
            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);
 
            // Jump
            if (canJump && Input.GetButton("Jump")) {
                GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }
        } else {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;
 
            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange/2, maxVelocityChange/2)/2;
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange/2, maxVelocityChange/2)/2;
            velocityChange.y = 0;
            GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);
        }
 
        Vector3 turnAxis = new Vector3();
        if(Input.GetAxis("Mouse X") != 0){
            turnAxis.x = Input.GetAxis("Mouse X") * mouseSpeedX;
        }
        this.transform.Rotate(0, turnAxis.x, 0);
 
 
        if(Input.GetAxis("Mouse Y") != 0){
            turnAxis.y = Input.GetAxis("Mouse Y");
        }
        if(inverted){
            verticalLook += turnAxis.y * mouseSpeedY;
        } else {
            verticalLook -= turnAxis.y * mouseSpeedY;
        }
        verticalLook = Mathf.Clamp(verticalLook, -playerVerticalLookAngle, playerVerticalLookAngle);
 
        float look = 360+verticalLook;
        this.camera.transform.eulerAngles = this.transform.eulerAngles + new Vector3(look, 0, 0);
 
        // We apply gravity manually for more tuning control
        GetComponent<Rigidbody>().AddForce(new Vector3 (0, -gravity * GetComponent<Rigidbody>().mass, 0));
 
        grounded = false;
    }
 
    void OnCollisionStay () {
        grounded = true;
    }
 
    float CalculateJumpVerticalSpeed () {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
}