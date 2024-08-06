using UnityEngine;
using UnityEngine.UI;

namespace DT.Scripts.Utils
{
    public class ToggleExpandMenu : MonoBehaviour
    {
        [SerializeField] private RectTransform _menuContent;
        [SerializeField] private RectTransform _menu;

        [SerializeField]
        private Vector2[] _menuState =
        {
            new Vector2(0,70),
            new Vector2(300,70)
        };

        [SerializeField] private RectTransform _button;

        private bool _isExpanded = false;

        public void ToggleMenu()
        {
            _isExpanded = !_isExpanded;

            _menu.sizeDelta = _menuState[_isExpanded ? 1 : 0];
            _button.rotation = Quaternion.Euler(0, 0, _isExpanded ? 180 : 0);

            LayoutRebuilder.ForceRebuildLayoutImmediate(_menuContent);
        }
    }
}