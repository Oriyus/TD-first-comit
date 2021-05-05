namespace TD
{
    using System;
    using UnityEngine;

    public class Collector : MonoBehaviour
    {
        public Loot allLoot;

        public GameObjectEvent OnLootCollected = null;

        public float speed = 5f;
        public float rotSpeed = 5f;

        // Moving enemy unit on given path
        private void UnitPathFollow()
        {
            // Calculate closest loot
            int targetindex = 0;
            for (int i = 0; i < allLoot.Items.Count; i++)
            {
                if ((allLoot.Items[i].transform.position - this.transform.position).magnitude < 
                    (allLoot.Items[targetindex].transform.position - this.transform.position).magnitude)
                {
                    targetindex = i;
                }
            }

            // Move to position
            this.transform.position = Vector2.MoveTowards(this.transform.position, allLoot.Items[targetindex].transform.position, this.speed * Time.deltaTime);
            
            // Rotate towards target
            var dir = allLoot.Items[targetindex].transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * this.rotSpeed);
            
            //Check to see if loot reached
            if (this.transform.position == allLoot.Items[targetindex].transform.position)
            {
                // Reached index loot
                GameObject obj = allLoot.Items[targetindex];
                allLoot.Items.Remove(obj);
                this.OnLootCollected.Raise(obj);
                //this.parts += obj.GetComponent<Loot_A>().resources;
                Destroy(obj);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (allLoot.Items.Count > 0)
            {
                this.UnitPathFollow();
            }
        }
    }
}
