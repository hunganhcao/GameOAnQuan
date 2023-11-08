using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using XXX.UI.Popup;
using static SocketCall;
using static UnityEditor.Progress;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private ListCircular<Tile> tiles;
    [SerializeField] private SamplePool<Item> itemPool;
    [SerializeField] private SamplePool<Item> itemQuanPool;
    [SerializeField] private Tile myCart;
    [SerializeField] private Tile otherCart;
    [SerializeField] private Tile myCollection;
    [SerializeField] private Tile otherCollection;
    [SerializeField] private PlayerGame playerMine;
    [SerializeField] private PlayerGame playerOther;
    [SerializeField] private PopupManager popupManager;
    //[SerializeField]private ListenMessageSocket<SMessageChooseTile> l_SocketChooseItem;


    private Tile _currentTile;
    public Tile _preTile { get; set; }

	private Tile _currentCart => _isMyTurn ? myCart : otherCart;
    private Tile _currentCollection => _isMyTurn ? myCollection : otherCollection;

    private bool _isMyTurn = true;
    public bool IsMyTurn => _isMyTurn;

    public bool IsRaiSoi;



    public const int INIT_COUNT_ITEM_IN_TILE = 5;
    public const float TIME_ITEM_TO_TILE = 0.3f;
    private List<int> _indexQuans = new List<int>()
    {
        0, 6
    };

    private void Awake()
    {
        itemPool.Prepare(10);
        itemQuanPool.Prepare(2);
        InitializedTiles();

        myCart.Initialized();
        otherCart.Initialized();

        myCollection.Initialized();
        otherCollection.Initialized();
        SpawnItemToTile();

        EventManager.AddEvent(EventName.PutItemsInCart, PutItemsInCart);
        EventManager.AddEvent(EventName.SelectTile, SelectTile);
        EventManager.AddEvent(EventName.ChooseLeft, ChooseLeft);
        EventManager.AddEvent(EventName.ChooseTile, ChooseTile);
        //l_SocketChooseItem.RegisterEvent(EventName.Socket_ChooseTile, SocketChooseTile);
        var mine = new Player();
        //playerMine.Initialized( 0);
        var other = new Player();
        //playerOther.Initialized(0);
    }
	private void Start()
	{
		_isMyTurn=true;
	}
	public void ChooseTile(object data)
    {
        var datas = data as MessageChooseTile;
        // _isMyTurn = DataManager.CheckMineByUsername(data.Username);
        var indexTile = datas.Index;//GetIndexTile(datas.Index, _isMyTurn);
        var isLeft = _isMyTurn ? datas.IsLeft : !datas.IsLeft;

        _currentTile = tiles.List[indexTile];
        StartCoroutine(IERaiQuan(isLeft));
        
    }

    int GetIndexTile(int index, bool isMine)
    {
        if(isMine) return index;
        return 12 - index;
    }

    private void OnDestroy()
    {
        EventManager.RemoveEvent(EventName.PutItemsInCart, PutItemsInCart);
        EventManager.RemoveEvent(EventName.SelectTile, SelectTile);
        EventManager.RemoveEvent(EventName.ChooseLeft, ChooseLeft);
		EventManager.RemoveEvent(EventName.ChooseTile, ChooseTile);

	}

    private void SelectTile(object data)
    {
       //_preTile = _currentTile;
       //_currentTile.ToNormal();
        var tile = data as Tile;
        if (!tile) return;
        _currentTile = tile;
        tiles.SetIndex(tile);
    }

    private void ChooseLeft(object data)
    {
        var chooseLeft = (bool)data;

        StartCoroutine(IERaiQuan(chooseLeft));
    }

    private IEnumerator IERaiQuan(bool chooseLeft)
    {
        Tile lastTile = null;

        // bỏ toàn bộ item trong tile được chọn vào giỏ
        _currentTile.PutItemsToCart();
        yield return new WaitForSeconds(TIME_ITEM_TO_TILE);
        while (true)
        {
            var item = _currentCart.RemoveFistItem();
            if (!item)
            {
                // đoạn này là dải từ giỏ vào đồ
                var tileContinute = GetNextTile(chooseLeft);

                // nếu dải hết thì ăn đúp
                if (tileContinute.Items.Count == 0)
                {
                    while (true)
                    {
                        tileContinute = GetNextTile(chooseLeft);
						if (tileContinute.Items.Count == 0) break;
						tileContinute.TransferItemToOtherTile(_currentCollection);

                        tileContinute = GetNextTile(chooseLeft);
                        if (tileContinute.Items.Count != 0) break;
                        yield return new WaitForSeconds(TIME_ITEM_TO_TILE);
                    }
                    break;
                }

                // lấy và rải tiếp
                if (CheckOQuan(tileContinute)) break;
                tileContinute.PutItemsToCart();
                yield return new WaitForSeconds(2 * TIME_ITEM_TO_TILE);
                continue;
            }
            lastTile = GetNextTile(chooseLeft);
            lastTile.AddItem(item, true);

            yield return new WaitForSeconds(TIME_ITEM_TO_TILE);
        }

        if (CheckEndGame())
        {
            for (int i = 1; i < 6; i++)
            {
                tiles.List[i].TransferItemToOtherTile(myCollection);
            }
            for (int i = 7; i < 12; i++)
            {
                tiles.List[i].TransferItemToOtherTile(otherCollection);
            }
            if(myCollection.GetSumScore() >= otherCollection.GetSumScore())
            {
                popupManager.ShowPopupWin(null);
            }
            else
            {
                popupManager.ShowPopupLose(null);
            }
        }
        ChangeTurn();
    }

    private void Update()
    {

    }
    private Tile GetNextTile(bool chooseLeft)
    {
        return chooseLeft ? tiles.Pre() : tiles.Next();
    }
    private void ChangeTurn()
    {
        _isMyTurn = !_isMyTurn;
        Debug.Log(_isMyTurn ? "myTurn" : "yourTurn");

        int isEmpty=0;
        if(_isMyTurn)
        {
			for (int i = 1; i < 6; i++)
			{
				if (tiles.List[i].Items.Count == 0)
				{
					isEmpty++;
				}
			}
		}
        else
        {
           // AIFindPath(tiles);
			for (int i = 7; i < 12; i++)
			{
				if (tiles.List[i].Items.Count == 0)
				{
					isEmpty++;
				}
			}
		}

        if(isEmpty==5 && _currentCollection.Items.Count>=5) {
            //Rai soi
            StartCoroutine(IEHetSoi());
        }
        if (isEmpty == 5 && _currentCollection.Items.Count < 5)
        {
            //end game
            for (int i = 1; i < 6; i++)
            {
                tiles.List[i].TransferItemToOtherTile(myCollection);
            }
            for (int i = 7; i < 12; i++)
            {
                tiles.List[i].TransferItemToOtherTile(otherCollection);
            }
            if (myCollection.GetSumScore() >= otherCollection.GetSumScore())
            {
                popupManager.ShowPopupWin(null);
            }
            else
            {
                popupManager.ShowPopupLose(null);
            }
        }

    }
	private IEnumerator IEHetSoi()
    {
        IsRaiSoi= true;
        _currentCollection.PutItemsToCart(5);
		yield return new WaitForSeconds(TIME_ITEM_TO_TILE);
		
		
		if (_isMyTurn)
		{
			for (int i = 1; i < 6; i++)
			{
				var item = _currentCart.RemoveFistItem();
				tiles.List[i].AddItem(item, true);
			}
		}
		else
		{
			for (int i = 7; i < 12; i++)
			{
				var item = _currentCart.RemoveFistItem();
				tiles.List[i].AddItem(item, true);
			}
		}
        IsRaiSoi = false;
	}


	private void PutItemsInCart(object data)
    {
        var items = data as List<Item>;
        if (items == null) return;

        var cart = _isMyTurn ? myCart : otherCart;
        foreach (var item in items)
        {
            cart.AddItem(item, true);
        }
    }

    private void SpawnItemToTile()
    {
        for(int i = 0; i < tiles.List.Count; i++)
        {
            var tile = tiles.List[i];

            if (_indexQuans.Contains(i))
            {
               // Debug.Log("nguyen");
                var item = itemQuanPool.Get();
                tile.AddItem(item, true);
                continue;
            }

            //Debug.Log("nguyen");
            for (int j = 0; j < INIT_COUNT_ITEM_IN_TILE; j++)
            {
                var item = itemPool.Get();
                tile.AddItem(item, true);
            }
        }
    }
    private void InitializedTiles()
    {
        foreach (var tile in tiles.List)
        {
            tile.Initialized();
        }

    }
    public int GetIndexTile(Tile tile)
    {
        for(int i = 0; i < tiles.List.Count; i++)
        {
            if (tiles.List[i].Equals(tile))
            {
                return i;
            }
        }
        return -1;
    }
    private bool CheckOQuan(Tile tile)
    {
        var index = GetIndexTile(tile);
        return _indexQuans.Contains(index);
    }
    private bool CheckEndGame()
    {
        foreach(var i in _indexQuans)
        {
            if (tiles.List[i].Items.Count != 0) return false;
        }
        return true;
    }
	public void AIFindPath(ListCircular<Tile> tiles)
	{
        List<AINodePath> path = new List<AINodePath>();
		Tile lastTile = null;
		bool ischooseLeft = true;
		for (int i = 7; i < 12; i++)
		{
			if (tiles.List[i].Items.Count == 0)
			{
				continue;
			}
			else
			{
                Tile collection=null;
                var currentCart = tiles.List[i];
				while (true)
				{
					var item = currentCart.AIRemoveFirstItem();
					if (!item)
					{
						// đoạn này là dải từ giỏ vào đồ
						var tileContinute = GetNextTile(ischooseLeft);

						// nếu dải hết thì ăn đúp
						if (tileContinute.Items.Count == 0)
						{
							while (true)
							{
								tileContinute = GetNextTile(ischooseLeft);
								if (tileContinute.Items.Count == 0) break;
								tileContinute.AITransferItemToOtherTile(collection);

								tileContinute = GetNextTile(ischooseLeft);
								if (tileContinute.Items.Count != 0) break;
							}
							break;
						}

						// lấy và rải tiếp
						if (CheckOQuan(tileContinute)) break;
						AIPutItemToCart(tileContinute, currentCart);
						continue;
					}
					lastTile = GetNextTile(ischooseLeft);
					lastTile.AIAddItem(item);
				}
                path.Add(new AINodePath(collection.Items.Count,ischooseLeft,i));
			}
		}
        foreach(var node in path)
        {
            Debug.Log(node.ToString());
        }
	
    }
    public void AIPutItemToCart(Tile from,Tile to)
    {
        var items=from.Items;
		foreach (var item in items)
		{
			to.AIAddItem(item);
		}
		from.Items.Clear();
    }

}
public class AINodePath
{
    int value;
    bool isChooseLeft;
    int indexTile;

	public AINodePath(int value, bool isChooseLeft, int indexTile)
	{
		this.value = value;
		this.isChooseLeft = isChooseLeft;
		this.indexTile = indexTile;
	}

	public override string ToString()
	{
        string Left = isChooseLeft ? "Left" : "Right";
        return indexTile + "; " + Left + "; " + value;
	}
} 
