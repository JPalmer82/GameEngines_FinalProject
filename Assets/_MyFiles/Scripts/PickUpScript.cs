using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    InventoryScript inventory;
    public Sprite itemImage;
    public string itemName;

    // Start is called before the first frame update
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryScript>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    //Below Code Adds Item to Inventory
                    inventory.isFull[i] = true;                                      // Makes the slot full
                    //Instantiate(itemImage, inventory.slots[i].transform, false);     //Makes image appear in slot
                    inventory.slots[i].gameObject.SetActive(true);
                    inventory.slots[i].sprite = itemImage;
                    inventory.itemNames[i] = itemName;                               //Sets the name of the item
                    Destroy(this.gameObject);                                        //Destroys the pickup item
                    break;                                                           //Stops the Loop
                }
            }
        }
    }
}
