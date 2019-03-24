using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{
    int goal;
    [SerializeField]
    Image GoalImage;
    [SerializeField]
    Sprite[] ItemsSprite = new Sprite[5];
    [SerializeField]
    GameObject Prefab;
    SpriteRenderer prefabSpriterenderer;
    
    void Start()
    {
        goal = Random.Range(0, 5);
        GoalImage.sprite = ItemsSprite[goal];//產生UI
        
        prefabSpriterenderer = Prefab.GetComponent<SpriteRenderer>();
    }
    public int ItemsGnerator(List<MapPosition> itemMapPos)
    {
        for(var i=2;i<7;i++)
        {
            prefabSpriterenderer.sprite=ItemsSprite[i-2];
            Prefab.name="Item"+(i-1);
            if(i==goal+2) Prefab.gameObject.tag="Goal";
            else Prefab.gameObject.tag="Untagged";
            Instantiate(Prefab, new Vector2(1.8f * itemMapPos[i].mapIndexC_X, -1.8f * itemMapPos[i].mapIndexR_Y),new Quaternion(0,0,0,0)); 
        }
        return goal;
    }
    // Update is called once per frame
    void Update()
    {
    }
}
