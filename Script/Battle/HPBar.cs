using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject health;
    [SerializeField] Text HealthText;

    public void SetHP(int HP, int MaxHP)
    {
        float hpScale = (float)HP / MaxHP;
        health.transform.localScale = new Vector3(hpScale, 1f);
        HealthText.text = HP.ToString() + "/" + MaxHP.ToString();
    }
    public IEnumerator SetHPSmooth(int HP, int Hpchange, int MaxHP)
    {
        int change;
        if (Hpchange > 0)
        {
            if ((HP + Hpchange) <= MaxHP) change = Hpchange;
            else change = MaxHP - HP;
        }
        else
        {
            if ((HP + Hpchange) > 0) change = Hpchange;
            else change = -HP;
        }
        float hpScale;

        for (int i = 1; i < 101; i++)
        {
            hpScale = (HP * 1f + (change * i * 1f / 100)) / MaxHP;
            health.transform.localScale = new Vector3(hpScale, 1f);
            HealthText.text = ((int) (HP + change * i / 100)).ToString() + "/" + MaxHP.ToString();
            yield return new WaitForSeconds(0.01f);
        }
    }
}
