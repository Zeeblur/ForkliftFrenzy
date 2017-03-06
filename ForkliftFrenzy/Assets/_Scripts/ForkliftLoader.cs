using UnityEngine;
using System.Collections.Generic;

public struct ForkliftData
{
    public int speed;
    public int carryCap;
    public int price;
    public Object forkModel;
    public bool unlocked;
}

// Hard coded values for forklift. Possibly need to find a way to serialise this

public class ForkliftLoader : MonoBehaviour {


    private PlayerData playerData;

    public List<ForkliftData> forkliftList = new List<ForkliftData>();

    private Object[] prefabs;

	// Use this for initialization
	void Awake ()
    {
        // load models into prefabs
        prefabs = Resources.LoadAll("");//as GameObject[];

        // init redneck 
        ForkliftData speedy = new ForkliftData();
        speedy.speed = 10;
        speedy.carryCap = 20;
        speedy.price = 10000;
        speedy.forkModel = prefabs[(int)ForkLift.SPEEDY];
        speedy.unlocked = true;

        forkliftList.Add(speedy);

        ForkliftData redneck = new ForkliftData();
        redneck.speed = 4;
        redneck.carryCap = 50;
        redneck.price = 10000;
        redneck.forkModel = prefabs[(int)ForkLift.ENGIE];
        redneck.unlocked = true;

        forkliftList.Add(redneck);

        ForkliftData tank = new ForkliftData();
        tank.speed = 2;
        tank.carryCap = 100;
        tank.price = 10000;
        tank.forkModel = prefabs[(int)ForkLift.TANK];
        tank.unlocked = true;

        forkliftList.Add(tank);

        ForkliftData tricksy = new ForkliftData();
        tricksy.speed = 4;
        tricksy.carryCap = 50;
        tricksy.price = 10000;
        tricksy.forkModel = prefabs[(int)ForkLift.TRICKSY];
        tricksy.unlocked = true;

        forkliftList.Add(tricksy);

    }



}
