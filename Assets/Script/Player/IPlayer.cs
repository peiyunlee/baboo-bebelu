using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayer : MonoBehaviour
{
    GameObject gameController;
    PlayerInput input;
    PlayerAnimation anim;
    public IGameSystem iGameSystem = null;
    public UIMain uIMain = null;
    [SerializeField]
    public List<PlayerInputInfo> InputResults { get { return input.InputResults; } }
    // List<MapPosition> m_mapStartPos = new List<MapPosition>();
    MapPosition m_mapStartPos;
    Vector2 playerPos;
    public string GetPlayerInputString { get { return input.PlayerInputString; } }

    MapPosition nearestItemMapPos = new MapPosition();
    void Awake()
    {
        input = GetComponent<PlayerInput>();
        input.Parent = this;
        anim = GetComponent<PlayerAnimation>();
        anim.Parent = this;
        gameController = GameObject.Find("GameController");
        iGameSystem = gameController.GetComponent<IGameSystem>();
        uIMain = gameController.GetComponent<UIMain>();
    }

    public void SetStartPos(List<MapPosition> mapStartPos)
    {
        int k;
        if (GetPlayerInputString == "Player1") k = 0;
        else k = 1;
        m_mapStartPos = new MapPosition(mapStartPos[k]);
        playerPos = new Vector2(m_mapStartPos.mapIndexC_X * 1.8f, m_mapStartPos.mapIndexR_Y * -1.8f);
        anim.SetPlayerMapPosition = m_mapStartPos;
        this.gameObject.transform.position = playerPos;

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            GameManager.IsGameWin = true;
            GameManager.IsSongPlay = false;
            uIMain.ShowGameResult(GetPlayerInputString, iGameSystem.goal);
            iGameSystem.musicManager.StopSong();
            anim.anim.SetBool("BWalk", false);
        }
    }
    public MapPosition WhichIsNearestItem(MapPosition playerMapPosition)
    {
        int tmpItem = 2;
        var playerPos = new Vector2(playerMapPosition.mapIndexC_X, playerMapPosition.mapIndexR_Y);
        var tmpPos = new Vector2(iGameSystem.mapStartPos[2].mapIndexC_X, iGameSystem.mapStartPos[2].mapIndexR_Y);
        for (var i = 3; i < 7; i++)
        {
            Vector2 itemPos = new Vector2(iGameSystem.mapStartPos[i].mapIndexC_X, iGameSystem.mapStartPos[i].mapIndexR_Y);
            if (Vector2.Distance(playerPos, itemPos) < Vector2.Distance(playerPos, tmpPos))
            {
                tmpItem = i;
                tmpPos = itemPos;
            }
        }
        nearestItemMapPos = iGameSystem.mapStartPos[tmpItem];
        return nearestItemMapPos;
    }
}