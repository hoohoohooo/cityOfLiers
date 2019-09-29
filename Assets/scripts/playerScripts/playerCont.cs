using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerCont : MonoBehaviour
{
    public Camera cam;
    enum status
    {
        idle, crouch, walk, run, attack,
    }
    status st;
    public playerData data;

    public void eatFood()
    {

    }
    public void takeAcid()
    {

    }
    public void getItem(itemBase item)
    {
        itemBase tmp = null;
        foreach (itemBase i in data.inventory)
        {
            if (i.itemName == item.itemName)
            {
                if (tmp == null) {
                    tmp = i;
                }
                else if (tmp.count > i.count)
                {
                    tmp = i;
                }
                else
                {
                    continue;
                }
            }
        }
        if (tmp != null)
        {
            tmp.checkQuantCount(ref data.inventory, item);
        }
        else
        {
            data.inventory.Add(item);
        }
    }

    #region cameraCode
    public Transform camPivot;
    enum camState
    {
        q,
        mid,
        e
    }
    camState curCamState = camState.mid;
    float camTweenTime = 0.5f;
    public float smoothness = 0.01f;
    //1.5f
    Vector3 midCamPos = new Vector3(0, 2, 0);
    Vector3 qCamPos = new Vector3(-1.5f, 2, 0);
    Vector3 eCamPos = new Vector3(1.5f, 2, 0);
    IEnumerator camAnim(Vector3 from, Vector3 to)
    {
        float progress = 0;
        float increment = smoothness / camTweenTime;
        float prop = 0.5f;
        camState inCorState = curCamState;
        while (progress < 1)
        {
            if(inCorState != curCamState)
            {
                break;
            }
            camPivot.localPosition = Vector3.Lerp(from, to, progress);
            progress += prop;
            prop /= 2;
            if (prop < 0.01f)
            {
                progress = 1;
            }
            yield return new WaitForSeconds(increment);
        }
        yield return null;
    }
    #endregion

    //do i need this?
    public void getInput()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {

        }
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)) {
            if (Input.GetKey(KeyCode.Q))
            {
                curCamState = camState.q;
                StartCoroutine(camAnim(camPivot.localPosition, qCamPos));
            }
            else
            {
                curCamState = camState.e;
                StartCoroutine(camAnim(camPivot.localPosition, eCamPos));
            }
        }
        else
        {
            curCamState = camState.mid;
            StartCoroutine(camAnim(camPivot.localPosition, midCamPos));
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            hitOutput = rayFromCamera();
        }
    }

    Ray ray;
    RaycastHit hit;
    Vector3 dir;
    public Transform hitOutput = null;
    public Transform rayFromCamera()
    {
        dir = cam.transform.forward;
        ray = new Ray(cam.transform.position, dir);
        if(Physics.Raycast(cam.transform.position,dir,out hit))
        {
            print(hit.transform.name);
            return hit.transform;
        }
        return null;
    }

    void onDebugOnly()
    {
        for(int i = 0; i < 6; i++)
        {
            dummyItem tmp = new dummyItem();
            getItem(tmp);
            
        }
    }
    private void Awake()
    {
        //data = GetComponent<playerData>();
        onDebugOnly();
        cam = camPivot.GetChild(0).GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
    }
}
