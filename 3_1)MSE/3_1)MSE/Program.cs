using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _3_1_MSE
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] replace = { ' ', ',', '\t', '\n' };
            string[] Datalines = File.ReadAllLines("data.txt", Encoding.Default);
            string[] SiO2lines = File.ReadAllLines("SiO2_1000nm_on_Si_new_alpha_beta.dat", Encoding.Default);

            int Data_lineNum = Datalines.Length;
            int SiO2_nm_linesNum = SiO2lines.Length;

            List<Si_new_cal_Data> Sirecords = new List<Si_new_cal_Data>();
            List<SiO2_2nm_exp_Data> SiO2records = new List<SiO2_2nm_exp_Data>();

            foreach (var line in Datalines)
            {
                string[] splitData = line.Split(replace, StringSplitOptions.RemoveEmptyEntries);
                Sirecords.Add(new Si_new_cal_Data
                {
                    nm = splitData[0],
                    thick = splitData[1],
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

            Cal.Cal_MSE_cal(Sirecords, SiO2records, Data_lineNum, SiO2_nm_linesNum);
        }
    }
}
