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

		public bool tutorialCompleted;
		public List<TimeSpan> freeplayScores;
		public List<TimeSpan> arcadeScores;

		public Save()
		{
			Current = this;
			if (!File.Exists(path))
			{
				//No file
				SetDefault();
				File.Create(path);
				return;
			}
			FileStream stream = new FileStream(path, FileMode.Open);
			if (stream.Length <= 0)
			{
				//Empty file
				SetDefault();
				return;
			}

			try
			{
				BinaryReader reader = new BinaryReader(stream);

				tutorialCompleted = reader.ReadBoolean();
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
			catch
			{
				Debug.LogError("Save file is corrupted, creating new save");
				SetDefault();
			}
		}

		public void Write()
		{
			SortScores();

			FileStream stream = new FileStream(path, FileMode.Truncate);
			BinaryWriter writer = new BinaryWriter(stream);

			writer.Write(tutorialCompleted);
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

		public void SortScores()
		{
			arcadeScores.Sort(ArcadeCompare);
			freeplayScores.Sort(FreeplayCompare);
		}
		private int ArcadeCompare(TimeSpan x, TimeSpan y)
		{
			if (x > y) return 1;
			if (x < y) return -1;
			return 0;
		}
		private int FreeplayCompare(TimeSpan x, TimeSpan y) => -ArcadeCompare(x, y);

		private void SetDefault()
		{
			tutorialCompleted = false;
			freeplayScores = new List<TimeSpan>();
			arcadeScores = new List<TimeSpan>();
		}
	}
}
