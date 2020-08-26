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

namespace com.kbs.empire.common.game.map
{
    //holds map stats
	public class CMapUtil
	{
        //width and wrap
        public readonly int width_;
        public readonly bool hwrap_;
        //height and wrap
        public readonly bool vwrap_;
        public readonly int height_;

	    public CMapUtil(int width, bool hwrap, int height, bool vwrap)
	    {
	        width_ = width;
	        hwrap_ = hwrap;
	        vwrap_ = vwrap;
	        height_ = height;
	    }

        //////////////////////////////////////////////////////////////////////
        //Directions definined
        //can be iterated NORTH to < NODIR
        public const int NORTH = 0;
        public const int NORTHEAST = 1;
        public const int EAST = 2;
        public const int SOUTHEAST = 3;
        public const int SOUTH = 4;
        public const int SOUTHWEST = 5;
        public const int WEST = 6;
        public const int NORTHWEST = 7;
        public const int NODIR = 8;

        //////////////////////////////////////////////////////////////////////
        //array for quick random direction fun
        public static int[] rdir_tab =
        {
            SOUTH, SOUTHWEST, WEST, NORTHWEST, NORTH, NORTHEAST, EAST, SOUTHEAST
        };

        //////////////////////////////////////////////////////////////////////
        //get a vertical coord adjustment based on direction
        public static int YDir(int dir)
        {
            switch (dir)
            {
                case NORTHWEST:
                case NORTHEAST:
                case NORTH:
                    return -1;
                case SOUTHWEST:
                case SOUTH:
                case SOUTHEAST:
                    return 1;
                default:
                    return 0;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //get a horizontal coord adjustment based on direction
        public static int XDir(int dir)
        {
            switch (dir)
            {
                case NORTHWEST:
                case SOUTHWEST:
                case WEST:
                    return -1;
                case NORTHEAST:
                case SOUTHEAST:
                case EAST:
                    return 1;
                default:
                    return 0;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //wrap adjust a horizontal coord
        public int hwash(int x)
        {
            int ux = x;
            if (hwrap_)
            {
                while (ux < 0)
                    ux += width_;
                while (ux >= width_)
                    ux -= width_;
            }
            return ux;
        }
        //////////////////////////////////////////////////////////////////////
        //wrap adjust a vertical coord
        public int vwash(int y)
        {
            int uy = y;
            if (vwrap_)
            {
                while (uy < 0)
                    uy += height_;
                while (uy >= height_)
                    uy -= height_;
            }
            return uy;
        }
        //////////////////////////////////////////////////////////////////////
        //Returns Location based on x/y shifts
        //Returns null if invalid
        public CLoc getLocationFromAdjustment(CLoc loc, int xAdj, int yAdj)
        {
            if (!validLoc(loc, xAdj, yAdj))
                return null;
            int ux = hwash(loc.x + xAdj);
            int uy = vwash(loc.y + yAdj);
            return new CLoc(ux, uy);
        }
        //////////////////////////////////////////////////////////////////////
        //Determines Validity of an X/Y shifted location
        public bool validLoc(CLoc loc, int xAdj, int yAdj)
        {
            return validLoc(loc.x + xAdj, loc.y + yAdj);
        }
        //////////////////////////////////////////////////////////////////////
        //Returns the validity of x/y coords
        public bool validLoc(int x, int y)
        {
            int ux = hwash(x);
            int uy = vwash(y);
            return (ux >= 0 && ux < width_ && uy >= 0 && uy < height_);
        }

        //////////////////////////////////////////////////////////////////////
        //Returns a location based on shifting from a dirextion and distance
        //Returns null if invalid
        public CLoc getLocationFromDir(CLoc L, int dir, int dist)
        {
            return getLocationFromDir(L.x, L.y, dir, dist);
        }
        public CLoc getLocationFromDir(int x, int y, int dir, int dist)
        {
            int ux = hwash(x);
            int uy = vwash(y);
            if (dist == 0 || dir == NODIR)
            {
                if (validLoc(ux, uy))
                    return washLoc(ux, uy);
            }
            switch (dir)
            {
                case NORTH:
                    uy -= dist;
                    break;
                case NORTHEAST:
                    uy -= dist;
                    ux += dist;
                    break;
                case NORTHWEST:
                    uy -= dist;
                    ux -= dist;
                    break;
                case SOUTH:
                    uy += dist;
                    break;
                case SOUTHWEST:
                    uy += dist;
                    ux -= dist;
                    break;
                case SOUTHEAST:
                    uy += dist;
                    ux += dist;
                    break;
                case WEST:
                    ux -= dist;
                    break;
                case EAST:
                    ux += dist;
                    break;
                //case NODIR:
                default:
                    return null;
            }
            if (validLoc(ux, uy))
                return washLoc(ux, uy);
            return null;

        }

        //////////////////////////////////////////////////////////////////////
        //returns shortest distance between two locations
        public int getLocRange(CLoc A, CLoc B)
        {
            return getLocRange(A.x, A.y, B.x, B.y);
        }
        //////////////////////////////////////////////////////////////////////
        //returns shortest distance between two coord sets
        public int getLocRange(int x1, int y1, int x2, int y2)
        {
            /* find the deltas */
            int dx = Math.Abs(x1 - x2);
            int dy = Math.Abs(y1 - y2);

            /* correct for map wrap */
            if (hwrap_ && dx > width_ / 2)
                dx = width_ - dx;
            if (vwrap_ && dy > height_ / 2)
                dy = height_ - dy;

            if (dx > dy)
            {
                return (dx);
            }
            else
            {
                return (dy);
            }
        }

        //////////////////////////////////////////////////////////////////////
        //returns List of all valid locations surrounding specifieed loc up to specified range
        public List<CLoc> getSurroundingRng(CLoc loc, int rng)
        {
            var ret = new List<CLoc>();
            if (rng == 0)
                return ret;


            int ec = 1;
            int sc = 2;
            int wc = 2;
            int nc = 2;


            int rc = 1;
            int x = loc.x;
            int y = loc.y;
            while (rc <= rng)
            {
                y--;//1N
                if (validLoc(x, y))
                    ret.Add(washLoc(x, y));
                //east
                for (int i = 0; i < ec; i++)
                {
                    x++;
                    if (validLoc(x, y))
                        ret.Add(washLoc(x, y));
                }
                //south
                for (int i = 0; i < sc; i++)
                {
                    y++;
                    if (validLoc(x, y))
                        ret.Add(washLoc(x, y));
                }
                //west
                for (int i = 0; i < wc; i++)
                {
                    x--;
                    if (validLoc(x, y))
                        ret.Add(washLoc(x, y));
                }
                //north
                for (int i = 0; i < nc; i++)
                {
                    y--;
                    if (validLoc(x, y))
                        ret.Add(washLoc(x, y));
                }

                rc++;
                ec += 2;
                sc += 2;
                wc += 2;
                nc += 2;
            }

            return ret;
        }
        //////////////////////////////////////////////////////////////////////
        //wrap adjusts a set of coords into a location
        public CLoc washLoc(int x, int y)
        {
            return new CLoc(hwash(x), vwash(y));
        }

        //////////////////////////////////////////////////////////////////////
        //Returns a direction based on coord sets from and to
        //range is assumed to be 1 (next to each other)
        public int getDirection(int fromx, int fromy, int tox, int toy)
        {
            int ufx = fromx;
            int ufy = fromy;
            int utx = tox;
            int uty = toy;

            int dirx = 0;
            int diry = 0;

            //edge cases
            if (hwrap_)
            {
                //from on the inner edge
                if (ufx == 0 && utx == width_ - 1)
                    ufx += width_;
                else if (utx == 0 && ufx == width_ - 1)
                    utx += width_;
            }
            if (vwrap_)
            {
                if (ufy == 0 && uty == height_ - 1)
                    ufy += height_;
                else if (uty == 0 && ufy == height_ - 1)
                    uty += height_;
            }

            dirx = utx - ufx;
            diry = uty - ufy;

            if (dirx == 0 && diry < 0)
                return NORTH;
            if (dirx > 0 && diry < 0)
                return NORTHEAST;
            if (dirx > 0 && diry == 0)
                return EAST;
            if (dirx > 0 && diry > 0)
                return SOUTHEAST;
            if (dirx == 0 && diry > 0)
                return SOUTH;
            if (dirx < 0 && diry > 0)
                return SOUTHWEST;
            if (dirx < 0 && diry == 0)
                return WEST;
            if (dirx < 0 && diry < 0)
                return NORTHWEST;
            return NODIR;
        }


        //////////////////////////////////////////////////////////////////////
        //got col/row or x/y, gimme  the location in array
        //Used for single array representation
        //where the map is laid out left to right, top to bottom
        public int XY2MAP(int x, int y)
        {
            int c = hwash(x);
            int r = vwash(y);
            return (((r) * width_) + (c));
        }
        public CLoc MAP2XY(int index)
        {
            int uindex = index;
            //will not normalize
            if (uindex < 0 || uindex >= height_ * width_) 
                return null;

            return new CLoc(uindex%width_, uindex/width_);
        }

    }
}
