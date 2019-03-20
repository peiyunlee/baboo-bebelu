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

    public Vector2 playerMapPos;

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
    
    public void SetStartPos(){
        // anim.transformPosition=playerMapPos;
    }
    // private void OnTriggerEnter2D (Collider2D other) {
    //     if (other.CompareTag ("destination")) {
    //         GameEnd (Input.inputString);
    //     }
    // }
    // void GameEnd (string winner) {
    //     GameManager.instance.GameGetWinner (winner);
    // }
}