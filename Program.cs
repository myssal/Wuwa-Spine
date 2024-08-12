using System;
using System.Buffers.Binary;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Spine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            if (args.Length < 1 || args[0] == "--help" || args[0] == "-h")
            {
                Console.WriteLine("Wuwa_Spine.exe {spine folder} or directly drag folder into .exe");
                return;
            }
            string root = args[0];*/
            string test = "a_nvzhu_0s.png\r\nsize:1024,1024\r\nfilter:Linear,Linear\r\nscale:0.22\r\na_nvzhu_fuhao\r\nbounds:801,797,85,125\r\na_nvzhu_hz\r\nbounds:230,440,21,39\r\nnz_a_body\r\nbounds:925,554,94,37\r\noffsets:0,9,94,46\r\nrotate:90\r\nnz_a_body2\r\nbounds:2,34,71,103\r\nnz_a_body3\r\nbounds:952,218,38,81\r\noffsets:9,0,47,81\r\nnz_a_body4\r\nbounds:444,274,122,190\r\nnz_a_d1\r\nbounds:647,971,45,51\r\nnz_a_d10\r\nbounds:888,179,97,70\r\nrotate:90\r\nnz_a_d11\r\nbounds:841,695,73,100\r\nnz_a_d13\r\nbounds:587,167,44,99\r\noffsets:0,0,44,100\r\nnz_a_d14\r\nbounds:560,279,48,102\r\nnz_a_d15\r\nbounds:2,646,23,43\r\nnz_a_d16\r\nbounds:885,771,54,105\r\nnz_a_d17\r\nbounds:863,268,53,101\r\nnz_a_d2\r\nbounds:871,636,82,75\r\nrotate:270\r\nnz_a_d3\r\nbounds:941,822,54,42\r\nnz_a_d4\r\nbounds:801,600,65,152\r\nnz_a_d5\r\nbounds:895,491,71,67\r\nnz_a_d6\r\nbounds:389,250,83,81\r\nrotate:90\r\nnz_a_d7\r\nbounds:654,809,32,46\r\nnz_a_d8\r\nbounds:167,967,115,55\r\nrotate:180\r\nnz_a_d9\r\nbounds:2,688,51,42\r\nnz_a_e1\r\nbounds:317,699,45,9\r\nnz_a_e10\r\nbounds:695,538,67,27\r\nrotate:90\r\nnz_a_e11\r\nbounds:428,802,30,42\r\nnz_a_e12\r\nbounds:2,734,17,18\r\nnz_a_e13\r\nbounds:2,412,37,48\r\nnz_a_e14\r\nbounds:951,963,59,49\r\nrotate:90\r\nnz_a_e2\r\nbounds:416,1011,48,11\r\nnz_a_e3\r\nbounds:54,701,35,9\r\nnz_a_e4\r\nbounds:970,866,79,28\r\nrotate:90\r\nnz_a_e5\r\nbounds:401,478,26,44\r\nnz_a_e6\r\nbounds:279,1001,12,21\r\nnz_a_e7\r\nbounds:947,674,44,51\r\nrotate:90\r\nnz_a_e8\r\nbounds:916,66,66,50\r\nrotate:90\r\nnz_a_e9\r\nbounds:219,700,29,9\r\nnz_a_er\r\nbounds:572,661,30,42\r\nnz_a_f1\r\nbounds:954,363,38,121\r\nnz_a_h1\r\nbounds:224,671,54,62\r\nnz_a_h10\r\nbounds:510,8,272,112\r\nrotate:270\r\nnz_a_h11\r\nbounds:767,257,129,233\r\nnz_a_h2\r\nbounds:344,26,39,59\r\nrotate:90\r\nnz_a_h3\r\nbounds:360,514,128,215\r\nnz_a_h4\r\nbounds:639,316,124,248\r\nnz_a_h5\r\nbounds:591,534,137,273\r\nnz_a_h6\r\nbounds:2,253,106,191\r\nrotate:180\r\nnz_a_h7\r\nbounds:252,335,195,352\r\nnz_a_h8\r\nbounds:625,934,44,35\r\nnz_a_h9\r\nbounds:155,2,231,336\r\nnz_a_head\r\nbounds:2,446,264,251\r\nnz_a_hud\r\nbounds:745,334,112,146\r\nrotate:180\r\nnz_a_l1\r\nbounds:691,783,70,239\r\nrotate:180\r\nnz_a_l2\r\nbounds:730,656,70,191\r\noffsets:2,0,72,191\r\nnz_a_m1\r\nbounds:132,1015,26,7\r\nnz_a_mo\r\nbounds:22,1010,9,4\r\nnz_a_mo2\r\nbounds:192,687,30,11\r\nnz_a_mo3\r\nbounds:773,609,51,27\r\nrotate:90\r\nnz_a_n1\r\nbounds:22,1004,3,4\r\nnz_a_s1\r\nbounds:434,2,45,66\r\nrotate:90\r\nnz_a_s2\r\nbounds:362,947,26,47\r\nnz_a_s3\r\nbounds:276,340,37,46\r\nnz_a_s4\r\nbounds:103,353,20,83\r\noffsets:0,0,22,83\r\nnz_a_s5\r\nbounds:931,720,37,78\r\nnz_a_s6\r\nbounds:58,7,28,78\r\nrotate:90\r\nnz_b_body\r\nbounds:674,135,124,141\r\nrotate:90\r\nnz_b_body2\r\nbounds:796,929,113,93\r\nnz_b_body3\r\nbounds:564,565,128,87\r\nrotate:90\r\nnz_b_body4\r\nbounds:812,2,104,90\r\noffsets:0,0,108,90\r\nrotate:180\r\nnz_b_body4a\r\nbounds:916,268,48,90\r\noffsets:0,0,108,90\r\nnz_b_d1\r\nbounds:858,442,86,87\r\noffsets:0,0,87,87\r\nrotate:90\r\nnz_b_d2\r\nbounds:892,936,86,57\r\noffsets:0,0,86,62\r\nrotate:90\r\nnz_b_d3\r\nbounds:762,966,40,36\r\nnz_b_d4\r\nbounds:894,367,70,75\r\noffsets:7,7,84,88\r\nrotate:180\r\nnz_b_d5\r\nbounds:550,456,79,87\r\noffsets:27,0,106,87\r\nnz_b_d6\r\nbounds:2,3,54,29\r\nnz_b_e2\r\nbounds:344,718,39,11\r\nnz_b_e3\r\nbounds:864,560,74,59\r\nrotate:90\r\nnz_b_er\r\nbounds:297,801,27,38\r\nnz_b_er2\r\nbounds:917,9,51,55\r\nnz_b_h1\r\nbounds:277,2,212,67\r\nnz_b_h11\r\nbounds:525,706,156,256\r\noffsets:26,32,202,325\r\nrotate:180\r\nnz_b_h12\r\nbounds:2,980,13,11\r\nnz_b_h13\r\nbounds:350,996,64,26\r\nnz_b_h14\r\nbounds:376,59,133,228\r\noffsets:0,6,223,234\r\nrotate:180\r\nnz_b_h15\r\nbounds:283,678,181,314\r\nrotate:180\r\nnz_b_h16\r\nbounds:344,731,253,236\r\nnz_b_h17\r\nbounds:12,139,266,311\r\nnz_b_h18\r\nbounds:290,839,80,183\r\noffsets:0,0,128,206\r\nrotate:180\r\nnz_b_h19\r\nbounds:534,934,110,88\r\nnz_b_h2\r\nbounds:970,764,44,63\r\nnz_b_h20\r\nbounds:573,42,184,125\r\nnz_b_h21\r\nbounds:532,248,251,233\r\nrotate:90\r\nnz_b_h3\r\nbounds:773,484,108,116\r\noffsets:0,4,108,120\r\nnz_b_h4\r\nbounds:626,185,95,50\r\nrotate:90\r\nnz_b_h5\r\nbounds:574,4,83,84\r\nrotate:90\r\nnz_b_h6\r\nbounds:252,931,40,34\r\nnz_b_h7\r\nbounds:465,196,46,74\r\nnz_b_h8\r\nbounds:441,476,136,282\r\noffsets:8,31,161,353\r\nnz_b_head\r\nbounds:2,698,293,324\r\noffsets:0,18,293,342\r\nnz_b_l1\r\nbounds:799,163,99,109\r\nnz_b_l2\r\nbounds:722,2,128,132\r\nrotate:90\r\nnz_b_l2a\r\nbounds:915,865,92,61\r\noffsets:46,97,153,158\r\nrotate:90\r\nnz_b_l3\r\nbounds:751,848,98,127\r\nnz_b_l4\r\nbounds:74,36,104,79\r\nrotate:90\r\nnz_b_l5\r\nbounds:438,953,85,69\r\noffsets:46,88,131,157\r\nnz_b_m1\r\nbounds:939,650,58,12\r\nnz_b_m2\r\nbounds:22,1016,15,6\r\nnz_b_s1\r\nbounds:855,100,69,88\r\nrotate:180\r\nnz_b_s2\r\nbounds:465,881,26,59\r\nnz_b_s3\r\nbounds:2,993,18,29\r\nnz_b_z2\r\nbounds:262,657,20,37\r\nnz_b_z3\r\nbounds:588,883,42,56\r\nnz_b_z4\r\nbounds:478,584,23,53\r\nyinying\r\nbounds:724,406,248,47\r\nrotate:90\r\n";
            File.WriteAllText("test.atlas",test);
            Console.ReadLine();
               
        }

       
        public static List<Root> ParseJsonData(string root)
        {
            //List<string> fileList = Directory.EnumerateFiles(root);
            List<string> filePath = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories).ToList();
            List<Root> spineData = new List<Root>();

            
            foreach (var file in filePath)
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    //pass empty file
                    if (new FileInfo(file).Length > 0 && !file.Contains("Textures"))
                    {
                        try
                        {
                            Console.WriteLine($"Parse {file}");
                            string jsonData = sr.ReadToEnd();
                            spineData = JsonSerializer.Deserialize<List<Root>>(file);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
            }
            return spineData;
        }

        public static void DecodeBytes(List<int> skel, string subFolder)
        {
            int[] inputInt = skel.ToArray();
            byte[] buffer = inputInt.Select(x => (byte)x).ToArray();
            File.WriteAllBytes($"{subFolder}.skel", buffer);
        }

        public static void WriteAtlasToFile(string data, string subFolder)
        {
            File.WriteAllText($"{subFolder}.atlas", data);
        }
    }
}