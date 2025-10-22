using TMPro;
using UnityEngine;

namespace Homework.EventBus
{
    public sealed class TextWidget : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text;

        public void SetText(string value)
        {
            text.text = value;
        }
    }
}