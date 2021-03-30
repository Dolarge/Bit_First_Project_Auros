using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Numerics;

namespace _1_3_1_700nm_spectrum
{
    class Cal_reflect
    {
        public static void Si_relect(List<Si_nm_Data> records, int linenum)
        {
            StreamWriter streamWriter = new StreamWriter(new FileStream("Si_new_700.txt", FileMode.Create));
            streamWriter.WriteLine("wave(nm)\t AOI\t P반사율\t S반사율");

            double si_nm = 0.0;
            double si_n = 0.0;
            double si_k = 0.0;

            // 각도 갯수
            int AOI_num = (85 - 40) / 2;
            // 각도
            int AOI = 65;

            Complex Rad2deg(Complex radian)
            {
                return Math.PI * (radian / 180.0f);
            }

            // 우리가 구해야 할 값            
            // sin65도 -- SIO2에 AOI


            // 반사계수            
            for (int i = 1; i < linenum; i++)
            {
                si_nm = Convert.ToSingle(records[i].nm);
                si_n = Convert.ToSingle(records[i].n);
                si_k = Convert.ToSingle(records[i].k);
                if (si_nm > 350 && si_nm < 980)
                {
                    //WriteLine("{0}", si_n);
                    for (int k = 0; k <= AOI_num; k++)
                    {
                        AOI = 40 + k * 2;
                        Complex N0 = new Complex(1, 0); // 공기 = 1
                        
                        // 반사율 (P, S)
                        double P_val = 0.0;
                        double S_Val = 0.0;
                        //P_val = reflect_P.Magnitude;
                        //S_Val = reflect_s.Magnitude;


                        // 브루스터 앵글 -> 그래프에서 값을 더 정확하게 보기 위함
                        double angle_P = Math.Pow(P_val, 2);
                        double angle_S = Math.Pow(S_Val, 2);

                        Complex sin_AOI = Complex.Sin(Rad2deg(AOI));
                        Complex cos_AOI = Complex.Cos(Rad2deg(AOI));

                        Complex N1_size = new Complex(si_n, -si_k); // 매질 = 복소수

                        Complex sintheta1_size = N0 * sin_AOI / N1_size;
                        Complex theta1_size = Complex.Asin(sintheta1_size);   // 굴절각 세타1
                        Complex costheta1_size = Complex.Cos(theta1_size);
                        Complex reflect_P_size = (N1_size * cos_AOI - N0 * costheta1_size) / (N1_size * cos_AOI + N0 * costheta1_size);
                        Complex reflect_s_size = (N0 * cos_AOI - N1_size * costheta1_size) / (N0 * cos_AOI + N1_size * costheta1_size);

                        P_val = reflect_P_size.Magnitude;
                        S_Val = reflect_s_size.Magnitude;
                        streamWriter.WriteLine("{0}\t {1}\t {2}\t {3}", si_nm, AOI, P_val, S_Val);
                    }
                }

                //WriteLine("{0:N3}\t {1:N3}\t {2:N3}", si_nm, P_val, S_Val);

            }
            streamWriter.Close();
        }
    }
}
