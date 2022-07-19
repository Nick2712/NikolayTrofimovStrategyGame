using NikolayTrofimov_StrategyGame.Abstractions;
using System.Text;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Presenter
{
    public class GameOverScreenPresenter : MonoBehaviour
    {
        [Inject] private IGameStatus _gameStatus;

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _view;

        [Inject]
        private void Init()
        {
            _gameStatus.Status.ObserveOnMainThread().Subscribe(result =>
            {
                var sb = new StringBuilder($"Game over! ");
                if (result == 0) sb.AppendLine("Ничья!");
                else sb.AppendLine($"Победила партия №{result}");

                _view.SetActive(true);
                _text.text = sb.ToString();
                Time.timeScale = 0;
            });
        }
    }
}