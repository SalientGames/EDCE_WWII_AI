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

using System.Collections.Generic;

namespace com.kbs.empire.common.util.xml
{
    public interface CEncodedObjectInputBufferI
    {
        //gets attributes in a dictionary
        //it is important to grab the attributes if need be before
        //advancing to a child
        Dictionary<string, string> getAttributes();

        //breaks if expected tag not found
        //it will skip data if the expected tag is not the next immediate element (be it child, peer, or parent)
        //will not move if the expect tag is the current from element
        void nextTag(string expectedTag);

        //will advance through the end of the specified element tag
        //will break if tag is not found
        //use to advance to next peer or parent
        void endTag(string expectedTag);

        //pulls text object of a simple text object
        //<tag>TEXT</tag>
        string getObjectText(string expectedTag);

        //string pullXml(string expectedTag); //returns null if not expected tag

        //current tag has children
        //only valid if at the frontof an element, otherwise will break
        bool hasChildren();

        //bool childIsNext(string expectedTag);

        //advances to next child
        //BREAKS if there are no children
        //use if you know there is a first child
        //returns child tag
        string firstChild();

        //returns true if the end element has been reached 
        //or if there is no seperate end element (no children)
        bool reachedEndTag(string tag); 


        //void openTextChild(string tag);//tape reading methods
        //string getNextToken(string skip, bool last);
        //void closeTextChild(string tag);

        //Checks as to which element you are looking at
        string thisTag(string expectedTag);//returns null if tape not at expected tag
        string thisTag();//returns current tag name, null if not on a tag
        
        


        // TODO make examples
        // Sample <XML A="a1" B="a2"><C1 CA="ca1"/><XML>
        //
        // output.openObject("XML");
        // output.addAttr("A", "a1");
        // output.addAttr("B", "a2");
        //      Then Add Child
        //      output.openObject("C1");
        //      output.addAttr("CA", "ca1");
        //      output.objectEnd();  //<-close C1
        // output.objectEnd(); //<-- close XML


    }
}
