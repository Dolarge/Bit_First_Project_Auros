using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_1_reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] replace = { ' ', ',', '\t', '\n' };
            string[] Silines = File.ReadAllLines("Si_new.txt", Encoding.Default);
            string[] SiO2lines = File.ReadAllLines("SiO2_new.txt", Encoding.Default);
            string[] SiO2_1000_nm_lines = File.ReadAllLines("SiO2_1000nm_on_Si.dat", Encoding.Default);

            int Si_nm_linesNum = Silines.Length;
            int SiO2_nm_linesNum = SiO2lines.Length;
            int SiO2_1000_nm_linesNum = 0;

            List<Si_new_Data> Sirecords = new List<Si_new_Data>();
            List<SiO2_new_Data> SiO2records = new List<SiO2_new_Data>();
            List<SiO2_1000nm_Data> SiO2_1000_records = new List<SiO2_1000nm_Data>();

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

            foreach (var line in SiO2lines)
            {
                string[] splitData = line.Split(replace, StringSplitOptions.RemoveEmptyEntries);
                SiO2records.Add(new SiO2_new_Data
                {
                    nm = splitData[0],
                    n = splitData[1],
                    k = splitData[2]                    
                });
            }

            foreach (var line in SiO2_1000_nm_lines)
            {
                string[] splitData = line.Split(replace, StringSplitOptions.RemoveEmptyEntries);
                SiO2_1000_records.Add(new SiO2_1000nm_Data
                {
                    nm = splitData[0],
                    AOI = splitData[1],
                    Psi = splitData[2],
                    Delta = splitData[3]
                });
                SiO2_1000_nm_linesNum++;
            }

            Cal_01_12.Cal_01(SiO2records, Sirecords, SiO2_1000_records, Si_nm_linesNum, SiO2_nm_linesNum, SiO2_1000_nm_linesNum);
        }
    }
}
