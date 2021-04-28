namespace TD
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class LootManager : Singleton<LootManager>
    {
        [SerializeField]
        private List<GameObject> lootOnMap;

        public Action<int> OnPartsChanged;
        public Action<int> OnGoldChanged;

        private int parts;
        private int gold;

        public List<GameObject> Loot
        {
            get { return this.lootOnMap; }
        }

        public int Parts
        {
            get { return this.parts; }
        }

        public int Gold
        {
            get { return this.gold; }
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
            this.parts = 0;
            this.gold = 0;
        }

        private void Start()
        {
            this.lootOnMap = new List<GameObject>();
        }
    }
}
