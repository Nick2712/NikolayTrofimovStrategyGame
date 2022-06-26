using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.UserControlSystem.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Presenter
{
    public sealed class BottomLeftPresenter : MonoBehaviour
    {
        [SerializeField] private Image _selectedImage;
        [SerializeField] private Image _healthSlider;
        [SerializeField] private Image _sliderBackground;
        [SerializeField] private TMP_Text _text;

        [SerializeField] private SelectableValue _selectedValue;


        private void Start()
        {
            _selectedValue.OnSelected += OnSelected;
        }

        private void OnSelected(ISelectable selected)
        {
            _selectedImage.enabled = selected != null;
            _healthSlider.gameObject.SetActive(selected != null);
            _sliderBackground.gameObject.SetActive(selected != null);
            _text.enabled = selected != null;

            if (selected != null)
            {
                _selectedImage.sprite = selected.Icon;
                _text.text = $"{selected.Health}/{selected.MaxHeath}";
                var healthSliderAmount = selected.Health / selected.MaxHeath;
                _healthSlider.fillAmount = healthSliderAmount;
                var color = Color.Lerp(Color.red, Color.green, healthSliderAmount);
                _healthSlider.color = color;
            }
        }
    }
}