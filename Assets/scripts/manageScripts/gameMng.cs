using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMng : MonoBehaviour
{
    public delegate void fP();

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
    
    public void coroutineStarter(IEnumerator corout)
    {
        StartCoroutine(corout);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
