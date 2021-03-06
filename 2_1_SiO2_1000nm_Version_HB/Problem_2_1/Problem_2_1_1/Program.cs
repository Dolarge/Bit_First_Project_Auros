using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Problem_2_1_1
{
    class Program
    {
        public class SiO2_1000nm_data
        {
            public string wavelength_nm { get; set; }
            public string AOI { get; set; }
            public string Psi { get; set; }
            public string Delta { get; set; }
        }
        public class SiO2_nk_data
        {
            public string wavelength_nm { get; set; }
            public string n { get; set; }
            public string k { get; set; }
        }

        public class Si_nk_data
        {
            public string wavelength_nm { get; set; }
            public string n { get; set; }
            public string k { get; set; }
        }
        
        static void Main(string[] args)
        {
            char[] replace = { ' ', ',', '\t', '\n' };
            
            string[] SiLine = File.ReadAllLines(@"C:\Users\bit\Desktop\Project\Aros\SiO2_1000nm_on_Si.dat", Encoding.Default);
            string[] SiO2Line = File.ReadAllLines(@"C:\Users\bit\Desktop\Project\Aros\SiO2_1000nm_on_Si.dat", Encoding.Default);
            string[] SiO2_1000nm_Line = File.ReadAllLines(@"C:\Users\bit\Desktop\Project\Aros\SiO2_1000nm_on_Si.dat", Encoding.Default);
            
            int SiLine_LineNum = SiLine.Length - 1; // 첫줄을 제외한 모든 라인의 수
            int SiO2Line_LineNum = SiO2Line.Length - 1; // 첫줄을 제외한 모든 라인의 수
            int SiO2_1000nm_Line_LineNum = SiO2_1000nm_Line.Length - 1; // 첫줄을 제외한 모든 라인의 수
            
            List<Si_nk_data> Si_nk_Records = new List<Si_nk_data>();
            List<SiO2_nk_data> SiO2_nk_Records = new List<SiO2_nk_data>();
            List<SiO2_1000nm_data> SiO2Records = new List<SiO2_1000nm_data>();

            // SiO2_1000nm 물성값 로딩
            foreach (var line in SiO2_1000nm_Line)
            {
                string[] splitData = line.Split(replace, StringSplitOptions.RemoveEmptyEntries);
                SiO2Records.Add(new SiO2_1000nm_data
                {
                    wavelength_nm   = splitData[0],
                    AOI             = splitData[1],
                    Psi             = splitData[2],
                    Delta           = splitData[3]
                }) ;
            }
            SiO2Records.RemoveAt(0);

            // Si_nk 물성값 로딩
            foreach (var line in SiLine)
            {
                string[] splitData = line.Split(replace, StringSplitOptions.RemoveEmptyEntries);
                Si_nk_Records.Add(new Si_nk_data
                {
                    wavelength_nm = splitData[0],
                    n = splitData[1],
                    k = splitData[2]
                });
            }
            Si_nk_Records.RemoveAt(0);

            // SiO2_nk 물성값 로딩
            foreach (var line in SiO2Line)
            {
                string[] splitData = line.Split(replace, StringSplitOptions.RemoveEmptyEntries);
                SiO2_nk_Records.Add(new SiO2_nk_data
                {
                    wavelength_nm = splitData[0],
                    n = splitData[1],
                    k = splitData[2]
                });
            }
            SiO2_nk_Records.RemoveAt(0);

            //계산함수
            //Func(a,b)
        }

        // 작성 필요
        //private static List<> Func(List<> Records, int LineNum)
        //{
        //    List<> SiO2_nk = new List<>();
        //
        //    return SiO2_nk;
        //
        //    double Rad2deg(double radian)
        //    {
        //        return Math.PI * (radian / 180.0f);
        //    }
        //
        //    double dou_Rad2deg(double radian)
        //    {
        //        return Math.PI * (radian / 180.0f);
        //
        //    }
        //}
    }
}

