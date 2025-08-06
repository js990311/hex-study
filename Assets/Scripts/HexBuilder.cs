using UnityEngine;

public class HexBuilder : MonoBehaviour
{
    [SerializeField] private int x_left = -3;
    [SerializeField] private int x_right = 3;
    [SerializeField] private int y_top = -3;
    [SerializeField] private int y_bottom = 3;
    
    [SerializeField]
    private GameObject hexPrefab;
    
    private float SIZE = 0.5f; // 중심점부터 육각형의 꼭짓점까지의 거리

    private Vector3 Q_BASE_VECTOR = new Vector3(1.5f, -Mathf.Sqrt(3f) / 2, 0f);
    private Vector3 R_BASE_VECTOR = new Vector3(0f, -Mathf.Sqrt(3f), 0f);
    
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
    }

    void CreateHex(Vector2Int qr)
    {
        CreateHex(qr.x, qr.y);
    }
    
    void CreateHex(int q, int r)
    { 
        Vector3 position = SIZE * (q * Q_BASE_VECTOR + r * R_BASE_VECTOR);
        Instantiate(hexPrefab, position, Quaternion.identity, transform);
    }

    Vector2Int toAxial(int x, int y)
    {
        int parity = x & 1;
        int q = x;
        int r = y - (x - parity) / 2;
        return new Vector2Int(q, r);
    }
}
