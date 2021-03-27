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
    class Cal_40_85_alpha
    {
        public static void Cal_40_85_Alpha(List<Si_new_Data> records, int linenum)
        {
            StreamWriter streamWriter = new StreamWriter(new FileStream("Si_nm_40~85_alpha.txt", FileMode.Create));
            streamWriter.WriteLine("wave(nm)\t\t AOI\t alpha\t\t\t beta");

            double si_nm = 0.0;
            double si_n = 0.0;
            double si_k = 0.0;

            // 각도 갯수
            int AOI_num = (85 - 40) / 5;
            // 각도
            int AOI = 65;

            // Complex형 라디안
            Complex Rad2deg(Complex radian)
            {
                return Math.PI * (radian / 180.0f);
            }

            // double형 라디안
            double dou_Rad2deg(double radian)
            {
                return Math.PI * (radian / 180.0f);
            }

            // 우리가 구해야 할 값            
            // sin65도 --> SIO2에 AOI


            // 반사계수            
            for (int i = 1; i < linenum; i++)
            {
                si_nm = Convert.ToSingle(records[i].nm);
                si_n = Convert.ToSingle(records[i].n);
                si_k = Convert.ToSingle(records[i].k);

                if (si_nm > 350 && si_nm < 1000)
                {
                    for (int k = 0; k <= AOI_num; k++)
                    {
                        // 65도에 대한 반사계수 비
                        ////////////////////////////////////////////////////////////////////////
                        Complex N0 = new Complex(1, 0); // 공기 = 1                        

                        // 반사율 (P, S)
                        double P_val = 0.0;
                        double S_Val = 0.0;

                        AOI = 40 + 5 * k;
                        
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
                        // 반사계수의 비
                        Complex row = (reflect_P / reflect_s);
                        double row_size = row.Magnitude;

                        // 65도(AOI)에서 Psi, Delta 값 구하기                       
                        double Psi = Math.Atan(row_size);

                        double Delta = row.Phase;
                
                        double tan_pow = 0.0;
                        double a_numeator = 0.0, a_denominator = 0.0;
                        double b_numeator = 0.0, b_denominator = 0.0;
                        double alpha = 0.0, beta = 0.0;

                        tan_pow = Math.Pow(Math.Tan((Psi)), 2);
                        a_numeator = tan_pow - Math.Pow(Math.Tan(dou_Rad2deg(45)), 2);
                        a_denominator = tan_pow + Math.Pow(Math.Tan(dou_Rad2deg(45)), 2);
                        alpha = a_numeator / a_denominator;

                        b_numeator = 2 * Math.Tan((Delta)) * Math.Cos((Delta)) * Math.Tan(dou_Rad2deg(45));
                        b_denominator = tan_pow + Math.Pow(Math.Tan(dou_Rad2deg(45)), 2);
                        beta = b_numeator / b_denominator;

                        //WriteLine("{0}\t {1:N4}\t {2}\t {3}", AOI, si_nm, alpha, beta);
                        //
                        // alpha, beta 구하는 공식
                        streamWriter.WriteLine("{0}\t {1}\t {2}\t {3}", si_nm, AOI, alpha, beta);
                    }                   
                }
            }
            WriteLine("Si_nm_40~85_alpha 생성 완료");
            streamWriter.Close();
        }
    }
}
