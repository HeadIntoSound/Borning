using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryController : MonoBehaviour
{
    [System.Serializable]
    public struct itemSlot
    {
        public GameObject item;
        public GameObject spaceInInventory;
        public Button btn;
        public bool inUse;
    };

    public itemSlot[] slots;
    public int freeSlot;
    public bool full;
    skillsController babySkills;
    babyItems babyItems;
    grabController babyGrab;
    audioManager aManager;
    [SerializeField] Image handItem;

    public void addItem(GameObject item, float deleteTime)
    {
        if (!full)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (!slots[i].inUse && i <= freeSlot)
                {
                    freeSlot = i;
                }
            }

            if (slots[freeSlot].item == null)
            {
                slots[freeSlot].item = Instantiate(slots[0].item, slots[freeSlot].spaceInInventory.transform.position, Quaternion.identity, slots[freeSlot].spaceInInventory.transform);
            }

            slots[freeSlot].item.name = item.name;
            slots[freeSlot].item.tag = item.tag;
            slots[freeSlot].item.SetActive(true);
            slots[freeSlot].btn = slots[freeSlot].item.GetComponent<Button>();
            slots[freeSlot].item.GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
            slots[freeSlot].inUse = true;
            freeSlot++;
            Destroy(item, deleteTime);
        }

        foreach (itemSlot slot in slots)
        {
            if (slot.item)
            {
                slot.btn.onClick.AddListener(delegate { useItem(slot); });
            }
        }
    }

    void useItem(itemSlot s)
    {
        bool canClean = false;
        switch (s.item.tag)
        {
            case "Food":
                if (babySkills.diaperCharges < 4)
                {
                    babySkills.diaperCharges++;
                }
                aManager.play("Eat");
                canClean = true;
                break;
            case "Prop":
                babyItems.handObject = (GameObject)Resources.Load("Prefabs/" + s.item.name, typeof(GameObject));
                babyGrab.holding = true;
                handItem.sprite = s.item.gameObject.GetComponent<Image>().sprite;
                canClean = true;
                break;
        }
        if (canClean)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item.GetInstanceID() == s.item.GetInstanceID())
                {
                    slots[i].inUse = false;
                    slots[i].item.SetActive(false);
                }
            }
            canClean = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        aManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<audioManager>();
        babySkills = GameObject.FindGameObjectWithTag("Baby").GetComponent<skillsController>();
        babyGrab = babySkills.transform.GetComponent<grabController>();
        babyItems = babySkills.transform.GetComponent<babyItems>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
