using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new Move")]

public class MoveBase : ScriptableObject
{
    [SerializeField] string moveName;
    [SerializeField] int power;
    [SerializeField] int energy;
    [SerializeField] int accuracy = 100;
    [SerializeField] int heal;
    [SerializeField] int recoil;
    [SerializeField] int energyUp;
    [SerializeField] float sheild = 1;
    [SerializeField] TargetSelect target;
    [SerializeField] MoveEffect moveEffect;
    [SerializeField] MoveEffect selfEffect;
    [SerializeField] float attackIncrease;
    [SerializeField] float defenseIncrease;



    public float Sheild
    {
        get { return sheild; }
    }
    public string Name
    {
        get { return moveName; }
    }
    public int Power
    {
        get { return power; }
    }

    public int Energy
    {
        get { return energy; }
    }

    public int Accuracy
    {
        get { return accuracy; }
    }

    public int Heal
    {
        get { return heal; }
    }

    public int Recoil
    {
        get { return recoil; }
    }

    public int EnergyUp
    {
        get { return energyUp; }
    }
    public TargetSelect Target
    {
        get { return target; }
    }
    public MoveEffect Effect
    {
        get { return moveEffect; }
    }
    public MoveEffect SelfEffect
    {
        get { return selfEffect; }
    }
    public float AttackIncrease
    {
        get { return attackIncrease; }
    }
    public float DefenseIncrease
    {
        get { return defenseIncrease; }
    }

    public enum MoveEffect
    {
        None,
        Burned,
        Paralyzed,
        Poisoned,
        Confused,
    }
    public enum TargetSelect
    {
        None,
        Single,
        Dual,
        Multiple
    }

}
