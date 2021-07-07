using Android.Opengl;
using MathNet;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinForms.LocationService.Services
{
    // bluetooth scanresult determines when neighbor matrix if (re) filled
    class NeighborMatrices
    {
        public Matrix<double> m;
        public static int OptimalNeighs = 0;
        public int ConwayRows { get; set; } = 0;
        public int ConwayCols { get; set; } = 0;
        public int ConwayDeath { get; set; } = 0;
        public int ConwayBirth { get; set; } = 0;
        public int ConwayRemain { get; set; } = 0;

        public int TotalNeighbors { get; set; } = 0;
        public NeighborMatrices()
        {
            #region starting_neigh_matrix
            /*
             *      Matrix m:
             *      
             *      | 0 0 0 |
             *      | 0 1 0 |     1 = this node, here with no bluetooth peers detected
             *      | 0 0 0 |
             *      
             *      [r,c] = [ 1 if neigh was found during this scan time interval, 0 otherwise]
             *      
             *      eg m => 
             *      [r(t),c(t)] = 1/0 (r,c) is occupied/unoccupied 
             *      filling goes in order of scanresult 0..7
             *      
             *      => row 1 is [1/0 scan(n), 1/0 scan(n), 1/0 scan(n)]
             *      => row 2 is [1/0 scan(n), 1==1,        1/0 scan(n)]
             *      => row 3 is [1/0 scan(n), 1/0 scan(n), 1/0 scan(n)]
             */
            #endregion
            m = Matrix<double>.Build.Dense(3, 3);
            m[1, 1] = 1;
        }
        public void SetNeighs(int recvFrom)
        {
            int i = recvFrom;    
            /*
             *     kvp for neighs
             * 
             *     i | r,c
             *     0 | 0,0
             *     1 | 0,1
             *     2 | 0,2
             *     3 | 1,0
             *     4 | 1,1 ...
             */
            switch (i)
            {
                case 0: m[0, 0] = 1; break;
                case 1: m[0, 1] = 1; break;
                case 2: m[0, 2] = 1; break;
                case 3: m[1, 0] = 1; break;
                case 4: m[1, 2] = 1; break;
                case 5: m[2, 0] = 1; break;
                case 6: m[2, 1] = 1; break;
                case 7: m[2, 2] = 1; break;
            }
        }
        //public void AppendTheseNeighborSsidsToYours()
        //{
        //    foreach(KeyValuePair<int,string> kvp in p2p.NeighPositionToSsid)
        //    {
        //        p2p.combinedSsids += DateTime.Now.ToString() + ":" + kvp.Value + "|";
        //    }
        //    p2p.NeighPositionToSsid.Clear();
        //}
        public int ComputeOptimalNumberNeighsToPropogate(int rows, int cols, int death, int birth, int remain, int totalNeighbors)
        {
            bool success = false;
            while (!success)
            {
                OptimalNeighs = 8;
                success = true;
            }
            return OptimalNeighs;
        }
        /*
         * conway >example< params
         * 
         * death = 4 => 4 neighbors detected => no self SSID append from those 4 neighbors
         * birth = 3 => 3 neighbors detected => self append SSID from those 3 neighbors
         * remain= 5 => 5 neighbors detected => keep same self SSID from previous iteration
         * 
         */
        public bool ComputeConwayResultOfHologramTraversalFitness(int rows, int cols, int death, int birth, int remain, int totalNeighbors)
        {
            // this is the optimization routine!
            ConwayRows = rows;
            ConwayCols = cols;
            ConwayDeath = death;
            ConwayBirth = birth;
            ConwayRemain = remain;
            TotalNeighbors = totalNeighbors;
            ComputeOptimalNumberNeighsToPropogate(ConwayRows, ConwayCols, ConwayDeath, ConwayBirth, ConwayRemain, TotalNeighbors);
            return true;
        }
    }
}
