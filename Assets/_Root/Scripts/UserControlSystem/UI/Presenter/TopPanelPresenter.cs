using NikolayTrofimov_StrategyGame.Abstractions;
using System;
using TMPro;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.UI;


namespace NikolayTrofimov_StrategyGame
{
    public class TopPanelPresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textField;
        [SerializeField] private Button _menuButton;
        [SerializeField] private GameObject _menuGO;


        [Inject]
        private void Init(ITimeModel timeModel)
        {
            timeModel.GameTime.Subscribe(seconds =>
            {
                var t = TimeSpan.FromSeconds(seconds);
                _textField.text = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
            });

            _menuButton.OnClickAsObservable().Subscribe(_ => _menuGO.SetActive(true));
        }
    }
}