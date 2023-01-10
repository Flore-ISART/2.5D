using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PLAYERTWO.PlatformerProject
{
	[RequireComponent(typeof(Collider))]
	public class Sign : MonoBehaviour
	{
		[TextArea(15, 20)]
		public string text = "Hello World";
		public float scaleDuration = 0.25f;

		public RectTransform panel;
		public Text uiText;

		private Vector3 m_initialScale;

		private IEnumerator Scale(Vector3 from, Vector3 to)
		{
			var elapsedTime = 0f;
			var scale = panel.transform.localScale;

			while (elapsedTime < scaleDuration)
			{
				scale = Vector3.Lerp(from, to, (elapsedTime / scaleDuration));
				panel.transform.localScale = scale;
				elapsedTime += Time.deltaTime;
				yield return null;
			}

			panel.transform.localScale = to;
		}

		private void Awake()
		{
			uiText.text = text;
			m_initialScale = panel.transform.localScale;
			panel.transform.localScale = Vector3.zero;
			panel.gameObject.SetActive(true);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag(Tags.Player))
			{
				StopAllCoroutines();
				StartCoroutine(Scale(Vector3.zero, m_initialScale));
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag(Tags.Player))
			{
				StopAllCoroutines();
				StartCoroutine(Scale(panel.transform.localScale, Vector3.zero));
			}
		}
	}
}
