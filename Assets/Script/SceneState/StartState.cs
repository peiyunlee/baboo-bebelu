using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartState : ISceneState
{
    public StartState(SceneStateController Controller):base(Controller){
        this.StateName="StartState";
    }

    //開始
    public override void StateBegin() {
        
    }

    //開始
    public override void StateEnd() {
    }

    //更新
    public override void StateUpdate(){
    }


}