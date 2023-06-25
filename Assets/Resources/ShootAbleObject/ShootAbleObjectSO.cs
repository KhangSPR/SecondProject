using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootAbleObject", menuName = "SO/ShootAbleObject")]
public class ShootAbleObjectSO : ScriptableObject
{
    public string ObjName = "ShootAble Object";
    public ObjectType objectType;
    public int hpMax = 3;
    //public List<Droprate> dropList;
}
