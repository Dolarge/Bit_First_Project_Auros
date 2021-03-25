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
    class Cal
    {
        public static void Si_nm_Cal(List<Si_Data> records, int linenum)
        {
            StreamWriter streamWriter = new StreamWriter(new FileStream("Si_nm.txt", FileMode.Create));
            streamWriter.WriteLine("wavelength(nm)\t n\t\t\t k");

            double si_ev = 0.0;
            double si_e1 = 0.0;
            double si_e2 = 0.0;

            double si_nm = 0.0;
            double n, k = 0.0;
            double n_permitivity, k_permitivity = 0.0;
            double sqrt_permitivity = 0.0;

            for (int i = 1; i < linenum; i++)
            {
                si_ev = Convert.ToSingle(records[i].eV);
                si_e1 = Convert.ToSingle(records[i].e1);
                si_e2 = Convert.ToSingle(records[i].e2);

                // 수식 적용
                // 파장변경
                si_nm = (1240) / si_ev;

                // e1 제곱 + e2 제곱
                sqrt_permitivity = Math.Pow(si_e1, 2) + Math.Pow(si_e2, 2);

                //e1 값 ->n(굴절율) 변경
                n_permitivity = 0.5 * (Math.Sqrt(sqrt_permitivity) + si_e1);
                n = Math.Sqrt(n_permitivity);

                //e1 값 -> k(소광계수)
                k_permitivity = 0.5 * (Math.Sqrt(sqrt_permitivity) - si_e2);
                k = Math.Sqrt(k_permitivity);

                
                // 350이상 1000 이하인거 new 파일에 출력
                if (si_nm >= 350 && si_nm <= 1000)
                {
                    streamWriter.WriteLine("{0}\t {1}\t {2}", si_nm, n, k);
                }
            }
            streamWriter.Close();
        }

        public static void SiO2_nm_Cal(List<SiO2_txt_data> records, int linenum)
        {
            StreamWriter streamWriter = new StreamWriter(new FileStream("SIO2_nm.txt", FileMode.Create));
            streamWriter.WriteLine("wavelength(nm)\t n\t k");

            float float_nm = 0.0f;
            float float_n = 0.0f;
            float float_k = 0.0f;
            double sio2_nm = 0.0;

            for (int i = 1; i < linenum; i++)
            {
                float_nm = Convert.ToSingle(records[i].ANGSTROMS);
                float_n = Convert.ToSingle(records[i].n);
                float_k = Convert.ToSingle(records[i].k);

                // 수식 적용
                sio2_nm = float_nm * Math.Pow(10, -1);
                WriteLine(float_n);
                // 350이상 1000 이하인거 new 파일에 출력
                if (sio2_nm >= 350 && sio2_nm <= 1000)
                {
                    streamWriter.WriteLine("{0}\t {1}\t {2}", sio2_nm, float_n, float_k);
                }
            }
            streamWriter.Close();
        }
    }
}
