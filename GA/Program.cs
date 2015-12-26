using System;
using System.Collections.Generic;
using System.Linq;

namespace GA
{
    class Program
    {
		
		//w+2x+2y+3z=30
        static void Main(string[] args)
        {
            var randoPop = InitialPoulation(100);
            var f = Fitnesses(randoPop);
            int[] selected = f.OrderBy(x=>x).Take(2).ToArray();
            int j = 0;
            var parrents = new List<Chromosom>(); 
            while (j<selected.Length)
            {
                for (int i = 0; i < f.Length; i++)
                {
                    if (f[i] == selected[j])
                    {
                        parrents.Add( randoPop[i]);                    
                        break;
                    }
                }
                j++;

            }
            var b = true;
            int k = 0;
            while (b)
            {
                var children = Crossover(parrents);
                children = Mutation(children);
                parrents =new List<Chromosom>();
                parrents.AddRange(children);
                foreach (var chromosom in parrents)
                {
                    Console.WriteLine("this generation is: " + k + " ");
                    Console.WriteLine(chromosom.X + " "+ chromosom.Y + " "+ chromosom.W +" " + chromosom.Z );
                }
                f = Fitnesses(parrents);
                foreach (var i in f)
                {
                    if (i==0) b=false;
                }
                k++;
            }
         
            Console.WriteLine("done !!");
            Console.Read();
        }

        private static int[] Fitnesses(List<Chromosom> randoPop)
        {
            int[] f = new int[randoPop.Count];
            for (int i = 0; i < randoPop.Count; i++)
            {
                f[i] = FitnessScor(randoPop[i]);
            }
            return f;
        }

        static List<Chromosom> InitialPoulation(int max)
        {
            var pop = new List<Chromosom>();
            Random rnd = new Random();
            for (int i = 0; i < max; i++)
            {
               
                var r1 = rnd.Next(0, 99);
                var r2 = rnd.Next(0, 99);
                var r3 = rnd.Next(0, 99);
                var r4 = rnd.Next(0, 99);
                pop.Add(new Chromosom
                {
                    X = r1,
                    Y = r2,
                    Z = r3,
                    W = r4
                });
                
            }
            return pop;
        }

        static int FitnessScor(Chromosom chromosom)
        {
            return chromosom.W + chromosom.X*2 + chromosom.Y*2 + chromosom.Z*3 - 30;
        }

        static List<Chromosom> Mutation(List<Chromosom> children)
        {
            
            Random rnd = new Random();
            foreach (var chromosom in children)
            {
                var r = rnd.Next(1, 5);
                switch (r)
                {
                    case 1: chromosom.X = rnd.Next(-99, 99);break;
                    case 2: chromosom.Y = rnd.Next(-99, 99); break;
                    case 3: chromosom.Z = rnd.Next(-99, 99); break;
                    case 4: chromosom.W = rnd.Next(-99, 99); break;
                }
                     
                
            }
            return children;
        } 
        static List<Chromosom> Crossover(List<Chromosom> parents)
        {
            var result = new List<Chromosom>();
            Random rnd = new Random();
            
            foreach (var chromosom in parents)
            {
                
                var r = rnd.Next(0, 5);
                switch (r)
                {
                    case 0:result.Add(parents[1]);break;
                    case 1:result.Add(new Chromosom
                    {
                        X = chromosom.W,
                        Y= parents[1].Y,
                        Z = parents[1].Z,
                        W = parents[1].W,
                    });break;
                    case 2: result.Add(new Chromosom
                    {
                        X = chromosom.W,
                        Y = chromosom.Z,
                        Z = parents[1].Z,
                        W = parents[1].W,
                    });break;
                    case 3: result.Add(new Chromosom
                    {
                        X = chromosom.W,
                        Y = chromosom.Z,
                        Z = chromosom.Y,
                        W = parents[1].W,
                    });break;
                    case 4: result.Add(chromosom); break;
                }
            }
            return result;
        } 
      
    }
}
