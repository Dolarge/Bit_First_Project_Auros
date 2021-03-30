using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _2_1_reflection
{
    class Cal_01_12
    {
        public static void Cal_01(List<SiO2_new_Data> SiO2records, List<Si_new_Data> Sirecords, List<SiO2_1000nm_Data> SiO2_1000_records, int linenum1, int linenum2, int linenum3)
        {
            StreamWriter streamWriter = new StreamWriter(new FileStream("SiO2_1000nm_on_Si_new.dat", FileMode.Create));
            streamWriter.WriteLine("wave(nm)\t\t AOI\t alpha\t\t\t beta");

            double sio2_nm = 0.0;
            double sio2_n = 0.0;
            double sio2_k = 0.0;
            double si_nm = 0.0;
            double si_n = 0.0;
            double si_k = 0.0;
            
            int AOI = 65;
            //int AOI = 65;

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
                
            
            // 반사계수            
            for (int i = 1; i < linenum1; i++)
            {
                sio2_nm = Convert.ToSingle(SiO2records[i].nm);
                sio2_n = Convert.ToSingle(SiO2records[i].n);
                sio2_k = Convert.ToSingle(SiO2records[i].k);

                si_nm = Convert.ToSingle(Sirecords[i].nm);
                si_n = Convert.ToSingle(Sirecords[i].n);
                si_k = Convert.ToSingle(Sirecords[i].k);                

                if (si_nm > 350 && si_nm < 980)
                {
                    Complex N0 = new Complex(1, 0); // 공기 = 1                        

                    // 반사율 (P, S)
                    double P_val = 0.0;
                    double S_Val = 0.0;
                    //double sin_AOI = Math.Sin(dou_Rad2deg(AOI));
                    //double cos_AOI = Math.Cos(dou_Rad2deg(AOI));
                    Complex sin_AOI = Complex.Sin(Rad2deg(AOI)); // 입사각
                    Complex cos_AOI = Complex.Cos(Rad2deg(AOI));
                    
                    Complex N1 = new Complex(sio2_n, -sio2_k); // 매질 = 복소수
                    Complex N2 = new Complex(si_n, -si_k);

                    Complex sintheta1 = (N0 * sin_AOI) / N1;
                    Complex theta1 = Complex.Asin(sintheta1);   // 굴절각 세타1
                    Complex costheta1 = Complex.Cos(theta1);

                    Complex sintheta2 = sin_AOI / N2;
                    Complex theta2 = Complex.Asin(sintheta2);
                    Complex costheta2 = Complex.Cos(theta2);
                    WriteLine("{0} {1}", theta1, theta2);
                    Complex reflect_P_01 = (N1 * cos_AOI - N0 * costheta1) / (N1 * cos_AOI + N0 * costheta1);
                    Complex reflect_s_01 = (N0 * cos_AOI - N1 * costheta1) / (N0 * cos_AOI + N1 * costheta1);
                    Complex trans_P_01 = (2 * cos_AOI) / (N1 * cos_AOI + costheta1);
                    Complex trans_s_01 = (2 * cos_AOI) / (cos_AOI + N1 * costheta1);

                    Complex reflect_P_12 = (N2 * costheta1 - N1 * costheta2) / (N2 * costheta1 + N1 * costheta2);
                    Complex reflect_s_12 = (N1 * costheta1 - N2 * costheta2) / (N1 * costheta1 + N2 * costheta2);
                    Complex trans_P_12 = (2 * N1 * costheta1) / (N2 * costheta1 + N1 * costheta2);
                    Complex trans_s_12 = (2 * N1 * costheta1) / (N1 * costheta1 + N2 * costheta2);

                    //WriteLine("{0} {1} {2} {3}", reflect_P_01, reflect_s_01, reflect_P_12, reflect_s_12);

                    // Beta -> 위상 두께                    
                    Complex Beta_thick = (2 * Math.PI * 1000 * N1 * costheta1) / sio2_nm;

                    // 반사율(크기)
                    P_val = Math.Pow(reflect_P_01.Magnitude, 2);
                    S_Val = Math.Pow(reflect_s_01.Magnitude, 2);

                    //
                    Complex A = new Complex(0, -1) * (2 * Beta_thick);

                    // 통합반사계수(P,S)
                    Complex Total_reflect_P = (reflect_P_01 + (reflect_P_12 * Complex.Exp(A)))
                                                / (1 + reflect_P_01 * (reflect_P_12 * Complex.Exp(A)));
                    Complex Total_reflect_S = (reflect_s_01 + (reflect_s_12 * Complex.Exp(A)))
                                                / (1 + reflect_s_01 * (reflect_s_12 * Complex.Exp(A)));

                    Complex row = (Total_reflect_P / Total_reflect_S);
                    double row_size = row.Magnitude; // tan(psi)

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

                    streamWriter.WriteLine("{0} {1} {2} {3}", sio2_nm, AOI, alpha, beta);
                }
            }
            streamWriter.Close();
            WriteLine("SiO2_1000nm_on_Si_new.dat 생성 완료");
        }
    }
}

