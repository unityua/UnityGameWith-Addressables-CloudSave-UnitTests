using UnityEngine;

namespace PesPatron.Bundles
{
    public class Banner : MonoBehaviour
    {
        [SerializeField] private Renderer _content;

        public void SetAdMaterial(Material material)
        {
            _content.sharedMaterial = material;
        }
    }
}