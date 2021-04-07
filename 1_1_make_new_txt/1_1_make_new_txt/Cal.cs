using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_1_make_new_txt
{
    class Cal
    {
        public static void SiO2_2nm_Cal(List<SIO2Data_dat> records, int linenum)
        {
            StreamWriter streamWriter = new StreamWriter(new FileStream("SiO2_2nm_on_Si_new.dat", FileMode.Create));
            streamWriter.WriteLine("wavelength(nm)\t AOI\t\t alpha\t beta");

            double nm = 0.0f;
            double Psi = 0.0f;
            double Delta = 0.0f;
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
                nm = Convert.ToSingle(records[i].wavelength);
                Psi = Convert.ToSingle(records[i].Psi);
                Delta = Convert.ToSingle(records[i].Delta);

                // 수식 적용
                tan_sq = Math.Pow(Math.Tan(Rad2deg(Psi)), 2);
                a_numeator = tan_sq - Math.Pow(Math.Tan(Rad2deg(45)), 2);
                a_denominator = tan_sq + Math.Pow(Math.Tan(Rad2deg(45)), 2); ;
                alpha = a_numeator / a_denominator;

                b_numeator = 2 * Math.Tan(Rad2deg(Psi)) * Math.Cos(Rad2deg(Delta));
                b_denominator = tan_sq + Math.Pow(Math.Tan(Rad2deg(45)), 2); ;
                beta = b_numeator / b_denominator;

                if (nm > 350 && nm < 980)
                {
                    streamWriter.WriteLine("{0}\t {1}\t {2}\t {3}", nm, records[i].AOI, alpha, beta);
                }
            }
            streamWriter.Close();
        }
    }
}
