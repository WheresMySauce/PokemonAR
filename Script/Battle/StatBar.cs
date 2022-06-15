using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    [SerializeField] Text Def;
    [SerializeField] Text Atk;
    [SerializeField] Text Effect;

    public void SetStat(float def, float atk, string eff)
    {
        if (def != 1)
        {
            Def.enabled = true;
            Def.text = "Def " + def.ToString("0.0");
        }
        else Def.enabled = false;
        if (atk != 1)
        {
            Atk.enabled = true;
            Atk.text = "Atk " + atk.ToString("0.0");
        }
        else Atk.enabled = false;
        if (eff != "None")
        {
            Effect.enabled = true;
            Effect.text = eff;
        }
        else Effect.enabled = false;
    }
}
