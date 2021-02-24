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
            foreach (var item in ele.andarMenosUtilizado())
            {
                Console.WriteLine("Andares menos utilizados:"+ "" + item);
            }
            foreach (var item in ele.elevadorMaisFrequentado())
            {
                Console.WriteLine("Elevador mais Frequentados:" + "" + item);
            }
            foreach (var item in ele.elevadorMenosFrequentado())
            {
                Console.WriteLine("Elevador menos Frequentados:" + "" + item);
            }
            ele.periodoMaiorFluxoElevadorMaisFrequentado();
            ele.periodoMenorFluxoElevadorMenosFrequentado();
            foreach (var item in ele.periodoMaiorUtilizacaoConjuntoElevadores())
            {
                Console.WriteLine("Periodo com a maior utilização do conjunto de Elevadores: "+item);
            }
            Console.WriteLine("Percentual de utilizacao do Elevador A:" + "" + ele.percentualDeUsoElevadorA());
            Console.WriteLine("Percentual de utilizacao do Elevador B:" + "" + ele.percentualDeUsoElevadorB());
            Console.WriteLine("Percentual de utilizacao do Elevador C:" + "" + ele.percentualDeUsoElevadorC());
            Console.WriteLine("Percentual de utilizacao do Elevador D:" + "" + ele.percentualDeUsoElevadorD());
            Console.WriteLine("Percentual de utilizacao do Elevador E:" + "" + ele.percentualDeUsoElevadorE());

            Console.ReadLine();
        }

        public List<int> andarMenosUtilizado()
        {   
            List<int> result = new List<int>();
            ///Crio uma variavel anonima para pegar a listagem de utilização do andar
            var objGroup = inputs
                .GroupBy(p => p.Andar)
                .Select(p => new {
                    Andar = p.Key,
                    Qtd = p.Count()
                });
            ///Pego o menor valor de utilização para utilizar no Where
            var minGroup = objGroup.Min(p => p.Qtd);

            ///faço um loop foreach para jogar na List os anderes ou o andar menos utilizado
            foreach (var item in objGroup.Where(p => p.Qtd ==minGroup))
            {

                result.Add(item.Andar);
            }
            
            
            return result;
        }
        public List<char> elevadorMaisFrequentado()
        {
            List<char> result = new List<char>();
            ///Crio uma variavel anonima para pegar a listagem de utilização dos elevadores
            var objGroup = inputs
                .GroupBy(p => p.Elevador)
                .Select(p => new
                {
                    Elevador = p.Key,
                    Qtd = p.Count()
                });
            ///Pego o maior valor de utilização para utilizar no Where
            var maxGroup = objGroup.Max(p => p.Qtd);
            ///faço um loop foreach para jogar na List os elevadores ou o elevador mais frequentado
            foreach (var item in objGroup.Where(p=>p.Qtd == maxGroup))
            {
                result.Add(item.Elevador);
            }

            return result;
            
        }

        public List<char> elevadorMenosFrequentado()
        {
            List<char> result = new List<char>();
            ///Crio uma variavel anonima para pegar a listagem de utilização dos elevadores
            var objGroup = inputs
                .GroupBy(p => p.Elevador)
                .Select(p => new
                {
                    Elevador = p.Key,
                    Qtd = p.Count()
                });
            ///Pego o menor valor de utilização para utilizar no Where
            var minGroup = objGroup.Min(p => p.Qtd);
            foreach (var item in objGroup.Where(p => p.Qtd == minGroup))
            {
                result.Add(item.Elevador);
            }

            return result;
        }
        ///Crio um metodo apenas que recebe como parametro uma string para poder reaproveitalo de forma mais facil
        ///no codigo, recebendo apenas o elevador como parametro para utilizar a quantidade dele no calculo 
        public float calculoPercentualUsoElevadores(string elevador)
        {
            ///Crio uma variavel anonima para pegar a listagem de utilização do elevador
            var objGroup = inputs
               .GroupBy(p => p.Elevador)
               .Select(p => new
               {
                   Elevador = p.Key,
                   Qtd = p.Count()
               });
            ///Pego o numero maximo de uso dos elevadores para ter como base para o 100%
            float maxUso = objGroup.Sum(p => p.Qtd);
            ///Pego o uso do determinado elevador que foi passado como parametro
            float usoElevador = (float)objGroup
                .Where(p => p.Elevador == Convert.ToChar(elevador))
                .Select(q => q.Qtd)
                .FirstOrDefault();
            ///Realizo uma regra de 3 para poder pegar a porcentagem e faço um arredondamento para pegar apenas duas casas decimais
            float result = (float)Math.Round((usoElevador * 100) / 23, 2);
            return result;
        }
        public float percentualDeUsoElevadorA()
        {
            ///Apenas chamo o metodo generico criado passando o elevador como parametro
            float result = calculoPercentualUsoElevadores("A");
            
            return result;
        }

        public float percentualDeUsoElevadorB()
        {
            ///Apenas chamo o metodo generico criado passando o elevador como parametro
            float result = calculoPercentualUsoElevadores("B");
            return result;
        }

        public float percentualDeUsoElevadorC()
        {
            ///Apenas chamo o metodo generico criado passando o elevador como parametro
            float result = calculoPercentualUsoElevadores("C");
            return result;
        }

        public float percentualDeUsoElevadorD()
        {
            ///Apenas chamo o metodo generico criado passando o elevador como parametro
            float result = calculoPercentualUsoElevadores("D");
            return result;
        }

        public float percentualDeUsoElevadorE()
        {
            ///Apenas chamo o metodo generico criado passando o elevador como parametro
            float result = calculoPercentualUsoElevadores("E");
            return result;
        }

        public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
        {
            List<char> result = new List<char>();
            ///Pego o elevador mais frequentado para utilizar como base no where na variavel objGroup
            char elevadorMaisFrequentado = this.elevadorMaisFrequentado().First();
            ///Crio uma variavel anonima pegando o periodo de maior fluxo do elevador mais frequentado
            var objGroup = inputs
                .Where(p => p.Elevador == elevadorMaisFrequentado)
                .GroupBy(p => p.Turno)
                .Select(p => new
                {
                    Turno = p.Key,
                    Qtd = p.Count()

                })
                .OrderByDescending(p => p.Qtd);
            ///Jogo o periodo para dentro da lista para poder retornalo
            foreach (var item in objGroup)
            {
                result.Add(item.Turno);
            }
            Console.WriteLine("Periodo com o maior Fluxo no Elevador " + elevadorMaisFrequentado +": "+result.First());
            return result;
        }

        public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
        {
            List<char> result = new List<char>();
            ///Crio a variavel anonima contendo a listagem de utilização do conjunto de elevadores
            var objGroup = inputs
                .GroupBy(p => p.Turno)
                .Select(p => new
                {
                    Turno = p.Key,
                    Qtd = p.Count()
                });
            ///Crio a variavel para ser utilizado no where pegando o maior valor de utilização
            var maxGroup = objGroup.Max(p => p.Qtd);
            ///realizo o loop jogando o periodo ou periodos na lista para poder retornar
            foreach (var item in objGroup.Where(p => p.Qtd == maxGroup))
            {
                result.Add(item.Turno);
            }            
            return result;
        }

        public List<char> periodoMenorFluxoElevadorMenosFrequentado()
        {
            List<char> result = new List<char>();
            ///Pego o elevador menos frequentado para utilizar como base no where na variavel objGroup
            char elevadorMenosFrequentado = this.elevadorMenosFrequentado().First();
            ///Crio uma variavel anonima pegando o periodo de menor fluxo do elevador menos frequentado
            var objGroup = inputs
                .Where(p => p.Elevador == elevadorMenosFrequentado)
                .GroupBy(p => p.Turno)
                .Select(p => new
                {
                    Turno = p.Key,
                    Qtd = p.Count()

                })
                .OrderBy(p => p.Qtd);
            ///Jogo o periodo para dentro da lista para poder retornalo
            foreach (var item in objGroup)
            {
                result.Add(item.Turno);
            }
            Console.WriteLine("Periodo com o menor Fluxo no Elevador " + elevadorMenosFrequentado + ": " + result.First());
            return result;
        }
    }
}
