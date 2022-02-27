using UnityEngine;

namespace Code
{
	public class ShootController : MonoBehaviour
	{
		[SerializeField] private BulletController bulletPrefabs;

		public float Delay => delay;
		[SerializeField] private float delay;
	
		public void Shoot(Vector3 direction, Vector3 position)
		{
			var bullet = Instantiate(bulletPrefabs, position, Quaternion.identity);
			bullet.Fire(direction);
		}
	}
}