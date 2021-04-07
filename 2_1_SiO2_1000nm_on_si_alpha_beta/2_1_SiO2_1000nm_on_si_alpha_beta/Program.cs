using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_1_SiO2_1000nm_on_si_alpha_beta
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] SIO2lines = File.ReadAllLines("SiO2_1000nm_on_Si.dat", Encoding.Default);

            // 라인수
            int SIO2linesNum = SIO2lines.Length;

            // split 기준이 되는 replace
            char[] replace = { ' ', ',', '\t', '\n' };

            // 데이터 저장 list
            List<SIO2Data_dat> SIO2records = new List<SIO2Data_dat>();

            foreach (var line in SIO2lines)
            {
                string[] splitData = line.Split(replace, StringSplitOptions.RemoveEmptyEntries);
                SIO2records.Add(new SIO2Data_dat
                {
                    wavelength = splitData[0],
                    AOI = splitData[1],
                    Psi = splitData[2],
                    Delta = splitData[3]
                });
            }

            Cal.SiO2_2nm_Cal(SIO2records, SIO2linesNum);
            Console.WriteLine("SiO2_1000nm_new_alpha_beta.txt 파일 생성");
        }
    }
}
