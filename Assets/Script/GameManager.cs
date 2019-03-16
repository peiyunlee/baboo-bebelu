using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //場景狀態
    SceneStateController m_SceneStateController=new SceneStateController();
    
    void Awake()
    {
        //轉換場景不會被刪除
        GameObject.DontDestroyOnLoad(this.gameObject);
        Debug.Log("gameloopawake");
    }
    void Start()
    {
        //設定起始場景
        m_SceneStateController.SetState(new StartState(m_SceneStateController),"");
        Debug.Log("gameloopstart");
    }
    void Update()
    {
        m_SceneStateController.StateUpdate();
    }
}
