using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FakeNewsDetectorApp
{
    class FileManager
    {
        public static bool SaveToFile(string _filename, string[] _data)
        {
            if (_data.Length <= 0)
                return false;

            if (!File.Exists(_filename))
                File.Create(_filename);

                File.WriteAllText(_filename, "");

            File.AppendAllLines(_filename, _data);
            return true;
        }

        public static string[] LoadFromFile(string _filename)
        {
            string[] returnval = null;

            if (!File.Exists(_filename))
                throw new Exception("No File Found!");

            returnval = File.ReadAllLines(_filename);
            return returnval;
        }
    }
}
