﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _1_3_Cal_relect_s_p_Cal_alpha_beta
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] replace = { ' ', ',', '\t', '\n' };
            string[] Silines = File.ReadAllLines(@"C:\Users\bit\Desktop\BIT-auros\BIT\Si_nm.txt", Encoding.Default);

            int Si_nm_linesNum = Silines.Length;

            List<Si_nm_Data> Sirecords = new List<Si_nm_Data>();

            foreach (var line in Silines)
            {
                string[] splitData = line.Split(replace, StringSplitOptions.RemoveEmptyEntries);
                Sirecords.Add(new Si_nm_Data
                {
                    nm = splitData[0],
                    n = splitData[1],
                    k = splitData[2]
                });
                //Console.WriteLine($"{splitData[0]}\t{splitData[1]}\t{splitData[2]}");
            }

            Cal_reflect.Si_relect(Sirecords, Si_nm_linesNum);
            WriteLine("Si_new.txt 생성");
        }
    }
}
