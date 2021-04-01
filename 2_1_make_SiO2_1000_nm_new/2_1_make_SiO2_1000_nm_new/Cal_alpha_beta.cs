using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _2_1_make_SiO2_1000_nm_new
{

    class Cal_alpha_beta
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

            //무한등비가 아닌 등비급수 계산하기 위해
            //새로운 파일쓰기 위한 배열
            string[] Array_new_NM = new string[linenum1 - 1];
            string[] Array_new_Alpha = new string[linenum1 - 1];
            string[] Array_new_Beta = new string[linenum1 - 1];
            //------------------------

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
                //공비에 따로 마이너스해준 Rp Rs를 만들어 비교
                //R,S
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
                    //WriteLine("{0} {1}", theta1, theta2);
                    Complex reflect_P_01 = (N1 * cos_AOI - N0 * costheta1) / (N1 * cos_AOI + N0 * costheta1);   //Rp
                    Complex reflect_s_01 = (N0 * cos_AOI - N1 * costheta1) / (N0 * cos_AOI + N1 * costheta1);   //Rs
                    Complex trans_P_01 = (2 * cos_AOI) / (N1 * cos_AOI + costheta1);
                    Complex trans_s_01 = (2 * cos_AOI) / (cos_AOI + N1 * costheta1);

                    Complex reflect_P_12 = (N2 * costheta1 - N1 * costheta2) / (N2 * costheta1 + N1 * costheta2);
                    Complex reflect_s_12 = (N1 * costheta1 - N2 * costheta2) / (N1 * costheta1 + N2 * costheta2);

                    Complex reflect_P_21 = (N1 * costheta2 - N2 * costheta1) / (N1 * costheta2 + N2 * costheta1);
                    Complex reflect_s_21 = (N2 * costheta2 - N1 * costheta1) / (N2 * costheta2 + N1 * costheta1);


                    Complex trans_P_12 = (2 * N1 * costheta1) / (N2 * costheta1 + N1 * costheta2);
                    Complex trans_s_12 = (2 * N1 * costheta1) / (N1 * costheta1 + N2 * costheta2);

                    Complex trans_P_21 = (2 * N2 * costheta2) / (N1 * costheta2 + N2 * costheta1);
                    Complex trans_s_21 = (2 * N2 * costheta2) / (N2 * costheta2 + N1 * costheta1);



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
                    Complex Total_reflect_S =
                        (reflect_s_01 + (reflect_s_12 * Complex.Exp(A)))
                        / (1 + reflect_s_01 * (reflect_s_12 * Complex.Exp(A)));


                    //Complex Rp = reflect_P_01 + (trans_P_01 * reflect_P_12 * trans_P_12 * Complex.Exp(A)) * Sigma_P(200);
                    //Complex Rs = reflect_s_01 + (trans_s_01 * reflect_s_12 * trans_s_12 * Complex.Exp(A)) * Sigma_S(200);

                    //Console.WriteLine("{0} {1}", Total_reflect_P, Total_reflect_S);
                    Complex New_sigma_Pa = (reflect_P_01 + (reflect_P_12 * Complex.Exp(A)));
                    Complex New_sigma_Sa = (reflect_s_01 + (reflect_s_12 * Complex.Exp(A)));

                    Complex New_sigma_Pr = -(reflect_P_01 * (reflect_P_12 * Complex.Exp(A)));
                    Complex New_sigma_Sr = -(reflect_s_01 * (reflect_s_12 * Complex.Exp(A)));

                    Complex New_Rp= New_Sigma_P(10);
                    Complex New_Rs= New_Sigma_S(10);

                    Complex row = (Total_reflect_P / Total_reflect_S);
                    //Complex row_new = (Rp / Rs);
                    Complex row_new = (New_Rp / New_Rs);

                    double row_size = row.Magnitude; // tan(psi)
                    double row_new_size = row_new.Magnitude;

                    double Psi = Math.Atan(row_size);
                    double Psi_new = Math.Atan(row_new_size);

                    double Delta = row.Phase;
                    double Delta_new = row_new.Phase;

                    double tan_pow = 0.0;
                    double a_numeator = 0.0, a_denominator = 0.0;
                    double b_numeator = 0.0, b_denominator = 0.0;
                    double alpha = 0.0, beta = 0.0;

                    //등비급수의 "항의 개수(n)"
                    double new_alpha = 0.0, new_beta = 0.0;


                    Complex Sigma_P(int n)
                    {
                        Complex Sigma_Value = 0;
                        for (int K = 0; K < n; K++)
                        {
                            Sigma_Value += Complex.Pow((reflect_P_12 * reflect_P_21 * Complex.Exp(A)), K);
                        }

                        return Sigma_Value;
                    }

                    Complex Sigma_S(int n)
                    {
                        Complex Sigma_Value = 0;
                        for (int K = 0; K < n; K++)
                        {
                            Sigma_Value += Complex.Pow((reflect_s_12 * reflect_s_21 * Complex.Exp(A)), K);
                        }
                        return Sigma_Value;
                    }

                   
                    Complex New_Sigma_P(int n)
                    {
                        Complex Sigma_Value = 0;
                        for (int K = 1; K < n; K++)
                        {
                            Sigma_Value += New_sigma_Pa * Complex.Pow(New_sigma_Pr, K - 1);
                        }

                        return Sigma_Value;
                    }

                    Complex New_Sigma_S(int n)
                    {
                        Complex Sigma_Value = 0;
                        for (int K = 1; K < n; K++)
                        {
                            Sigma_Value += New_sigma_Sa * Complex.Pow(New_sigma_Sr, K - 1);
                        }
                        return Sigma_Value;
                    }


                    // WriteLine("{0} {1} {2} {3}", Sigma_P(8000), Total_reflect_P, Sigma_S(8000), Total_reflect_S);

                    tan_pow = Math.Pow(Math.Tan((Psi)), 2);
                    a_numeator = tan_pow - Math.Pow(Math.Tan(dou_Rad2deg(45)), 2);
                    a_denominator = tan_pow + Math.Pow(Math.Tan(dou_Rad2deg(45)), 2);
                    alpha = a_numeator / a_denominator;

                    b_numeator = 2 * Math.Tan((Delta)) * Math.Cos((Delta)) * Math.Tan(dou_Rad2deg(45));
                    b_denominator = tan_pow + Math.Pow(Math.Tan(dou_Rad2deg(45)), 2);
                    beta = b_numeator / b_denominator;

                    streamWriter.WriteLine("{0}\t{1}\t{2}\t{3}", sio2_nm, AOI, alpha, beta);

                    //값 초기화 후 
                    //새로 변경한 값 적용
                    tan_pow = Math.Pow(Math.Tan((Psi_new)), 2);
                    a_numeator = tan_pow - Math.Pow(Math.Tan(dou_Rad2deg(45)), 2);
                    a_denominator = tan_pow + Math.Pow(Math.Tan(dou_Rad2deg(45)), 2);
                    new_alpha = a_numeator / a_denominator;

                    b_numeator = 2 * Math.Tan((Delta_new)) * Math.Cos((Delta_new)) * Math.Tan(dou_Rad2deg(45));
                    b_denominator = tan_pow + Math.Pow(Math.Tan(dou_Rad2deg(45)), 2);
                    new_beta = b_numeator / b_denominator;

                    //배열에 저장 -> 추가로 뽑아내기
                    //streamWriter.WriteLine("{0} {1} {2} {3}", sio2_nm, AOI, alpha, beta);


                    //무한등비급수 수열 하기전에 "항의 개수"내가 정해서 구하기
                    //Rp 등비수열 구하기
                    Array_new_NM[i - 1] = Convert.ToString(sio2_nm);
                    Array_new_Alpha[i - 1] = Convert.ToString(new_alpha);
                    Array_new_Beta[i - 1] = Convert.ToString(new_beta);



                }


            }
            streamWriter.Close();
            WriteLine("SiO2_1000nm_on_Si_new.dat 생성 완료");

            //2-1)등비수열 "항의 개수" 파일 생성
            StreamWriter streamWriter_new = new StreamWriter(new FileStream("SiO2_1000nm_on_Si_new_dolarge.dat", FileMode.Create));
            streamWriter_new.WriteLine("wave(nm)\t\t AOI\t alpha_new\t\t beta_new");
            List<SiO2_1000nm_Data> SiO2_1000nm_NEW_Data = new List<SiO2_1000nm_Data>();

            for (int a = 0; a < Array_new_NM.Length; a++)
            {
                SiO2_1000nm_NEW_Data.Add(new SiO2_1000nm_Data
                {
                    nm = Array_new_NM[a],
                    AOI = "65",
                    Psi = Array_new_Alpha[a],
                    Delta = Array_new_Beta[a]
                });
                streamWriter_new.WriteLine("{0}\t{1}\t{2}\t{3}", SiO2_1000nm_NEW_Data[a].nm, SiO2_1000nm_NEW_Data[a].AOI, SiO2_1000nm_NEW_Data[a].Psi, SiO2_1000nm_NEW_Data[a].Delta);
            }
            streamWriter_new.Close();

            WriteLine("SiO2_1000nm_on_Si_new_dolarge.dat 생성 완료");

        }
    }
}
