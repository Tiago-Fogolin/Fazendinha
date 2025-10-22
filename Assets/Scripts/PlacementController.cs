using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;
using static UnityEditor.PlayerSettings;
using NUnit.Framework.Constraints;



public struct ItemData
{
    public ItemType type;
    public GameObject objeto;
    public bool clickable;
}

public struct TileData
{
    public List<ItemData> items;
}

public class PlacementController : MonoBehaviour
{
    public GridRaycast gridRaycast;


    private ItemType tipoAtual = ItemType.NONE;
    private TileData[,] gridArray;
    private int gridSizeX = 800;
    private int gridSizeZ = 800;

    private GameObject placeObj;
    private GameObject placeObjOutline;
    private ItemType removeObjType = ItemType.NONE;

    public Material red_outline;
    public Material green_outline;
    public Material remove_outline;

    private Vector3 offset;
    private Vector3 lastPos;
    private int rotationY = 0;

    private Dictionary<ItemType, ItemConfig> mapItemConfigs;

    public ItemConfigs itemsConfig;

    void Start()
    {
        gridArray = new TileData[gridSizeX, gridSizeZ];

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                gridArray[x, z] = new TileData
                {
                    items = new List<ItemData>()
                };
            }
        }

        mapItemConfigs = new Dictionary<ItemType, ItemConfig> {
            { ItemType.BLOCK, itemsConfig.terraConfig},
            { ItemType.TOMATE, itemsConfig.tomateConfig}
        };
    }

    void Update()
    {
        Vector3 hitPos;
        if (gridRaycast.hasHit)
        {
            hitPos = gridRaycast.hitPoint;
        }
        else
        {
            hitPos = lastPos;
        }


        double x = Math.Ceiling(hitPos.x);
        x = x - offset.x;

        double z = Math.Ceiling(hitPos.z);
        z = z - offset.z;

        double y = offset.y;

        lastPos = hitPos;

        Vector3 pos = new Vector3((float)x, (float)y, (float)z);

        if (tipoAtual == ItemType.SHOVEL)
        {
            updateRemoveObjOutline(pos);
        }

        if (Input.GetMouseButtonDown(1))
        {
            switch (tipoAtual) {
                case ItemType.SHOVEL:
                    RemoverItem(pos);
                    break;
                case ItemType.NONE:
                    harvest(pos);
                    break;
                default:
                    break;
            }
        }

        if (tipoAtual != ItemType.NONE && tipoAtual != ItemType.SHOVEL)
        {
            SetOutlineColor(pos);
        }

        if (placeObjOutline != null && tipoAtual != ItemType.SHOVEL)
        {
            SetOutlinePos(pos);
        }


        if (placeObj == null) return;


        if (Input.GetKeyDown(KeyCode.R))
        {
            Rotate();
        }



        if (Input.GetMouseButtonDown(0))
        {
            ColocarItem(pos);
        }
    }

    void harvest(Vector3 pos)
    {
        var referencia = gridArray[(int)pos.x, (int)pos.z];

        if (referencia.items[referencia.items.Count - 1].type != ItemType.TOMATE) return;

        var objeto = referencia.items[referencia.items.Count - 1].objeto;
        var planta = objeto.gameObject.GetComponent<Planta>();
        var estagio = planta.stage;

        if (estagio != planta.transform.childCount - 1) return;

        planta.firstStage();
    }

    void updateRemoveObjOutline(Vector3 pos)
    {
        var referencia = gridArray[(int)pos.x, (int)pos.z];

        if (referencia.items.Count == 0)
        {
            if(placeObjOutline)
            {
                Destroy(placeObjOutline);
            }
            removeObjType = ItemType.NONE;
            return;
        }

        var item = referencia.items[referencia.items.Count - 1];


        if (item.type == removeObjType)
        {
            return;
        }

        if (placeObjOutline)
        {
            Destroy(placeObjOutline);
        }

        removeObjType = item.type;

        var itemConfig = mapItemConfigs[item.type];
        placeObjOutline = Instantiate(itemConfig.prefab, item.objeto.transform.position , item.objeto.transform.rotation);
        placeObjOutline.transform.localScale = item.objeto.transform.localScale * 1.01f;
        Transverse(placeObjOutline).GetComponent<Renderer>().material = remove_outline;

        offset = itemConfig.offset;

    }

    void Rotate()
    {
        if (rotationY == 0)
        {
            rotationY = 90;
            return;
        }

        rotationY = 0;
    }


    GameObject Transverse(GameObject obj)
    {
        if (obj.transform.childCount <= 0)
        {
            return obj;
        }

        return Transverse(obj.transform.GetChild(0).gameObject);
    }

    void SetOutlineColor(Vector3 pos)
    {
        var obj = Transverse(placeObjOutline).GetComponent<Renderer>();
        if (CanPlaceItem(pos))
        {
            obj.material = green_outline;
            return;
        }

        obj.material = red_outline;
    }

    void SetOutlinePos(Vector3 pos)
    {
        placeObjOutline.transform.position = pos;
        placeObjOutline.transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }

    bool CanPlaceItem(Vector3 pos)
    {
        var referencia = gridArray[(int)pos.x, (int)pos.z];

        if (referencia.items.Count == 2) return false;

        if (tipoAtual == ItemType.TOMATE && referencia.items.Count < 1) return false;

        if (tipoAtual == ItemType.BLOCK && referencia.items.Count >= 1) return false;

        return true;
    }

    void ColocarItem(Vector3 pos)
    {
        if (!CanPlaceItem(pos))
            return;

        GameObject objeto = Instantiate(placeObj, pos, Quaternion.Euler(0f, rotationY, 0f));

        ItemData newItem = new ItemData
        {
            type = tipoAtual,
            objeto = objeto,
            clickable = false
        };


        gridArray[(int)pos.x, (int)pos.z].items.Add(newItem);
    }

    void RemoverItem(Vector3 pos)
    {
        if (placeObj != null) return;

        var referencia = gridArray[(int)pos.x, (int)pos.z];

        if (referencia.items.Count == 0) return;
        int indiceRemover = referencia.items.Count - 1;

        var objeto = referencia.items[indiceRemover].objeto;
        Destroy(objeto);

        referencia.items.RemoveAt(indiceRemover);

        if(placeObjOutline)
        {
            Destroy(placeObjOutline);
        }
    }

    public void SetTipo(ItemType novoTipo)
    {
        tipoAtual = novoTipo;
    }

    private void destroyOutlineObjs()
    {
        Destroy(placeObjOutline);
        placeObjOutline = null;
    }

    public void setObj(ItemType type) {
        destroyOutlineObjs();
        var item = mapItemConfigs[type];
        placeObj = item.prefab;
        placeObjOutline = Instantiate(item.outlinePrefab, new Vector3(0f, -100f, 0f), Quaternion.identity);
        offset = item.offset;
        SetTipo(type);
    }

    public void resetObj()
    {
        destroyOutlineObjs();
        placeObj = null;
        placeObjOutline = null;
    }
}
