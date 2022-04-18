using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mini Game Data", menuName = "ScriptableObjects/MiniGameSystem/MiniGames/Hook")]
public class MiniGameData_Hook : MiniGameData
{
    public override MiniGame GetMiniGame()
    {
        return new MiniGame_Hook(this);
    }
}
