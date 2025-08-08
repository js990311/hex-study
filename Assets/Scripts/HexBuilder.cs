using System.Collections.Generic;
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
}
