using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterStat
{

    public string id;
    public float hp;
    public float attack;
    public int coin;
    public int stage;

    public Object monsterPrefab;

    [HideInInspector]
    public Vector3 pos = new Vector3(0, 3.2f, 0);




}
