using UnityEngine;
using UnityEngine.UI;

enum ItemType
{
    NONE,
    BLOCK,
    TOMATE
}

public class Hud : MonoBehaviour
{

    public Canvas hudCanvas;
    public GridRaycast gridRayCast;

    private int selectedItem = 0;
    private int totalItens = 3;
    private ItemType[] items;
    private RawImage img;

    void Start()
    {
        items = new ItemType[] { ItemType.BLOCK, ItemType.TOMATE, ItemType.NONE };
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

        switch (item)
        {
            case ItemType.BLOCK:
                gridRayCast.setGroundObj();
                return;
            case ItemType.TOMATE:
                gridRayCast.setTomateObj();
                return;
            default:
                gridRayCast.resetObj();
                return;
        }
    }
}
