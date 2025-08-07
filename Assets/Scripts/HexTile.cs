using UnityEngine;

public class HexTile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool _pathable = true;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
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
