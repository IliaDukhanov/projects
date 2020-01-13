using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatKingdoomRefactored
{
    class CompareScores : IComparer<Winners>
    {
        public int Compare(Winners o1, Winners o2)
        {
            if (o1.score > o2.score)
            {
                return 1;
            }
            else if (o1.score< o2.score)
            {
                return -1;
            }



            return 0;
        }
    }
}
