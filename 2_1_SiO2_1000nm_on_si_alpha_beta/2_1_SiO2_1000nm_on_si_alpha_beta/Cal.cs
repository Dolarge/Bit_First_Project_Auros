using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_1_SiO2_1000nm_on_si_alpha_beta
{
    class Cal
    {
        public static void SiO2_2nm_Cal(List<SIO2Data_dat> records, int linenum)
        {
            StreamWriter streamWriter = new StreamWriter(new FileStream("SiO2_1000nm_on_Si_new_alpha_beta.dat", FileMode.Create));
            streamWriter.WriteLine("wavelength(nm)\t AOI\t\t alpha\t beta");

            float floatwavelength = 0.0f;
            float floatPsi = 0.0f;
            float floatDelta = 0.0f;
            double tan_sq = 0.0;
            double a_numeator, a_denominator = 0.0;
            double b_numeator, b_denominator = 0.0;
            double alpha, beta = 0.0;

            double Rad2deg(double radian)
            {
                return Math.PI * (radian / 180.0f);
            }

            for (int i = 1; i < linenum; i++)
            {
                floatwavelength = Convert.ToSingle(records[i].wavelength);
                floatPsi = Convert.ToSingle(records[i].Psi);
                floatDelta = Convert.ToSingle(records[i].Delta);

                // 수식 적용
                tan_sq = Math.Pow(Math.Tan(Rad2deg(floatPsi)), 2);
                a_numeator = tan_sq - Math.Pow(Math.Tan(Rad2deg(45)), 2);
                a_denominator = tan_sq + Math.Pow(Math.Tan(Rad2deg(45)), 2); ;
                alpha = a_numeator / a_denominator;

                b_numeator = 2 * Math.Tan(Rad2deg(floatPsi)) * Math.Cos(Rad2deg(floatDelta));
                b_denominator = tan_sq + Math.Pow(Math.Tan(Rad2deg(45)), 2); ;
                beta = b_numeator / b_denominator;

                if (floatwavelength > 350 && floatwavelength < 980)
                {
                    streamWriter.WriteLine("{0}\t {1}\t {2}\t {3}", floatwavelength, records[i].AOI, alpha, beta);
                }
            }
            streamWriter.Close();
        }
    }
}
