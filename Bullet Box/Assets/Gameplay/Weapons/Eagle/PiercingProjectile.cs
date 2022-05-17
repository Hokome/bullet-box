using BulletBox.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class PiercingProjectile : Projectile
    {
		[SerializeField] private int pierce;
		[SerializeField] private float damageBonus;
		[SerializeField] private float bonusCap;
		[SerializeField] private float pitchMultiplier = 1f;
		//[Space]
		//[SerializeField] private float radius;
		//[SerializeField] private Vector2 offset;
		//[SerializeField] private int bufferSize = 8;

		//Used to avoid hitting the same object twice
		private List<IHittable> hits;

		//private Vector2 lastPos;
		private float damageMultiplier = 1f;
		private float pitchModifier = 1f;

		//private RaycastHit2D[] hitBuffer;

		//public Vector2 Position
		//{
		//	get
		//	{
		//		Vector2 xOffset = transform.right * offset.x;
		//		Vector2 yOffset = transform.up * offset.y;
		//		return xOffset + yOffset + Rb.position;
		//	}
		//}

		private void Awake()
		{
			//hitBuffer = new RaycastHit2D[bufferSize];
			hits = new List<IHittable>(pierce);
		}

		//protected override void Start()
		//{
		//	base.Start();
		//	lastPos = Position;
		//}

		//private void FixedUpdate()
		//{
		//	//Vector2 pos = Position;
		//	//Vector2 direction = pos - lastPos;

		//	//RaycastHit2D[] hits = Physics2D.CircleCastAll(lastPos, radius, direction, direction.magnitude, hitLayer | obstacleLayer);
		//	//for (int i = 0; i < hits.Length; i++)
		//	//{
		//	//	if (Utility.IsInLayerMask(hits[i].transform.gameObject.layer, hitLayer))
		//	//	{
		//	//		if (hits[i].collider.TryGetComponent(out IHittable h))
		//	//		{
		//	//			Hit(h);
		//	//		}
		//	//	}
		//	//	else
		//	//	{
		//	//		Obstacle();
		//	//	}
		//	//}

		//	//lastPos = pos;
		//}
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (Utility.IsInLayerMask(collision.gameObject.layer, hitLayer))
			{
				IHittable hit = collision.GetComponent<IHittable>();
				Hit(hit, null);
			}
			else if (Utility.IsInLayerMask(collision.gameObject.layer, obstacleLayer))
			{
				Obstacle(null);
			}
		}
		protected override void Hit(IHittable hit, Collision2D col)
		{
			if (hits.Contains(hit))
				return;
			else
				hits.Add(hit);

			hit.Hit(damage * damageMultiplier);
			damageMultiplier = Mathf.Min(bonusCap, damageMultiplier + damageBonus);
			pierce--;
			if (pierce <= 0)
				Destroy(gameObject);

			if (hitSound != null)
			{
				PlayOptions po = PlayOptions.Default;
				po.pitch *= pitchModifier;
				AudioManager.PlaySound(hitSound, transform.position, po);
				pitchModifier *= pitchMultiplier;
			}
		}

		//private void OnDrawGizmosSelected()
		//{
		//	Gizmos.color = Color.green;
		//	Gizmos.DrawWireSphere(Position, radius);
		//}
	}
}
