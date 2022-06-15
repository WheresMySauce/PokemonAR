using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using Mirror;


public class BattleSystem : NetworkBehaviour
{
    [SerializeField] List<BattleUnit> GameUnit;
    [SerializeField] List<BattleFrame> Frame;
    [SerializeField] EnergyBar energyBar;
    [SerializeField] MoveSelect moveSelect;
    [SerializeField] TargetSelecting targetSelect;
    [SerializeField] GameObject targetFrame;
    [SerializeField] GameObject moveFrame;
    [SerializeField] GameObject energyFrame;
    [SerializeField] GameObject EndingText;
    [SerializeField] GameObject BattleCanva;
    private List<BattleUnit> GameState;
    private bool targetChoose = false;
    [SyncVar] private int target = 0;
    [SyncVar] private int moveChoose = 5;
    private Move selectedMove = null;
    private uint NetId;
    private void Start()
    {
        StartCoroutine(SetupBase());
        StartCoroutine(SortGameState());
        StartCoroutine(SetupFrame(0));
        NetId = NetworkClient.localPlayer.netId;
    }

    private void Update()
    {

        if (isServer)
        {
            if ((selectedMove != null) && targetChoose == true)
            {
                MakeMovePlease();
            }
        }
        
        if (GameState[0].Pokemon.Name != GameUnit[(int)(NetId - 1)].Pokemon.Name)
        {
           energyFrame.SetActive(false);
           moveFrame.SetActive(false);
           targetFrame.SetActive(false);
        }
        
    }
    public IEnumerator SetupBase()
    {
        GameState = GameUnit;
        for (int i = 0; i < GameState.Count; i++) GameState[i].Setup();
        yield return null;
    }

    public IEnumerator SortGameState()
    {
        GameState = GameState.OrderByDescending(x => x.Pokemon.speed).ToList();
        for (int i = 0; i < GameState.Count; i++) { GameState[i].Setup(); GameState[i].SetupImageTarget(); }
        yield return null;
    }
    public IEnumerator SetupFrame(float sec)
    {
        yield return new WaitForSeconds(sec);
        for (int i = 0; i < GameState.Count; i++)
        {
            Frame[i].SetData(GameState[i].Pokemon);
            Frame[i].setStat(GameState[i].Pokemon.defense, GameState[i].Pokemon.attack, GameState[i].Pokemon.effecting);
        }
        StartCoroutine(energyBar.SetEnergy(GameState[0].Pokemon.PEnergy));

        if (GameState[0].Pokemon.Name == GameUnit[(int)(NetId - 1)].Pokemon.Name)
        {
            energyFrame.SetActive(true);
        }

        moveSelect.SetMoveText(GameState[0].Pokemon.MoveSet);
        targetSelect.SetTarget(GameState);
        moveFrame.SetActive(true);
        yield return null;

    }

    ///  Select Move ///
    public void MoveSelected1()
    {
        moveChoose = 0;
        SelectDone(moveChoose);
    }
    public void MoveSelected2()
    {
        moveChoose = 1;
        SelectDone(moveChoose);
    }
    public void MoveSelected3()
    {
        moveChoose = 2;
        SelectDone(moveChoose);
    }
    public void MoveSelected4()
    {
        moveChoose = 3;
        SelectDone(moveChoose);
    }

    [Command(requiresAuthority = false)]
    public void SelectDone(int MoveChoose)
    {
         CheckMoveAvailable(MoveChoose);
    }

    [ClientRpc]
    public void CheckMoveAvailable(int checkMove)
    {
        selectedMove = GameState[0].Pokemon.MoveSet[checkMove];
        if (selectedMove.Energy <= GameState[0].Pokemon.PEnergy)
        {
            if (selectedMove.Target == "Single")
            {
                moveFrame.SetActive(false);
                targetFrame.SetActive(true);
                targetChoose = false;
            }
            else if (selectedMove.Target == "Multiple")
            {
                target = 4;
                moveFrame.SetActive(false);
                targetChoose = true;
            }
            else
            {
                target = 0;
                targetChoose = true;
                moveFrame.SetActive(false);
            }
        }
        else { selectedMove = null; }
    }
    ///  Select Move Done ///


