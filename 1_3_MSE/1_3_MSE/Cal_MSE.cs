using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using static System.Console;
using System.Collections;

namespace _1_3_MSE
{
    class Cal_MSE
    {
        public static void Cal_MSE_cal(List<Si_new_cal_Data> Sirecords, List<SiO2_2nm_exp_Data> SiO2records, int linenum1, int linenum2)
        {
            StreamWriter streamWriter = new StreamWriter(new FileStream("MSE.txt", FileMode.Create));
            //streamWriter.WriteLine("wave(nm)\t\t AOI");
            double si_nm = 0.0;
            double si_AOI = 0.0;
            double si_alpha = 0.0;
            double si_beta = 0.0;
            List<double> Si_alpha_arr = new List<double>();
            List<double> Si_beta_arr = new List<double>();
            for (int i = 1; i < linenum1; i++)
            {
                si_nm = Single.Parse(Sirecords[i].nm);
                si_AOI = Single.Parse(Sirecords[i].AOI);
                si_alpha = Single.Parse(Sirecords[i].alpha);
                si_beta = Single.Parse(Sirecords[i].beta);

                Si_alpha_arr.Add(si_alpha);
                Si_beta_arr.Add(si_beta);
            }
            double SiO2_nm = 0.0;
            double SiO2_AOI = 0.0;
            double SiO2_alpha = 0.0;
            double SiO2_beta = 0.0;
            List<double> SiO2_alpha_arr = new List<double>();
            List<double> SiO2_beta_arr = new List<double>();
            double MSE_sum = 0.0;
            for (int i = 1; i < linenum2; i++)
            {
                SiO2_nm = Single.Parse(SiO2records[i].nm);
                SiO2_AOI = Single.Parse(SiO2records[i].AOI);
                SiO2_alpha = Single.Parse(SiO2records[i].alpha);
                SiO2_beta = Convert.ToSingle(SiO2records[i].beta);

                SiO2_alpha_arr.Add(SiO2_alpha);
                SiO2_beta_arr.Add(SiO2_beta);
                //streamWriter.WriteLine("{0} {1}", SiO2_nm, SiO2_AOI);
            }
            double[] dSi_alpha_arr = Si_alpha_arr.ToArray();
            double[] dSiO2_alpha_arr = SiO2_alpha_arr.ToArray();
            double[] dSi_beta_arr = Si_beta_arr.ToArray();
            double[] dSiO2_beta_arr = SiO2_beta_arr.ToArray();

            for (int i = 0; i < linenum2-1; i++)
            {
                MSE_sum += (Math.Pow((dSiO2_alpha_arr[i] - dSi_alpha_arr[i]), 2))
                         + (Math.Pow((dSiO2_beta_arr[i] - dSi_beta_arr[i]), 2));                
                //WriteLine(MSE_sum);
            }
            
            double MSE = 0.0;
            MSE = MSE_sum / 400;


            streamWriter.WriteLine("MSE = {0}", MSE);
            WriteLine("MSE.txt 생성 완료");
            streamWriter.Close();
        }
    }
}
