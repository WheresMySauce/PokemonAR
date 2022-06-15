using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new Pokemon")]
public class PokemonBase : ScriptableObject
{
    [SerializeField] string pokemonName;
    [SerializeField] PokemonType Type;
    [SerializeField] Sprite TypeImage;
    [SerializeField] GameObject ImageTarget;
    [SerializeField] Sprite WaitingImage;

    //Base Stats
    [SerializeField] int MaxHP;
    [SerializeField] float Attack;
    [SerializeField] float Defense;
    [SerializeField] int Speed;
    [SerializeField] int Energy = 0;
    [SerializeField] float sheild = 1;

    [SerializeField] List<SelectableMove> selectableMoves;

    public string Name
    {
        get { return pokemonName; }
    }

    public Sprite typeImage
    {
        get { return TypeImage; }
    }
    public Sprite waitingImage
    {
        get { return WaitingImage; }
    }

    public PokemonType type
    {
        get { return Type; }
    }

    public GameObject imageTarget
    {
        get { return ImageTarget; }
    }
    public int maxHP
    {
        get { return MaxHP; }
    }
    public float Sheild
    {
        get { return sheild; }
    }
    public float attack
    {
        get { return Attack; }
    }

    public float defense
    {
        get { return Defense; }
    }

    public int speed
    {
        get { return Speed; }
    }
    public int energy
    {
        get { return Energy; }
    }
    public List<SelectableMove> SelectableMoves
    {
        get { return selectableMoves; }
    }

}

[System.Serializable]
public class SelectableMove
{
    public MoveBase moveBase;
    
    public MoveBase Base
    {
        get { return moveBase; }
    }

}


public enum PokemonType
{
    None,
    Normal,
    Fire,
    Water,
    Grass,
    Electric,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon,
    Dark,
    Steel,
    Fairy,
}

