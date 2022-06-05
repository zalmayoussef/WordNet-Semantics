using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace word_net
{
    class Program
    {
       public static void createRelation(int child, string[] parents, List<nodeGraph> myNodes)
        {
            for (int i = 0; i < parents.Length; i++)
            {
                myNodes[child].next.Add(int.Parse(parents[i]));
            }

        }
        public static int shortestCommonAncestor(int id1, int id2, int distance, List<nodeGraph> myNodes)
        {


            char mark = 'm';
            distance = 0;
            markParents(id1, mark, myNodes);
            mark = 'f';
            int sca = markParents(id2, mark, myNodes);
            int distance1 = myNodes[sca].levelFromParent1 + myNodes[sca].levelFromParent2;
            mark = 'u';
            markParents(id1, mark, myNodes);
            markParents(id2, mark, myNodes);



            mark = 'm';
            distance = 0;
            markParents(id2, mark, myNodes);
            mark = 'f';
            int sca2 = markParents(id1, mark, myNodes);
            int distance2 = myNodes[sca2].levelFromParent1 + myNodes[sca2].levelFromParent2;
            mark = 'u';
            markParents(id1, mark, myNodes);
            markParents(id2, mark, myNodes);
            distance = Math.Min(distance1, distance2);
            if (distance == distance1)
                return sca;
            else
                return sca2;

        }
        public static int shortestCommonAncestordistance(int id1, int id2,int distance, List<nodeGraph> myNodes)
        {
           

            char mark = 'm';
            distance = 0;
            markParents(id1, mark, myNodes);
            mark = 'f';
            int sca = markParents(id2, mark, myNodes);
            int distance1 = myNodes[sca].levelFromParent1 + myNodes[sca].levelFromParent2;
            mark = 'u';
            markParents(id1, mark, myNodes);
            markParents(id2, mark, myNodes);



            mark = 'm';
            distance = 0;
            markParents(id2, mark, myNodes);
            mark = 'f';
            int sca2 = markParents(id1, mark, myNodes);
            int distance2 = myNodes[sca2].levelFromParent1 + myNodes[sca2].levelFromParent2;
            mark = 'u';
            markParents(id1, mark, myNodes);
            markParents(id2, mark, myNodes);
            distance = Math.Min(distance1, distance2);
            if (distance == distance1)
                return distance;
            else
                return distance;

        }
        public static int markParents(int id, char mark, List<nodeGraph> myNodes)
        {
            Queue<int> Q = new Queue<int>();
            Q.Enqueue(id);
            while (Q.Count != 0)
            {

                int u = Q.Dequeue();
                if (mark == 'm')
                    myNodes[u].check = true;
                else if (mark == 'u')
                    myNodes[u].check = false;
                else if (myNodes[u].check == true && mark == 'f')
                    return myNodes[u].value;

                for (int i = 0; i < myNodes[u].next.Count; i++)
                {
                    int m = myNodes[u].next[i];
                    if (mark == 'm')
                    {
                        myNodes[m].check = true;
                        if (myNodes[m].levelFromParent1 == 0)
                        {
                            myNodes[m].levelFromParent1 = myNodes[u].levelFromParent1 + 1;
                        }

                        else if (myNodes[m].levelFromParent1 > myNodes[u].levelFromParent1 + 1)
                            myNodes[m].levelFromParent1 = myNodes[u].levelFromParent1 + 1;
                    }
                    else if (mark == 'u')
                    {
                        myNodes[m].check = false;
                        myNodes[m].levelFromParent1 = 0;
                        myNodes[m].levelFromParent2 = 0;
                    }
                    else if (myNodes[m].check == true && mark == 'f')
                    {

                        myNodes[m].levelFromParent2 = myNodes[u].levelFromParent2 + 1;
                        return myNodes[m].value;
                    }
                    else
                    {
                        myNodes[m].levelFromParent2 = myNodes[u].levelFromParent2 + 1;
                    }
                    Q.Enqueue(m);

                }
            }
            return -1;
        }
        //Map id to noun
        public static List<string> Map_ID_noun(Dictionary<int, string> dicID, int id)
        {
            //Dictionary<int,string[]> dict2 = new Dictionary<int,string[]>();
            List<string> xy = new List<string>();
            /* foreach (var item in dict.Keys)
             {

                 // string word= item.Value.ToString();
                 // string[] words = word.Split(' ');
                 if (item == id) {
                     foreach (var i in dict[item])
                     {
                         //if (id == int.Parse(item.[i]))
                         {
                             xy.Add(i);

                         }
                     }
                 }
                 //dict2.Add((item.Key), xy.ToArray());
             }
             */
            string[] word = dicID[id].Split(' ');
            foreach (var w in word)
            {
                xy.Add(w);
            }

            return xy;
        }

        //map noun to id
        public static List<int> Map_Noun_id(Dictionary<int, string> dict, string target)
        {
            List<int> xyz = new List<int>();

            foreach (var item in dict.Keys)
            {
                string[] words = dict[item].Split(' ');
                foreach (var word in words)
                {
                    if (target == word)
                    {
                        xyz.Add(item);
                        break;
                    }

                }
            }

            return xyz;
        }
       
        public static string[] shortestCommonAncestor_Nounsd(string Noun1, string Noun2,int distance, List<nodeGraph> myNodes, Dictionary<int, string> dicID)
        {

            List<int> Ids_1 =Map_Noun_id(dicID,Noun1);
            List<int> Ids_2 = Map_Noun_id(dicID,Noun2);
            string[] CommonSynsets;
            int MinDistance = 100000;
            int sca;
            int minSCA = 0;

            for (int i = 0; i < Ids_1.Count; i++)
            {
                for (int j = 0; j < Ids_2.Count; j++)
                {
                    sca = shortestCommonAncestor(Ids_1[i], Ids_2[j],distance,myNodes);
                    distance = shortestCommonAncestordistance(Ids_1[i], Ids_2[j], distance, myNodes);
                    if (MinDistance > distance)
                    {
                        MinDistance = distance;
                        minSCA = sca;
                    }
                }
            }
            CommonSynsets = Map_ID_noun(dicID,minSCA).ToArray();
            distance = MinDistance;
            Console.WriteLine("distance=" + distance);
            return CommonSynsets;
        }
        public static string[] shortestCommonAncestor_Nouns(string Noun1, string Noun2, int distance, List<nodeGraph> myNodes, Dictionary<int, string> dicID)
        {

            List<int> Ids_1 = Map_Noun_id(dicID, Noun1);
            List<int> Ids_2 = Map_Noun_id(dicID, Noun2);
            string[] CommonSynsets;
            int MinDistance = 100000;
            int sca;
            int minSCA = 0;

            for (int i = 0; i < Ids_1.Count; i++)
            {
                for (int j = 0; j < Ids_2.Count; j++)
                {
                    sca = shortestCommonAncestor(Ids_1[i], Ids_2[j], distance, myNodes);
                    distance = shortestCommonAncestordistance(Ids_1[i], Ids_2[j], distance, myNodes);
                    if (MinDistance > distance)
                    {
                        MinDistance = distance;
                        minSCA = sca;
                    }
                }
            }
            CommonSynsets = Map_ID_noun(dicID, minSCA).ToArray();
            distance = MinDistance;
            
            return CommonSynsets;
        }
        public static int shortestCommonAncestor_Nouns_distance(string Noun1, string Noun2, int distance, List<nodeGraph> myNodes, Dictionary<int, string> dicID)
        {

            List<int> Ids_1 = Map_Noun_id(dicID, Noun1);
            List<int> Ids_2 = Map_Noun_id(dicID, Noun2);
            string[] CommonSynsets;
            int MinDistance = 100000;
            int sca;
            int minSCA = 0;

            for (int i = 0; i < Ids_1.Count; i++)
            {
                for (int j = 0; j < Ids_2.Count; j++)
                {
                    sca = shortestCommonAncestor(Ids_1[i], Ids_2[j], distance, myNodes);
                    distance = shortestCommonAncestordistance(Ids_1[i], Ids_2[j], distance, myNodes);
                    if (MinDistance > distance)
                    {
                        MinDistance = distance;
                        minSCA = sca;
                    }
                }
            }
            CommonSynsets = Map_ID_noun(dicID, minSCA).ToArray();
            distance = MinDistance;

            return distance;
        }
        public static string outcast_word(List<string> inputs,int distance, List<nodeGraph> myNodes, Dictionary<int, string> dicID)
        {
            List<string> words = new List<string>();
            List<int> ids = new List<int>();

            for (int i = 0; i < inputs.Count(); i++)
            {
                int temp = 0;
                for (int j = 0; j < inputs.Count(); j++)
                {
                    if (i != j)
                    {
                     
                        //shortestCommonAncestor_Nouns(inputs[i], inputs[j],distance,myNodes,dicID);
                        distance = shortestCommonAncestor_Nouns_distance(inputs[i], inputs[j], distance, myNodes, dicID);
                        temp += distance;
                    }
                }

                ids.Add(temp);
                words.Add(inputs[i]);
            }
            int max = 0;
            int index = 0;
            for (int i = 0; i < ids.Count; i++)
            {
                if (max < Math.Max(max, ids[i]))
                {
                    max = Math.Max(max, ids[i]);
                    index = i;
                }
            }

            return words[index];

        }
        static void Main(string[] args)
        {
            List<string> hyper = File.ReadAllLines(@"D:\fcis\Ssemester 6\Algo\Labs\[StudentsVer] Lab7 - Project Release\[5] WordNet Semantics\Testcases\Complete\3-Large\Case2_82K_300K_1500RQ\Input\2hypernyms.txt").ToList();
            List<string> synset = File.ReadAllLines(@"D:\fcis\Ssemester 6\Algo\Labs\[StudentsVer] Lab7 - Project Release\[5] WordNet Semantics\Testcases\Complete\3-Large\Case2_82K_300K_1500RQ\Input\1synsets.txt").ToList();
            List<string> relation_query = File.ReadAllLines(@"D:\fcis\Ssemester 6\Algo\Labs\[StudentsVer] Lab7 - Project Release\[5] WordNet Semantics\Testcases\Complete\3-Large\Case2_82K_300K_1500RQ\Input\3RelationsQueries.txt").ToList();
            List<string> outcast = File.ReadAllLines(@"D:\fcis\Ssemester 6\Algo\Labs\[StudentsVer] Lab7 - Project Release\[5] WordNet Semantics\Testcases\Complete\3-Large\Case2_82K_300K_1500RQ\Input\4OutcastQueries.txt").ToList();
            Dictionary<int, string> dicID = new Dictionary<int, string>();
            Dictionary<int, string[]> adj = new Dictionary<int, string[]>();
            Dictionary<string, int> dicnoun = new Dictionary<string, int>();
            //1st: put synset file as dictionary of key IDs and Value of synsets
            foreach (var line in synset)
            {
                string[] words = line.Split(',');
                dicID.Add(int.Parse(words[0]), words[1]);
                //dicnoun.Add(words[1], int.Parse(words[0]));
            }
            int root;
            List<nodeGraph> myNodes;//basic nodes
            int distance;
            List<int> mydistance;

           
            //2nd: put hypernyms file as dictionary of  key IDs and adj List of neighbors 

            distance = 0;
            myNodes = new List<nodeGraph>();
            mydistance = new List<int>();
            foreach (var line in hyper)
            {
                string[] words = line.Split(',');
                if (words.ElementAtOrDefault(1) == null)
                {
                    
                    root = int.Parse(words[0]);
                    Console.WriteLine("root=" + root);
                }
                string[] updated = words.Skip(1).ToArray();
                adj.Add(int.Parse(words[0]), updated);
                nodeGraph newNode = new nodeGraph(int.Parse(words[0]));
                myNodes.Add(newNode);
            }

            foreach (var i in adj.Keys)
            {
                createRelation(i, adj[i],myNodes);
            }
            foreach (var contents in adj.Keys)
            {

                foreach (var listMember in adj[contents])
                {
                     Console.WriteLine("Key : " + contents + " adj :" + listMember);
                }
            }
         
            foreach (var r in relation_query)
            {
                if (r.Contains(','))
                {
                    string[] line = r.Split(',');
                    distance = 0;
                    string[] SCAnoun = shortestCommonAncestor_Nounsd(line[0],line[1],distance,myNodes,dicID);
                     for (int j = 0; j < SCAnoun.Length; j++)
                    {
                        Console.WriteLine(line[0]+","+line[1]);
                        Console.WriteLine("common ancestor is "+SCAnoun[j] + " ");
                    }
                }
            }
          
            foreach (var o in outcast)
            {
                if (o.Contains(','))
                {
                    List<string> inputs = new List<string>();
                    distance = 0;

                    string[] words = o.Split(',');
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (words.Length != 1)
                            inputs.Add(words[i]);
                    }
                    Console.WriteLine("outcast word is " + outcast_word(inputs, distance, myNodes, dicID));
                }
            }
            
            
        }
    }
}










