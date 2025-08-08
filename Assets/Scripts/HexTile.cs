using UnityEngine;

public class HexTile : MonoBehaviour, IClickable
{
    public static Vector2Int[] neighbours = new Vector2Int[]
    {
        new Vector2Int(1,0), new Vector2Int(0,1), new Vector2Int(1,-1),
        new Vector2Int(-1,0), new Vector2Int(0,-1), new Vector2Int(-1,1),
    };
    
    public Vector2Int position;
    private SpriteRenderer spriteRenderer;
    private bool _pathable = true;
    private Player _player;
    public bool moveable; 

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
        if (_player != null)
        {
            _player.OnClick();
            return;
        }

        if (HexBuilder.Instance().isMovealbePath(position))
        {
            Debug.Log("Player 이동가능");
            HexBuilder.Instance().MovePlayer(this);
        }
        else
        {
            Debug.Log("Player 이동불가능");
            HexBuilder.Instance().ClearPath();
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
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }
    
    public void EnableMove()
    {
        moveable = true;
        spriteRenderer.color = Color.cyan;
    }

    public void DisableMove()
    {
        spriteRenderer.color = Color.white;
        moveable = false;
    }

    public void ClearPlayer()
    {
        _player = null;
    }
}
