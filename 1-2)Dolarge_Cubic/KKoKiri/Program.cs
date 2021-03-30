using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dat_cublic_spline;
using static Dat_cublic_spline.Cal;
//using System.Data;

namespace KKoKiri
{
    class EntryPoint
    {
        static void Main(string[] args)
        {
            char[] replace = { ' ', ',', '\t', '\n', Convert.ToChar(10), Convert.ToChar(13) };
            char[] replace2SiO2 = { ' ', '\t', '\n', Convert.ToChar(10), Convert.ToChar(13) };
            double ChangeX, ChangeY1, ChangeY2;

          
            #region 1.Dat 파일 읽기
            string[] Dat_sioDat = File.ReadAllLines("SiO2 1000nm_on_Si.dat", Encoding.Default);
            List<sio2_2nm_on_si_dat> Dat_wavelen = new List<sio2_2nm_on_si_dat>();
            StreamReader sio2_2nm_text = new StreamReader(new FileStream("SiO2 1000nm_on_Si.dat", FileMode.Open));
            foreach (var line in Dat_sioDat)
            {
                string[] splietData = line.Split(replace, StringSplitOptions.RemoveEmptyEntries);
                Dat_wavelen.Add(new sio2_2nm_on_si_dat
                {
                    wavelength = splietData[0],
                    AOI = splietData[1],
                    Alpha = splietData[2],
                    Beta = splietData[3],
                });

            }
            List<double> List_dat_wave = new List<double>();
            List<double> List_dat_aoi = new List<double>();
            List<double> List_dat_alpha = new List<double>();
            List<double> List_dat_beta = new List<double>();
            double waveToDoubeWabe;
            for (int i = 1; i < Dat_sioDat.Length; i++)
            {
                waveToDoubeWabe = Convert.ToDouble(Dat_wavelen[i].wavelength);
                if (waveToDoubeWabe > 350 && waveToDoubeWabe < 980)
                {
                    List_dat_wave.Add(waveToDoubeWabe);
                    List_dat_aoi.Add(Convert.ToDouble(Dat_wavelen[i].AOI));
                    List_dat_alpha.Add(Convert.ToDouble(Dat_wavelen[i].Alpha));
                    List_dat_beta.Add(Convert.ToDouble(Dat_wavelen[i].Beta));
                }
            }
            double[] Dat_double_wavelength = List_dat_wave.ToArray();
            double[] Dat_double_AOI = List_dat_aoi.ToArray();
            double[] Dat_double_Alpha = List_dat_alpha.ToArray();
            double[] Dat_double_Beta = List_dat_beta.ToArray();

            sio2_2nm_text.Close();
            #endregion

            #region 2.SiN 읽기---------------------------------------
            string[] siLines = File.ReadAllLines("SiN.txt", Encoding.Default);
            List<SiData> ReadData = new List<SiData>();
            
            StreamWriter newdatfile = new StreamWriter(new FileStream("SIN.txt", FileMode.Open));
            foreach (var line in siLines)
            {
                string[] splitData = line.Split(replace, StringSplitOptions.RemoveEmptyEntries);
                if (ReadData.Count <= 1)
                {
                    ReadData.Add(new SiData());
                }
                if (ReadData.Count > 1)
                {
                    ReadData.Add(
                    new SiData
                    {
                        NM = splitData[0],
                        N = splitData[1],
                        K = splitData[2]
                    });
                }
            }
            List<double> List_SIN_NM = new List<double>();
            List<double> List_SIN_N = new List<double>();
            List<double> List_SIN_K = new List<double>();

            for (int i = 3; i < siLines.Length; i++)
            {
                ChangeX = double.Parse(ReadData[i].NM);
                ChangeY1 = double.Parse(ReadData[i].N);
                ChangeY2 = double.Parse(ReadData[i].K);

                List_SIN_NM.Add(ChangeX);
                List_SIN_N.Add(ChangeY1);
                List_SIN_K.Add(ChangeY2);
            }
            double[] Sin_nm = List_SIN_NM.ToArray();
            double[] Sin_n = List_SIN_N.ToArray();
            double[] Sin_K = List_SIN_K.ToArray();

            Dat_cublic_spline.Cal.CubicSplineInterpolation Sin_CSN = new Dat_cublic_spline.Cal.CubicSplineInterpolation(Sin_nm, Sin_n);
            Dat_cublic_spline.Cal.CubicSplineInterpolation Sin_CSK = new Dat_cublic_spline.Cal.CubicSplineInterpolation(Sin_nm, Sin_K);


            newdatfile.Close();
            #endregion

            #region 3. SiO2_nm 읽기
            string[] string_sio2_nm = File.ReadAllLines("SIO2_nm.txt", Encoding.Default);
            List<SiData> List_sion_NM = new List<SiData>();
            StreamWriter newSio2Txt = new StreamWriter(new FileStream("SIO2_nm.txt", FileMode.Open));
            foreach (var lines in string_sio2_nm)
            {
                string[] splitData = lines.Split(replace2SiO2, StringSplitOptions.RemoveEmptyEntries);
                List_sion_NM.Add(new SiData
                {
                    NM = splitData[0],
                    N = splitData[1],
                    K = splitData[2]
                });
            }

            List<double> List_SiO2_NM = new List<double>();
            List<double> List_SiO2_N = new List<double>();
            List<double> List_SiO2_K = new List<double>();

            for (int i = 1; i < string_sio2_nm.Length; i++)
            {
                ChangeX = double.Parse(List_sion_NM[i].NM);
                ChangeY1 = double.Parse(List_sion_NM[i].N);
                ChangeY2 = double.Parse(List_sion_NM[i].K);

                List_SiO2_NM.Add(ChangeX);
                List_SiO2_N.Add(ChangeY1);
                List_SiO2_K.Add(ChangeY2);
            }
            double[] SiO2_nm = List_SiO2_NM.ToArray();
            double[] SiO2_n = List_SiO2_N.ToArray();
            double[] SiO2_k = List_SiO2_K.ToArray();

            Cal.CubicSplineInterpolation SiO2_CSN = new Cal.CubicSplineInterpolation(SiO2_nm, SiO2_n);
            Cal.CubicSplineInterpolation SiO2_CSK = new Cal.CubicSplineInterpolation(SiO2_nm, SiO2_k);

            newSio2Txt.Close();

            #endregion

            #region 4. Si_nm 읽기

            string[] string_si_nm = File.ReadAllLines("Si_nm.txt", Encoding.Default);
            List<SiData> List_si_NM = new List<SiData>();
            StreamWriter newSiTxt = new StreamWriter(new FileStream("Si_nm.txt", FileMode.Open));
            foreach (var lines in string_si_nm)
            {
                string[] splitData = lines.Split(replace2SiO2, StringSplitOptions.RemoveEmptyEntries);
                List_si_NM.Add(new SiData
                {
                    NM = splitData[0],
                    N = splitData[1],
                    K = splitData[2]
                });
            }

            List<double> List_Si_NM = new List<double>();
            List<double> List_Si_N = new List<double>();
            List<double> List_Si_K = new List<double>();

            for (int i = 1; i < string_si_nm.Length; i++)
            {
                ChangeX = double.Parse(List_si_NM[i].NM);
                ChangeY1 = double.Parse(List_si_NM[i].N);
                ChangeY2 = double.Parse(List_si_NM[i].K);

                List_Si_NM.Add(ChangeX);
                List_Si_N.Add(ChangeY1);
                List_Si_K.Add(ChangeY2);
            }
            double[] Si_nm = List_Si_NM.ToArray();
            double[] Si_n = List_Si_N.ToArray();
            double[] Si_k = List_Si_K.ToArray();

            CubicSplineInterpolation Si_CSN = new CubicSplineInterpolation(Si_nm, Si_n);
            CubicSplineInterpolation Si_CSK = new CubicSplineInterpolation(Si_nm, Si_k);

            newSiTxt.Close();

            #endregion

            #region 5. SiN_New 파일 쓰기

            StreamWriter ChangeTxt = new StreamWriter(new FileStream("SIN_NEW.txt", FileMode.Create));
            List<NewData> List_SiN = new List<NewData>();
            ChangeTxt.WriteLine("wavelength(nm)\tn\tk");

            for (int i = 1; i < Dat_double_wavelength.Length; i++)
            {
                if (Dat_double_wavelength[i] > 350 && Dat_double_wavelength[i] < 980)
                {
                    List_SiN.Add(new NewData
                    {
                        NEWnm = Dat_double_wavelength[i],
                        NEWN = (double)Sin_CSN.Interpolate(Dat_double_wavelength[i]),
                        NEWK = (double)Sin_CSK.Interpolate(Dat_double_wavelength[i])
                    });
                }
            }

            for (int i = 0; i < List_SiN.Count; i++)
            {
                if (List_SiN[i].NEWnm >= 350)
                {
                    ChangeTxt.WriteLine("{0}\t{1}\t{2}", List_SiN[i].NEWnm, List_SiN[i].NEWN, List_SiN[i].NEWK);
                }
            }
            ChangeTxt.Close();

            #endregion

            #region 6. SiO2_new 파일 쓰기

            
            StreamWriter ChageSio2Txt = new StreamWriter(new FileStream("SiO2_new.txt", FileMode.Create));
            List<NewData> List_SiO2 = new List<NewData>();
            ChageSio2Txt.WriteLine("wavelength(nm)\tn\tk");
            //i=0은 해더, Dat_Double_NM= 파장값 ~.dat파일에서 뽑아온거
            for (int i = 0; i < Dat_double_wavelength.Length; i++)
            {
                if (Dat_double_wavelength[i] > 980)
                    break;

                List_SiO2.Add(new NewData
                {
                    NEWnm = Dat_double_wavelength[i],
                    NEWN = (double)SiO2_CSN.Interpolate(Dat_double_wavelength[i]),
                    NEWK = (double)SiO2_CSK.Interpolate(Dat_double_wavelength[i])
                });
                ChageSio2Txt.WriteLine("{0}\t{1}\t{2}", List_SiO2[i].NEWnm, List_SiO2[i].NEWN, List_SiO2[i].NEWK);
            }
            ChageSio2Txt.Close();

            #endregion

            #region 7. Si_new 파일 쓰기
            StreamWriter ChageSiTxt = new StreamWriter(new FileStream("Si_new.txt", FileMode.Create));
            List<NewData> List_Si = new List<NewData>();
            ChageSiTxt.WriteLine("wavelength(nm)\tn\tk");

            for (int i = 0; i < Dat_double_wavelength.Length; i++)
            {
                if (Dat_double_wavelength[i] > 980)
                    break;

                List_Si.Add(new NewData
                {
                    NEWnm = Dat_double_wavelength[i],
                    NEWN = (double)Si_CSN.Interpolate(Dat_double_wavelength[i]),
                    NEWK = (double)Si_CSK.Interpolate(Dat_double_wavelength[i])
                });
                ChageSiTxt.WriteLine("{0}\t{1}\t{2}", List_Si[i].NEWnm, List_Si[i].NEWN, List_Si[i].NEWK);
            }
            ChageSiTxt.Close();

            #endregion

        }


    }
}





