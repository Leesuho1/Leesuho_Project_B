using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public enum SLOTSTATE
    {
        EMPTY,
        FULL
    }


    public int id;
    public Item itemObject;
    public SLOTSTATE state = SLOTSTATE.EMPTY;

    private void ChangeStateto(SLOTSTATE targetState)
    {
        state = targetState;
    }

    public void ItemGrabbed()
    {
        Destroy(itemObject.gameObject);
        ChangeStateto(SLOTSTATE.EMPTY);

    }

    public void Createitem(int id)
    {
        string itemPath = "Prefabs/item_" + id.Tostring("000");
        var itemGo = (gameObject)Instantiate(Resourse.Load(itemPath));

        itemGo.transform.SetParent(this.transform);
        itemGo.transform.localPosition = Vector3.Zero;
        itemGo.transform.localScale=Vector3.one;
        itemObject = itemGo.GetComponent<item>();
        itemObject.init(id, this);

        ChangeStateto(SLOTSTATE.FULL);
    }
}
