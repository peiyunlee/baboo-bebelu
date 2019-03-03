using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : ISceneState
{
    public StartState(SceneStateController Controller):base(Controller){
        this.StateName="StartState";
        Debug.Log("StartState建構");
    }

    //開始
    public override void StateBegin() {}

    //開始
    public override void StateEnd() {
        Debug.Log("StartStateEnd");
    }

    //更新
    public override void StateUpdate(){
        Debug.Log("StateUpdate");
        //m_Controller.SetState(new MainState(m_Controller));
    }
}
