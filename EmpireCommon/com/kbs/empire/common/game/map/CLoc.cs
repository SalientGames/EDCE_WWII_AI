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

using System;
using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.common.game.map
{
    //Location X and Y
    public class CLoc
    {
        /////////////////////////////////////////////
        public int x;
        public int y;

        /////////////////////////////////////////////
        public CLoc(int xo, int yo)
        {
            x = xo;
            y = yo;
        }

        public CLoc(CLoc loc)
        {
            x = loc.x;
            y = loc.y;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool areEqual(CLoc other)
        {
            if (other == null) return false;
            return (x == other.x && y == other.y);
        }

        //////////////////////////////////////////
        public CLoc copy()
        {
            return new CLoc(x, y);
        }

        public void set(CLoc loc)
        {
            x = loc.x;
            y = loc.y;
        }

        public override string ToString()
        {
            return ("[" + x + ", " + y + "]");
        }

        public string getKey()
        {
            return (Convert.ToString(x) + "_" + Convert.ToString(y));
        }
        public static CLoc fromKey(string k)
        {
            int si = k.IndexOf("_", StringComparison.Ordinal);
            string sx = k.Substring(0, si);
            string sy = k.Substring(si + 1);

            return new CLoc(EncodeUtil.parseInt(sx), EncodeUtil.parseInt(sy));
        }
    }
}
