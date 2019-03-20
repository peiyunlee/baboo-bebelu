using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    //場景狀態
    SceneStateController m_SceneStateController=new SceneStateController();
    //SceneStateController m_SceneStateController;
    // public IPlayer player1;
    // public IPlayer player2;
    //IGameSystem iGameSystem;
    // UIManager uIManager;
    [SerializeField]
    static public int m_GoState;
    [SerializeField]
    static public bool IsGameStartflag; //true=啟動音樂開始播 flase=已啟動
    [SerializeField]
    static public bool IsSongPlay;
    [SerializeField]
    static  public bool IsGameWin;
    [SerializeField]
    static  public bool IsGameEnd;
    static public string state;
    void Awake()
    {
        //轉換場景不會被刪除
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        //設定起始場景
        m_SceneStateController.SetState(new StartState(m_SceneStateController),"");
        state="Start";
        // m_SceneStateController.SetState(new MainState(m_SceneStateController),"");
        // state="Main";
        IsGameStartflag=false;
        IsSongPlay=false;
    }
    void Update()
    {
        m_SceneStateController.StateUpdate();
        switch(m_GoState){
            case 1:
                m_SceneStateController.SetState(new StartState(m_SceneStateController),"Start");
                m_GoState=0;
                state="Start";
                IsGameStartflag=false;
                IsSongPlay=false;
                IsGameEnd=false;
                IsGameWin=false;
                break;
            case 2:
                m_SceneStateController.SetState(new MainState(m_SceneStateController),"Main");
                m_GoState=0;
                state="Main";
                break;
            default:
                break;
        }
        
    }
}
