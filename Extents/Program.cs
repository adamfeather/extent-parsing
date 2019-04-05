using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Extents
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var stream = File.OpenRead(@"C:\Users\FEATHEA\source\repos\Extents\example.dat"))
            {
                var extents = GetExtentsFromStream(stream);

                Console.WriteLine($"Extents Found: {extents.Count()}");
            }
        }

        private static IEnumerable<Extent> GetExtentsFromStream(Stream stream)
        {
            var result = new List<Extent>();

            var buffer = new byte[16];

            while (stream.Read(buffer) > 0)
            {
                var bitArray = new BitArray(buffer);

                OutputBitArrayToConsole(bitArray);

                var integer = CreateIntegerFromByteArray(bitArray);

                result.Add(new Extent(false, integer, integer, integer));
            }

            return result;
        }

        private static void OutputBitArrayToConsole(BitArray bitArray)
        {
            for (int i = bitArray.Length - 1; i >= 0; i--)
            {
                Console.Write((bool)bitArray[i] ? 1 : 0);
            }
        }

        private static int CreateIntegerFromByteArray(BitArray bitArray)
        {
            int maximumArraySize = 32;

            if (bitArray.Length > maximumArraySize)
            {
                throw new ArgumentException($"Argument length shall be at most {maximumArraySize} bits.");
            }

            var array = new int[1];

            bitArray.CopyTo(array, 0);

            return array[0];
        }
    }

    public class Extent
    {
        public Extent(bool flag, long a, long b, long c)
        {
        }
    }
}