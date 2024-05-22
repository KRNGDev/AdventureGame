using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

[ExecuteAlways]
public class ScenePopulator : MonoBehaviour
{
    [Header("IMPORTANT! Check to populate")]
    public bool populate;
    [Header("Check to clear before population")]
    public bool clear = true;
    [Header("Collider to populate")]
    public Collider baseCollider;
    [Header("Items to populate")]
    public List<Items> itemList;

    private static string rootGameObjectName = "PopulationContainer";
    public void Awake()
    {
        GameObject root = GameObject.Find(rootGameObjectName);
        if (clear && root != null)
        {
            clear = false;
            Transform[] existingItems = root.GetComponentsInChildren<Transform>();
            foreach (Transform t in existingItems)
            {
                if (t != null && t != transform)
                {
                    DestroyImmediate(t.gameObject);
                }
            }
        }
        if (populate && !Application.isPlaying)
        {
            populate = false;
            clear = false;
            Bounds bounds = baseCollider.bounds;
            float x0 = bounds.min.x;
            float x1 = bounds.max.x;
            float z0 = bounds.min.z;
            float z1 = bounds.max.z;
            float y = 0;
            if (root == null)
            {
                root = new GameObject(rootGameObjectName);
                root.transform.SetParent(transform);
            }
            foreach (Items item in itemList)
            {
                for (int i = 0; i < item.numberOfItems; i++)
                {
                    GameObject newGO = Instantiate(item.item, root.transform);
                    float randomX = Random.Range(x0, x1);
                    float randomZ = Random.Range(z0, z1);
                    Ray yTerrainDetectorRay = new Ray(
                        new Vector3(randomX, 100000, randomZ),
                        Vector3.down);
                    RaycastHit rch;
                    if (Physics.Raycast(yTerrainDetectorRay, out rch))
                    {
                        if (rch.transform.name == baseCollider.transform.name)
                        {
                            y = rch.point.y;
                            newGO.transform.position = new Vector3(randomX, y + item.verticalOffset, randomZ);
                            if (item.randomRotation)
                            {
                                newGO.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
                            }
                            else
                            {
                                newGO.transform.rotation = Quaternion.identity;
                            }

                            float localScale = Random.Range(item.minScale, item.maxScale);
                            newGO.transform.localScale = new Vector3(localScale, localScale, localScale);
                        }
                    }
                }
            }
        }
    }
}
