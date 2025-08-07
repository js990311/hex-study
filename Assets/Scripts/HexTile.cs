using UnityEngine;

public class HexTile : MonoBehaviour, IClickable
{
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
    
    public void OnClick()
    {
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
