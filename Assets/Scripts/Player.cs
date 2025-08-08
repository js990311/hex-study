using UnityEngine;

public class Player : MonoBehaviour, IClickable
{
    public Vector2Int position;
    [SerializeField]
    private int moveSpeed = 3;

    public void setPosition(Vector2Int position)
    {
        this.position = position;
    }
    
    public void OnClick()
    {
        HexBuilder.Instance().PathFind(position, moveSpeed);
        Debug.Log(position.ToString());
    }
}
