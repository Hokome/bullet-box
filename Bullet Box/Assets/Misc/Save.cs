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

		public List<TimeSpan> freeplayScores;
		public List<TimeSpan> arcadeScores;

		public Save()
		{
			Current = this;
			if (!File.Exists(path))
			{
				freeplayScores = new List<TimeSpan>();
				arcadeScores = new List<TimeSpan>();
				File.Create(path);
				return;
			}
			FileStream stream = new FileStream(path, FileMode.Open);
			if (stream.Length <= 0)
			{
				freeplayScores = new List<TimeSpan>();
				arcadeScores = new List<TimeSpan>();
				return;
			}
			BinaryReader reader = new BinaryReader(stream);
			freeplayScores = new List<TimeSpan>(reader.ReadInt32());
			for (int i = 0; i < freeplayScores.Capacity; i++)
			{
				freeplayScores.Add(TimeSpan.FromSeconds(reader.ReadSingle()));
			}
			arcadeScores = new List<TimeSpan>(reader.ReadInt32());
			for (int i = 0; i < arcadeScores.Capacity; i++)
			{
				arcadeScores.Add(TimeSpan.FromSeconds(reader.ReadSingle()));
			}
		}

		public void Write()
		{
			FileStream stream = new FileStream(path, FileMode.Truncate);
			BinaryWriter writer = new BinaryWriter(stream);

			writer.Write(freeplayScores.Count);
			for (int i = 0; i < freeplayScores.Count; i++)
			{
				writer.Write((float)freeplayScores[i].TotalSeconds);
			}
			writer.Write(arcadeScores.Count);
			for (int i = 0; i < arcadeScores.Count; i++)
			{
				writer.Write((float)arcadeScores[i].TotalSeconds);
			}
		}
	}
}
