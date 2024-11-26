using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Base
{
    public class OreScanner : MonoBehaviour
    {
        public bool Active;
        public float ScanRange;

        private List<Ore> FindAllOreInRange()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, ScanRange);
            List<Ore> founded = new List<Ore>();
            foreach (var hit in hits)
            {
                if (hit.gameObject.TryGetComponent<Ore>(out Ore ore))
                {
                    if (!ore.Engaged)
                    {
                        founded.Add(ore);
                    }
                }
            }
            return founded;
        }
        
        public Ore ClosestOre()
        {
            List<Ore> oreList = FindAllOreInRange();
            if (oreList.Count == 0)
            {
                return null;
            }
            Ore closest = oreList[0];
            foreach (Ore ore in oreList)
            {
                if (Vector3.Distance(closest.transform.position, transform.position) > Vector3.Distance(ore.transform.position, transform.position))
                {
                    closest = ore;
                }
            }

            return closest;
        }
    }
}