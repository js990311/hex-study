using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexBuilder : MonoBehaviour
{

    private static HexBuilder _instance;

    public static HexBuilder Instance()
    {
        return _instance;
    }
    
    [SerializeField] private int x_left = -3;
    [SerializeField] private int x_right = 3;
    [SerializeField] private int y_top = -3;
    [SerializeField] private int y_bottom = 3;
    
    [SerializeField]
    private GameObject hexPrefab;
    
    [SerializeField]
    private GameObject playerPrefab;
    
    private Dictionary<Vector2Int, HexTile> tilemap = new Dictionary<Vector2Int, HexTile>();
    private HashSet<Vector2Int> moveableTileSets = new HashSet<Vector2Int>();
    private Player focusedPlayer;

    
    private float SIZE = 0.5f; // 중심점부터 육각형의 꼭짓점까지의 거리

    private Vector3 Q_BASE_VECTOR = new Vector3(1.5f, -Mathf.Sqrt(3f) / 2, 0f);
    private Vector3 R_BASE_VECTOR = new Vector3(0f, -Mathf.Sqrt(3f), 0f);

    void Awake()
    {
        _instance = this;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int x = x_left; x <= x_right; x++)
        {
            for (int y = y_top; y <= y_bottom; y++)
            {
                CreateHex(toAxial(x,y));
            }
        }
        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, transform);
        Player component = player.GetComponent<Player>();
        tilemap[new Vector2Int(0,0)].SetPlayer(component);
        component.SetTile(tilemap[new Vector2Int(0,0)]);
    }

    void CreateHex(Vector2Int qr)
    {
        CreateHex(qr.x, qr.y);
    }
    
    void CreateHex(int q, int r)
    { 
        Vector3 position = SIZE * (q * Q_BASE_VECTOR + r * R_BASE_VECTOR);
        GameObject tile = Instantiate(hexPrefab, position, Quaternion.identity, transform);
        HexTile hexTile = tile.GetComponent<HexTile>();
        tilemap[new Vector2Int(q,r)] = hexTile; 
        hexTile.Initalize(q,r);
    }

    Vector2Int toAxial(int x, int y)
    {
        int parity = x & 1;
        int q = x;
        int r = y - (x - parity) / 2;
        return new Vector2Int(q, r);
    }

    public void PathFind(Player player, int speed)
    {
        moveableTileSets = new HashSet<Vector2Int>();
        
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        focusedPlayer = player;
        queue.Enqueue(player.position);

        for (int step = 1; step <= speed; step++)
        {
            Queue<Vector2Int> nextQueue = new Queue<Vector2Int>();

            while (queue.Count > 0)
            {
                Vector2Int now = queue.Dequeue();
                foreach (Vector2Int dir in HexTile.neighbours)
                {
                    Vector2Int next = now + dir;
                    if (!tilemap[next].pathable || moveableTileSets.Contains(next)) // 통과 불가능하거나 이미 들렸으면
                    {
                        continue;
                    }
                    else
                    {
                        tilemap[next].EnableMove();
                        moveableTileSets.Add(next);
                        nextQueue.Enqueue(next);
                    }
                }
            }
            queue = nextQueue;
        }
    }

    public void MovePlayer(HexTile tile)
    {
        Debug.Log("Move Player");
        tile.SetPlayer(focusedPlayer);
        focusedPlayer.SetTile(tile);
        ClearPath();
    }
    
    public void ClearPath()
    {
        Debug.Log("clear path");
        focusedPlayer = null;
        foreach (Vector2Int position in moveableTileSets)
        {
            tilemap[position].DisableMove();
        }
        moveableTileSets.Clear();
    }

    public bool isMovealbePath(Vector2Int position)
    {
        return moveableTileSets.Contains(position);
    }
    
    public Vector3 positionToPixel(Vector2Int position)
    {
        return SIZE * (position.x * Q_BASE_VECTOR + position.y * R_BASE_VECTOR);
    }
}
