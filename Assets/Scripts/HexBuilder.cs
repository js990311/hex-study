using UnityEngine;

public class HexBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject hexPrefab;
    
    private float SIZE = 0.5f; // 중심점부터 육각형의 꼭짓점까지의 거리 

    private Vector3 Q_BASE_VECTOR = new Vector3(1.5f, -Mathf.Sqrt(3f) / 2, 0f);
    private Vector3 R_BASE_VECTOR = new Vector3(0f, -Mathf.Sqrt(3f), 0f);
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateHex(0,0);
        for (int q = -1; q <= 1; q++)
        {
            for (int r = -1; r <= 1; r++)
            {
                if (q == 0 && r == 0)
                {
                    continue;
                }
                CreateHex(q, r);
            }
        }
    }

    void CreateHex(int q, int r)
    { 
        Vector3 position = SIZE * (q * Q_BASE_VECTOR + r * R_BASE_VECTOR);
        Instantiate(hexPrefab, position, Quaternion.identity, transform);
    }
}
