using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Presenter
{
    public class MenuPresenter : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _exitButton;

        private void Start()
        {
            _backButton.OnClickAsObservable().Subscribe(_ => gameObject.SetActive(false));
            _exitButton.OnClickAsObservable().Subscribe(_ =>
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false);
#else
            Application.Quit());
#endif
        }
    }
}