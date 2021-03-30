using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using static System.Console;

namespace _1_3_MSE
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] replace = { ' ', ',', '\t', '\n' };
            string[] Silines = File.ReadAllLines("Si_new_65_alpha.txt", Encoding.Default);
            string[] SiO2lines = File.ReadAllLines("SiO2_2nm_on_Si_new.dat", Encoding.Default);

            int Si_nm_linesNum = Silines.Length;
            int SiO2_nm_linesNum = SiO2lines.Length;

            List<Si_new_cal_Data> Sirecords = new List<Si_new_cal_Data>();
            List<SiO2_2nm_exp_Data> SiO2records = new List<SiO2_2nm_exp_Data>();

            foreach (var line in Silines)
            {
                string[] splitData = line.Split(replace, StringSplitOptions.RemoveEmptyEntries);
                Sirecords.Add(new Si_new_cal_Data
                {
                    nm = splitData[0],
                    AOI = splitData[1],
                    alpha = splitData[2],
                    beta = splitData[3]
                });
            }

            foreach (var line in SiO2lines)
            {
                string[] splitData = line.Split(replace, StringSplitOptions.RemoveEmptyEntries);
                SiO2records.Add(new SiO2_2nm_exp_Data
                {
                    nm = splitData[0],
                    AOI = splitData[1],
                    alpha = splitData[2],
                    beta = splitData[3]
                });
            }

            Cal_MSE.Cal_MSE_cal(Sirecords, SiO2records, Si_nm_linesNum, SiO2_nm_linesNum);
        }
    }
}
