using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMove : MonoBehaviour
{
    public DynamicJoystick dynamicJoystick;
    private float speed;
    private float turnSpeed;
    private float horizontal;
    private float vertical;
    private Vector3 addPos;
    private Vector3 direction;
    [SerializeField] Rigidbody rb;
    //bounding for game area
    private float boundXMax=29;
    private float boundXMin=-7;
    private float boundZMax=0;
    private float boundZMin=-55;
    [SerializeField] private playerScriptable playerScriptable;
    public bool gamePlayable ;
    public void GameStarting()
    {
        gamePlayable = true;
    }
    private void Start()
    {
       
        speed = playerScriptable.speed;
        turnSpeed = playerScriptable.turnSpeed;
        GetComponent<Renderer>().material.color = playerScriptable.playerColor;
    }
    private void FixedUpdate()
    {

        Movement();


    }
    private void Update()
    {

        Rotation();

    }

    public void Movement()
    {
       
        
        if (Input.GetButton("Fire1")&&gamePlayable)
        { 

            addPos = new Vector3(horizontal * speed * Time.fixedDeltaTime, 0, vertical * speed * Time.fixedDeltaTime);
            Debug.Log(addPos);
            rb.MovePosition(new Vector3(Mathf.Clamp(transform.position.x + addPos.x, boundXMin, boundXMax), 0, Mathf.Clamp(transform.position.z + addPos.z, boundZMin, boundZMax)));
        }




    }

    private void Rotation()
    {
        horizontal = dynamicJoystick.Horizontal;
        vertical = dynamicJoystick.Vertical;
        if (Input.GetButton("Fire1")&& gamePlayable)

        {
            direction = Vector3.forward * vertical + Vector3.right * horizontal;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        }
    }
}

