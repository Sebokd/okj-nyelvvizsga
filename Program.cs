using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyelvvizsga
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] sorokSikeres = File.ReadAllLines("sikeres.csv", Encoding.UTF8);
            string[] sorokSikertelen = File.ReadAllLines("sikertelen.csv", Encoding.UTF8);

            List<Stat> Statlist = new List<Stat>();

            for (int i = 0; i < sorokSikertelen.Length; i++)
            {
                Statlist.Add(new Stat(sorokSikertelen[i], sorokSikeres[i]));
            }

            masodikFeladat(Statlist);
            harmadikFeladat(Statlist);
            void masodikFeladat(List<Stat> statList)
            {
                Dictionary<string, int> tizevlistaja = new Dictionary<string, int>();
                foreach (var stat in Statlist)
                {
                    tizevlistaja.Add(stat.nyelv, stat.osszesNyelvvizsga);
                }

                var top3 = tizevlistaja.OrderByDescending(pair => pair.Value).Take(3);

                Console.WriteLine("2. feladat: a legnépszerűbb nyelvek:");
                foreach (var top in top3)
                {
                    Console.WriteLine(top.Key);
                }
            }

            int harmadikFeladat(List<Stat> statList)
            {
                Console.WriteLine("3. feladat: ");
                int queriedYear = 0;

                do
                {
                    Console.Write("Vizsgálandó év: ");
                    queriedYear = Convert.ToInt32(Console.ReadLine());

                } while (!(2009 <= queriedYear && queriedYear <= 2018));

                return queriedYear;
            }


            Console.ReadLine();
        }



    }


    class Stat
    {
        public string nyelv { get; set; }
        public Dictionary<int,int> evSikeres { get; set; }
        public Dictionary<int,int> evSikertelen { get; set; }
        public int osszesNyelvvizsga { get; set; }
        public Dictionary<int, double> evsikertelenPerOsszesAranya { get; set; }
        public Dictionary<int, double> evsikeresPerOsszesAranya { get; set; }

        public Stat(string sorokSikertelen, string sorokSikeres)
        {

            evSikeres = new Dictionary<int, int>();
            evSikertelen = new Dictionary<int, int>();


            evsikeresPerOsszesAranya = new Dictionary<int, double>();
            evsikertelenPerOsszesAranya = new Dictionary<int, double>();


            string[] SikeretelenSplitted = sorokSikertelen.Split(';');
            string[] SikeresSplitted = sorokSikeres.Split(';');


            nyelv = SikeretelenSplitted[0];


            int evszamlalo = 2009;
            for (int i = 1; i < SikeretelenSplitted.Length; i++)
            {
                evSikertelen.Add(evszamlalo, Convert.ToInt32(SikeretelenSplitted[i]));
                evszamlalo++;
            }

            evszamlalo = 2009;
            for (int i = 1; i < SikeresSplitted.Length; i++)
            {
                evSikeres.Add(evszamlalo, Convert.ToInt32(SikeresSplitted[i]));
                evszamlalo++;
            }


            for (int i = 1; i < SikeretelenSplitted.Length; i++)
            {
                osszesNyelvvizsga += Convert.ToInt32(SikeresSplitted[i]) + Convert.ToInt32(SikeretelenSplitted[i]);
            }


            evszamlalo = 2009;
            for (int i = 1; i < SikeretelenSplitted.Length; i++)
            {
                int oszto = Convert.ToInt32(SikeresSplitted[i]) + Convert.ToInt32(SikeretelenSplitted[i]);


                if (oszto == 0)
                {
                    evsikertelenPerOsszesAranya.Add(evszamlalo, Convert.ToInt32(SikeretelenSplitted[i]) / 1);
                    evszamlalo++;
                }
                else
                {
                    evsikertelenPerOsszesAranya.Add(evszamlalo, Convert.ToInt32(SikeretelenSplitted[i]) / oszto);
                    evszamlalo++;
                }
            }

            evszamlalo = 2009;
            for (int i = 1; i < SikeresSplitted.Length; i++)
            {
                int oszto = Convert.ToInt32(SikeresSplitted[i]) + Convert.ToInt32(SikeretelenSplitted[i]);


                if (oszto == 0)
                {
                    evsikeresPerOsszesAranya.Add(evszamlalo, Convert.ToDouble(SikeresSplitted[i]) / 1);
                    evszamlalo++;
                }
                else
                {
                    evsikeresPerOsszesAranya.Add(evszamlalo, Convert.ToDouble(SikeresSplitted[i]) / oszto);
                    evszamlalo++;
                }
            }




        }

    }

}


