using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimation : MonoBehaviour
{
    [System.Serializable]
    enum EPlayerState
    {
        IDLE,
        WALK,
        // ANIM,
    }
    protected IPlayer parent = null;
    public IPlayer Parent { set { if (parent == null) parent = value; } }
    protected bool bGetInput;
    [SerializeField]
    private int inputState;
    private EPlayerState playerState;
    [SerializeField]
    private List<PlayerInputInfo> inputResults = new List<PlayerInputInfo>();
    private bool bFcolorScan;
    private bool bScolorScan;
    List<Vector2> stepsPosition = new List<Vector2>();  //儲存每一步的距離
    Animator anim;
    public float ANIMATE_TIME = 2.0f;
    // public List<MapPosition> mapPosition = new List<MapPosition>();
    [SerializeField]
    public MapPosition playerMapPosition = new MapPosition();   //儲存idle時Map位置
    Map mapInfo = new Map();
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        bGetInput = false;
        bFcolorScan = true;
        bScolorScan = true;
    }
    void Update()
    {
        if (GameManager.IsSongPlay)
        {
            inputState = parent.iGameSystem.inputSystem.ConditionType;
            if (inputState == (int)EConditionType.ANIMATION)
                playerState = EPlayerState.WALK;
            else playerState = EPlayerState.IDLE;
            switch (playerState)
            {
                case EPlayerState.IDLE:
                    anim.SetBool("BWalk", false);
                    break;
                case EPlayerState.WALK:
                    if (!bGetInput)
                    {
                        inputResults.AddRange(parent.InputResults);
                        bGetInput = true;
                    }
                    else if (bGetInput && inputResults.Count > 0)   //如果已取得輸入結果且有輸入成功
                    {
                        if (bFcolorScan) //尚未掃描完color1
                        {
                            bFcolorScan = FindNextStep(inputResults[0].color);//掃描color1，傳入color1
                        }
                        else if (bScolorScan)    //尚未掃描color2，且color1一定掃完
                        {
                            bFcolorScan = FindNextStep(inputResults[1].color);//掃描color2，傳入color1
                        }
                        else    //一定兩個都掃描完
                        {
                            anim.SetBool("BWalk", true);
                        }
                    }
                    break;
                default:
                    break;

            }
        }

        bool FindNextStep(EColor color) //一個frame掃依次
        {
            int x = playerMapPosition.mapIndexC_X, y = playerMapPosition.mapIndexR_Y;
            // if(y-1>=0&&mapInfo.Maps[y-1][x]==(int)color)
            // {
            //     stepsPosition.Add(new Vector2(x*1.8f,(y-1)*-1.8f)); 
            //     return true;    //未掃完
            // }
            // else if(x+1<=51&&mapInfo.Maps[y][x+1]==(int)color)
            // {
            //     stepsPosition.Add(new Vector2((x+1)*1.8f,y*-1.8f));
            //     return true;    //未掃完
            // }
            // else if(y+1<=50&&mapInfo.Maps[y+1][x]==(int)color)
            // {
                // stepsPosition.Add(new Vector2(x*1.8f,(y+1)*-1.8f));
            //     return true;    //未掃完
            // }
            // else if(x-1>=0&&mapInfo.Maps[y][x-1]==(int)color)
            // {
                // stepsPosition.Add(new Vector2((x-1)*1.8f,y*-1.8f));
            //     return true;    //未掃完
            // }

            return false;   //已掃完
        }
    }
}
