using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class NetworkController : NetworkBehaviour
{
    [SerializeField] List<PokemonBase> ListPokemon;
    [SerializeField] List<MoveBase> ListMove;
    [SerializeField] List<BattleUnit> ListUnit;
    [SerializeField] List<GameObject> WaitingBackground;
    public GameObject ChoosePokemon;
    public GameObject BattleCanva;
    public GameObject Waiting;
    private int choosePokemon = 19;
    private int ListUnitIndex = 0;
    private uint NetId;
    private int NumberClientConnected = 0;

    private void Start()
    {
        NetId = NetworkClient.localPlayer.netId;
    }
    private void Update()
    {
        if ((int)NetId > NumberClientConnected) UpdateNumberOfClient((int)NetId);
        if (NumberClientConnected >= 4)
        {
            if ((ListUnitIndex > 0) && (ListUnitIndex <= 3))
            {
                //ChoosePokemon.SetActive(true);

                if (ListUnitIndex == NetId - 1)
                {
                    ChoosePokemon.SetActive(true);
                    Waiting.SetActive(false);
                }
                else
                {
                    ChoosePokemon.SetActive(false);
                    Waiting.SetActive(true);
                }

            }
            else if (NetId == 1 && isLocalPlayer && ListUnitIndex == 0)
            {
                ChoosePokemon.SetActive(true);
                Waiting.SetActive(false);
            }
            else if (ListUnitIndex == 4)
            {
                ChoosePokemon.SetActive(false);
                Waiting.SetActive(true);
            }
            else
            {
                ChoosePokemon.SetActive(false);
                Waiting.SetActive(false);
            }
            for (int i = 0; i < 4; i++)
            {
                if (i != ((int)NetId - 1))
                {
                    WaitingBackground[i].SetActive(false);
                }
                else WaitingBackground[i].SetActive(true);
            }
        }
    }

    [Command(requiresAuthority = false)]
    public void UpdateNumberOfClient(int id)
    {
        RpcUpdateNumberOfClient(id);
    }
    [ClientRpc]
    public void RpcUpdateNumberOfClient(int id)
    {
        NumberClientConnected = id;
    }
    [ClientRpc]
    public void ChooseDoneFromServer(int Cpokemon)
    {
        ListUnit[ListUnitIndex].pokemonBase = ListPokemon[Cpokemon];
        WaitingBackground[ListUnitIndex].GetComponent<Image>().sprite = ListPokemon[Cpokemon].waitingImage;
        ListUnitIndex = ListUnitIndex + 1;
        if (ListUnitIndex >= 4)
        {
            RandomFromServer();
            StartCoroutine(JustDelay(10f));
        }
    }

    [Command(requiresAuthority = false)]
    public void ChooseDone(int Cpokemon)
    {
        ChooseDoneFromServer(Cpokemon);
    }

    public void choose0()
    {
        choosePokemon = 0;

        ChooseDone(choosePokemon);

    }
    public void choose1()
    {
        choosePokemon = 1;

        ChooseDone(choosePokemon);

    }
    public void choose2()
    {
        choosePokemon = 2;

        ChooseDone(choosePokemon);

    }
    public void choose3()
    {
        choosePokemon = 3;

        ChooseDone(choosePokemon);

    }
    public void choose4()
    {
        choosePokemon = 4;

        ChooseDone(choosePokemon);

    }
    public void choose5()
    {
        choosePokemon = 5;

        ChooseDone(choosePokemon);

    }
    public void choose6()
    {
        choosePokemon = 6;

        ChooseDone(choosePokemon);


    }
    public void choose7()
    {
        choosePokemon = 7;

        ChooseDone(choosePokemon);


    }
    public void choose8()
    {
        choosePokemon = 8;

        ChooseDone(choosePokemon);


    }
    public void choose9()
    {
        choosePokemon = 9;

        ChooseDone(choosePokemon);


    }
    public void choose10()
    {
        choosePokemon = 10;

        ChooseDone(choosePokemon);


    }
    public void choose11()
    {
        choosePokemon = 11;

        ChooseDone(choosePokemon);


    }
    public void choose12()
    {
        choosePokemon = 12;

        ChooseDone(choosePokemon);


    }
    public void choose13()
    {
        choosePokemon = 13;

        ChooseDone(choosePokemon);


    }
    public void choose14()
    {
        choosePokemon = 14;

        ChooseDone(choosePokemon);


    }
    public void choose15()
    {
        choosePokemon = 15;

        ChooseDone(choosePokemon);


    }
    public void choose16()
    {
        choosePokemon = 16;

        ChooseDone(choosePokemon);


    }
    public void choose17()
    {
        choosePokemon = 17;

        ChooseDone(choosePokemon);


    }
    public void choose18()
    {
        choosePokemon = 18;

        ChooseDone(choosePokemon);
    }
    public IEnumerator JustDelay(float tim)
    {
        ChoosePokemon.SetActive(false);
        yield return new WaitForSeconds(tim);
        gameObject.GetComponent<BattleSystem>().enabled = true;
        Waiting.SetActive(false);
        BattleCanva.SetActive(true);
        ListUnitIndex++;
    }
    [Command]
    public void RandomFromServer()
    {
        int randomFirstMove, randomSecondMove;
        for (int i = 0; i < 4; i++)
        {
            randomFirstMove = Random.Range(0, 8);
            randomSecondMove = randomFirstMove;
            while (randomSecondMove == randomFirstMove) randomSecondMove = Random.Range(0, 8);
            UpdateMove(i, randomFirstMove, randomSecondMove);
        }
    }
    [ClientRpc]
    public void UpdateMove(int unit, int first, int second)
    {
        ListUnit[unit].pokemonBase.SelectableMoves[2].moveBase = ListMove[first];
        ListUnit[unit].pokemonBase.SelectableMoves[3].moveBase = ListMove[second];
    }
}
