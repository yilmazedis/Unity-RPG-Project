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

[System.Serializable]
public class Stats
{
    public string name;
    public Stat maxHealth;
    public Stat damage; // stat
    public Stat armor; // stat
    public Stat attackRange; // stat
    public Stat attackSpeed; // stat
    public Stat healtRegen; // stat,
    public Stat level;
}
