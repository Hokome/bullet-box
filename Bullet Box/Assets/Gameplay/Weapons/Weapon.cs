using BulletBox.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class Weapon : MonoBehaviour, IShooter
    {
		[SerializeField] private string id;
		[SerializeField] private Sprite icon;
		[SerializeField] private string description;
		[Space]
		[SerializeField] protected ProjectilePattern pattern;
		[SerializeField] private Crosshair crosshair;
		[SerializeField] private SFXClip fireSound;

		public string Description => description;
		private SpriteRenderer sr;
		public SpriteRenderer Sr
		{
			get
			{
				if (sr == null)
					sr = GetComponentInChildren<SpriteRenderer>();
				return sr;
			}
		}

		public string ID => id;
		public Sprite Icon => icon;
		public Crosshair Crosshair => crosshair;

		public bool CanShoot => true;
		public Transform Transform => transform;
		public float AttackSpeed => 1f;
		public MonoBehaviour Behaviour => this;

		protected float lastShot = float.NegativeInfinity;
		protected bool isFiring;

		private void Awake()
		{
			crosshair = Instantiate(crosshair);
		}
		protected virtual void OnEnable()
		{
			crosshair.gameObject.SetActive(true);
		}
		protected virtual void OnDisable()
		{
			if (crosshair != null)
				crosshair.gameObject.SetActive(false);
			isFiring = false;
		}

		protected virtual void Update()
		{
			TryShoot();
		}
		protected void TryShoot()
		{
			if (isFiring && CanShoot && Time.time - lastShot >= 1f / (pattern.fireRate * AttackSpeed))
			{
				PlayOptions p = PlayOptions.Default;
				p.RandomizeVolume(0.1f);
				p.RandomizePitch(0.05f);

				if (fireSound != null)
					AudioManager.PlaySound(fireSound, transform.position, p);
				StartCoroutine(pattern.ShootOnce(this));
				lastShot = Time.time;
			}
		}
		public virtual void OnFireDown()
		{
			isFiring = true;
		}
		public virtual void OnFireUp()
		{
			isFiring = false;
		}
	}
}
