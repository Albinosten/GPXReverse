using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Reverser
{
    public class FileHandler
    {
        private static string input => "GBDIVIDE_v400.gpx";
        private static string output => @"/Users/albinost/Programming/GPXReverse/Reverser/Results/Reversed";
        // private static string output => @"/Results/Reversed";
        private static string fileType => ".gpx";

        public IList<string> Read()
        {
            var result = new List<string>();

            if(File.Exists(input))
            {
                var lines = File.ReadAllLines(input, Encoding.UTF8);
                foreach(var line in lines)
                {
                    result.Add(line);
                }
            }

            return result;
        }

        public void SaveResult(List<string> first, List<string> middle, List<string> last, string prefix = "")
        {
            var path = output + prefix + fileType;
            var file = File.Create(path);
            var streamWriter = new StreamWriter(file);

            foreach(var result in first)
            {
                streamWriter.WriteLine(result);
            }
            foreach(var result in middle)
            {
                streamWriter.WriteLine(result);
            }
            foreach(var result in last)
            {
                streamWriter.WriteLine(result);
            }

            streamWriter.Close();
            file.Close();
        }
    }
}