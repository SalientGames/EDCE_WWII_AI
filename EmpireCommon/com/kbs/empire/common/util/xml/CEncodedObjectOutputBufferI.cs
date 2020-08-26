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

namespace com.kbs.empire.common.util.xml
{
    public interface CEncodedObjectOutputBufferI
    {
        //Opens up a new element
        void openObject(string tag);
        //Addiging of attributes
        void addAttr(string a, string v);
        //adds a text element as a child
        void addTextObject(string tag, string text);
        //closes the element
        void objectEnd();
    }
}
