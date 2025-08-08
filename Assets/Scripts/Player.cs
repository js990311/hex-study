using UnityEngine;

public class Player : MonoBehaviour, IClickable
{
    public Vector2Int position;
    [SerializeField]
    private int moveSpeed = 3;

    private HexTile _tile;

    public void SetTile(HexTile tile)
    {
        if (_tile != null)
        {
            _tile.ClearPlayer();
        }
        _tile = tile;
        this.position = tile.position;
        transform.position = HexBuilder.Instance().positionToPixel(position);
    }
    
    public void OnClick()
    {
        HexBuilder.Instance().ClearPath();
        HexBuilder.Instance().PathFind(this, moveSpeed);
    }
}
