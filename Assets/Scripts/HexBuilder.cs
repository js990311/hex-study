using UnityEngine;

public class HexBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject hexPrefab;
    
    private float SIZE = 0.5f; // 중심점부터 육각형의 꼭짓점까지의 거리 
    private float HORIZONTAL_SPACING = 1.5f;
    private float VERTICAL_SPACING = Mathf.Sqrt(3f);
    
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
        int s = -q - r;
        
        float x = 0f;
        float y = 0f;
        
        /*
         * q가 증가하는 방향 = 0H,1V  
         */
        y += VERTICAL_SPACING * q * SIZE;
        
        /*
         * R이 증가하는 방향 = -1H, -1/2V
         */
        x += -HORIZONTAL_SPACING * r * SIZE;
        y += -0.5f *VERTICAL_SPACING * r * SIZE;
        
        /*
         * S이 증가하는 방향 = -1H, 1/2V
         */
        x += -HORIZONTAL_SPACING * s * SIZE;
        y += 0.5f * VERTICAL_SPACING * s * SIZE;
        
        Vector3 position = new Vector3(x, y, 0);
        Instantiate(hexPrefab, position, Quaternion.identity, transform);
    }
}
