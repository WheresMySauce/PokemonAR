using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public MoveBase Base { get; set; }
    public int Accuracy  { get; set; }
    public Move(MoveBase mBase)
    {
        Base = mBase;
        Accuracy = Base.Accuracy;
    }
    public string Name
    {
        get { return Base.Name; }
    }
    public float Sheild
    {
        get { return Base.Sheild; }
    }
    public int Power
    {
        get { return Base.Power; }
    }

    public int Energy
    {
        get { return Base.Energy; }
    }

    

    public int Heal
    {
        get { return Base.Heal; }
    }

    public int Recoil
    {
        get { return Base.Recoil; }
    }

    public int EnergyUp
    {
        get { return Base.EnergyUp; }
    }
    public string Target
    {
        get { return Base.Target.ToString(); }
    }
    public string Effect
    {
        get { return Base.Effect.ToString(); }
    }
    public string SelfEffect
    {
        get { return Base.SelfEffect.ToString(); }
    }
    public float AttackIncrease
    {
        get { return Base.AttackIncrease; }
    }
    public float DefenseIncrease
    {
        get { return Base.DefenseIncrease; }
    }
}
