using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using WorldForKid.ConnectSocket;
using static SocketCall;
using static UnityEngine.GraphicsBuffer;

public class DataChoose
{
    public string Username;
    public int IndexTile;
    public bool IsLeft;
}

public class Tile : MonoBehaviour
{
    [SerializeField] private SpawnRandomPosition spawnRandomPosition;
    [SerializeField] private ButtonEventDisplay btn;
    [SerializeField] private Button btnLeft;
    [SerializeField] private Button btnRight;
    [SerializeField] private TextMeshProUGUI numScore;

    private List<Item> _items;
    public List<Item> Items => _items;

    public int GetSumScore()
    {
        var score = 0;
        foreach (var item in _items) 
        {
            score += item.Score;
        }
        return score;
    }

    public void Initialized()
    {
        _items = new List<Item>();

        btn.RegisterOnClick(OnClickSelect);

        btnLeft.RegisterOnClick(OnClickLeft);

        btnRight.RegisterOnClick(OnClickRight);
        numScore.text=GetSumScore().ToString();
    }

    private int index => GameManager.Instance.GetIndexTile(this);

    private void OnClickRight()
    {
        //EventManager.Notify(EventName.ChooseLeft, false);
        Debug.Log(index + ":" + false);
        var tmp = new MessageChooseTile(index,false);
        EventManager.Notify(EventName.ChooseTile, tmp);
		//SocketCall.CallChooseTile(index, false);
        btn.SetNormal();
    }
    public void ToNormal()
    {
        btn.SetNormal();
        
    }
    private void OnClickLeft()
    {
		//EventManager.Notify(EventName.ChooseLeft, true);
		var tmp = new MessageChooseTile(index, true);
		EventManager.Notify(EventName.ChooseTile, tmp);
		//SocketCall.CallChooseTile(index, true);
		// GameManager.Instance.
		btn.SetNormal();
    }

    public void AddItem(Item item, bool hasAnimation)
    {
        if (!_items.Contains(item))
        {
            _items.Add(item);
            var pos = SpawnRandomPosition();
            item.MoveToPosition(pos, GameManager.TIME_ITEM_TO_TILE, hasAnimation, null);
        }
		numScore.text = GetSumScore().ToString();
	}
    public void AIAddItem(Item item)
    {
        if (!_items.Contains(item))
        {
            _items.Add(item);
        }
    }
    public Item RemoveFistItem()
    {
		if (_items.Count == 0) return null;
		var item = _items[0];
		_items.RemoveAt(0);
		numScore.text = GetSumScore().ToString();
		return item;
	}
    public Item AIRemoveFirstItem()
    {
		if (_items.Count == 0) return null;
		var item = _items[0];
		_items.RemoveAt(0);
		return item;
	}

    private void OnClickSelect()
    {
        if(GameManager.Instance._preTile!=null)
            GameManager.Instance._preTile.ToNormal();
        if (!GameManager.Instance.IsMyTurn)
        {
            if(index<=5&& index>=1)
                return;
        }
        else
        {
			if (index <=12 && index >= 7)
				return;
		}
        if (index == 0 || index == 6) return;
        btn.interactable = false;
        GameManager.Instance._preTile = this;

		//PutItemsToCart();
	}
    public void PutItemsToCart()
    {
        EventManager.Notify(EventName.PutItemsInCart, _items);
        EventManager.Notify(EventName.SelectTile, this);
        _items.Clear();
		numScore.text = GetSumScore().ToString();
	}
    public void PutItemsToCart(int num)
    {
        var tempItems= new List<Item>();
        for(int i = 0; i < num; i++)
        {
            var item = this.RemoveFistItem();

			if (GameManager.Instance.IsRaiSoi == true){
                if (item.Score == 10)
                {
                    i--;
                    this.AddItem(item,false);
                    continue;
                }
            }
           tempItems.Add( item);
        }
		EventManager.Notify(EventName.PutItemsInCart, tempItems);
	}
    public Vector3 SpawnRandomPosition()
    {
        return spawnRandomPosition.Get();
    }
    public void TransferItemToOtherTile(Tile other)
    {
        if (_items.Count == 0) return;
        foreach(var item in _items)
            other.AddItem(item, true);
        _items.Clear();
		numScore.text = GetSumScore().ToString();
	}
	public void AITransferItemToOtherTile(Tile other)
	{
		if (_items.Count == 0) return;
		foreach (var item in _items)
			other.AIAddItem(item);
		_items.Clear();
	}

}
public class MessageChooseTile
{
	public int Index;
	public bool IsLeft;

	public MessageChooseTile(int index, bool isLeft)
	{
		Index = index;
		IsLeft = isLeft;
	}
}
