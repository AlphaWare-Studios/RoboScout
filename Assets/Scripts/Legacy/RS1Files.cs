using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class RS1Files : MonoBehaviour
{
	public static string[] LoadRS1Files(string Directory)
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream stream = new FileStream(Directory, FileMode.Open);

		PlayerData data = bf.Deserialize(stream) as PlayerData;

		stream.Close();
		return data.stats;
	}
}

public class PlayerStats
{
	public static string roundid;
	public static string saveid;
	public static string DataType;
	public static string option1;
	public static string option2;
	public static string option3;
	public static string option4;
	public static string option5;
	public static string option6;
	public static string option7;
	public static string comment;
	public static int filetype;
}

	[Serializable]
public class PlayerData
{

	public string[] stats;

	public PlayerData()
	{
		stats = new string[10];
		stats[0] = "RSFSV1";
		stats[1] = PlayerStats.DataType;
		stats[2] = PlayerStats.option1;
		stats[3] = PlayerStats.option2;
		stats[4] = PlayerStats.option3;
		stats[5] = PlayerStats.option4;
		stats[6] = PlayerStats.option5;
		stats[7] = PlayerStats.option6;
		stats[8] = PlayerStats.option7;
		stats[9] = PlayerStats.comment;
	}
}
