using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGameSystem : MonoBehaviour
{

    float songPosition;
    // Start is called before the first frame update
    IPlayer Player1;
    IPlayer Player2;
    public MusicManager musicManager;
    public InputSystem inputSystem;
    public ItemsManager itemsManager;

    public Timer timer;
    [SerializeField]
    List<MapPosition> mapStartPos = new List<MapPosition>();
    bool flag=false;
    public int goal;
    void Awake()
    {
        musicManager = GetComponent<MusicManager>();
        itemsManager = GetComponent<ItemsManager>();
        inputSystem = GetComponent<InputSystem>();
        timer = GetComponent<Timer>();
        Player1 = GameObject.Find("Player1").GetComponent<IPlayer>();
        Player2 = GameObject.Find("Player2").GetComponent<IPlayer>();
    }
    void Start()
    {
        int x, y;
        for (int i = 0; i < 2; i++)
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
        Player1.SetStartPos(mapStartPos);
        Player2.SetStartPos(mapStartPos);
        flag=false;
    }
    void Update()
    {
        ///TEST
        if(Input.GetButtonDown("P"))  GameManager.IsGameStartflag=true;
        if(Input.GetButtonDown("O"))  GameManager.IsGameWin=true;
        if(Input.GetButtonDown("I"))  GameManager.IsGameEnd=true;
        if (GameManager.IsGameStartflag)
        {
            //倒數完
            musicManager.PlaySong();
            GameManager.IsGameStartflag = false;

        }
        else if (!GameManager.IsGameStartflag&&!flag)
        {
            int x, y;
            for (int i = 0; i < 5; i++)
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
            goal=itemsManager.ItemsGnerator(mapStartPos);
            flag=true;
        }
        if (GameManager.IsGameEnd)       //換場景
            GameManager.m_GoState = 1;
        if (GameManager.IsSongPlay)
        {
            songPosition = musicManager.GetSongPosition;
            inputSystem.SongPosition = songPosition;
        }
    }
}
