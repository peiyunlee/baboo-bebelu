using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorToPrefab[] colorMappings;
    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x+=30)
        {
            for (int y = 0; y < map.height; y+=32)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
        if(pixelColor.a==0){
            return;
        }
        Debug.Log(pixelColor);
        /*foreach(ColorToPrefab colorMappings in colorMappings){
            if(colorMappings.color.Equals(pixelColor)){
                Vector2 position=new Vector2(x,y);
                Instantiate(colorMappings.prefab,position,Quaternion.identity,transform);
                Debug.Log("match");
            }
        }*/
    }
}
