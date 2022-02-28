using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Originally from AssetFactory
namespace AssetFactory
{
	public class BufferedBoolean
	{
		public BufferedBoolean(float bufferDuration)
		{
			BufferDuration = bufferDuration;
			bufferedValue = true;
			Value = false;
			bufferTime = -1f;
		}

		public float BufferDuration { get; private set; } = 0.1f;
		public bool Value { get; private set; }

		bool bufferedValue;
		float bufferTime;

		public void Buffer(bool value)
		{
			bufferTime = Time.time;
			bufferedValue = value;
			Value = value;
		}

		public void Update()
		{
			if (bufferTime == -1f || Time.time - bufferTime > BufferDuration)
			{
				Value = !bufferedValue;
			}
		}

		public void Reset()
		{
			bufferTime = -1f;
			Update();
		}

		public override string ToString()
		{
			Update();
			if (Value == bufferedValue)
				return Value + " : " + (BufferDuration - (Time.time - bufferTime));
			else return Value.ToString();
		}

		public static implicit operator bool(BufferedBoolean b)
		{
			b.Update();
			return b.Value;
		}
	}
}