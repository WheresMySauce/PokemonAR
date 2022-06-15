using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] Image EnergyImage1;
    [SerializeField] Image EnergyImage2;
    [SerializeField] Image EnergyImage3;
    [SerializeField] Image EnergyImage4;
    [SerializeField] Image EnergyImage5;
    [SerializeField] Sprite NoEnergyImage;
    [SerializeField] Sprite EnergyImage;

    public IEnumerator SetEnergy(int Energy)
    {
        EnergyImage1.sprite = NoEnergyImage;
        EnergyImage2.sprite = NoEnergyImage;
        EnergyImage3.sprite = NoEnergyImage;
        EnergyImage4.sprite = NoEnergyImage;
        EnergyImage5.sprite = NoEnergyImage;
        if (Energy >= 1)
        {
            yield return new WaitForSeconds(0.1f);
            EnergyImage1.sprite = EnergyImage;
            if (Energy >= 2)
            {
                yield return new WaitForSeconds(0.1f);
                EnergyImage2.sprite = EnergyImage;
                if (Energy >= 3)
                {
                    yield return new WaitForSeconds(0.1f);
                    EnergyImage3.sprite = EnergyImage;
                    if (Energy >= 4)
                    {
                        yield return new WaitForSeconds(0.1f);
                        EnergyImage4.sprite = EnergyImage;
                        if (Energy >= 5)
                        {
                            yield return new WaitForSeconds(0.1f);
                            EnergyImage5.sprite = EnergyImage;
                        } 
                    }
                }
            }
        }
    }
}
