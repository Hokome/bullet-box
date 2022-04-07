using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class Weapon : MonoBehaviour, IShooter
    {
		[SerializeField] private string id;
		[SerializeField] private Sprite icon;
		[SerializeField] private string levelsPath;
		[SerializeField] private Projectile projectile;
		private ProjectilePattern pattern;

		private CSVReader levelTable;
		public CSVReader LevelTable
		{
			get => levelTable ??= new CSVReader(levelsPath);
		}

		private void Awake()
		{
			pattern = new ProjectilePattern
			{
				projectile = Instantiate(projectile, transform)
			};
			Level = 1;
		}

		private SpriteRenderer sr;
		public SpriteRenderer Sr
		{
			get
			{
				if (sr == null)
					sr = GetComponent<SpriteRenderer>();
				return sr;
			}
		}

		public string ID => id;
		public Sprite Icon => icon;

		private float lastShot;

		private int level;
		public int Level
		{
			get => level;
			set
			{
				level = value;
				SetLevel(value);
			}
		}
		public bool CanShoot => true;
		public Transform Transform => transform;
		public float AttackSpeed => 1f;
		public MonoBehaviour Behaviour => this;
		public bool IsMaxLevel => level >= LevelTable.height - 1;


		protected void SetLevel(int level) 
		{
			pattern.projectile.SetLevel(level, LevelTable);
			pattern.fireRate = LevelTable.ReadFloat("Fire rate", level);
			pattern.projectileCount = LevelTable.ReadInt("P Count", level);
			pattern.totalAngle = LevelTable.ReadFloat("Total Angle", level);
			pattern.imprecision = LevelTable.ReadFloat("Imprecision", level);
			pattern.spread = LevelTable.ReadFloat("Spread", level);
		}

		public void Fire()
		{
			if (Time.time - lastShot >= AttackSpeed / pattern.fireRate)
			{
				StartCoroutine(pattern.ShootOnce(this));
				lastShot = Time.time;
			}
		}
		public string GetNextLevelDescription()
		{
			return LevelTable.ReadString("Description", level + 1);
		}
	}
}
