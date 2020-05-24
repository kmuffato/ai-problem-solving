using System;
using System.Collections.Generic;

namespace ProblemSolving.BySearching.Strategy.Utilities
{
    public class BasicPriorityQueue<TK, TV>
    {
        private readonly SortedDictionary<TK, List<TV>> sortedDict;
        private readonly Func<TV, TK> evalFunc;

        public BasicPriorityQueue(Func<TV, TK> evalFunc)
        {
            this.evalFunc = evalFunc;

            sortedDict = new SortedDictionary<TK, List<TV>>();
        }

        public int Count
        {
            get { return sortedDict.Values.Count; }
        }

        public void Enqueue(TV obj)
        {
            var key = evalFunc(obj);

            if (!sortedDict.ContainsKey(key))
            {
                sortedDict.Add(key, new List<TV>());
            }

            sortedDict[key].Add(obj);
        }

        public TV Dequeue()
        {
            foreach (var item in sortedDict)
            {
                if (item.Value != null && item.Value.Count > 0)
                {
                    var obj = item.Value[0];
                    item.Value.RemoveAt(0);
                    return obj;
                }
            }

            throw new Exception();
        }

        public void ReplaceWith(TV oldObj, TV newObj)
        {
            var key = evalFunc(oldObj);

            if (sortedDict.ContainsKey(key) && sortedDict[key] != null)
            {
                sortedDict[key].Remove(oldObj);
            }

            Enqueue(newObj);
        }

        public TV CherryPeek(Predicate<TV> p)
        {
            foreach (var item in sortedDict)
            {
                if (item.Value == null || item.Value.Count == 0)
                {
                    continue;
                }

                var obj = item.Value.Find(p);

                if (obj != null)
                {
                    return obj;
                }
            }

            return default;
        }
    }
}
