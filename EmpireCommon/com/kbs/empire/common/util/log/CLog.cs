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
using System.IO;

namespace com.kbs.empire.common.util.log
{
    //used for AI player output
    public class CLog
    {
        private StreamWriter logFile_ = null;

        public const int INFO = 0;
        public const int ERROR = 1;

        public int logLevel_ = INFO;
        public CLog(string path, string logname, int logLevel)
        {
            logLevel_ = logLevel;
            if(path != null && logname != null)
            {
                try
                {
                    logFile_ = new StreamWriter(path + logname);
                    logFile_.WriteLine("Log Open: " + logname);
                    logFile_.Flush();
                }
                catch (Exception)
                {
                    logFile_ = null;
                }
            }
        }

        public void info(string s)
        {
            if(logFile_ != null && logLevel_ == INFO)
            {
                lock (logFile_)
                {
                    if (logFile_ != null)
                    {
                        logFile_.WriteLine(s);
                        logFile_.Flush();
                    }
                }
            }
        }

        public void infof(string s)
        {
            lock (logFile_)
            {
                if (logFile_ != null)
                {
                    logFile_.WriteLine(s);
                    logFile_.Flush();
                }
            }
        }

        public void endLog()
        {
            if(logFile_ != null)
            {
                lock (logFile_)
                {
                    if (logFile_ != null)
                    {
                        logFile_.WriteLine("Closing Log File");
                        logFile_.Close();
                        logFile_ = null;
                    }
                }
            }
        }

        public void info(string sname, Exception T)
        {
            if (logFile_ != null)
            {
                lock (logFile_)
                {
                    if (logFile_ != null)
                    {
                        logFile_.WriteLine(sname + " Exception: " + T);
                        logFile_.WriteLine(T.StackTrace);
                        logFile_.Flush();
                    }
                }
            }
        }
    }
}