    /// Select Target  /// 
    public void Target1()
    {
        target = 1;
        TargetDone(target);
    }
    public void Target2()
    {
        target = 2;
        TargetDone(target);
    }
    public void Target3()
    {
        target = 3;
        TargetDone(target);
    }
    public void Back()
    {
        TargetFalse();
    }

    [Command(requiresAuthority = false)]
    public void TargetDone(int targetDone)
    {
        SelectTargetDone(targetDone);
    }

    [ClientRpc]
    public void SelectTargetDone(int selectTargetDone)
    {
        if (GameState[selectTargetDone].Pokemon.HP != 0)
        {
            target = selectTargetDone;
            targetChoose = true;
            targetFrame.SetActive(false);
        }
        else
        {
            target = 0;
        }
    }

    [Command(requiresAuthority = false)]
    public void TargetFalse()
    {
        TurnBack();
    }

    [ClientRpc]
    public void TurnBack()
    {
        target = 0;
        targetChoose = false;
        targetFrame.SetActive(false);
        moveFrame.SetActive(true);
    }

    /// Select Target Done  ///  

    [Command]
    public void MakeMovePlease()
    {
        MakeMove();
    }

    [ClientRpc]
    public void MakeMove()
    {
        int dmg = 0;
        float power = selectedMove.Power;
        float eff;
        StartCoroutine(SetAttackAnimation(GameState[0]));
        GameState[0].Pokemon.PEnergy -= selectedMove.Energy;
        GameState[0].Pokemon.sheild = selectedMove.Sheild;
        if (target == 0) //None
        {
            if (selectedMove.Name == "Heal Bell")
            {
                if (GameState[0].Pokemon.defense < 1) GameState[0].Pokemon.defense = 1;
                if (GameState[0].Pokemon.attack < 1) GameState[0].Pokemon.attack = 1;
                GameState[0].Pokemon.effecting = "None";
            }
            else if (selectedMove.Name == "Protect")
            {
                if (selectedMove.Accuracy == 0) GameState[0].Pokemon.sheild = 1;
                selectedMove.Accuracy = 100 - selectedMove.Accuracy;
            }
        }
        else if (target == 4) //Multiple
        {
            float damage = 0;
            if (selectedMove.Name == "Earthquake")
            {
                int count = 0;
                for (int k = 0; k < 4; k++)
                {
                    if (GameState[k].Pokemon.HP != 0) count++;
                }
                damage = power * 1f / count;
            }
            else damage = power * GameState[0].Pokemon.attack;

            eff = TypeChart.GetEffectiveness(GameState[0].Pokemon.Type, GameState[1].Pokemon.Type);
            if (GameState[1].Pokemon.sheild == 1.5)
                HpCalculator(-(int)(damage * eff / GameState[1].Pokemon.defense), 0);
            else
                HpCalculator(-(int)(damage * eff * GameState[1].Pokemon.sheild / GameState[1].Pokemon.defense), 1);

            eff = TypeChart.GetEffectiveness(GameState[0].Pokemon.Type, GameState[2].Pokemon.Type);
            if (GameState[2].Pokemon.sheild == 1.5)
                HpCalculator(-(int)(damage * eff / GameState[2].Pokemon.defense), 0);
            else
                HpCalculator(-(int)(damage * eff * GameState[2].Pokemon.sheild / GameState[2].Pokemon.defense), 2);

            eff = TypeChart.GetEffectiveness(GameState[0].Pokemon.Type, GameState[3].Pokemon.Type);
            if (GameState[3].Pokemon.sheild == 1.5)
                HpCalculator(-(int)(damage * eff / GameState[3].Pokemon.defense), 0);
            else
                HpCalculator(-(int)(damage * eff * GameState[3].Pokemon.sheild / GameState[3].Pokemon.defense), 3);

            if (GameState[1].Pokemon.sheild != 0) GameState[1].Pokemon.sheild = 1;
            if (GameState[2].Pokemon.sheild != 0) GameState[2].Pokemon.sheild = 1;
            if (GameState[3].Pokemon.sheild != 0) GameState[3].Pokemon.sheild = 1;

        }
        else //Single
        {
            if (selectedMove.Name == "Energy Bomb")
            {
                GameState[0].Pokemon.PEnergy += selectedMove.Energy;
                power = 40 * GameState[0].Pokemon.PEnergy;
                GameState[0].Pokemon.PEnergy = 0;
            }
            else if (selectedMove.Name == "Electro Ball")
            {
                float speed = GameState[target].Pokemon.speed;
                if (GameState[target].Pokemon.effecting == "Paralyzed") speed = speed / 2f;
                if (GameState[0].Pokemon.speed > speed) power = (GameState[0].Pokemon.speed - GameState[target].Pokemon.speed) * 2;
                else power = 20;
            }
            else if (selectedMove.Name == "Venoshock")
            {
                if (GameState[target].Pokemon.effecting == "Poisoned") Heal(40);
            }

            else if (selectedMove.Name == "Blue Flare")
            {
                if (GameState[target].Pokemon.effecting == "Burned") power = power * 1.5f;
            }

            else if (selectedMove.Name == "Thief")
            {
                if (GameState[target].Pokemon.PEnergy >= 1)
                {
                    GameState[target].Pokemon.PEnergy -= 1;
                    GameState[0].Pokemon.PEnergy += 1;
                }
            }

            else if (selectedMove.Name == "Night Slash")
            {
                power = 15 * GameState[target].Pokemon.PEnergy;
                GameState[target].Pokemon.PEnergy = 0;
            }
            else if (selectedMove.Name == "Magic Trick")
            {
                int random = Random.Range(1, 4);
                if (random != 1) power = 0;
            }
            else if (selectedMove.Name == "Screech")
            {
                GameState[target].Pokemon.defense -= 0.2f;
                if (GameState[target].Pokemon.defense < 0.2f) GameState[target].Pokemon.defense = 0.2f;
            }
            else if (selectedMove.Name == "Growl")
            {
                GameState[target].Pokemon.attack -= 0.2f;
                if (GameState[target].Pokemon.attack < 0.2f) GameState[target].Pokemon.attack = 0.2f;
            }
            if (GameState[0].Pokemon.Name == "Aggron") power = power * GameState[0].Pokemon.defense / GameState[0].Pokemon.attack;

            eff = TypeChart.GetEffectiveness(GameState[0].Pokemon.Type, GameState[target].Pokemon.Type);
            if (GameState[target].Pokemon.sheild == 1.5)
            {
                dmg = (int)(power * eff * GameState[0].Pokemon.attack / GameState[target].Pokemon.defense);
                HpCalculator(-dmg, 0);
            }
            else
            {
                dmg = (int)(power * eff * GameState[0].Pokemon.attack * GameState[target].Pokemon.sheild / GameState[target].Pokemon.defense);
                if ((selectedMove.Name == "Close Combat") && (GameState[target].Pokemon.sheild == 0))
                {
                    dmg = (int)(power * eff * GameState[0].Pokemon.attack * 1.5 / GameState[target].Pokemon.defense);
                }
                HpCalculator(-dmg, target);
                if (selectedMove.Name == "Rock Throw")
                {
                    for (int i = 1; i < 4; i++)
                    {
                        if (i != target)
                        {
                            dmg = (int)(10 * eff * GameState[0].Pokemon.attack * GameState[i].Pokemon.sheild / GameState[i].Pokemon.defense);
                            HpCalculator(-dmg, i);
                        }
                    }
                }
            }
            if (GameState[target].Pokemon.sheild != 0) GameState[target].Pokemon.sheild = 1;

        }

        if (selectedMove.Heal > 0) { Heal(selectedMove.Heal); StartCoroutine(Delay(1)); }
        if (selectedMove.Recoil > 0) {Recoil(selectedMove.Recoil); StartCoroutine(Delay(1)); }

        if (selectedMove.SelfEffect != "None" && GameState[0].Pokemon.effecting == "None") GameState[0].Pokemon.effecting = selectedMove.SelfEffect;

        GameState[0].Pokemon.attack += selectedMove.AttackIncrease;
        if (GameState[0].Pokemon.attack > 2) GameState[0].Pokemon.attack = 2;
        else if (GameState[0].Pokemon.attack < 0.2f) GameState[0].Pokemon.attack = 0.2f;
        GameState[0].Pokemon.defense += selectedMove.DefenseIncrease;
        if (GameState[0].Pokemon.defense > 2) GameState[0].Pokemon.defense = 2;
        else if (GameState[0].Pokemon.defense < 0.2f) GameState[0].Pokemon.defense = 0.2f;

        if ((GameState[0].Pokemon.effecting == "Burned") && (GameState[0].Pokemon.HP != 0))
        {
            { Recoil(10); StartCoroutine(Delay(1)); }
        }
        else if ((GameState[0].Pokemon.effecting == "Poisoned") && (GameState[0].Pokemon.HP != 0))
        {
            { Recoil(20); StartCoroutine(Delay(1)); }
        }

        GameState[0].Pokemon.PEnergy += (1 + selectedMove.EnergyUp);
        if (GameState[0].Pokemon.PEnergy > 5) GameState[0].Pokemon.PEnergy = 5;
        StartCoroutine(energyBar.SetEnergy(GameState[0].Pokemon.PEnergy));
        ChangeState(5f);

        while ((GameState[0].Pokemon.HP == 0) | (GameState[0].Pokemon.effecting == "Confused"))
        {
            ChangeState(2f);
            if (GameState[3].Pokemon.effecting == "Confused")
            {
                GameState[3].Pokemon.effecting = "None";
                GameState[3].Pokemon.PEnergy += 1; if (GameState[3].Pokemon.PEnergy > 5) GameState[3].Pokemon.PEnergy = 5;
            }
        }

        selectedMove = null;
        targetChoose = false;
        ResetMove();
    }

