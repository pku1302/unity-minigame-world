using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace MiniGameWorld.FlowerGame
{
    public class LaserSpawner : MonoBehaviour
    {
        [SerializeField]
        private Laser m_LaserPrefab;
        
        private Laser m_Laser;
        private LaserDirection m_Direction;

        public void Initialize(LaserDirection direction)
        {
            m_Direction = direction;
        }

        public void Fire()
        {
            StartCoroutine(FireRoutine());
        }

        private IEnumerator FireRoutine()
        {
            Warning();

            yield return new WaitForSeconds(0.8f);

            FireLaser();

            yield return new WaitForSeconds(0.3f);
        }
        private void Warning()
        {
            Debug.Log("Warning");
        }
        public void FireLaser()
        {
            Laser laser = Instantiate(
                m_LaserPrefab,
                transform.position,
                transform.rotation);

            laser.Initialize(6f, m_Direction);
            m_Laser = laser;
            Destroy(laser.gameObject, 1.3f);
        }
        void OnDisable()
        {
            if (m_Laser == null)
                return;

            Destroy(m_Laser.gameObject);   
            m_Laser = null;
        }
    }
}