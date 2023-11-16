using UnityEngine;

namespace PesPatron.Bundles
{
    public class LevelBanner : MonoBehaviour
    {
        [SerializeField] private Renderer _content;

        public void SetAdMaterial(Material material)
        {
            _content.sharedMaterial = material;
        }
    }
}