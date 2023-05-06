using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager main;

    public GameObject inventorySlot;

    public List<Item> inventory;

    public GameObject inventoryUI;

    public GameObject popup;

    Item selectedItem;

    Dictionary<string, Item> items;

    // Start is called before the first frame update
    void Start()
    {
        main = this;
        inventory = new List<Item>();
        items = new Dictionary<string, Item>();
        items.Add("ghostpowder", new Item("ghostpowder", "GHOST POWDER"));
    }

    public void pickup(string name)
    {
        Item item = items[name];
        item.create(inventorySlot);
        item.obj.transform.SetParent(inventoryUI.transform);
        inventory.Add(item);
    }

    public class Item
    {
        public string name;
        public string displayname;
        public GameObject obj;

        public Item(string name, string displayname)
        {
            this.name = name;
            this.displayname = displayname;
        }

        public GameObject create(GameObject slot)
        {
            obj = Instantiate(slot);
            obj.name = name;
            return obj;
        }
    }

    public void use()
    {
        if(selectedItem.name == "ghostpowder")
        {
            GameObject.Find("ghosty").GetComponent<Ghost>().appear();
        }

        inventory.Remove(selectedItem);
        Destroy(selectedItem.obj);
        selectedItem = null;
    }

    public void slotClicked(string name)
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].name == name)
            {
                popup.GetComponent<Popup>().open("Use " + inventory[i].displayname + "?");
                selectedItem = inventory[i];
            }
        }
    }
}
