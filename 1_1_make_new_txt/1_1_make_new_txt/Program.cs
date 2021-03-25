using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_1_make_new_txt
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] SIO2lines = File.ReadAllLines(@"C:\Users\bit\Desktop\BIT-auros\BIT\SiO2_2nm_on_Si.dat", Encoding.Default);

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
            Console.WriteLine("SiO2_2nm_new.txt 파일 생성");
        }
    }
}
