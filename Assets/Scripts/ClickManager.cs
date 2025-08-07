using UnityEngine;

public class ClickManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject obj = hit.collider.gameObject;
                HexTile tile = obj.GetComponent<HexTile>();

                if (tile != null)
                {
                    tile.OnClick();
                } 
            }
        }

    }
}
