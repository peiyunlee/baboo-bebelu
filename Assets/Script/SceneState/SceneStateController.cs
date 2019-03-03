using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateController
{
    private ISceneState m_State;
    private bool m_bRunBegin=false;

    public SceneStateController(){Debug.Log("SceneStateController建構");}

    //設定狀態
    public void SetState(ISceneState State,string LoadSceneName){
        Debug.Log("SetState"+LoadSceneName);
        m_bRunBegin=false;
        //載入場景
        LoadScene(LoadSceneName);
        
        //通知前一個State結束
        if(m_State!=null){
            m_State.StateEnd();
            Debug.Log("m_State!=null11");
        }

        //設定
        m_State=State;
    }

    //載入場景
    private void LoadScene(string LoadStateName){
        if(LoadStateName==null||LoadStateName.Length==0)
            return;
        SceneManager.LoadScene(LoadStateName);
        Debug.Log("LoadScene");
    }

    //更新
    public void StateUpdate() {
        //通知新的State開始
        if(m_bRunBegin==false){
            m_State.StateBegin();
            m_bRunBegin=true;
            Debug.Log("通知新的State開始");
        }
        Debug.Log("StateUpdate");

        if(m_State!=null){
            m_State.StateUpdate();
            Debug.Log("m_State.StateUpdate");}
    }
}
