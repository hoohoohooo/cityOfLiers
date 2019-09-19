using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMng : MonoBehaviour
{
    public static gameMng instance = null;
    public Transform player;
    public playerCont plCont;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
