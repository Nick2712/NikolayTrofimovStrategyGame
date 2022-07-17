using NikolayTrofimov_StrategyGame.Abstractions;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.View
{
    public class BottomCenterView : MonoBehaviour
    {
        public IObservable<int> CancelButtonClicks => _cancelButtonClicks;
        private Subject<int> _cancelButtonClicks = new Subject<int>();

        [SerializeField] private Image _currentUnitIcon;
        [SerializeField] private Image _productionProgressSlider;
        [SerializeField] private TextMeshProUGUI _currentUnitName;

        [SerializeField] private Image[] _images;
        [SerializeField] private GameObject[] _imageHolders;
        [SerializeField] private Button[] _buttons;

        private IDisposable _unitProductionTaskCt;

        [Inject]
        private void Init()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                var index = i;
                _buttons[i].onClick.AddListener(() => _cancelButtonClicks.OnNext(index));
            }
        }

        public void Clear()
        {
            for(int i = 0; i < _images.Length; i++)
            {
                _imageHolders[i].SetActive(false);
            }

            _productionProgressSlider.gameObject.SetActive(false);
            _currentUnitName.enabled = false;
            _unitProductionTaskCt?.Dispose();
        }

        public void SetTask(IUnitProductionTask task, int index)
        {
            if (task == null)
            {
                _imageHolders[index].SetActive(false);
                
                if (index == 0)
                {
                    _productionProgressSlider.gameObject.SetActive(false);
                    _currentUnitName.enabled = false;
                    _unitProductionTaskCt?.Dispose();
                }
            }
            else
            {
                _imageHolders[index].SetActive(true);
                _images[index].sprite = task.Icon;

                if (index == 0)
                {
                    _productionProgressSlider.gameObject.SetActive(true);
                    _currentUnitName.text = task.UnitName;
                    _currentUnitName.enabled = true;
                    _unitProductionTaskCt = Observable.EveryUpdate()
                        .Subscribe(_ =>
                        {
                            _productionProgressSlider.fillAmount = task.TimeLeft / task.ProductionTime;
                        });

                    _currentUnitIcon.sprite = task.Icon;
                }
            }
        }
    }
}