using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[System.Serializable]
public class RankingItem : BaseCellView<SRankingItem>
{
    [SerializeField] private AvatarDisplay avatar;
    [SerializeField] private TMP_Text txtScore;
    [SerializeField] private TMP_Text txtRanking;
    public override void SetData(SRankingItem data)
    {
        avatar.Initialized(data.Username);
        txtRanking.text = data.indexRanking.ToString();
        txtScore.text = string.Format("{0}/{1}/{2}", data.victory, data.draw, data.defeat);
    }
}
