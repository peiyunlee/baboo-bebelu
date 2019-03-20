using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{
    public int rightThing { get { return goal; } }
    int goal;
    public Image GoalImage;
    public Sprite[] ItemsSprite = new Sprite[5];
    public GameObject Prefab;
    SpriteRenderer prefabSpriterenderer;
    [SerializeField]
    public List<MapPosition> ItemsPos = new List<MapPosition>();
    // Start is called before the first frame update
    void Start()
    {
        goal = Random.Range(0, 5);
        GoalImage.sprite = ItemsSprite[goal];//產生UI
        
        prefabSpriterenderer = Prefab.GetComponent<SpriteRenderer>();
    }
    public int ItemsGnerator(List<MapPosition> itemMapPos)
    {
        for(int i=2;i<7;i++)
        {
            prefabSpriterenderer.sprite=ItemsSprite[i-2];
            Prefab.name="Item"+(i-1);
            if(i==goal+2) Prefab.gameObject.tag="Goal";
            Instantiate(Prefab, new Vector2(1.8f * itemMapPos[i].mapPosX, -1.8f * itemMapPos[i].mapPosY),new Quaternion(0,0,0,0)); 
        }
        return goal;
    }
    // Update is called once per frame
    void Update()
    {
        //方向箭頭
    }
}
