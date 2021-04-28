namespace TD
{
    using System.Collections.Generic;
    using UnityEngine;

    public class LootManager : Singleton<LootManager>
    {
        [SerializeField]
        private List<GameObject> lootOnMap;

        private int parts;

        public List<GameObject> Loot
        {
            get { return this.lootOnMap; }
        }

        public int Parts
        {
            get { return this.parts; }
        }

        public void CreateLoot(GameObject obj, Transform tsfm)
        {
            GameObject newLoot = Instantiate(obj, tsfm.position, Quaternion.identity);
            this.lootOnMap.Add(newLoot);
        }

        public void RemoveLoot(GameObject obj)
        {
            this.lootOnMap.Remove(obj);
            this.parts += obj.GetComponent<Loot_A>().resources;
            Destroy(obj);
        }

        private void Awake()
        {
            EnemyUnit.OnLootDropedEvent += CreateLoot;
            Collector.OnLootCollectedEvent += RemoveLoot;
        }

        private void Start()
        {
            this.lootOnMap = new List<GameObject>();
        }
    }
}
