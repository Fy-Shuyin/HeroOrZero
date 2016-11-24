using UnityEngine;
using System.Collections;
using System.IO;
using Mono.Data.Sqlite;

public class GameSystem
{
	private DBAccess sql;
    SqliteDataReader reader;

	public string[] Heros;

	public void HEROS()
	{
		Heros = new string[3];
		Heros[0] = "DinosaurEgg";
		Heros[1] = "DinosaurEgg";
	}
}
