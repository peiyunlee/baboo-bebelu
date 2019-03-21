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
    // List<MapPosition> m_mapStartPos = new List<MapPosition>();
    MapPosition m_mapStartPos;
    public Vector2 playerPos;

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
        
    }

    public void SetStartPos(List<MapPosition> mapStartPos)
    {
        int k;
        if (input.PlayerInputString == "Player1")   k = 0;
        else k = 1;
        // m_mapStartPos.Add(mapStartPos[k]);
        m_mapStartPos=new MapPosition(mapStartPos[k].mapIndexC_X,mapStartPos[k].mapIndexR_Y);
        // playerPos = new Vector2(m_mapStartPos[0].mapPosX * 1.8f, m_mapStartPos[0].mapPosY * -1.8f);
        playerPos = new Vector2(m_mapStartPos.mapIndexC_X * 1.8f, m_mapStartPos.mapIndexR_Y * -1.8f);
        // anim.mapPosition.Add(m_mapStartPos[0]);
        anim.playerMapPosition=m_mapStartPos;
        this.gameObject.transform.position=playerPos;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter");
        if (other.CompareTag("Goal"))
        {
            Debug.Log("enter2");
            GameManager.IsGameWin = true;
            GameManager.IsSongPlay = false;
            uIManager.ShowGameResult(input.PlayerInputString,iGameSystem.goal);
            iGameSystem.musicManager.StopSong();
        }
    }
}