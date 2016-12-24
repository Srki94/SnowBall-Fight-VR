using UnityEngine;
using System.Collections;
using SnowMan.Data;

public static class GAMESESSION {

    public static ScoreData SCORE = new ScoreData();
    public static GameMgr.ControllerType controllerType = GameMgr.ControllerType.Touch;
    public static GameMgr.DiffModifier difficulty = GameMgr.DiffModifier.None;
    public static GameMgr.GameplayType gameplayType = GameMgr.GameplayType.VR;
}
