using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSelect : MonoBehaviour
{
    [SerializeField] List<Text> MoveTexts;
    public void SetMoveText(List<Move> moves)
    {
        for (int i = 0; i < MoveTexts.Count; i++)
        {
            MoveTexts[i].text = moves[i].Base.Name;
        }
    }

}

