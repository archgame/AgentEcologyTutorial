using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log("Controller");
        if (Input.GetButton("North"))
        {
            Debug.Log("North");
        }
        if (Input.GetButtonDown("South"))
        {
            Debug.Log("South");
        }

        Vector3 leftJoystick = new Vector3(Input.GetAxis("LeftJoyX"), Input.GetAxis("LeftJoyY"), 0);
        Vector3 rightJoystick = new Vector3(Input.GetAxis("RightJoyX"), Input.GetAxis("RightJoyY"), 0);
        Debug.Log(Input.GetAxis("RightJoyX"));
    }
}