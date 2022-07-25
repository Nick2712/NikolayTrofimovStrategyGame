using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Core
{
    public class ColorChanger : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Color _color;

        private void Start()
        {
            _renderer.material.color = _color;
        }
    }
}