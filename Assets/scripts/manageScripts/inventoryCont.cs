using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryCont : MonoBehaviour
{
    public RectTransform invWinTrn;
    List<Button> btnList;
    List<inventoryBtn> btnContList;
    [SerializeField]
    playerData data;
    public void initInventoryOnAwake()
    {
        //invWinTrn.rect.right = Screen.width / 2;
        Transform tmp;
        RectTransform tmpRect; 
        invWinTrn.offsetMax = new Vector2(Screen.width / 2 * -1, invWinTrn.offsetMax.y);
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                tmp = Instantiate(transform.GetChild(0));
                btnList.Add(tmp.GetComponent<Button>());
                tmp.SetParent(transform);
                tmpRect = tmp.GetComponent<RectTransform>();
                tmpRect.anchoredPosition = new Vector2(j * (Screen.width / 6) - (Screen.width / 6), i * (Screen.height / -5f) + (Screen.height/2.5f));
            }
        }
        transform.GetChild(0).gameObject.SetActive(false);
        initInventory();
    }

    public void initInventory()
    {
        foreach(Button b in btnList)
        {
            b.transform.GetChild(0).GetComponent<Text>().text = "";
            b.transform.GetChild(1).GetComponent<Text>().text = "";
            b.onClick.RemoveAllListeners();
            
        }
        btnContList.Clear();
        int count = 0;
        foreach(itemBase i in data.inventory)
        {
            btnList[count].transform.GetChild(0).GetComponent<Text>().text = i.itemName;
            btnList[count].transform.GetChild(1).GetComponent<Text>().text = i.count.ToString();
            btnContList.Add(new inventoryBtn(i, btnList[count].transform, this));
            count++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        data = gameMng.instance.plCont.data;
        btnList = new List<Button>();
        btnContList = new List<inventoryBtn>();
        initInventoryOnAwake();
    }

    // Update is called once per frame
    void Update()
    {
        initInventory();
    }
}

public class inventoryBtn
{
    public itemBase associatedInfo;
    inventoryCont cnt;
    Button btn;

    public void onClickEvent(ref List<itemBase> ibl)
    {
        associatedInfo.useItem(ref ibl);
        cnt.initInventory();
    }

    public inventoryBtn(itemBase item, Transform btnTrn, inventoryCont invCont)
    {
        associatedInfo = item;
        btn = btnTrn.GetComponent<Button>();
        cnt = invCont;
        //bn.onClick.AddListener(() => associatedInfo.useItem(ref gameMng.instance.plCont.data.inventory));
        
        btnTrn.GetComponent<Button>().onClick.AddListener(() => onClickEvent(ref gameMng.instance.plCont.data.inventory));
    }

}
