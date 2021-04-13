using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _3_1_MSE
{
    class Cal
    {
        public static void Cal_MSE_cal(List<Si_new_cal_Data> Sirecords, List<SiO2_2nm_exp_Data> SiO2records, int linenum1, int linenum2)
        {
            StreamWriter streamWriter = new StreamWriter(new FileStream("2_2_MSE.txt", FileMode.Create));
            double data__nm = 0.0;
            double data__thick = 0.0;
            double data_alpha = 0.0;
            double data_beta = 0.0;
            List<double> data_alpha_arr = new List<double>();
            List<double> data_beta_arr = new List<double>();
            streamWriter.WriteLine("thick\t\t mse");
            for (int i = 1; i < linenum1; i++)
            {
                data__nm = Single.Parse(Sirecords[i].nm);
                data__thick = Single.Parse(Sirecords[i].thick);
                data_alpha = Single.Parse(Sirecords[i].alpha);
                data_beta = Single.Parse(Sirecords[i].beta);

                data_alpha_arr.Add(data_alpha);
                data_beta_arr.Add(data_beta);
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
            double[] ddata_alpha_arr = data_alpha_arr.ToArray();
            double[] dSiO2_alpha_arr = SiO2_alpha_arr.ToArray();
            double[] ddata_beta_arr = data_beta_arr.ToArray();
            double[] dSiO2_beta_arr = SiO2_beta_arr.ToArray();

            for (int j = 0; j<121; j++)
            {
                MSE_sum = 0.0;

                for (int i = 0; i < linenum2 - 1; i++)
                {
                    MSE_sum += (((Math.Pow((dSiO2_alpha_arr[i] - ddata_alpha_arr[i+j*400]), 2)))
                              + (Math.Pow((dSiO2_beta_arr[i] - ddata_beta_arr[i+j*400]), 2)));
                    //WriteLine(MSE_sum);
                }
                double MSE = 0.0;
                MSE = MSE_sum / (linenum2-1);
                streamWriter.WriteLine("{0}\t{1}",700+j*5,MSE);
            }
            streamWriter.Close();
        }
    }
}
