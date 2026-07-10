using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MiniGameWorld.FlowerGame
{
    public enum LaserDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public class LaserSystem : MonoBehaviour
    {
        [SerializeField]
        private LaserSpawner m_LaserSpawnerPrefab;

        [SerializeField]
        private Board m_Board;

        [SerializeField]
        private float m_Interval = 3f;

        private LaserSpawner[] m_TopSpawners;
        private LaserSpawner[] m_BottomSpawners;
        private LaserSpawner[] m_LeftSpawners;
        private LaserSpawner[] m_RightSpawners;

        Coroutine m_Coroutine;

        void Awake()
        {
            m_TopSpawners = new LaserSpawner[m_Board.Width];
            m_BottomSpawners = new LaserSpawner[m_Board.Width];
            m_LeftSpawners = new LaserSpawner[m_Board.Height];
            m_RightSpawners = new LaserSpawner[m_Board.Height];

            CreateSpawners();
        }

        void Start()
        {
            StartSystem();
        }

        public void StartSystem()
        {
            if (m_Coroutine != null)
                return;

            m_Coroutine = StartCoroutine(FireLoop());
        }
        public void StopSystem()
        {
            if (m_Coroutine == null)
                return;

            StopCoroutine(m_Coroutine);
            m_Coroutine = null;
        }
        private IEnumerator FireLoop()
        {
            while (true)
            {
                FireRandom();

                yield return new WaitForSeconds(m_Interval);
            }
        }

        private void CreateSpawners()
        { 
            CreateTopSpawners();
            CreateBottomSpawners();
            CreateLeftSpawners();
            CreateRightSpawners();
        }

        private void CreateTopSpawners()
        {
            for (int x = 0; x < m_Board.Width; x++)
            {
                m_TopSpawners[x] = CreateSpawner(
                    new Vector2Int(x, m_Board.Height), 
                    LaserDirection.Down);
            }
        }

        private void CreateBottomSpawners()
        {
            for (int x = 0; x < m_Board.Width; x++)
            {
                m_BottomSpawners[x] = CreateSpawner(
                    new Vector2Int(x, -1),
                    LaserDirection.Up);
            }
        }

        private void CreateLeftSpawners()
        {
            for (int y = 0; y < m_Board.Height; y++)
            {
                m_LeftSpawners[y] =  CreateSpawner(
                    new Vector2Int(-1, y),
                    LaserDirection.Right);
            }
        }
        private void CreateRightSpawners()
        {
            for (int y = 0; y < m_Board.Height; y++)
            {
                m_RightSpawners[y] = CreateSpawner(
                    new Vector2Int(m_Board.Width, y),
                    LaserDirection.Left);
            }
        }

        private LaserSpawner CreateSpawner(Vector2Int gridPosition, LaserDirection direction)
        {
            LaserSpawner spawner = Instantiate(
                m_LaserSpawnerPrefab,
                m_Board.GridToWorld(gridPosition),
                GetRotation(direction),
                transform);

            spawner.Initialize(direction);

            return spawner;
        }

        private Quaternion GetRotation(LaserDirection direction)
        {
            return direction switch
            { 
                LaserDirection.Up => Quaternion.Euler(0, 0, 90),
                LaserDirection.Down => Quaternion.Euler(0, 0, -90),
                LaserDirection.Left => Quaternion.Euler(0, 0, 180),
                LaserDirection.Right => Quaternion.identity,
                _ => Quaternion.identity,
            };
        }
        public void FireRandom()
        {
            int side = Random.Range(0, 4);

            switch (side)
            {
                case 0:
                    m_TopSpawners[Random.Range(0, m_TopSpawners.Length)].Fire();
                    break;

                case 1:
                    m_BottomSpawners[Random.Range(0, m_BottomSpawners.Length)].Fire();
                    break;

                case 2:
                    m_LeftSpawners[Random.Range(0, m_LeftSpawners.Length)].Fire();
                    break;

                case 3:
                    m_RightSpawners[Random.Range(0, m_RightSpawners.Length)].Fire();
                    break;
            }

        }
    }
}