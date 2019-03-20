using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{
    public int rightThing{ get { return x; }} 
    int x;
    public Image GoalImage;
    public Sprite [] ItemsSprite = new Sprite[5];
    public GameObject Prefab ;
    Sprite prefabSprite;
    [SerializeField]
    public List<Vector2> ItemsPos=new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("aa");
        x = Random.Range( 0, 5 );
        GoalImage.sprite=ItemsSprite[x];//產生UI
        // prefabSprite=Prefab.GetComponent<Sprite>();
        // Instantiate(Prefab, new Vector2(1.8f,-1.8f),new Quaternion(0,0,0,0));
    }
    public void ItemsGnerator(){
        
        // for(int i=0;i<5;i++)
        // {
        //     // prefabSprite=ItemsSprite[i];
        //     // Prefab.name="Item1";
        //     Instantiate(Prefab, ItemsPos[i],new Quaternion(0,0,0,0));
        // }
    }
    // Update is called once per frame
    void Update()
    {
        //方向箭頭
    }
}
