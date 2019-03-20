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
    List<Vector2Int> steps = new List<Vector2Int>();
    Animator anim;
    public float ANIMATE_TIME = 2.0f;
    public Vector2 transformPosition;

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
        this.gameObject.transform.position=transformPosition;
        if (GameManager.IsSongPlay)
        {
            inputState = parent.iGameSystem.inputSystem.ConditionType;
            if (inputState == (int)EConditionType.ANIMATION)
                playerState = EPlayerState.WALK;
            else playerState = EPlayerState.IDLE;
            switch (playerState)
            {
                case EPlayerState.IDLE:
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
                            //依儲存路線行動播動畫
                        }
                    }
                    break;
                default:
                    break;

            }
        }

        bool FindNextStep(EColor color) //一個frame掃依次
        {
            return true;    //未掃完
            //return false;   //已掃完
        }
    }
}
