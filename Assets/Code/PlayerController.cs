using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Code
{
	public class PlayerController : MyCharacterController
	{
		public static PlayerController Instance { get; private set; }

		[SerializeField] private ScreenTouchController input;
		[SerializeField] private ShootController shootController;

		private readonly List<Transform> _enemies = new();
		private bool _isShooting;


		private void Awake()
		{
			Instance = this;
		}


		private void FixedUpdate()
		{
			var direction = new Vector3(input.Direction.x, 0, input.Direction.y);
			Move(direction);
		}

		private void Update()
		{
			if (_enemies.Count > 0)
				transform.LookAt(_enemies[0]);
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.transform.CompareTag($"Enemy"))
			{
				Dead();
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag($"FinishPoint"))
			{
				OnReachSavePoint();
			}
		}

		private void OnTriggerStay(Collider other)
		{
			if (other.transform.CompareTag($"Enemy"))
			{
				if (!_enemies.Contains(other.transform))
					_enemies.Add(other.transform);

				AutoShoot();
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.transform.CompareTag($"Enemy"))
			{
				_enemies.Remove(other.transform);
			}
		}

		private void AutoShoot()
		{
			IEnumerator Do()
			{
				while (_enemies.Count > 0)
				{
					var enemy = _enemies[0];
					var myTransform = transform;
					var position = myTransform.position;
					var direction = enemy.transform.position - position;
					direction.y = 0;
					direction = direction.normalized;
					shootController.Shoot(direction, position);
					_enemies.RemoveAt(0);
					yield return new WaitForSeconds(shootController.Delay);
				}

				_isShooting = false;
			}

			if (!_isShooting)
			{
				_isShooting = true;
				StartCoroutine(Do());
			}
		}

		private void Dead()
		{
			GameManager.Instance.GameOver();
		}

		private void OnReachSavePoint()
		{
			GameManager.Instance.Win();
		}
	}
}