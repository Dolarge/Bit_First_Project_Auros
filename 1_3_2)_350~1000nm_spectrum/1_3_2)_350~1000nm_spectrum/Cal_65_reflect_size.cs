using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using static System.Console;

namespace _1_3_2__350_1000nm_spectrum
{
    class Cal_65_reflect_size
    {
        public static void Cal_65_ref(List<Si_new_Data> records, int linenum)
        {
            StreamWriter streamWriter = new StreamWriter(new FileStream("Si_nm_65_ref.txt", FileMode.Create));
            streamWriter.WriteLine("wave(nm)\t\t AOI\t P_reflect\t\t\t S_reflect");

            double si_nm = 0.0;
            double si_n = 0.0;
            double si_k = 0.0;

            // 각도
            int AOI = 65;

            // Complex형 라디안
            Complex Rad2deg(Complex radian)
            {
                return Math.PI * (radian / 180.0f);
            }          


            // 반사계수            
            for (int i = 1; i < linenum; i++)
            {
                si_nm = Convert.ToSingle(records[i].nm);
                si_n = Convert.ToSingle(records[i].n);
                si_k = Convert.ToSingle(records[i].k);

                if (si_nm > 350 && si_nm < 1000)
                {

                    // 65도에 대한 반사계수 비
                    ////////////////////////////////////////////////////////////////////////
                    Complex N0 = new Complex(1, 0); // 공기 = 1                        

                    // 반사율 (P, S)
                    double P_val = 0.0;
                    double S_Val = 0.0;                    

                    Complex sin_AOI = Complex.Sin(Rad2deg(AOI)); // 입사각
                    Complex cos_AOI = Complex.Cos(Rad2deg(AOI));

                    Complex N1 = new Complex(si_n, -si_k); // 매질 = 복소수

                    Complex sintheta1 = (N0 * sin_AOI) / N1;
                    Complex theta1 = Complex.Asin(sintheta1);   // 굴절각 세타1

                    Complex costheta1 = Complex.Cos(theta1);

                    Complex reflect_P = (N1 * cos_AOI - N0 * costheta1) / (N1 * cos_AOI + N0 * costheta1);
                    Complex reflect_s = (N0 * cos_AOI - N1 * costheta1) / (N0 * cos_AOI + N1 * costheta1);

                    // 반사율(크기)
                    P_val = Math.Pow(reflect_P.Magnitude, 2);
                    S_Val = Math.Pow(reflect_s.Magnitude, 2);
                    streamWriter.WriteLine("{0}\t {1}\t {2}\t {3}", si_nm, AOI, P_val, S_Val);

                    //WriteLine("{0}\t {1}\t {2}", AOI, P_val, S_Val);
                }
            }
            WriteLine("Si_nm_65_ref 생성 완료");
            streamWriter.Close();
        }
    }
}
