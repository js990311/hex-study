using UnityEngine;

public class Player : MonoBehaviour, IClickable
{
    [SerializeField]
    private int moveSpeed = 3;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("OnClick");
    }
}
