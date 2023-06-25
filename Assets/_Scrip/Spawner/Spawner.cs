using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Spawner : SaiMonoBehaviour
{
    [SerializeField] protected int spawnerCount = 0;
    public int SpawnerCount => spawnerCount;
    [SerializeField] protected Transform Holders;
    [SerializeField] protected List<Transform> prefabs;
    [SerializeField] protected List<Transform> poolobjs;
    protected override void LoadComponents()
    {
        this.LoadHolders();
        this.LoadPrefabs();
    }
    protected virtual void LoadHolders()
    {
        if (this.Holders != null) return;
        this.Holders = transform.Find("Holder");
        Debug.Log(transform.name + " LoadHolders " + gameObject);
    }
    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;
        Transform prefabojbs = transform.Find("Prefab");
        foreach (Transform prefab in prefabojbs)
        {
            this.prefabs.Add(prefab);
        }
        this.HidePrefabs();
    }
    protected virtual void HidePrefabs()
    {
        foreach (Transform prefab in this.prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }
    public virtual Transform Spawn(string prefabname, Vector3 spawnPos, Quaternion rotation)
    {
        Transform prefab = this.GetPrefabByName(prefabname); //Spawn OJC 
        if (prefab == null) //if haven't
        {
            Debug.LogWarning("Prefab not found" + prefabname);
            return null;
        }
        return this.Spawn(prefab, spawnPos, rotation);

    }
    public virtual Transform Spawn(Transform prefab, Vector3 spawnPos, Quaternion rotation)
    {
        Transform newPrefab = this.getOBJFROMPOOL(prefab);
        newPrefab.SetLocalPositionAndRotation(spawnPos, rotation);
        newPrefab.parent = this.Holders;
        this.spawnerCount++;
        return newPrefab;

    }
    public virtual Transform GetPrefabByName(string prefabname)
    {
        foreach (Transform prefab in this.prefabs) //duyet tung phan tu tranform tim ra = chuoi string
        {
            if (prefab.name == prefabname) return prefab;
        }
        return null;
    }
    public virtual void DeSpawn(Transform obj)
    {
        this.poolobjs.Add(obj);
        obj.gameObject.SetActive(false);
        this.spawnerCount--;
    }
    protected virtual Transform getOBJFROMPOOL(Transform prefab)
    {
        foreach (Transform poolobj in this.poolobjs)
        {
            //if (this.poolobjs == null) continue;
            if (poolobj.name == prefab.name)
            {
                this.poolobjs.Remove(poolobj);
                return poolobj;
            }
        }
        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        return newPrefab;

    }
    public virtual Transform RandomPrefab()
    {
        int rand = Random.Range(0, this.prefabs.Count);
        return this.prefabs[rand];
    }
}
