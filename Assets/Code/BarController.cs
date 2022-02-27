using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
	public class BarController : MonoBehaviour
	{
		[SerializeField] private Image fillImage;
		[SerializeField] private TextMeshProUGUI percentText;

		public void Display(float normalized)
		{
			fillImage.fillAmount = normalized;
			percentText.SetText($"%{(int)(normalized * 100)}");
		}
	}
}