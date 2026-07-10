using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MiniGameWorld.FlowerGame
{
    public class Board : MonoBehaviour
    {
        [SerializeField] int m_Width = 6;
        [SerializeField] int m_Height = 6;
        [SerializeField] float m_SpawnInterval = 3f;
        [SerializeField] Tile m_TilePrefab;
        [SerializeField] Flower m_FlowerPrefab;
        [SerializeField] Color m_LightColor = Color.white;
        [SerializeField] Color m_DarkColor = Color.gray;

        public event Action<Vector2Int> FlowerSpawned;
        public int Width => m_Width;
        public int Height => m_Height;

        Tile[,] m_Tiles;
        Coroutine m_SpawnCoroutine;
        private void Awake()
        {
            m_Tiles = new Tile[m_Width, m_Height];
            CreateBoard();
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

                    tile.Initialize(new Vector2Int(x, y));

                    Color color = GetTileColor(x, y);

                    tile.SetColor(color);

                    m_Tiles[x, y] = tile;
                }
            }
        }
        public void ResetBoard()
        {
            StopSpawn();

            ClearFlowers();
        }

        public void StartSpawn()
        {
            if (m_SpawnCoroutine != null)
                return;

            m_SpawnCoroutine = StartCoroutine(SpawnLoop());
        }
        public void StopSpawn()
        {
            if (m_SpawnCoroutine != null)
            {
                StopCoroutine(m_SpawnCoroutine);
                m_SpawnCoroutine = null;
            }
        }
        private void ClearFlowers()
        {
            foreach (Tile tile in m_Tiles)
            {
                if (!tile.HasFlower)
                    continue;

                Destroy(tile.Flower.gameObject);
                tile.RemoveFlower();
            }
        }
        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(m_SpawnInterval);

                SpawnFlower();
            }
        }
        private void SpawnFlower()
        {
            Tile tile = GetRandomEmptyTile();

            if (tile == null)
                return;

            Flower flower = Instantiate(
                m_FlowerPrefab,
                GridToWorld(tile.Position),
                Quaternion.identity,
                transform);

            tile.SetFlower(flower);
            FlowerSpawned?.Invoke(tile.Position);
        }
        private Tile GetRandomEmptyTile()
        {
            List<Tile> emptyTiles = new();

            foreach (Tile tile in m_Tiles)
            {
                if (!tile.HasFlower)
                    emptyTiles.Add(tile);
            }

            if (emptyTiles.Count == 0)
                return null;

            return emptyTiles[UnityEngine.Random.Range(0, emptyTiles.Count)];
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
                position.x < m_Width &&
                position.y < m_Height;
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
        public Tile GetTile(Vector2Int position)
        {
            return m_Tiles[position.x, position.y];
        }
    }
}