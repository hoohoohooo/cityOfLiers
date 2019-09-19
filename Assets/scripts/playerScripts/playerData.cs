using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerData : MonoBehaviour
{
    public float stamina;
    public float hunger;
    public float hp;
    [SerializeField]
    public List<itemBase> inventory;


    private void Awake()
    {
        //inventory = new List<itemBase>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}