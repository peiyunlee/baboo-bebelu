using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Rolling : MonoBehaviour
{
    IGameSystem iGameSystem;
    RawImage img;
    float speed = 0.16666f;
    bool flag=true;
    // Use this for initialization
    void Start()
    {
        this.img = this.GetComponent<RawImage>();
        iGameSystem = GameObject.Find("GameController").GetComponent<IGameSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsSongPlay)    //音樂開始播放
        {
            float s = this.speed * Time.deltaTime;
            Rect r = this.img.uvRect;
            r.x += s;
            if (r.x >= 0.8f){
                r.x = 0;
                flag=false;
            }
            if(iGameSystem.GetSongPosition<=0.1f&&!flag){
                r.x = 0f;
                flag=true;
            }
            this.img.uvRect = r;
            Debug.Log(iGameSystem.GetSongPosition);
        }
    }
}
