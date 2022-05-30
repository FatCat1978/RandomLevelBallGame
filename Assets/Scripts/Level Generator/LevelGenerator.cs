using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
	// Start is called before the first frame update

	public static int tileSize = 4; //how many "unity units" per tile?
	public static int roomSize = 5; //RoomSize*RoomSize

	public GameObject StartRoom;
	public GameObject EndRoom;

	private bool AllowedToGenerate = false;

	public int desiredLevelComplexity = 15; //how many rooms does it take to get to the end?? does NOT include side tangents;
	public int desiredLevelTangents = 3;
	public int desiredTangentLength = 2;

	ArrayList AllRoomInfo = new ArrayList();

	public GameObject startNewBallHandler;

	public GameObject PLUS_ROOM;
	[Header("Room Templates")]	
	public GameObject SOUTH_NORTH;
	public GameObject NORTH_EAST;
	public GameObject NORTH_WEST;
	public GameObject SOUTH_EAST;
	public GameObject SOUTH_WEST;
	public GameObject WEST_EAST;

	[Header("Room Splits")]
	public GameObject WEST_EAST_NORTH;
	public GameObject WEST_EAST_SOUTH;
	public GameObject NORTH_SOUTH_EAST;
	public GameObject NORTH_SOUTH_WEST;

	[Header("Cap Rooms")]
	public GameObject CAP_NORTH;
	public GameObject CAP_EAST;
	public GameObject CAP_SOUTH;
	public GameObject CAP_WEST;

	[Header("Generated Rooms")]
	public GameObject generatedEndRoom;

	private int generationiters = 0; 

	public GameObject RoomTemplateFromDirs(ArrayList dirs) //TODO, think of a better system for this?? This seems horrendous.
	{
		bool exitNorth = false;
		bool exitSouth = false;
		bool exitWest = false;
		bool exitEast = false;

		int NumberOfExits = 0;

		//we don't do any iterations or loop shit here - keep it simple.
		if(dirs.Contains(Directions.North))
		{ 
			exitNorth = true;
			NumberOfExits++;
		}
		if (dirs.Contains(Directions.East))
		{ 
			exitEast = true;
			NumberOfExits++;
		}
		if (dirs.Contains(Directions.South))
		{ 
			exitSouth = true;
			NumberOfExits++;
		}
		if (dirs.Contains(Directions.West))
		{ 
			exitWest = true;
			NumberOfExits++;
		}

		switch(NumberOfExits) //nested switches :(
		{ 	
			case 0:
				{
					print("Zero dirs!!");
					return null; //this'll cause errors. good. 
				}
			case 1: //easy stiff jere/
				{
					if (exitNorth)
						return CAP_NORTH;
					if (exitEast)
						return CAP_EAST;
					if (exitWest)
						return CAP_WEST;
					if(exitSouth)
						return CAP_SOUTH;
				break ;
				}
			case 2:
				{
					if (exitSouth && exitNorth)
						return SOUTH_NORTH;
					if(exitSouth && exitEast)
						return SOUTH_EAST;
					if (exitSouth && exitWest)
						return SOUTH_WEST;
					if (exitEast && exitWest)
						return WEST_EAST;
					if (exitNorth && exitWest)
						return NORTH_WEST;
					if (exitNorth && exitEast)
						return NORTH_EAST;
					break;
				}
			case 3:
				{
					if (exitEast && exitWest && exitNorth)
						return WEST_EAST_NORTH;
					if (exitEast && exitWest && exitSouth)
						return WEST_EAST_SOUTH;
					if (exitNorth && exitSouth && exitEast)
						return NORTH_SOUTH_EAST;
					if (exitNorth && exitSouth && exitWest)
						return NORTH_SOUTH_WEST;
					break;
				}
			case 4:
				{
					return PLUS_ROOM;
				}
			default:
				{
					break;
				}

		}
		String dirsstr = "";
		
		foreach(Directions dir in dirs)
		{
			dirsstr += dir.ToString() + ",";
		}

		print("Dirs:" + dirs.Count + "Uh oh! returning null with this. " + dirsstr);

		return null;


	} //Arraylist Input??/

	void Start()
	{
		VerifyRoomSets();
		GenerateLevel();
	}

	private void VerifyRoomSets()
	{
		AllowedToGenerate = true;

		bool issues = false;
		if (StartRoom == null)
			issues = true;
		if (EndRoom == null)
			issues = true;

		if (issues)
			AllowedToGenerate = false;
		else
			AllowedToGenerate = true;

	}

	public enum Directions
	{
		North, //XPositive
		East, //ZNeg
		South, //XNeg
		West //Zpos
	}

	private void GenerateLevel()
	{
		if (!AllowedToGenerate)
		{
			print("Good job idiot!");
			return;
		}

		Vector2Int currentCoords = new Vector2Int(0, 1);

		//forst off, we generate a path - no directions or anything, just a grid.
		//only can move east/west/north - south can't happen except for tangents
		
		Directions lastNextDir = Directions.North; //what this is is the direction the LAST room had to take in order to get to the current one!
		Debug.Log("Desired Complexity is: " + desiredLevelComplexity);
		for (int i = 0; i < desiredLevelComplexity; i++)
		{
			RoomGenInfo newRoom = new RoomGenInfo();
			newRoom.x = currentCoords.x;
			newRoom.y = currentCoords.y;

			Directions prevRoomDir = InverseDir(lastNextDir);
			newRoom.exits.Add(prevRoomDir);

			ArrayList possibleNextDirs = getAllDirsList();

			possibleNextDirs.Remove(prevRoomDir); //we CAN'T physically go the direction we just came from!
			if (possibleNextDirs.Contains(Directions.South)) //and we CANT go soutH!
				possibleNextDirs.Remove(Directions.South); //so we remove it!
			//now we check to make sure it's not possible to return to 00
			Vector2Int nonoZone = new Vector2Int(0, 1);
			foreach(Directions PosNextDir in possibleNextDirs)
			{
				nonoZone = StepInDir(PosNextDir, currentCoords);
				if (nonoZone.Equals(new Vector2Int(0, 0))) 
					possibleNextDirs.Remove(PosNextDir);
			}

			//remove the last dir we went?? nah

			//alright, now we just choose out of the possible directions to go
			int spot = Random.Range(0, possibleNextDirs.Count);
			Directions nextDir = (Directions)possibleNextDirs[spot];
			newRoom.exits.Add((Directions)nextDir);
			lastNextDir = nextDir;
			currentCoords = StepInDir(nextDir, currentCoords);
			AllRoomInfo.Add(newRoom);
		}
		//spawn the exit.
		Vector2Int EndCoords = currentCoords;
		Directions EndEntranceGaurenteed = InverseDir(lastNextDir);



		ArrayList ChooseTangentsFrom = (ArrayList)AllRoomInfo.Clone();
		ArrayList ChosenTangentSpawns = new ArrayList();
		//pick 3 random ones!
		for(int i = 0; i < desiredLevelTangents; i++)
		{
			int ind = Random.Range(0, ChooseTangentsFrom.Count);
			ChosenTangentSpawns.Add(ChooseTangentsFrom[ind]);
			ChooseTangentsFrom.RemoveAt(ind);
		}

		//with these three indexes: at this pt it's just a single path! no tangents!
		
		//so, at every tangent start pt, we generate a tangent. these should all end with a cap!
		foreach(RoomGenInfo tangentStart in ChosenTangentSpawns)
		{
			Vector2Int TngentStartPt = tangentStart.getCoordsAsVector();
			//alright, pick a direction that we're not already headed.
			ArrayList allDirs = getAllDirsList();
			foreach(Directions dir in tangentStart.exits)
			{
				allDirs.Remove(dir);
			}
			

			if(allDirs.Count == 0) continue; //shouldn't ever happen??

			int DirectionInd = Random.Range(0, allDirs.Count);
			Directions TangentDirectionPriority = (Directions)allDirs[DirectionInd];
			tangentStart.exits.Add(TangentDirectionPriority); //add a dir to the exits!
			
			for (int i = 0; i < desiredTangentLength-1; i++)
			{
				TngentStartPt = StepInDir(TangentDirectionPriority, TngentStartPt);
				RoomGenInfo newTangentRoom = new RoomGenInfo();
				//set loc
				newTangentRoom.x = TngentStartPt.x;
				newTangentRoom.y = TngentStartPt.y;

				//add the exit we KNOW we're gonna have
				newTangentRoom.exits.Add(InverseDir(TangentDirectionPriority)); //we just came from here.
				ArrayList possibleTangentDirs = getAllDirsList();
				possibleTangentDirs.Remove(InverseDir(TangentDirectionPriority));
				Vector2Int nonoZone = new Vector2Int(1, 1);
				foreach (Directions PosNextDir in possibleTangentDirs)
				{
					nonoZone = StepInDir(PosNextDir, currentCoords);
					if (nonoZone.Equals(new Vector2Int(0, 0)))
						possibleTangentDirs.Remove(PosNextDir);
				}

				if(possibleTangentDirs.Count > 0)
				{ 
					int DirPick = Random.Range(0, possibleTangentDirs.Count); //random index

					Directions NextDir = (Directions)possibleTangentDirs[DirPick];
					newTangentRoom.exits.Add(NextDir);
					TangentDirectionPriority = NextDir;
					AllRoomInfo.Add(newTangentRoom);
				}
				//no preference for direction! can't implicitly spawn + or T rooms, but there's zero preference for direction other than that. we don't even check to see if we're going in circles.
				//only thing is that the room we just generated can't go BACK to where we just came from. ofc we want to at least start facing away, hence the earlier schmoo
			}
			//now that we've made the tangent...
			TngentStartPt = StepInDir(TangentDirectionPriority, TngentStartPt);
			RoomGenInfo TangentEndCap = new RoomGenInfo();
			TangentEndCap.x = TngentStartPt.x;
			TangentEndCap.y = TngentStartPt.y;
			TangentEndCap.exits.Add(InverseDir(TangentDirectionPriority)); //inverse?
			AllRoomInfo.Add(TangentEndCap);


			//spawn a cap


		}

		ValidateAndMerge();

		//after that? merge everything.

		//special behavior for the start room.


		//TEST CODE
		print("Spawning rooms....");
		GameObject startRoom = Instantiate(StartRoom);

		startRoom.transform.parent = this.transform;
		ArrayList ZeroZeroCoords = getRoomsFromCoords(new Vector2Int(0, 0));
		if(ZeroZeroCoords.Count > 0)
		{ 
			RoomGenInfo SpawnRoomInfo = (RoomGenInfo)getRoomsFromCoords(new Vector2Int(0,0))[0];
			AllRoomInfo.Remove(SpawnRoomInfo);

			//no pt checking for north.
			if (SpawnRoomInfo.exits.Contains(Directions.East))
			{
				GameObject wall = startRoom.transform.Find("WALL_EAST").gameObject;
				wall.SetActive(false);

			}
			if (SpawnRoomInfo.exits.Contains(Directions.South))
			{
				GameObject wall = startRoom.transform.Find("WALL_SOUTH").gameObject;
				wall.SetActive(false);

			}
			if (SpawnRoomInfo.exits.Contains(Directions.West))
			{
				GameObject wall = startRoom.transform.Find("WALL_WEST").gameObject;
				wall.SetActive(false);

			}
		}
		//we always remove it!
		GameObject wallN = startRoom.transform.Find("WALL_NORTH").gameObject;
		wallN.SetActive(false);

		//now the end.
		GameObject theEnd = Instantiate(EndRoom);
		generatedEndRoom = theEnd;
		theEnd.transform.parent = this.gameObject.transform;
		theEnd.transform.position = new Vector3(currentCoords.x * tileSize * roomSize, 0, currentCoords.y * tileSize * roomSize);

		ArrayList EndCoordOverlap = getRoomsFromCoords(EndCoords);

		if (EndCoordOverlap.Count > 0)
		{
			RoomGenInfo EndRoomInfo = (RoomGenInfo)EndCoordOverlap[0];
			AllRoomInfo.Remove(EndRoomInfo);

			//no pt checking for north.
			if (EndRoomInfo.exits.Contains(Directions.East))
			{
				GameObject wall = theEnd.transform.Find("WALL_EAST").gameObject;
				wall.SetActive(false);

			}
			if (EndRoomInfo.exits.Contains(Directions.South))
			{
				GameObject wall = theEnd.transform.Find("WALL_SOUTH").gameObject;
				wall.SetActive(false);

			}
			if (EndRoomInfo.exits.Contains(Directions.West))
			{
				GameObject wall = theEnd.transform.Find("WALL_WEST").gameObject;
				wall.SetActive(false);

			}
		}
		if(EndEntranceGaurenteed == Directions.North)
		{
			GameObject wall = theEnd.transform.Find("WALL_NORTH").gameObject;
			if (wall != null)
				wall.SetActive(false);
		}
		if(EndEntranceGaurenteed == Directions.East)
		{
			GameObject wall = theEnd.transform.Find("WALL_EAST").gameObject;
			if (wall != null)
				wall.SetActive(false);
		}
		if (EndEntranceGaurenteed == Directions.South)
		{
			GameObject wall = theEnd.transform.Find("WALL_SOUTH").gameObject;
			if (wall != null)
				wall.SetActive(false);
		}
		if (EndEntranceGaurenteed == Directions.West)
		{
			GameObject wall = theEnd.transform.Find("WALL_WEST").gameObject;
			if(wall != null)
				wall.SetActive(false);
		}

		//spawn 'em


		foreach(RoomGenInfo newRoom in AllRoomInfo)
		{
			Debug.Log("wow!");
			GameObject Hehehehaw = Instantiate(RoomTemplateFromDirs(newRoom.exits));
			Hehehehaw.transform.SetParent(this.transform);
			Hehehehaw.transform.name = "ROOM GENERATED IN ITERATION:" + generationiters;
			

			Hehehehaw.transform.position = new Vector3(newRoom.x * tileSize * roomSize, 0, newRoom.y*tileSize*roomSize);
			RoomInfo roomInfo = Hehehehaw.GetComponent<RoomInfo>();
			roomInfo.x = newRoom.x;
			roomInfo.y = newRoom.y;
		}

		//after that, we "trim" - this converts the path to directions
		//THEN, we generate the tangents.
		//Then, we spawn in the rooms.
	}

	private void ValidateAndMerge()
	{
		foreach(RoomGenInfo RoomInfo in (ArrayList)AllRoomInfo.Clone())
		{
			ArrayList conflicts = getRoomsFromCoords(new Vector2Int(RoomInfo.x, RoomInfo.y));
			if (conflicts.Count > 0)
			{
				RoomGenInfo replacementForConflicts = new RoomGenInfo();
				replacementForConflicts.x = RoomInfo.x;
				replacementForConflicts.y = RoomInfo.y;

				foreach(RoomGenInfo conflictedRoom in conflicts)
				{
					replacementForConflicts.exits.AddRange(conflictedRoom.exits);
					AllRoomInfo.Remove(conflictedRoom);
				}
				AllRoomInfo.Add(replacementForConflicts);
			}
		
		
		}
		//iterate over everything in the AllRooms list
		//get every "conflict"
		//add all the dirs from every "conflicted" room together
		//remove the conflicted rooms from the allrooms list, replace with the new "merged" one
		
	}


	public void delete_level()
	{
		foreach (Transform child in this.gameObject.transform)
		{
			UnityEngine.Object.Destroy(child.gameObject);

		}
		AllRoomInfo = new ArrayList();
	}

	public void RegenerateLevel()
	{
		delete_level();

		GenerateLevel();


	}

	public ArrayList getRoomsFromCoords(Vector2Int coordsOfRoom) //todo, return an array of X size
	{
		ArrayList foundRooms = new ArrayList();
		foreach(RoomGenInfo room in AllRoomInfo)
		{
			if (room.x == coordsOfRoom.x && room.y == coordsOfRoom.y)
				foundRooms.Add(room);
		}

		return foundRooms;
	}


	private Vector2Int StepInDir(Directions nextDir, Vector2Int coords)
	{
		switch(nextDir)
		{
			case Directions.North:
				coords.y++;
				return coords;
			case Directions.South:
				coords.y--;
				return coords;
			case Directions.East:
				coords.x++;
				return coords;
			case Directions.West:
				coords.x--;
				return coords;
		}
		return coords;
	}

	public static Directions InverseDir(Directions lastNextDir)
	{
		switch(lastNextDir)
		{
			case Directions.North: return Directions.South;
			case Directions.East : return Directions.West;
			case Directions.West : return Directions.East;
			case Directions.South: return Directions.North;
		}
		//hmm.
		return Directions.North;
	}

	private ArrayList getAllDirsList()
	{
		ArrayList returnValue = new ArrayList();
		returnValue.Add(Directions.North);
		returnValue.Add(Directions.East);
		returnValue.Add(Directions.West);
		returnValue.Add(Directions.South);
		return returnValue;
	}
}

public class RoomGenInfo
{
	//entrances
	public ArrayList exits = new ArrayList();
	public int x;
	public int y;

	public GameObject actualRoomObject;

	public Vector2Int getCoordsAsVector()
	{ 
		return new Vector2Int(x,y);
	}
}
