namespace TD
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LootManager : Singleton<LootManager>
    {
        private List<GameObject> lootOnMap;

        public List<GameObject> Loot
        {
            get { return this.lootOnMap; }
        }

        public void CreateLoot(GameObject obj, Transform tsfm)
        {
            GameObject newLoot = Instantiate(obj, tsfm.position, Quaternion.identity);
            this.lootOnMap.Add(newLoot);
        }

        public void RemoveLoot(int index)
        {
            GameManager.Instance.Parts = this.lootOnMap[index].GetComponent<Loot_A>().resources;
            Destroy(this.lootOnMap[index]);
            this.lootOnMap.RemoveAt(index);
        }

        private void Start()
        {
            this.lootOnMap = new List<GameObject>();
        }
    }
}
