using UnityEngine;

public class HexTile : MonoBehaviour, IClickable
{
    private static Vector2Int[] neighbours = new Vector2Int[]
    {
        new Vector2Int(1,0), new Vector2Int(0,1), new Vector2Int(1,-1),
        new Vector2Int(-1,0), new Vector2Int(0,-1), new Vector2Int(-1,1),
    };
    
    public Vector2Int position;
    private SpriteRenderer spriteRenderer;
    private bool _pathable = true;
    private Player _player;

    public bool pathable
    {
        get
        {
            return _pathable;
        }
        private set
        {
            _pathable = value;
        }
    }

    void Start()
    {  
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initalize(int q, int r)
    {
        position = new Vector2Int(q, r);
    }
    
    public void OnClick()
    {
        Debug.Log(position.ToString());
        if (_player != null)
        {
            _player.OnClick();
            return;
        }
        if (pathable)
        {
            spriteRenderer.color = Color.black;
            pathable = false;
        }
        else
        {
            spriteRenderer.color = Color.white;
            pathable = true;
        }
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }
}
