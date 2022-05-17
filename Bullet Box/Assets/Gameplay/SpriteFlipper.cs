using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class SpriteFlipper : MonoBehaviour
    {
		private Rigidbody2D rb;
		private bool flipped;
		private bool Flipped
		{
			get => flipped;
			set
			{
				flipped = value;
				transform.localScale = new Vector3(value ? -1f : 1f, 1f, 1f);
			}
		}

		private void Start()
		{
			rb = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			if (Flipped)
				if (rb.velocity.x > 0.05f)
					Flipped = false;
				else
				if (rb.velocity.x < 0.05f)
					Flipped = true;
		}
	}
}