    [Command]
    public void ResetMove()
    {
        selectedMove = null;
        targetChoose = false;
    }

    // Make Move Done // 


    // Set Attack Animation // 
    public IEnumerator SetAttackAnimation(BattleUnit battleUnit)
    {
        battleUnit.PokemonController.SetBool(battleUnit.Pokemon.Name + "Protect", true);
        yield return new WaitForSeconds(0.1f);
        battleUnit.PokemonController.SetBool(battleUnit.Pokemon.Name + "Protect", false);
        yield return new WaitForSeconds(0.1f);
    }

    // Heal Pokemon //
    public void Heal(int heal)
    {
        HpCalculator(heal, 0);
    }
    // Recoil //
    public void Recoil(int recoil)
    {
        HpCalculator(-recoil, 0);
    }
    
    // Change the State order //
    public void ChangeState(float tim)
    {
        BattleUnit tempUnit = GameState[0];
        for (int i = 0; i < 3; i++)
        {
            GameState[i] = GameState[i + 1];
        }
        GameState[3] = tempUnit;
        if (GameState[0].Pokemon.sheild == 0) GameState[0].Pokemon.sheild = 1;
        StartCoroutine(SetupFrame(tim));
    }
    
    [Client]
    public void HpCalculator(int d, int t)
    {
        StartCoroutine(Frame[t].UpdateHp(GameState[t].Pokemon.HP, d, GameState[t].Pokemon.MaxHP,0));
        if (d != 0) StartCoroutine(GetHitAnimation(GameState[t]));
        GameState[t].Pokemon.HP = GameState[t].Pokemon.HP + d;
        if (GameState[t].Pokemon.HP <= 0)
        {
            GameState[t].Pokemon.HP = 0;
            StartCoroutine(SetFaintAnimation(GameState[t]));
        }
        if (GameState[t].Pokemon.HP > GameState[t].Pokemon.MaxHP)
        {
            GameState[t].Pokemon.HP = GameState[t].Pokemon.MaxHP;
        }
        if ( t!= 0 && selectedMove.Effect != "None" && GameState[t].Pokemon.effecting == "None" && GameState[t].Pokemon.HP != 0 && GameState[t].Pokemon.sheild != 0) 
        { 
            GameState[t].Pokemon.effecting = selectedMove.Effect;
            Frame[t].setStat(GameState[t].Pokemon.defense, GameState[t].Pokemon.attack, GameState[t].Pokemon.effecting);
        }
    }

    public IEnumerator GetHitAnimation(BattleUnit un)
    {
        yield return new WaitForSeconds(0.5f);
        un.PokemonController.SetBool(un.Pokemon.Name + "GetHit", true);
        yield return new WaitForSeconds(0.1f);
        un.PokemonController.SetBool(un.Pokemon.Name + "GetHit", false);
        yield return new WaitForSeconds(0.1f);
    }
    
    public IEnumerator SetFaintAnimation(BattleUnit un)
    {
        un.PokemonController.SetBool(un.Pokemon.Name + "Faint", true);
        yield return new WaitForSeconds(0.1f);
    }
    public IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
    }

}