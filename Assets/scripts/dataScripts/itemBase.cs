using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class itemBase
{
    public string itemName;
    public int maxQuantity;
    public int count;

    public void checkQuantCount(ref List<itemBase> ibL, itemBase item)
    {
        if (count >= maxQuantity)
        {
            //do stuff
            ibL.Add(item);
        }
        else
        {
            count++;
        }
    }
    public virtual void useItem(ref List<itemBase> ibl)
    {
        if(count == 1)
        {
            //remove this from inventory
            ibl.Remove(this);
        }
        else if (count > 1)
        {
            count--;
        }
        else
        {
            //error
        }
        
    }

}
public class dummyItem : itemBase
{
    public dummyItem()
    {
        count = 1;
        maxQuantity = 4;
        itemName = "dummy";
    }
    public override void useItem(ref List<itemBase> ibl)
    {
        base.useItem(ref ibl);
        Debug.Log("dummy item used");
    }
}
public class acid : itemBase
{
    public acid()
    {
        count = 1;
        maxQuantity = 1;
        itemName = "acid";
    }
    public override void useItem(ref List<itemBase> ibl)
    {
        base.useItem(ref ibl);

    }
}

