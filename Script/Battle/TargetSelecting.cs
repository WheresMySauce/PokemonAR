using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetSelecting : MonoBehaviour
{
    [SerializeField] List<Text> TargetText;
    public void SetTarget(List<BattleUnit> GameState)
    {
        for (int i = 0; i < 3; i++)
        {
            TargetText[i].text = GameState[i + 1].Pokemon.Name;
        }
        TargetText[3].text = "Back";
    }
}
