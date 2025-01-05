using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class LevelGrydka
{
    public int level;
    public List<Grydka> newGrydka;
    public List<GameObject> border;
    public Sprite sprite;
   public GameObject forestGroup;
    public int numberOfOrders;
}
