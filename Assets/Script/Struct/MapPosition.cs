[System.Serializable]
public struct MapPosition
{
    public int mapIndexC_X,mapIndexR_Y;
    public MapPosition(int mapIndexC_X,int mapIndexR_Y){
        this.mapIndexC_X=mapIndexC_X;
        this.mapIndexR_Y=mapIndexR_Y;
    } 
    public MapPosition(MapPosition mapPosition){
        this.mapIndexC_X=mapPosition.mapIndexC_X;
        this.mapIndexR_Y=mapPosition.mapIndexR_Y;
    }  
}