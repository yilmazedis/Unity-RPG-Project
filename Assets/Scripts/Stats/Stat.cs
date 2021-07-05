using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue;


    public int GetValue()
    {
        return baseValue;
    }

    public void SetValue(int value)
    {
        baseValue = value;
    }

    public void IncreaseValue(int value)
    {
        baseValue += value;
    }
}
