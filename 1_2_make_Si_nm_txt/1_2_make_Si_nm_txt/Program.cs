using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Numerics;

namespace _1_2_make_Si_nm_txt
{
    class Program
    {
        static void Main(string[] args)
        {
            // 공통
            char[] replace = { ' ', ',', '\t', '\n' };
            string[] Silines = File.ReadAllLines(@"C:\Users\bit\Desktop\BIT-auros\BIT\Si.txt", Encoding.Default);
            string[] SIO2lines = File.ReadAllLines(@"C:\Users\bit\Desktop\BIT-auros\BIT\SIO2.txt", Encoding.Default);

            int SilinesNum = Silines.Length;
            int SIO2linesNum = SIO2lines.Length;

            List<Si_Data> Sirecords = new List<Si_Data>();
            List<SiO2_txt_data> SIO2records = new List<SiO2_txt_data>();


            //////////////////////////////////////////////////////////////////////////////
            // Si.txt 파일 읽음            
            foreach (var line in Silines)
            {
                string[] splitData = line.Split(replace, StringSplitOptions.RemoveEmptyEntries);
                Sirecords.Add(new Si_Data
                {
                    eV = splitData[0],
                    e1 = splitData[1],
                    e2 = splitData[2]
                });
            }

            Cal.Si_nm_Cal(Sirecords, SilinesNum);
            WriteLine("Si_nm.txt 파일 생성");
            //////////////////////////////////////////////////////////////////////////////


            //////////////////////////////////////////////////////////////////////////////
            // SiO2.txt 파일 읽음            
            foreach (var line in SIO2lines)
            {
                string[] splitData = line.Split(replace, StringSplitOptions.RemoveEmptyEntries);
                SIO2records.Add(new SiO2_txt_data
                {
                    ANGSTROMS = splitData[0],
                    n = splitData[1],
                    k = splitData[2]
                });
            }
            
            Cal.SiO2_nm_Cal(SIO2records, SIO2linesNum);
            WriteLine("SiO2_nm.txt 파일 생성");
            //////////////////////////////////////////////////////////////////////////////
        }
    }
}
