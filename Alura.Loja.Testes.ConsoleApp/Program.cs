using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var p1 = new Produto() { Nome = "Suco", Categoria = "Bebidas", PrecoUnitario = 1.79, Unidade = "Litros" };
            var p2 = new Produto() { Nome = "Cafe", Categoria = "Bebidas", PrecoUnitario = 8.90, Unidade = "Gramas"};
            var p3 = new Produto() { Nome = "Macarrão", Categoria = "Alimentos", PrecoUnitario = 2.90, Unidade = "Gramas" };

            var promocaoDePascoa = new Promocao();
            promocaoDePascoa.Descricao = "Pascoa Feliz";
            promocaoDePascoa.DataInicio = DateTime.Now;
            promocaoDePascoa.DataTermino = DateTime.Now.AddMonths(3);

            promocaoDePascoa.IncluirProdtuo(p1);
            promocaoDePascoa.IncluirProdtuo(p2);
            promocaoDePascoa.IncluirProdtuo(p3);


            using (var contexto = new LojaContext())
            {
                contexto.Promocoes.Add(promocaoDePascoa);
                contexto.SaveChangesAsync();
                
            }
        }
    }
}
