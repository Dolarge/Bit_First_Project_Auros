using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Numerics;

namespace _1_3_Cal_relect_s_p_Cal_alpha_beta
{
    class Cal_reflect
    {
        public static void Si_relect(List<Si_nm_Data> records, int linenum)
        {
            StreamWriter streamWriter = new StreamWriter(new FileStream("Si_new.txt", FileMode.Create));
            streamWriter.WriteLine("wave(nm)\t seta_1\t reflect_P\t\t reflect_S\t\t\t P반사율\t\t S반사율");

            double si_nm = 0.0f;
            double si_n = 0.0f;
            double si_k = 0.0f;

            Complex Rad2deg(Complex radian)
            {
                return Math.PI * (radian / 180.0f);
            }

            // 우리가 구해야 할 값
            Complex seta_1;
            Complex sin_seta_1;
            Complex cos_seta_1;
            // sin65도 -- SIO2에 AOI
            Complex sin_AOI = Complex.Sin(Rad2deg(65));
            Complex cos_AOI = Complex.Cos(Rad2deg(65));
            //
            //WriteLine(sin_AOI);
            // AOI 가 (40~85도)
            //double sin
            // asin (역sin)의 내부 각도 값
            //float inner = 0.0;

            // 반사계수            
            for (int i = 1; i < linenum; i++)
            {
                si_nm = Convert.ToSingle(records[i].nm);
                si_n = Convert.ToSingle(records[i].n);
                si_k = Convert.ToSingle(records[i].k);
                Complex N0 = new Complex(1, 0);
                //Console.WriteLine($"{si_nm}\t{ si_n }\t{ si_k}");
                Complex N1 = new Complex(si_n, -si_k);
                Complex sintheta1 = N0 * sin_AOI / N1;
                Complex theta1 = Complex.Asin(sintheta1);
                Complex costheta1 = Complex.Cos(theta1);
                //0.27 0.78
                //Console.WriteLine($"{theta1}\t{sintheta1}\t{costheta1}");
                Complex reflect_P = (N1 * cos_AOI - N0 * costheta1) / (N1 * cos_AOI + N0 * costheta1);
                Complex reflect_s = (N0 * cos_AOI - N1 * costheta1) / (N0 * cos_AOI + N1 * costheta1);

                // 반사율 (P, S)
                double P_val = 0.0;
                double S_Val = 0.0;             
                P_val = reflect_P.Magnitude;
                S_Val = reflect_s.Magnitude;


                // 브루스터 앵글 -> 그래프에서 값을 더 정확하게 보기 위함
                double angle_P = Math.Pow(P_val, 2);
                double angle_S = Math.Pow(S_Val, 2);


                streamWriter.WriteLine("{0:N3}\t {1:N3}\t {2:N3}", si_nm, P_val, S_Val);
                WriteLine("{0:N3}\t {1:N3}\t {2:N3}", si_nm, P_val, S_Val);

            }
        }
    }
}
