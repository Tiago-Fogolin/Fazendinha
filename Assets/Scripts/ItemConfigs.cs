using UnityEngine;


public struct ItemConfig
{
    public GameObject prefab;
    public GameObject outlinePrefab;
    public Vector3 offset;
    public Material defaultMaterial;
}
public class ItemConfigs : MonoBehaviour
{
    public GameObject terra;
    public GameObject terra_outline;
    public Material terra_material;

    public GameObject tomate;
    public GameObject tomate_outline;
    public Material tomate_material;


    public ItemConfig terraConfig;
    public ItemConfig tomateConfig;

    private void Awake()
    {
        terraConfig = new ItemConfig
        {
            prefab = terra,
            outlinePrefab = terra_outline,
            offset = new Vector3(0.5f, 0.22f, 0.5f),
            defaultMaterial = terra_material
        };

        tomateConfig = new ItemConfig
        {
            prefab = tomate,
            outlinePrefab = tomate_outline,
            offset = new Vector3(0.5f, 0.4f, 0.5f),
            defaultMaterial = tomate_material
        };
    }
}
