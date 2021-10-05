using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    public float Speed = 10.0f;

    public LayerMask SelectMask;
    public LayerMask PlaceMask;

    private RectTransform rect;

    // Start is called before the first frame update
    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private bool _isRelocating = false;
    private Vector3 _lastPosition = Vector3.zero;
    private GameObject _selectedFactory;

    // Update is called once per frame
    private void Update()
    {
        //check for clicks, do this after you've set up the get input
        Ray ray = Camera.main.ScreenPointToRay(rect.position);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.black);

        if (_isRelocating)
        {
            Debug.Log("Relocation");
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, PlaceMask))
            {
                Debug.Log("Terrain");
                _selectedFactory.transform.position = hit.point;
                if (Input.GetButtonDown("South"))
                {
                    Debug.Log("Placed");
                    Factory factory = _selectedFactory.GetComponent<Factory>();
                    factory.enabled = true;
                    _isRelocating = false;
                }
            }
        }
        else
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, SelectMask))
            {
                //Debug.Log("Factory");
                //Debug.Log(hit.collider.gameObject.name);
                if (Input.GetButtonDown("South"))
                {
                    //Debug.Log("Factory Selected");
                    _lastPosition = hit.transform.position;
                    _selectedFactory = hit.transform.gameObject;
                    Factory factory = _selectedFactory.GetComponent<Factory>();
                    factory.enabled = false;
                    _isRelocating = true;
                }
            }
        }

        //get input
        Vector2 joystick = new Vector2(Input.GetAxis("RightJoyX"), -Input.GetAxis("RightJoyY"));
        if (joystick.magnitude < 0.3) { return; }
        joystick.Normalize();

        //get screensize, current anchor, and multiplier
        float width = Screen.width;
        float height = Screen.height;
        float multiplier = Time.deltaTime * Speed;
        Vector2 anchor = rect.anchoredPosition;

        //update values
        float x = anchor.x + joystick.x * multiplier;
        x = Mathf.Clamp(x, -width / 2, width / 2);
        float y = anchor.y + joystick.y * multiplier;
        y = Mathf.Clamp(y, -height / 2, height / 2);

        //set anchor
        anchor = new Vector2(x, y);
        //Debug.Log(anchor);
        rect.anchoredPosition = anchor;
    }
}