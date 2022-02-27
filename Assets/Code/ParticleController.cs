using System.Collections;
using UnityEngine;

namespace Code
{
	public class ParticleController : MonoBehaviour
	{
		[SerializeField] private Vector2 scaleRange;
		[SerializeField] private float time;

		private IEnumerator Start()
		{
			transform.localScale = Vector3.one * Random.Range(scaleRange.x, scaleRange.y);
			yield return new WaitForSeconds(time);
			Destroy(gameObject);
		}
	}
}