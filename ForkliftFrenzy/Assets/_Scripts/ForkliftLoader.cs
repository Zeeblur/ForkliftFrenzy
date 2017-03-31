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

public class ForkliftLoader {

    private PlayerData playerData;

    public List<ForkliftData> forkliftList = new List<ForkliftData>();

    private Object[] prefabs;

    // contstructor with reference to player's data
    public ForkliftLoader(PlayerData pd)
    {
        playerData = pd;

        // load models into prefabs
        prefabs = Resources.LoadAll("");

        LoadForklifts();
    }

    void LoadForklifts()
    { 
        ForkliftData speedy = new ForkliftData();
        speedy.speed = 10;
        speedy.carryCap = 20;
        speedy.price = 10000;
        speedy.forkModel = prefabs[(int)ForkLift.SPEEDY];
        speedy.unlocked = playerData.IsForkUnlocked(ForkLift.SPEEDY);

        forkliftList.Add(speedy);

        ForkliftData redneck = new ForkliftData();
        redneck.speed = 4;
        redneck.carryCap = 50;
        redneck.price = 10000;
        redneck.forkModel = prefabs[(int)ForkLift.ENGIE];
        redneck.unlocked = playerData.IsForkUnlocked(ForkLift.ENGIE);

        forkliftList.Add(redneck);

        ForkliftData tank = new ForkliftData();
        tank.speed = 2;
        tank.carryCap = 100;
        tank.price = 10000;
        tank.forkModel = prefabs[(int)ForkLift.TANK];
        tank.unlocked = playerData.IsForkUnlocked(ForkLift.TANK);

        forkliftList.Add(tank);

        ForkliftData tricksy = new ForkliftData();
        tricksy.speed = 4;
        tricksy.carryCap = 50;
        tricksy.price = 10000;
        tricksy.forkModel = prefabs[(int)ForkLift.TRICKSY];
        tricksy.unlocked = playerData.IsForkUnlocked(ForkLift.TRICKSY);

        forkliftList.Add(tricksy);

    }



}
