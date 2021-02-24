using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevadorApisul
{
    class ElevadorService : IElevadorService
    {

        List<Input> inputs = JsonInputs();
        /// <summary>
        ///Realizo a leitura do json utilizando o Newtonsoft deserializando 
        ///ele para joga-lo para uma lista de objetos definidos na classe Input.cs 
        /// </summary>       
        public static List<Input> JsonInputs () {
            StreamReader r = new StreamReader("input.json");
            List<Input> inputs = JsonConvert.DeserializeObject<List<Input>>(r.ReadToEnd());
            return inputs;
        }
        public static void Main(string[] args)
        {
            IElevadorService ele = new ElevadorService();

            ele.andarMenosUtilizado();
            ele.elevadorMaisFrequentado();
            ele.elevadorMenosFrequentado();
            ele.percentualDeUsoElevadorA();
            ele.periodoMaiorFluxoElevadorMaisFrequentado();
            ele.periodoMenorFluxoElevadorMenosFrequentado();

            Console.ReadLine();
        }

        public List<int> andarMenosUtilizado()
        {
            List<int> result = new List<int>();
            var objGroup = inputs
                .GroupBy(p => p.Andar)
                .Select(p => new {
                    Andar = p.Key,
                    Qtd = p.Count()
                });
            var minGroup = objGroup.Min(p => p.Qtd);

            foreach (var item in objGroup.Where(p => p.Qtd ==minGroup))
            {

                result.Add(item.Andar);
            }
            
            
            return result;
        }
        public List<char> elevadorMaisFrequentado()
        {
            List<char> result = new List<char>();
            var objGroup = inputs
                .GroupBy(p => p.Elevador)
                .Select(p => new
                {
                    Elevador = p.Key,
                    Qtd = p.Count()
                });
            var maxGroup = objGroup.Max(p => p.Qtd);
            foreach (var item in objGroup.Where(p=>p.Qtd == maxGroup))
            {
                result.Add(item.Elevador);
            }

            return result;
            
        }

        public List<char> elevadorMenosFrequentado()
        {
            List<char> result = new List<char>();
            var objGroup = inputs
                .GroupBy(p => p.Elevador)
                .Select(p => new
                {
                    Elevador = p.Key,
                    Qtd = p.Count()
                });
            var minGroup = objGroup.Min(p => p.Qtd);
            foreach (var item in objGroup.Where(p => p.Qtd == minGroup))
            {
                result.Add(item.Elevador);
            }

            return result;
        }

        public float percentualDeUsoElevadorA()
        {
            float result = new float();
            var objGroup = inputs
               .GroupBy(p => p.Elevador)
               .Select(p => new
               {
                   Elevador = p.Key,
                   Qtd = p.Count()
               });
            float maxUso = objGroup.Sum(p => p.Qtd);
            float usoElevadorA = (float)objGroup.Where(p => p.Elevador == 'A').Select(q => q.Qtd).FirstOrDefault();
            result = (float)Math.Round((usoElevadorA * 100) / 23,2);
            return result;
        }

        public float percentualDeUsoElevadorB()
        {
            float result = new float();
            var objGroup = inputs
               .GroupBy(p => p.Elevador)
               .Select(p => new
               {
                   Elevador = p.Key,
                   Qtd = p.Count()
               });
            float maxUso = objGroup.Sum(p => p.Qtd);
            float usoElevadorB = (float)objGroup.Where(p => p.Elevador == 'B').Select(q => q.Qtd).FirstOrDefault();
            result = (float)Math.Round((usoElevadorB * 100) / 23, 2);
            return result;
        }

        public float percentualDeUsoElevadorC()
        {
            float result = new float();
            var objGroup = inputs
               .GroupBy(p => p.Elevador)
               .Select(p => new
               {
                   Elevador = p.Key,
                   Qtd = p.Count()
               });
            float maxUso = objGroup.Sum(p => p.Qtd);
            float usoElevadorC = (float)objGroup.Where(p => p.Elevador == 'C').Select(q => q.Qtd).FirstOrDefault();
            result = (float)Math.Round((usoElevadorC * 100) / 23, 2);
            return result;
        }

        public float percentualDeUsoElevadorD()
        {
            float result = new float();
            var objGroup = inputs
               .GroupBy(p => p.Elevador)
               .Select(p => new
               {
                   Elevador = p.Key,
                   Qtd = p.Count()
               });
            float maxUso = objGroup.Sum(p => p.Qtd);
            float usoElevadorD = (float)objGroup.Where(p => p.Elevador == 'D').Select(q => q.Qtd).FirstOrDefault();
            result = (float)Math.Round((usoElevadorD * 100) / 23, 2);
            return result;
        }

        public float percentualDeUsoElevadorE()
        {
            float result = new float();
            var objGroup = inputs
               .GroupBy(p => p.Elevador)
               .Select(p => new
               {
                   Elevador = p.Key,
                   Qtd = p.Count()
               });
            float maxUso = objGroup.Sum(p => p.Qtd);
            float usoElevadorE = (float)objGroup.Where(p => p.Elevador == 'E').Select(q => q.Qtd).FirstOrDefault();
            result = (float)Math.Round((usoElevadorE * 100) / 23, 2);
            return result;
        }

        public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
        {
            List<char> result = new List<char>();
            char elevadorMaisFrequentado = this.elevadorMaisFrequentado().First();
            var objGroup = inputs
                .Where(p => p.Elevador == elevadorMaisFrequentado)
                .GroupBy(p => p.Turno)
                .Select(p => new
                {
                    Turno = p.Key,
                    Qtd = p.Count()

                })
                .OrderByDescending(p => p.Qtd);
            foreach (var item in objGroup)
            {
                result.Add(item.Turno);
            }
            return result;
        }

        public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
        {
            List<char> result = new List<char>();
            var objGroup = inputs
                .GroupBy(p => p.Turno)
                .Select(p => new
                {
                    Turno = p.Key,
                    Qtd = p.Count()
                });
            var maxGroup = objGroup.Max(p => p.Qtd);
            foreach (var item in objGroup.Where(p => p.Qtd == maxGroup))
            {
                result.Add(item.Turno);
                Console.WriteLine(item);
            }
            return result;
        }

        public List<char> periodoMenorFluxoElevadorMenosFrequentado()
        {
            List<char> result = new List<char>();
            char elevadorMenosFrequentado = this.elevadorMenosFrequentado().First();
            var objGroup = inputs
                .Where(p => p.Elevador == elevadorMenosFrequentado)
                .GroupBy(p => p.Turno)
                .Select(p => new
                {
                    Turno = p.Key,
                    Qtd = p.Count()

                })
                .OrderBy(p => p.Qtd);
            foreach (var item in objGroup)
            {
                Console.WriteLine(item);
            }
            foreach (var item in objGroup)
            {
                result.Add(item.Turno);
            }
            return result;
        }
    }
}
