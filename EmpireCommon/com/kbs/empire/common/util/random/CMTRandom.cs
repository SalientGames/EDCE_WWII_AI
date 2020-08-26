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
using System.Collections.Generic;
using PseudoRandom;
using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.common.util.random
{
    //random class
    public class CMTRandom
    {
        private static readonly DateTime Jan1st1970 = new DateTime
            (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static ulong currentTimeMillis()
        {
            return (ulong) (DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        public const string SEED_TAG = "SEED";
        public const string USE_COUNT_ATTR = "UC";

        private readonly ulong seedUsed_;
        private int useCount_ = 0;

        private readonly CMersenneTwister twister_;

        public CMTRandom(ulong value)
        {
            seedUsed_ = value;
            twister_ = new CMersenneTwister(seedUsed_);
        }

        public CMTRandom()
        {
            seedUsed_ = currentTimeMillis();
            twister_ = new CMersenneTwister(seedUsed_);
        }

        public CMTRandom(string tag, CEncodedObjectInputBufferI bin)
        {
            bin.nextTag(tag);
            Dictionary<string, string> attr = bin.getAttributes();
            //with seeded constructor, run use count up to appropriate total
            int useCount = EncodeUtil.parseInt(attr[USE_COUNT_ATTR]);

            seedUsed_ = ulong.Parse(bin.getObjectText(SEED_TAG));
            twister_ = new CMersenneTwister(seedUsed_);
            for (int i = 0; i < useCount; i++)
                random(2);
            bin.endTag(tag);
        }


        public void encode(string tag, CEncodedObjectOutputBufferI output)
        {
            output.openObject(tag);
            output.addAttr(USE_COUNT_ATTR, Convert.ToString(useCount_));
            output.addTextObject(SEED_TAG, Convert.ToString(seedUsed_));
            output.objectEnd();
        }


        private void random(int i)
        {
            nextInt(i);
        }

        public int nextInt(int i)
        {
            useCount_++;
            return twister_.genrand_N(i);
        }

        public uint nextInt(uint i)
        {
            useCount_++;
            return (uint) twister_.genrand_N((int) i);
        }



        public string getSeedUsed()
        {
            return Convert.ToString(seedUsed_);
        }

        public uint threeD6()
        {
            return (nextInt(6u) + nextInt(6u) + nextInt(6u) + 3u);
        }

    }


}
