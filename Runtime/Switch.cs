using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Matty.Unity.UI {
    [RequireComponent(typeof(Toggle))]
    public class Switch : MonoBehaviour {
        [Header("General settings")]
        [SerializeField] private float switchDuration = 0.15f;
        [SerializeField] private float crossFadeDuration = 0.15f;
        [SerializeField] private AnimationCurve animationCurve;

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

        private int previousAnchorValue;
        private int targetAnchorValue;

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
            this.previousAnchorValue = value ? 0 : 1;
            this.targetAnchorValue = value ? 1 : 0;

            this.StartCoroutine(this.SwitchHandle());

            // Set color
            this.barImage.CrossFadeColor(value ? this.barColorOn : this.barColorOff, this.crossFadeDuration, false, true);
            this.handleImage.CrossFadeColor(value ? this.handleColorOn : this.handleColorOff, this.crossFadeDuration, false, true);
        }

        private IEnumerator SwitchHandle() {
            float curAnchorValue;
            float timeElapsed = 0;

            while (timeElapsed < this.switchDuration) {
                float progress = this.animationCurve.Evaluate(timeElapsed / this.switchDuration);

                curAnchorValue = Mathf.Lerp(this.previousAnchorValue, this.targetAnchorValue, progress);

                this.UpdateHandlePosition(curAnchorValue);

                timeElapsed += Time.deltaTime;

                yield return null;
            }

            curAnchorValue = this.targetAnchorValue;

            this.UpdateHandlePosition(curAnchorValue);
        }

        private void UpdateHandlePosition(float value) {
            this.handle.anchorMin = new Vector2(value, 0.5f);
            this.handle.anchorMax = new Vector2(value, 0.5f);
            this.handle.pivot = new Vector2(value, 0.5f);
        }
    }
}
