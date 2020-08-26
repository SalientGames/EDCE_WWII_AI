////////////////////////////////////////////////////////////////////////////////////////
//This Source Code File Is Part Of The 
//Empire Common DLL Assembly for 
//Empire Deluxe Combined Edition
//
//Copyright 2017 Mark Kinkead
//All rights reserved
//
//This is part of code included in the game
//you are not able to edit this code,
//but you should build the assembly and use it 
//as the API for World Building and AI Player
//Interfacing to the game with your custom code.
//
//Version Release Information Available
//In the file
//empire/version/Version.cs
//
//
////////////////////////////////////////////////////////////////////////////////////////

using System.Text;
using com.kbs.empire.common.game.map;

namespace com.kbs.empire.ai.common.map
{
    //Map Data At A Location
    public class CMapLocInfo
    {
        //The Loc
        public readonly CLoc loc_;
        //Terrain Value
        public readonly string terrain_;
        //gives icon value, for sea values > 0 indicate shore near
        public readonly int icon_;
        //has a road?
        public readonly bool road_;
        //number of resources
        public readonly uint resources_;
        //has a mine
        public readonly bool mine_;
        //is a wasteland sq
        public readonly bool wasteland_;

        public CMapLocInfo(CLoc loc, string terrain, int icon, bool road, uint resources, bool mine, bool wasteland)
        {
            loc_ = loc;
            terrain_ = terrain;
            icon_ = icon;
            road_ = road;
            resources_ = resources;
            mine_ = mine;
            wasteland_ = wasteland;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("{");
            sb.Append(loc_.ToString());
            if (terrain_ != null)
                sb.Append(" t: ").Append(terrain_).Append(" i:").Append(icon_);
            sb.Append(" r: ").Append(road_).Append(" res: ").Append(resources_).Append(" m: ").Append(mine_).Append(
                " w: ").Append(wasteland_).Append("}");
            

            return sb.ToString();
        }
    }
}
