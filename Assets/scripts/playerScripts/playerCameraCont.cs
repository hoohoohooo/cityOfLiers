using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCameraCont : MonoBehaviour
{
    float h = 0;
    float v = 0;
    [SerializeField]
    Transform target;
    [SerializeField]
    Transform cameraHolder;
    [SerializeField]
    float camDist = 3;

    bool getInput = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void getAxis()
    {
        h = Input.GetAxis("Mouse X");
        v = Input.GetAxis("Mouse Y");
    }

    // Update is called once per frame
    void Update()
    {
        if (getInput)
        {
            getAxis();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(getInput == true)
            {
                getInput = false;
            }
            else
            {
                getInput = true;
            }
        }
    }
}
