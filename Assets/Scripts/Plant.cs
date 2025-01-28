using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Plant
{
    public string namePlant;
    public ETypePlant typePlant;
    public int timeGrowth;
    public int defaultValueDelivery;
    public List<Texture2D> spritePlant;
    public int Level;

    public int QE;

    public SubscriptionField<int> quantity;

    public Plant()
    {
        Level = 0;
        quantity = new SubscriptionField<int>() { Value = 0 };
    }
}
