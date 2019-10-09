using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerData : MonoBehaviour
{
    const float maxStam = 50;
    const float maxHunger = 300;
    const float maxHp = 200;

    public float stamina;
    public float hunger;
    public float hp;
    [SerializeField]
    public List<itemBase> inventory;

    void initPlayer()
    {
        stamina = maxStam;
        hunger = maxHunger;
        hp = maxHp;
    }

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