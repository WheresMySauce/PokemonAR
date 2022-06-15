using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    public PokemonBase pokemonBase;
    public GameObject imageTarget;


    public Pokemon Pokemon { get; set; }
    public Animator PokemonController { get; set; }
    public void Setup()
    {
        Pokemon = new Pokemon(pokemonBase);
    }
    public void SetupImageTarget()
    {
        imageTarget = Instantiate(Pokemon.ImageTarget, transform.position, transform.rotation, transform.parent);
        PokemonController = imageTarget.GetComponentInChildren<Animator>();
    }    
}
    