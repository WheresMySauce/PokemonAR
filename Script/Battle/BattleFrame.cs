using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleFrame : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] HPBar hpBar;
    [SerializeField] Image Type;
    [SerializeField] StatBar stat;



    public void SetData(Pokemon pokemon)
    {
        nameText.text = pokemon.Base.Name;
        hpBar.SetHP(pokemon.HP,pokemon.MaxHP);
        Type.sprite = pokemon.TypeImage;
    }

    public IEnumerator UpdateHp(int HP, int Hpchange, int MaxHP,float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        yield return hpBar.SetHPSmooth(HP, Hpchange, MaxHP);
    }
    public void setStat(float def, float atk, string eff)
    {
        stat.SetStat(def, atk, eff);
    }
}
