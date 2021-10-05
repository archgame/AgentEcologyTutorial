using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float Speed = 3.0f;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //move guard statement
        Vector3 joystick = new Vector3(Input.GetAxis("LeftJoyX"), 0, -Input.GetAxis("LeftJoyY"));
        //Debug.Log(joystick.magnitude);
        //Debug.Log(joystick.z);
        if (joystick.magnitude < 0.3) { return; }

        //camera vectors
        Vector3 camForward = Camera.main.transform.forward;
        Debug.DrawRay(transform.position, camForward * 3, Color.yellow);
        Vector3 projForward = Vector3.ProjectOnPlane(camForward, Vector3.up).normalized;
        Debug.DrawRay(transform.position, projForward * 3, Color.green);
        Vector3 camRight = Camera.main.transform.right;
        Debug.DrawRay(transform.position, camRight * 3, Color.cyan);

        //apply movement input
        Vector3 move = camRight * joystick.x + projForward * joystick.z;// +
        //Debug.Log(move);
        transform.Translate(move.normalized * Time.deltaTime * Speed);
        Debug.DrawRay(transform.position, move * 3, Color.red);
    }
}