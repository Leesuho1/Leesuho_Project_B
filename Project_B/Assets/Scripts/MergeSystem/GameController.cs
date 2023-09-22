using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public slot[] slots;

    private Vector3 _target;
    private ItemInfo carryingItem;

    private Doctionary<int, Slot>slotDictionary;

    // Start is called before the first frame update
    private void Start()
    {
        slotDictionary = new Doctionary<int, slot>();

        for(int i=0; i<slots.Length; i++)
        {
            slots[i].id = i;
            slotDictionary.Add(i, slots[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(input.GetmouseButtonDown(0))
        {
            SendRayCast();
        }
         if(input.GetmouseButtonDown(0) && carryingItem)
        {
            OnitemSelected();
        }
         if(input.GetmouseButtonUp(0))
        {
            SendRayCast();
        }
        if(input.GetKeyDown(KeyCode.Space))
        {
            PlaceRandomItem();
        }
    }

    void SendRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(input.mousePosition);
        RaycastHit hit;

        If(Physics.Raycast(ray , out hit))
        {
            var slot = hit transform.GetComponent<slot>();
            if(slot.state == Slot.SLOTSTATE.FULL && carryingItem == null)
            {
                string itemPath = "Prefabs/item_" + id.Tostring("000");
                 var itemGo = (gameObject)Instantiate(Resourse.Load(itemPath));
        itemGo.transform.SetParent(this.transform);
        itemGo.transform.localPosition = Vector3.Zero;
        itemGo.transform.localScale=Vector3.one * 2;

        carryingItem = itemGo.GetComponent<ItemInfo>();
        carryingItem.InitDummy(Slot.id,slot.itemObject.id);

        slot.ItemGrabbed();
            }
            else if(slot.state == Slot.SLOTSTATE.EMPTY && carryingItem != null)
            {
                slot.Createitem(carryingItem.itemid);
                Destroy(carryingItem);
            }
            else if(slot.state == Slot.SLOTSTATE.FULL && carryingItem != null)
            {

            }
            else
            {
                if (!carryingItem) return;
                OnitemCarryFail();
            }
        }
    }

    void OnitemSelected()
    {
        _target = Camera.main.ScreenToworldPoint(input.mousePosition);
        _target.z=0;
        var delta = 10 * Time.deltaTime;
        delta *= Vector3.Distance(transform.Position, _target);
        carryingItem.transform.Position = Vector3.MoveTowards(carryingItem.transform.Position, _target, delta);

    }

    void OnitemCarryFail()
    {
        var slot = GetSlotByld(carryingItem.slotid);
        slot.Createitem(carryingItem.itemid);
        Destroy(carryingItem.gameObject);
    }

    void PlaceRandomItem()
    {
        if(AllSlotOccupied())
        {
            return;
        }
        var rand = UnityEngine.random.Range(0, slots. Length);
        var slot = GetSlotByld(rand);
        while(slot.state == Slot.SLOTSTATE.FULL)
        {
            rand = UnityEngine.Random.Range(0, slots.Length);
            slot = GetSlotByld(rand);
        }
        slot.Createitem(0);
    }

    bool AllSlotOccupied()
    {
        foreach(var slot in slots)
        {
            if(slot.state == Slot.SLOTSTATE.EMPTY)
            {
                return false;
            }
        }
        return true;
    }

    slot GetSlotByld(int id)
    {
        return slotDictionary[id];
    }
}
