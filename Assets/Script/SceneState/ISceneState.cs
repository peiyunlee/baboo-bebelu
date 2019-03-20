using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISceneState
{
    //狀態名稱
    private string m_StateName ="ISceneState";
    public string StateName{
        get{return m_StateName;}
        set{m_StateName = value;}
    }
    protected SceneStateController m_Controller;
    //控制者
    
    //建構者
    public ISceneState(SceneStateController Controller){
        m_Controller=Controller;
    }
    //開始
    public virtual void StateBegin(){
    }
    //結束
    public virtual void StateEnd(){}

    //更新
    public virtual void StateUpdate(){}
}