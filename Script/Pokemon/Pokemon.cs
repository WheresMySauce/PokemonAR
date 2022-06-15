using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    public PokemonBase Base { get; set; }

    public int HP { get; set; } 
    public float sheild { get; set; }
    public int PEnergy { get; set; }
    public string effecting  { get; set; }

    public float attack { get; set; }
    public float defense { get; set; }
    public float speed { get; set; }
    public List<Move> MoveSet { get; set; }

    public Pokemon(PokemonBase pBase)
    {
        Base = pBase;
        HP = pBase.maxHP;
        sheild = pBase.Sheild;
        PEnergy = 0;
        effecting = "None";
        attack = pBase.attack;
        defense = pBase.defense;
        speed = pBase.speed;
        MoveSet = new List<Move>();
        foreach (var move in pBase.SelectableMoves)
        {
            MoveSet.Add(new Move(move.Base));
            if (MoveSet.Count >= 4) break;
        }
    }

    public string Name 
    {
        get { return Base.Name; }
    }

    public int MaxHP
    {
        get { return Base.maxHP; }
    }
    public GameObject ImageTarget
    {
        get { return Base.imageTarget; }
    }

    public int PokemonEnergy
    {
        get { return Base.energy; }
    }

    public PokemonType Type
    {
        get { return Base.type; }
    }

    public Sprite TypeImage
    {
        get { return Base.typeImage; }
    }
    public Sprite WaitingImage
    {
        get { return Base.waitingImage; }
    }

}
