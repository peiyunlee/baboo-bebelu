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
        GETINUPT,
        WALK,
        // ANIM,
    }
    protected IPlayer parent = null;
    public IPlayer Parent { set { if (parent == null) parent = value; } }
    protected bool bGetInput;
    [SerializeField]
    private int inputState;
    private EPlayerState playerState;
    private List<PlayerInputInfo> inputResults = new List<PlayerInputInfo>();
    private bool bFcolorScan;
    private bool bScolorScan;
    [SerializeField]
    List<MapPosition> stepsMapPosition = new List<MapPosition>();  //儲存每一步的Map
    List<Vector2> stepsPosition = new List<Vector2>();  //儲存每一步的座標
    List<float> stepsFace = new List<float>();  //儲存每一步的座標
    Animator anim;
    [SerializeField]
    MapPosition playerMapPosition=new MapPosition();
    public MapPosition SetPlayerMapPosition {set {playerMapPosition=value;}}  //儲存idle時Map位置
    public MapPosition beforeStepMapPosition = new MapPosition();   //儲存上一步Map位置
    public MapPosition currentStepMapPosition = new MapPosition();   //儲存目前Map位置
    Map mapInfo = new Map();
    bool flag = false;
    int i = 0;
    // int straightStepCount=1;
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
            inputState = parent.iGameSystem.GetConditionType;
            if (inputState == (int)EConditionType.ANIMATION)
            {
                playerState = EPlayerState.WALK;
            }
            else if (inputState == (int)EConditionType.READY)
            {
                playerState = EPlayerState.IDLE;
            }
            switch (playerState)
            {
                case EPlayerState.IDLE:
                    stepsPosition.Clear();
                    stepsMapPosition.Clear();
                    inputResults.Clear();
                    bFcolorScan = true;
                    bScolorScan = true;
                    bGetInput = false;
                    flag = false;
                    i = 0;
                    break;
                case EPlayerState.WALK:
                    if (!bGetInput)
                    {
                        if (parent.InputResults.Count > 0)
                        {
                            inputResults.AddRange(parent.InputResults);
                            bGetInput = true;
                        }
                    }
                    else if (bGetInput && inputResults.Count > 0)   //如果已取得輸入結果且有輸入成功
                    {
                        if (bFcolorScan) //尚未掃描完color1
                        {
                            bFcolorScan = FindNextStep(inputResults[0].color);//掃描color1，傳入color1
                        }
                        else if (bScolorScan && inputResults.Count == 2 && inputResults[0].color != inputResults[1].color)    //尚未掃描color2，且color1一定掃完
                        {
                            bScolorScan = FindNextStep(inputResults[1].color);//掃描color2，傳入color1
                        }
                        else if (stepsPosition.Count > i)   //一定兩個都掃描完且有結果
                        {
                            if (!flag)
                            {
                                anim.SetBool("BWalk", true);
                                flag = true;
                                parent.uIManager.HideArrow(parent.GetPlayerInputString);
                            }
                            if (flag)
                            {
                                // CountStraightStep();
                                transform.rotation = Quaternion.Euler(.0f, .0f, stepsFace[i]);//轉向
                                // transform.position = Vector2.Lerp(transform.position, stepsPosition[i], 1f/(float)2*straightStepCount);//讀取位置lerp
                                transform.position = Vector2.Lerp(transform.position, stepsPosition[i], 0.66f);//讀取位置lerp
                                Debug.Log(stepsFace[i] + "," + transform.rotation.z);
                                if (transform.position == new Vector3(stepsPosition[i].x, stepsPosition[i].y))
                                {
                                    i++;
                                    // straightStepCount=1;
                                    // Debug.Log("straightStepCount done");
                                }
                                // this.gameObject.transform.position =  Vector2.Lerp(transform.position,stepsPosition[stepsPosition.Count - 1],0.5f); //直接到達結果
                            }
                        }
                        else if (i >= stepsPosition.Count)
                        {
                            anim.SetBool("BWalk", false);
                            transform.rotation = Quaternion.Euler(0f, 0f, 90f);//轉向
                            if (flag)
                            {
                                parent.uIManager.ShowArrow(currentStepMapPosition,parent.WhichIsNearestItem(currentStepMapPosition),parent.GetPlayerInputString);
                                flag = false;
                            }
                        }
                    }
                    break;
                default:
                    break;

            }
        }
        else
        {
            currentStepMapPosition = playerMapPosition;    //初始
        }
    }
    bool FindNextStep(EColor color) //一個frame掃依次
    {
        int cx = currentStepMapPosition.mapIndexC_X, ry = currentStepMapPosition.mapIndexR_Y;
        if (ry - 1 >= 0 && beforeStepMapPosition.mapIndexR_Y != ry - 1 && mapInfo.Maps[ry - 1, cx] == (int)color)   //不等於上一步、在範圍內、顏色相等
        {
            stepsMapPosition.Add(new MapPosition(cx, ry - 1));
            stepsPosition.Add(new Vector2(cx * 1.8f, (ry - 1) * -1.8f));
            stepsFace.Add(90.0f);
            beforeStepMapPosition = currentStepMapPosition; //先儲存目前位置(會變上一步)
            currentStepMapPosition = new MapPosition(cx, ry - 1); //更新位置
            return true;    //未掃完
        }
        else if (cx - 1 >= 0 && beforeStepMapPosition.mapIndexC_X != cx - 1 && mapInfo.Maps[ry, cx - 1] == (int)color)
        {
            stepsMapPosition.Add(new MapPosition(cx - 1, ry));
            stepsPosition.Add(new Vector2((cx - 1) * 1.8f, ry * -1.8f));
            stepsFace.Add(180.0f);
            beforeStepMapPosition = currentStepMapPosition; //先儲存目前位置(會變上一步)
            currentStepMapPosition = new MapPosition(cx - 1, ry); //更新位置
            return true;    //未掃完
        }
        else if (ry + 1 <= 49 && beforeStepMapPosition.mapIndexR_Y != ry + 1 && mapInfo.Maps[ry + 1, cx] == (int)color)
        {
            stepsMapPosition.Add(new MapPosition(cx, ry + 1));
            stepsPosition.Add(new Vector2(cx * 1.8f, (ry + 1) * -1.8f));
            stepsFace.Add(-90.0f);
            beforeStepMapPosition = currentStepMapPosition; //先儲存目前位置(會變上一步)
            currentStepMapPosition = new MapPosition(cx, ry + 1); //更新位置
            return true;    //未掃完
        }
        else if (cx + 1 <= 50 && beforeStepMapPosition.mapIndexC_X != cx + 1 && mapInfo.Maps[ry, cx + 1] == (int)color)   //不等於上一步、在範圍內、顏色相等
        {
            stepsMapPosition.Add(new MapPosition(cx + 1, ry));
            stepsPosition.Add(new Vector2((cx + 1) * 1.8f, ry * -1.8f));
            stepsFace.Add(.0f);
            beforeStepMapPosition = currentStepMapPosition; //先儲存目前位置(會變上一步)
            currentStepMapPosition = new MapPosition(cx + 1, ry); //更新位置
            return true;    //未掃完
        }
        return false;   //已掃完
    }
    // void CountStraightStep()
    // {
    //     for (int j = i + 1; j < stepsMapPosition.Count; j++)    //檢查直行
    //     {
    //         if (stepsFace[j] == stepsFace[i])   //繼續直行
    //         {
    //             i++;
    //             straightStepCount++;
    //         }
    //         else    //沒有執行了
    //         {
    //             break;
    //         }
    //     }
    // }
}
