using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace word_net
{
    class nodeGraph
    {
        public int value;
        public List<int> next;
        public int levelFromParent1, levelFromParent2;
        public bool check;
        public nodeGraph(int id)
        {
            value = id;
            levelFromParent1 = 0;
            levelFromParent2 = 0;
            next = new List<int>();
            check = false;
        }
        public nodeGraph()
        {
            value = 0;
            levelFromParent1 = 0;
            levelFromParent2 = 0;
            next = new List<int>();
            check = false;
        }

    }
}
