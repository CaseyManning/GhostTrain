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

    float startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = inventorySlot.GetComponent<RectTransform>().anchoredPosition.x;
        main = this;
        inventory = new List<Item>();
        items = new Dictionary<string, Item>();
        items.Add("ghostpowder", new Item("ghostpowder", "GHOST POWDER"));
        items.Add("glasses", new Item("glasses", "Ghost Glasses", "glasss"));


        pickup("glasses");
    }

    public void pickup(string name)
    {
        if(!items.ContainsKey(name)) {
            Debug.LogWarning("No such item to add: " + name);
            return;
        }
        Item item = items[name];
        item.create(inventorySlot);
        item.obj.transform.SetParent(inventoryUI.transform);
        inventory.Add(item);
        layoutInventory();
    }

    public class Item
    {
        public string name;
        public string displayname;
        public bool usable;
        public GameObject obj;

        public Item(string name, string displayname, string lore="")
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

    public void layoutInventory()
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            float slotWidth = 156;
            float offset = startPos + i * slotWidth;
            Vector2 anchored = inventory[i].obj.GetComponent<RectTransform>().anchoredPosition;
            anchored.x = offset;
            print(offset);
            print(slotWidth);
            print(startPos);
            inventory[i].obj.GetComponent<RectTransform>().anchoredPosition = anchored;

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
                if (inventory[i].usable)
                {
                    popup.GetComponent<Popup>().open("Use " + inventory[i].displayname + "?");
                    selectedItem = inventory[i];
                } else
                {
                    //TODO: lore popup
                }
            }
        }
    }
}
