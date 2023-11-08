using System.Collections.Generic;

[System.Serializable]
public class UserDTO
{
    private List<int> minExpLevel = new List<int>()
        {
            0, 100, 200, 400
        };
    public string Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string DisplayName { get; set; }
    public int IndexAvatar { get; set; }
    public int Coin { get; set; }
    public int Exp { get; set; }

    public int GetLevel()
    {
        int level = 0;
        for (int i = 0; i < minExpLevel.Count; i++)
        {
            var minExp = minExpLevel[i];
            if (Exp >= minExp) { level = i + 1; }
        }
        return level;
    }
    public int GetTargetExp()
    {
        for (int i = 0; i < minExpLevel.Count; i++)
        {
            var minExp = minExpLevel[i];
            if (Exp < minExp) return minExp;
        }
        return 999999;
    }
}

public enum MinExpLevel
{
    Level_1 = 0,
    Level_2 = 100,
    Level_3 = 200,
    Level_4 = 400,
    Level_5 = 800
}