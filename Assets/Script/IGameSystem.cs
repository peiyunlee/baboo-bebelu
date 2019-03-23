using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGameSystem : MonoBehaviour
{
    IPlayer iPlayer1;
    IPlayer iPlayer2;
    public MusicManager musicManager;
    public InputSystem inputSystem;
    public ItemsManager itemsManager;
    UIManager uIManager;
    Timer m_timer;
    [SerializeField]
    public List<MapPosition> mapStartPos = new List<MapPosition>();
    bool flag=false;
    public int goal;
    int conditionType;
    public int GetConditionType {get{return conditionType;}}
    float songPosition;
    public float GetSongPosition{get{return songPosition;}}
    void Awake()
    {
        musicManager = GetComponent<MusicManager>();
        uIManager = GetComponent<UIManager>();
        itemsManager = GetComponent<ItemsManager>();
        inputSystem = GetComponent<InputSystem>();
        m_timer = GetComponent<Timer>();
        iPlayer1 = GameObject.Find("Player1").GetComponent<IPlayer>();
        iPlayer2 = GameObject.Find("Player2").GetComponent<IPlayer>();
    }
    void Start()
    {
        int x, y;
        for (var i = 0; i < 2; i++)
        {
            x = Random.Range(0, 51);
            y = Random.Range(0, 50);
            foreach (var item in mapStartPos)
            {
                if (item.mapIndexC_X == x && item.mapIndexR_Y == y)
                {
                    flag = true;
                    break;
                }
            }
            if (flag) i -= 1;
            else mapStartPos.Add(new MapPosition(x, y));
        }
        iPlayer1.SetStartPos(mapStartPos);
        iPlayer2.SetStartPos(mapStartPos);
        flag=false;
        m_timer.PlayTimer();
    }
    void Update()
    {
        // ///TEST
        // if(Input.GetButtonDown("P"))  GameManager.IsGameWin=true;
        if (GameManager.IsGameStartflag)
        {
            musicManager.PlaySong();
            GameManager.IsGameStartflag = false;
        }
        else if (!GameManager.IsGameStartflag&&!flag)
        {
            RandomItems();
            goal=itemsManager.ItemsGnerator(mapStartPos);
            flag=true;
            uIManager.ShowArrow(mapStartPos[0],iPlayer1.WhichIsNearestItem(mapStartPos[0]),"Player1");
            uIManager.ShowArrow(mapStartPos[1],iPlayer2.WhichIsNearestItem(mapStartPos[1]),"Player2");
        }
        if (GameManager.IsGameEnd)       //換場景
            GameManager.m_GoState = 1;
        if (GameManager.IsSongPlay)
        {
            conditionType=inputSystem.GetConditionType;
            songPosition=musicManager.GetSongPosition;
            inputSystem.SongPosition = songPosition;
        }
    }
    void RandomItems(){
        int x, y;
            for (var i = 0; i < 5; i++)
            {
                x = Random.Range(0, 51);
                y = Random.Range(0, 50);
                foreach (var item in mapStartPos)
                {
                    if (item.mapIndexC_X == x && item.mapIndexR_Y == y)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag) i -= 1;
                else mapStartPos.Add(new MapPosition(x, y));
            }
    }

}
