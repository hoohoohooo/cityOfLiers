using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerData : MonoBehaviour
{
    public const float maxStam = 50;
    public const float maxHunger = 300;
    public const float maxHp = 200;

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
        initPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (hunger > 0)
        {
            hunger -= Time.deltaTime;
        }
    }
}