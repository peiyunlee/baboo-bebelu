[System.Serializable]
public struct PlayerInputInfo
{
    public EColor color;
    public EInputCondition inputCondition;
    public float inputTime;
    public PlayerInputInfo(EColor color,float inputTime,EInputCondition inputCondition){
        this.color=color;
        this.inputTime=inputTime;
        this.inputCondition=inputCondition;
    }   
}