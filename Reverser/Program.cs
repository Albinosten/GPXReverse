using System;
using System.Collections.Generic;
using System.Linq;

namespace Reverser
{
    public enum State
    {
        first,
        data,
        last,
    }
    public class Program
    {

      public   static  void Main(string[] args)
        {
            var fileHandler = new FileHandler();
            var lines = fileHandler.Read();

            var first = new List<string>();
            var data = new List<List<string>>();
            var last = new List<string>();

            var state = State.first;
            foreach(var line in lines)
            { 
                if(state == State.first)
                {
                    first.Add(line);
                    if(line.Contains("<trkseg>"))
                    {
                        state = State.data;
                        continue;
                    }
                }
                if(line.Contains("</trkseg>"))
                {
                    state = State.last;
                }
                if(state == State.data)
                {
                    
                    if(line.Contains("<trkpt"))
                    {
                        var row = new List<string>();
                        row.Add(line);
                        data.Add(row);
                    }
                    else 
                    {
                        data.Last().Add(line);
                    }
                }
                if(state == State.last)
                {
                    last.Add(line);
                }
            }
            var allPointsReversed = data.Reverse<List<string>>().ToList();
            fileHandler.SaveResult(first
                , allPointsReversed.SelectMany(x => x).ToList()
                , last
                );

            var smallerRouts = SplitList(allPointsReversed, 50).ToList();
            for(int i  = 0; i < smallerRouts.Count; i++)
            {
                fileHandler.SaveResult(first
                    , smallerRouts[i].SelectMany(x => x).ToList()
                    , last
                    , i.ToString() 
                    );
            }
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> locations, int nOfLists)  
        {        
            var nSize = locations.Count / nOfLists;
            for (int i = 0; i < locations.Count; i += nSize) 
            { 
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i)); 
            }  
        } 
        
    }
}
