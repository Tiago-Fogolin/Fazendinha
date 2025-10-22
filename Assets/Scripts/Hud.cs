using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    NONE,
    BLOCK,
    SHOVEL,
    TOMATE
}

public class Hud : MonoBehaviour
{

    public Canvas hudCanvas;
    public PlacementController placementController;

    private int selectedItem = 0;
    private int totalItens = 3;
    private ItemType[] items;
    private RawImage img;

    void Start()
    {
        items = new ItemType[] { ItemType.BLOCK, ItemType.TOMATE, ItemType.SHOVEL };
        img = hudCanvas.transform.GetChild(selectedItem).GetComponent<RawImage>();
        img.color = Color.green;
        setSelectedItem();
    }

    void Update()
    {

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            img.color = Color.white;

            if (scroll < 0)
            {
                selectedItem++;
            }
            else if (scroll > 0)
            {
                selectedItem--;
            }


            if (selectedItem == totalItens)
            {
                selectedItem = 0;
            }
            else if (selectedItem < 0)
            {
                selectedItem = totalItens - 1;
            }


            img = hudCanvas.transform.GetChild(selectedItem).GetComponent<RawImage>();
            img.color = Color.green;
            setSelectedItem();
        }        
    }

    private void setSelectedItem()
    {
        var item = items[selectedItem];

        if (item == ItemType.NONE || item == ItemType.SHOVEL)
        {
            placementController.SetTipo(item);
            placementController.resetObj();
            return;
        }


        placementController.setObj(item);
    }
}
