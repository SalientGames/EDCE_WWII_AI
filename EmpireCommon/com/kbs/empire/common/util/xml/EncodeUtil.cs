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

namespace com.kbs.empire.common.util.xml
{
    public class EncodeUtil
    {
        public static int parseInt(string s)
        {
            return int.Parse(s);
        }

        public static uint parseUInt(string s)
        {
            return uint.Parse(s);
        }

        public const string TRUE_STRING = "T";
        public const string FALSE_STRING = "F";

        public static bool fromBoolString(string boolStr)
        {
            if (boolStr == null)
                return false;

            return (TRUE_STRING == boolStr);
        }
        public static string makeBoolString(bool b)
        {
            return (b ? TRUE_STRING : FALSE_STRING);
        }

        public static int round(float f)
        {
            var w = (float)((int)f);
            if (Math.Abs(f - w) >= .5f)
            {
                if (f < 0)
                    w -= 1f;
                else
                    w += 1f;
            }
            return (int)w;
        }
        public static int round(double d)
        {
            var w = (double)((int)d);
            if (Math.Abs(d - w) >= .5)
            {
                if (d < 0)
                    w -= 1.0;
                else
                    w += 1.0;
            }
            return (int)w;
        }


        public static void encodeDSI(string tag, Dictionary<string, int> dict, CEncodedObjectOutputBufferI output)
        {
            output.openObject(tag);
            foreach (KeyValuePair<string, int> kvp in dict)
            {
                output.openObject(ENTRY);
                output.addAttr(VALUE, Convert.ToString(kvp.Value));
                output.addTextObject(KEY, kvp.Key);
                output.objectEnd();
            }

            output.objectEnd();
        }

        public static void decodeDSI(string tag, Dictionary<string, int> dict, CEncodedObjectInputBufferI bin)
        {
            bin.nextTag(tag);
            if (bin.hasChildren())
            {
                bin.firstChild();
                while (!bin.reachedEndTag(tag))
                {
                    bin.nextTag(ENTRY);
                    Dictionary<string, string> A = bin.getAttributes();

                    int v = parseInt(A[VALUE]);
                    bin.firstChild();
                    string k = bin.getObjectText(KEY);
                    bin.endTag(ENTRY);

                    dict.Add(k, v);

                }
            }

            bin.endTag(tag);
        }


        private const string ENTRY = "E";
        private const string KEY = "K";
        private const string VALUE = "V";
        public static void encodeStringList(string tag, List<string> list, CEncodedObjectOutputBufferI output)
        {
            output.openObject(tag);
            foreach (string s in list)
                output.addTextObject(ENTRY, s);

            output.objectEnd();
        }
        public static void decodeStringList(string tag, List<string> list, CEncodedObjectInputBufferI bin)
        {
            bin.nextTag(tag);
            if (bin.hasChildren())
            {
                bin.firstChild();
                while (!bin.reachedEndTag(tag))
                {
                    string s = bin.getObjectText(ENTRY);
                    list.Add(s);
                }
            }

            bin.endTag(tag);
        }

    }
}
