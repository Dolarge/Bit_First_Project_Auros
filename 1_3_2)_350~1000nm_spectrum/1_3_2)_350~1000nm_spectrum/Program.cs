using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;
using static System.Console;

namespace _1_3_2__350_1000nm_spectrum
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] replace = { ' ', ',', '\t', '\n' };
            string[] Silines = File.ReadAllLines("Si_nm.txt", Encoding.Default);

            int Si_nm_linesNum = Silines.Length;

            List<Si_new_Data> Sirecords = new List<Si_new_Data>();

            foreach (var line in Silines)
            {
                string[] splitData = line.Split(replace, StringSplitOptions.RemoveEmptyEntries);
                Sirecords.Add(new Si_new_Data
                {
                    nm = splitData[0],
                    n = splitData[1],
                    k = splitData[2]
                });
            }

            Cal_40_85_reflect_size.Cal_40_80_ref(Sirecords, Si_nm_linesNum);
            Cal_65_reflect_size.Cal_65_ref(Sirecords, Si_nm_linesNum);
            Cal_40_85_alpha.Cal_40_85_Alpha(Sirecords, Si_nm_linesNum);
            Cal_65_alpha.Cal_60_Alpha(Sirecords, Si_nm_linesNum);
        }
    }
}
