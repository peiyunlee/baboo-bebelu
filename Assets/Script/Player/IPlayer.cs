using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayer : MonoBehaviour
{
    GameObject gameController;
    PlayerInput input;
    PlayerAnimation anim;
    public IGameSystem iGameSystem = null;
    public UIManager uIManager = null;
    public List<PlayerInputInfo> InputResults { get { return input.InputResults; } }
    List<MapPosition> m_mapStartPos = new List<MapPosition>();
    public Vector2 playerMapPos;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        input.Parent = this;
        anim = GetComponent<PlayerAnimation>();
        anim.Parent = this;
        gameController = GameObject.Find("GameController");
        iGameSystem = gameController.GetComponent<IGameSystem>();
        uIManager = gameController.GetComponent<UIManager>();
    }
    // Update is called once per frame
    void Update()
    {
        //test
        // if(GameManager.IsGameWin)
        //     uIManager.ShowGameResult("Player2",iGameSystem.goal);
    }

    public void SetStartPos(List<MapPosition> mapStartPos)
    {
        int k;
        if (input.PlayerInputString == "Player1")   k = 0;
        else k = 1;
        m_mapStartPos.Add(mapStartPos[k]);
        playerMapPos = new Vector2(m_mapStartPos[0].mapPosX * 1.8f, m_mapStartPos[0].mapPosY * -1.8f);
        anim.transformPosition = playerMapPos;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter");
        if (other.CompareTag("Goal"))
        {
            Debug.Log("enter2");
            GameManager.IsGameWin = true;
            uIManager.ShowGameResult(input.PlayerInputString,iGameSystem.goal);
            //test
            // uIManager.ShowGameResult("Player1", iGameSystem.goal);
        }
    }
    // void GameEnd (string winner) {
    //     GameManager.instance.GameGetWinner (winner);
    // }
}