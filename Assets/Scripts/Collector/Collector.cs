namespace TD
{
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Collector : MonoBehaviour
    {
        public float speed = 5f;
        public float rotSpeed = 5f;

        // Moving enemy unit on given path
        private void UnitPathFollow()
        {
            // Calculate closest loot
            int targetindex = 0;
            for (int i = 0; i < LootManager.Instance.Loot.Count; i++)
            {
                if ((LootManager.Instance.Loot[i].transform.position - this.transform.position).magnitude < 
                    (LootManager.Instance.Loot[targetindex].transform.position - this.transform.position).magnitude)
                {
                    targetindex = i;
                }
            }
            // Move to position
            this.transform.position = Vector2.MoveTowards(this.transform.position, LootManager.Instance.Loot[targetindex].transform.position, this.speed * Time.deltaTime);
            // Rotate towards target
            var dir = LootManager.Instance.Loot[targetindex].transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * this.rotSpeed);
            //Check to see if loot reached
            if (this.transform.position == LootManager.Instance.Loot[targetindex].transform.position)
            {
                // Reached index loot
                LootManager.Instance.RemoveLoot(targetindex);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (LootManager.Instance.Loot.Count > 0)
            {
                this.UnitPathFollow();
            }
        }
    }
}
