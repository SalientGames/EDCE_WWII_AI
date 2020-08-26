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


namespace com.kbs.empire.common.util.random
{
    //an artifact of Mark Baldwin/Bob Rakosky code
    public class CProbTbl
    {
        public int n = 0;                   /* number of members in table               */
        public int tot = 0;             /* summation of weights of all members      */
        public int[] table;         /* pointer to list of bytes with weights    */

        private readonly CMTRandom random_;

        public CProbTbl(int size, CMTRandom random)
        {
            table = new int[size];
            n = size;
            random_ = random;
            init(0);
        }
        public int rand_tab()
        {
            /* get a random number */
            int r = random_.nextInt(tot);

            /* select the item */
            for (int i = 0; i < n; i++)
            {
                /* walk through the table */
                r -= table[i];
                if (r < 0)
                    return (i);
            }
            return (0);
        }
        public void recalc_ptab()
        {
            tot = 0;
            for (int i = 0; i < n; i++)
                tot += table[i];
        }
        public void init(int v)
        {
            for (int i = 0; i < table.Length; i++)
                table[i] = v;
        }

        public int getSize() { return n; }
        public void setWeight(int index, int wt)
        {
            table[index] = wt;
            recalc_ptab();
        }
        public void addWeight(int index, int wt)
        {
            table[index] += wt;
            tot += wt;
        }

        public int getWeight(int index)
        {
            return table[index];
        }

    }
}
