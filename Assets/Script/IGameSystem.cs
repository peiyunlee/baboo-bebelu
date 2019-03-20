using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGameSystem :MonoBehaviour
{
    
    float songPosition;
    // Start is called before the first frame update
    IPlayer Player1;
    IPlayer Player2;
    public MusicManager musicManager;
    public InputSystem inputSystem;
    public ItemsManager itemsManager;
    
    public Timer timer;
    public bool IsSongPlay=false;
    [SerializeField]
    // List<Position> mapStartPos = new List<Position>();
    void Awake()
    {
        musicManager = GetComponent<MusicManager>();
        itemsManager = GetComponent<ItemsManager>();
        inputSystem = GetComponent<InputSystem>();
        timer= GetComponent<Timer>();
        Player1= GameObject.Find("Player1").GetComponent<IPlayer>();
        Player2= GameObject.Find("Player2").GetComponent<IPlayer>();
    }
    void Start()
    {
        // int x, y;
        // bool flag = false;
        // for (int i = 0; i < 7; i++)
        // {
        //     x = Random.Range(0, 51);
        //     y = Random.Range(0, 50);
        //     foreach (var item in mapStartPos)
        //     {
        //         if (item.mapPosX == x)
        //         {
        //             flag = true;
        //             break;
        //         }
        //         else if (item.mapPosY== y)
        //         {
        //             flag = true;
        //             break;
        //         }
        //     }
        //     if (flag) i -= 1;
        //     else mapStartPos.Add(new Position(x, y));
        // }
        // for (int i = 0; i < 7; i++)
        // {
        //     switch (i)
        //     {
        //         case 0:
        //             Player1.playerMapPos=new Vector2(mapStartPos[i].transformPosX,mapStartPos[i].transformPosY);
        //             Player1.SetStartPos();
        //             // Player1.SetStartPos(new Vector2(mapStartPos[i].mapPosX*(1.8f),mapStartPos[i].mapPosY*(1.8f)));
        //             break;
        //         case 1:
        //             Player2.playerMapPos=new Vector2(mapStartPos[i].transformPosX,mapStartPos[i].transformPosY);
        //             Player2.SetStartPos();
        //             // Player2.SetStartPos(new Vector2(mapStartPos[i].mapPosX*(1.8f),mapStartPos[i].mapPosY*(-1.8f)));
        //             break;
        //         default:
        //             // itemsManager.ItemsPos.Add(new Vector2(mapStartPos[i].transformPosX,mapStartPos[i].transformPosY));
        //             // itemsManager.ItemsGnerator(new Vector2(mapStartPos[i].transformPosX,mapStartPos[i].transformPosY));
        //             break;
        //     }
        // }
        // Player1.SetStartPos();
        // Player2.SetStartPos();
        // itemsManager.ItemsGnerator();
    }
    void Update()
    {
        if(GameManager.IsGameStart){    //倒數完
            musicManager.PlaySong();
            GameManager.IsGameStart=false;
        }
        if(GameManager.IsGameEnd)       //換場景
            GameManager.m_GoState=1;
        if(musicManager.isSongPlay){
            IsSongPlay=true;
            GameManager.IsSongPlay=IsSongPlay;
            songPosition=musicManager.GetSongPosition;
            inputSystem.SongPosition=songPosition;
        }
    }
    

}
