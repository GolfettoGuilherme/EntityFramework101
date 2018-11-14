using Microsoft.EntityFrameworkCore;
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
            using(var contexto = new LojaContext())
            {
                var cliente = contexto.Clientes.Include(c => c.EnderecoDeEntrega).FirstOrDefault();

                Console.WriteLine(cliente.EnderecoDeEntrega.Logradouro);

                var produto = contexto.Produtos.Include(p => p.Compras).Where(p => p.Id == 4).FirstOrDefault();

                contexto.Entry(produto)
                    .Collection(p => p.Compras)
                    .Query()
                    .Where(c => c.Preco > 10)
                    .Load();

                foreach(var item in produto.Compras)
                {
                    Console.WriteLine(item);
                }


            }

            

            Console.ReadKey();
        }

        private static void ExibiProdutosDaPromocao()
        {
            using (var contexto2 = new LojaContext())
            {
                //select com Join na tabela Produto
                var promocao = contexto2.Promocoes.Include(p => p.Produtos).ThenInclude(pp => pp.Produto).FirstOrDefault();

                foreach (var item in promocao.Produtos)
                {
                    Console.WriteLine(item.Produto);
                }
            }
        }

        private static void IncluirPromocao()
        {
            using (var contexto = new LojaContext())
            {
                var promocao = new Promocao();
                promocao.Descricao = "Queima Total Janeiro";
                promocao.DataInicio = new DateTime(2017, 1, 1);
                promocao.DataTermino = new DateTime(2017, 1, 31);

                var produtos = contexto.Produtos.Where(X => X.Categoria == "Bebidas").ToList();

                foreach (var item in produtos)
                {
                    promocao.IncluirProdtuo(item);
                }

                contexto.Promocoes.Add(promocao);

                contexto.SaveChanges();
            }
        }

        private static void UmParaUum()
        {
            var fulano = new Cliente();
            fulano.Nome = "Fulano de Tal";
            fulano.EnderecoDeEntrega = new Endereco()
            {
                Numero = 12,
                Logradouro = "Rua dos Inválidos",
                Complemento = "Sobrado",
                Bairro = "Centro",
                Cidade = "São Paulo"
            };

            using (var contexto = new LojaContext())
            {
                contexto.Clientes.Add(fulano);
                contexto.SaveChanges();
            }
        }

        private static void MuitosParaMuitos()
        {
            var p1 = new Produto() { Nome = "Suco", Categoria = "Bebidas", PrecoUnitario = 1.79, Unidade = "Litros" };
            var p2 = new Produto() { Nome = "Cafe", Categoria = "Bebidas", PrecoUnitario = 8.90, Unidade = "Gramas" };
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
