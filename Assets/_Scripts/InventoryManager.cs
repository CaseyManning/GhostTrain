using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager main;

    public GameObject inventorySlot;

    public List<Item> inventory;

    public GameObject inventoryUI;

    public GameObject popup;
    public GameObject descpopup;

    [System.Serializable]
    public struct NamedImage {
        public string name;
        public Sprite image;
    }
    public NamedImage[] pictures;

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
        items.Add("firstGhost", new Item("firstGhost", "First Ghost"));
        items.Add("halfteddybear", new Item("halfteddybear", "Half a teddybear"));
        items.Add("otherhalfteddybear", new Item("otherhalfteddybear", "Other Half of a teddybear"));
        //pickup("glasses");
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
        public string desc;
        public GameObject obj;
        public Sprite objImage;

        public Item(string name, string displayname, string desc="")
        {
            this.name = name;
            this.displayname = displayname;
            this.desc = desc;

            foreach (NamedImage pic in InventoryManager.main.pictures) {
                if(pic.name == name) {
                    objImage = pic.image;
                    Debug.Log(name);
                }
            }
        }

        public GameObject create(GameObject slot)
        {
            obj = Instantiate(slot);
            obj.name = name;
            obj.transform.GetChild(0).GetComponent<Image>().sprite = objImage;
            Debug.Log("image changed into " + name);
            return obj;
        }
    }

    public void layoutInventory()
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            float slotWidth = 100;
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
                    popup.GetComponent<Popup>().open("Use " + inventory[i].displayname + "?", true);
                    selectedItem = inventory[i];
                } else
                {
                    descpopup.GetComponent<Popup>().open(inventory[i].displayname + "\n\n" + inventory[i].desc, false);
                    selectedItem = inventory[i];
                }
            }
        }
    }
}
