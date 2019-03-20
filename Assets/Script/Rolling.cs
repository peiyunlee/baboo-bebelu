using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Rolling : MonoBehaviour
{

    RawImage img;
    float speed = 0.1725f;
    // Use this for initialization
    void Start()
    {
        this.img = this.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsSongPlay)    //音樂開始播放
        {
            float s = this.speed * Time.deltaTime;
            Rect r = this.img.uvRect;
            if (r.x >= 0.8f){
                r.x = 0;
                Debug.Log("10");
            }
            r.x += s;
            this.img.uvRect = r;
        }
    }
}
