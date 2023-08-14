using UnityEngine;

namespace Scripts.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssets
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load(path);
            return Object.Instantiate(prefab) as GameObject;
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load(path);
            return Object.Instantiate(prefab, at, Quaternion.identity) as GameObject;
        }
    }
}