using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    public Slot ParentSlot;

    public void init(int if, Slot slot)
    {
        this.id = id;
        this.ParentSlot = slot;
    }
}
