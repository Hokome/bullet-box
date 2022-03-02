using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BulletBox
{
    public class Save
    { 
		public static readonly string path = $@"{Application.dataPath}/Save/scores.save";
		public static Save Current { get; private set; }

		public List<TimeSpan> scores;

		public Save()
		{
			Current = this;
			if (!File.Exists(path))
			{
				scores = new List<TimeSpan>();
				File.Create(path);
				return;
			}
			FileStream stream = new FileStream(path, FileMode.Open);
			if (stream.Length <= 0)
			{
				scores = new List<TimeSpan>();
				return;
			}
			BinaryReader reader = new BinaryReader(stream);

			scores = new List<TimeSpan>(reader.ReadInt32());

			for (int i = 0; i < scores.Capacity; i++)
			{
				scores.Add(TimeSpan.FromSeconds(reader.ReadSingle()));
			}
		}

		public void Write()
		{
			FileStream stream = new FileStream(path, FileMode.Truncate);
			BinaryWriter writer = new BinaryWriter(stream);

			writer.Write(scores.Count);
			for (int i = 0; i < scores.Count; i++)
			{
				writer.Write((float)scores[i].TotalSeconds);
			}
		}
	}
}
