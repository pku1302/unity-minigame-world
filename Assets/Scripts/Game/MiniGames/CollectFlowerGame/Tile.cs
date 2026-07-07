using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_SpriteRenderer;
    public Vector2Int Position { get; private set; }
    public void Initialize(Vector2Int position)
    {
        Position = position;
    }

    public void SetColor (Color color)
    {
        m_SpriteRenderer.color = color;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
