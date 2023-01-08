using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<PlayerInfo> Players;

    public PlayerInfo LeftPlayer;
    public PlayerInfo RightPlayer;
    public PlayerInfo TopPlayer;

    public GameObject PlayerObj;

    public TMP_InputField[] Inputs;

    public TextMeshProUGUI[] PlayerNames;

    public GameObject PlayerPrefab;

    public TextMeshProUGUI CurrentTimer;

    public PlayerInfo CurrentPlayerChoice;

    public bool GameStarted = false;

    void Update()
    {
        if(GameStarted)
        { 
        // Current, Left, Right, Top
            if (!CurrentPlayerChoice.GetComponent<PlayerTimer>().timerActive)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if(LeftPlayer != null)
                        LeftPlayer = CurrentPlayer(LeftPlayer, 1);
                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if(RightPlayer != null)
                        RightPlayer = CurrentPlayer(RightPlayer, 2);
                }

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if(TopPlayer != null)
                        TopPlayer = CurrentPlayer(TopPlayer, 3);


                }
            }

            else
            {
                StartCoroutine(WaitRoutine());
            }

            if (!CurrentPlayerChoice.GetComponent<PlayerTimer>().timerActive)
            {
                CurrentTimer.text = CurrentPlayerChoice?.PlayerStopwatch?.time.ToString(@"mm\:ss");
                CurrentTimer.color = Color.white;
            }

            else
            {
                CurrentTimer.text = CurrentPlayerChoice?.GetComponent<PlayerTimer>().time.ToString(@"mm\:ss");
                CurrentTimer.color = Color.red;
            }
        }
    }


    public IEnumerator WaitRoutine()
    {
        if (CurrentPlayerChoice.GetComponent<PlayerTimer>().timerActive)
            yield return new WaitForSeconds(1);

        else
            yield break;
    }

    public PlayerInfo CurrentPlayer(PlayerInfo NextPlayer, int Index)
    {
        PlayerInfo P1 = CurrentPlayerChoice;
        PlayerInfo P2 = NextPlayer;
       
        CurrentPlayerChoice = P2;
        NextPlayer = P1;

        PlayerNames[0].text = CurrentPlayerChoice.PlayerName;
        PlayerNames[Index].text = NextPlayer.PlayerName;

        NextPlayer.ActivePlayer = false;
        CurrentPlayerChoice.ActivePlayer = true;

        NextPlayer.PlayerStopwatch.stopWatchActive = false;
        CurrentPlayerChoice.PlayerStopwatch.stopWatchActive = true;

        Debug.Log("hi");

        CurrentPlayerChoice.GetComponent<PlayerTimer>().timerActive = true;

        return NextPlayer;

    }


    public void AddPlayersToList()
    {
        foreach (TMP_InputField Inp in Inputs)
        {
            if(Inp.text != "")
            { 
                GameObject NewPl = Instantiate(PlayerPrefab);

                NewPl.GetComponent<PlayerInfo>().PlayerName = Inp.text;

                Players.Add(NewPl.GetComponent<PlayerInfo>());

                NewPl.transform.parent = PlayerObj.transform;
            }
        }

        if(Players.Count >= 1)
            CurrentPlayerChoice = Players[0];

        if (Players.Count >= 2)
            LeftPlayer = Players[1];

        if (Players.Count >= 3)
            RightPlayer = Players[2];

        if (Players.Count >= 4)
            TopPlayer = Players[3];
    }

    public void StartGame()
    {

        int i = 0;

        foreach (TextMeshProUGUI Tex in PlayerNames)
        {
            Tex.text = Players[i].PlayerName;

            i++;

            if (i == Players.Count)
                break;
        }

        Players[0].ActivePlayer = true;
        Players[0].PlayerStopwatch.stopWatchActive = true;

        GameStarted = true;
    }

    
}
