using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelGrydka
{
    public int level;
    public List<Grydka> newGrydka;
    public List<GameObject> border;
    public Sprite sprite;
    public int numberOfOrders;
}
