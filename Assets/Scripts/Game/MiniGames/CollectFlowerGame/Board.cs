using UnityEngine;

namespace MiniGameWorld.FlowerGame
{
    public class Board : MonoBehaviour
    {
        [SerializeField] int m_Width = 6;
        [SerializeField] int m_Height = 6;
        [SerializeField] GameObject m_TilePrefab;
        [SerializeField] Color m_LightColor = Color.white;
        [SerializeField] Color m_DarkColor = Color.gray;

        Tile[,] m_Tiles;

        public const int Width = 6;
        public const int Height = 6;

        private void Awake()
        {
            m_Tiles = new Tile[m_Width, m_Height];
            CreateBoard();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Vector2 origin = GetOrigin();
            
            for (int x = 0; x <= m_Width; x++)
            {
                Gizmos.DrawLine(
                    new Vector3(origin.x + x, origin.y, 0),
                    new Vector3(origin.x + x, origin.y + m_Height, 0));
            }

            for (int y = 0; y <= m_Height; y++)
            {
                Gizmos.DrawLine(
                    new Vector3(origin.x, origin.y + y, 0),
                    new Vector3(origin.x + m_Width, origin.y + y, 0));
            }
        }
        private void CreateBoard()
        {
            for (int y = 0; y < m_Height; y++)
            {
                for (int x = 0; x < m_Width; x++)
                {
                    Vector3 position = GridToWorld(new Vector2Int(x, y));

                    Tile tile = Instantiate(
                        m_TilePrefab,
                        position,
                        Quaternion.identity,
                        transform
                     ).GetComponent<Tile>();

                    Color color = GetTileColor(x, y);

                    tile.SetColor(color);

                    m_Tiles[x, y] = tile;
                }
            }
        }
        private Color GetTileColor(int x, int y)
        {
            return (x + y) % 2 == 0
                ? m_LightColor
                : m_DarkColor;
        }

        public bool IsInside(Vector2Int position)
        {
            return position.x >= 0 &&
                position.y >= 0 &&
                position.x < Width &&
                position.y < Height;
        }

        private Vector2 GetOrigin()
        {
            return new Vector2(
                -m_Width * 0.5f,
                -m_Height * 0.5f);
        }

        public Vector3 GridToWorld(Vector2Int position)
        {
            Vector2 origin = GetOrigin();

            return new Vector3(
                origin.x + position.x + 0.5f,
                origin.y + position.y + 0.5f,
                0f);
        }
    }
}