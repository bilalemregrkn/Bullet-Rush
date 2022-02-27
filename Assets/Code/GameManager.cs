using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance { get; private set; }
		public int EnemyAmount { get; set; }

		public int EnemyDeadCounter
		{
			get => _enemyDeadCounter;
			set
			{
				_enemyDeadCounter = value;
				barController.Display(SuccessValue);
			}
		}

		private int _enemyDeadCounter;

		private float SuccessValue => EnemyDeadCounter / (float)EnemyAmount;

		[SerializeField] private BarController barController;

		private void Awake()
		{
			Instance = this;
		}

		private IEnumerator Start()
		{
			yield return new WaitForSeconds(.2f);
			barController.Display(SuccessValue);
		}

		public void GameOver()
		{
			Debug.Log("Game Over");
			Time.timeScale = 0;
		}

		public void Win()
		{
			Debug.Log("Win");
			Time.timeScale = 0;
			var current = FindObjectsOfType<EnemyController>().Length;
			var result = current / (float)EnemyAmount;
			var success = Mathf.Lerp(100, 0, result);
		}
	}
}