public class TypeChart
{
    static float[][] chart =
    { 
                                  //  Normal | Fire | Water | Grass | Electric | Ice | Fighting | Poison | Ground | Flying | Psychic | Bug | Rock | Ghost | Dragon | Dark | Steel | Fairy
    
        /*Normal*/     new float[] {   1f    ,  1f  ,  1f   , 1f    ,  1f      , 1f  ,   1f     ,   1f   ,   1f   ,   1f   ,   1f    ,  1f , 0.75f,  0f   ,   1f   ,  1f  , 0.75f ,  1f  },
        /*Fire*/       new float[] {   1f    , 0.75f, 0.75f , 1.5f  ,  1f      , 1.5f,   1f     ,   1f   ,   1f   ,   1f   ,   1f    , 1.5f, 0.75f,  1f   , 0.75f  ,  1f  , 1.5f  ,  1f  },
        /*Water*/      new float[] {   1f    , 1.5f , 0.75f , 0.75f ,  1f      , 1f  ,   1f     ,   1f   ,  1.5f  ,   1f   ,   1f    ,  1f , 1.5f ,  1f   , 0.75f  ,  1f  ,  1f   ,  1f  },
        /*Grass*/      new float[] {   1f    , 0.75f,  1.5f , 0.75f ,  1f      , 1f  ,   1f     , 0.75f  ,  1.5f  , 0.75f  ,   1f    ,0.75f, 1.5f ,  1f   , 0.75f  ,  1f  , 0.75f ,  1f  },
        /*Electric*/   new float[] {   1f    ,  1f  ,  1.5f , 0.75f , 0.75f    , 1f  ,   1f     ,   1f   ,   0f   ,  1.5f  ,   1f    ,  1f ,  1f  ,  1f   , 0.75f  ,  1f  ,  1f   ,  1f  },
        /*Ice*/        new float[] {   1f    , 0.75f, 0.75f , 1.5f  ,  1f      ,0.75f,   1f     ,   1f   ,  1.5f  ,  1.5f  ,   1f    ,  1f ,  1f  ,  1f   , 1.5f   ,  1f  , 0.75f ,  1f  },
        /*Fighting*/   new float[] {  1.5f   ,  1f  ,  1f   , 1f    ,  1f      , 1.5f,   1f     , 0.75f  ,   1f   ,  0.75f , 0.75f   ,0.75f, 1.5f ,  0f   ,   1f   ,1.5f  , 1.5f  , 0.75f},
        /*Poison*/     new float[] {   1f    ,  1f  ,  1f   , 1.5f  ,  1f      , 1f  ,   1f     , 0.75f  , 0.75f  ,   1f   ,   1f    ,  1f , 0.75f,  0.75f,   1f   ,  1f  ,  0f   ,  1.5f},
        /*Ground*/     new float[] {   1f    , 1.5f ,  1f   , 0.75f , 1.5f     , 1f  ,   1f     ,  1.5f  ,   1f   ,   0f   ,   1f    ,0.75f, 1.5f ,  1f   ,   1f   ,  1f  , 1.5f  ,  1f  },
        /*Flying*/     new float[] {   1f    ,  1f  ,  1f   , 1.5f  , 0.75f    , 1f  ,   1.5f   ,   1f   ,   1f   ,   1f   ,   1f    , 1.5f, 0.75f,  1f   ,   1f   ,  1f  , 0.75f ,  1f  },
        /*Psychic*/    new float[] {   1f    ,  1f  ,  1f   , 1f    ,  1f      , 1f  ,   1.5f   ,  1.5f  ,   1f   ,   1f   , 0.75f   ,  1f ,  1f  ,  1f   ,   1f   ,  0f  , 0.75f ,  1f  },
        /*Bug*/        new float[] {   1f    , 0.75f,  1f   , 1.5f  ,  1f      , 1f  ,   0.75f  , 0.75f  ,   1f   ,  0.75f ,   1.5f  ,  1f ,  1f  , 0.75f ,   1f   , 1.5f , 0.75f , 0.75f},
        /*Rock*/       new float[] {   1f    , 1.5f ,  1f   , 1f    ,  1f      , 1.5f,   0.75f  ,   1f   , 0.75f  ,   1.5f ,   1f    , 1.5f,  1f  ,  1f   ,   1f   ,  1f  , 0.75f ,  1f  },
        /*Ghost*/      new float[] {   0f    ,  1f  ,  1f   , 1f    ,  1f      , 1f  ,   1f     ,   1f   ,   1f   ,   1f   ,   1.5f  ,  1f ,  1f  ,  1.5f ,   1f   , 0.75f,  1f   ,  1f  },
        /*Dragon*/     new float[] {   1f    ,  1f  ,  1f   , 1f    ,  1f      , 1f  ,   1f     ,   1f   ,   1f   ,   1f   ,   1f    ,  1f ,  1f  ,  1f   , 1.5f   ,  1f  , 0.75f ,  0f  },
        /*Dark*/       new float[] {   1f    ,  1f  ,  1f   , 1f    ,  1f      , 1f  ,   0.75f  ,   1f   ,   1f   ,   1f   ,   1.5f  ,  1f ,  1f  ,  1.5f ,   1f   , 0.75f,  1f   , 0.75f},
        /*Steel*/      new float[] {   1f    , 0.75f, 0.75f , 1f    , 0.75f    , 1.5f,   1f     ,   1f   ,   1f   ,   1f   ,   1f    ,  1f , 1.5f ,  1f   ,   1f   ,  1f  , 0.75f ,  1.5f},
        /*Fairy*/      new float[] {   1f    , 0.75f,  1f   , 1f    ,  1f      , 1f  ,   1.5f   , 0.75f  ,   1f   ,   1f   ,   1f    ,  1f ,  1f  ,  1f   , 1.5f   , 1.5f , 0.75f ,  1f  },
    };
    
    public static float GetEffectiveness(PokemonType atkT, PokemonType defT)
    {
        int row = (int)atkT - 1;
        int col = (int)defT - 1;

        return chart[row][col];
    }

}
