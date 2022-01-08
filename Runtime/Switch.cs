using UnityEngine;
using UnityEngine.UI;

namespace Matty.Unity.UI {
    [RequireComponent(typeof(Toggle))]
    public class Switch : MonoBehaviour {
        [Header("Bar settings")]
        [SerializeField] private RectTransform bar;

        [SerializeField] private Color barColorOn = Color.white;
        [SerializeField] private Color barColorOff = Color.grey;

        [Header("Handle settings")]
        [SerializeField] private RectTransform handle;

        [SerializeField] private Color handleColorOn = Color.white;
        [SerializeField] private Color handleColorOff = Color.grey;

        private Toggle toggle;
        private Image barImage;
        private Image handleImage;

        private void Awake() {
            if (this.bar != null) {
                this.barImage = this.bar.GetComponent<Image>();
            }

            if (this.handle != null) {
                this.handleImage = this.handle.GetComponent<Image>();
            }

            this.toggle = this.GetComponent<Toggle>();

            this.toggle.onValueChanged.AddListener(OnSwitch);

            // Make sure the switch is in the correct state, when initialized
            this.OnSwitch(this.toggle.isOn);
        }

        private void OnSwitch(bool value) {
            int valueAsInt = value ? 1 : 0;

            // Flip position
            this.handle.anchorMin = new Vector2(valueAsInt, 0.5f);
            this.handle.anchorMax = new Vector2(valueAsInt, 0.5f);
            this.handle.pivot = new Vector2(valueAsInt, 0.5f);

            // Set color
            this.barImage.color = value ? this.barColorOn : this.barColorOff;
            this.handleImage.color = value ? this.handleColorOn : this.handleColorOff;
        }
    }
}
