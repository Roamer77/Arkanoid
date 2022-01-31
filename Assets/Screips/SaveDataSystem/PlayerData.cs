using System;

[Serializable]
public class PlayerData
{
    public int LevelIndex;

    public int PlayerScore;

    public string SaveFileName;

    public string SavingTime;

    public PlayerData(int levelIndex, int playerScore, string saveFileName, string savingTime)
    {
        LevelIndex = levelIndex;
        PlayerScore = playerScore;
        SaveFileName = saveFileName;
        SavingTime = savingTime;
    }
}