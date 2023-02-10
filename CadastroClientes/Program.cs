using Dapper;
using System.Data.SqlClient;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace CadastroClientes
{
    internal class Program
    {
        public int id { get; set; }
        public string? Descricao { get; set; }

        static void Main(string[] args)
        {
            int opcao;
            ExibeMenu();
            Console.Write("Escolha a sua opção: ");
            opcao = int.Parse(Console.ReadLine());



            while (opcao != 5)
            {
                if (opcao == 1)
                {
                    IncluirCadastro();
                }
                if (opcao == 2)
                {
                    ListarCadastros();
                }
                if (opcao == 3)
                {
                    AlterarCadastro();
                }
                if (opcao == 4)
                {
                    ExcluirCadastro();
                }
                Console.Write("Escolha opção:  ");
                opcao = int.Parse(Console.ReadLine());
            }



        }

        static string conexao = @"Data Source=(localdb)\MSSQLLocalDB; 
                               Initial Catalog= DB_CadastroClientes;
                               Integrated Security=True";

        static void ExibeMenu()
        {

            Console.WriteLine(new string('-', 50));
            Console.WriteLine("1 - Cadastrar novo cliente ");
            Console.WriteLine("2 - Consultar Cadastro");
            Console.WriteLine("3 - Alterar Cadastro");
            Console.WriteLine("4 - Excluir Cadastro");
            Console.WriteLine("5 - Sair");
            Console.WriteLine(new string('-', 50));

        }
        static void IncluirCadastro()
        {


            string resposta;
            do

            {
                Console.Write("Nome Completo: ");
                string NomeCliente = Console.ReadLine();
                if (NomeCliente == "")
                {
                    Console.WriteLine("------------------------------------");

                    Console.WriteLine("Nome Inválido!");


                    Console.WriteLine("------------------------------------");

                    IncluirCadastro();

                }



                Console.Write("Data de Nascimento:  (dd/mm/yyyy): ");
                string DataN = Console.ReadLine();

                // Tentativa de converter a string em um objeto do tipo DateTime
                if (DateTime.TryParseExact(DataN, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime Datan))
                {
                    Console.WriteLine("Data válida: " + Datan.ToString("dd/MM/yyyy"));
                }
                else
                {
                    Console.WriteLine("------------------------------------");

                    Console.WriteLine("Data inválida !");

                    Console.WriteLine("Inicie Novamente!");
                    Console.WriteLine("------------------------------------");
                    IncluirCadastro();
                }

                Console.Write("Informe o Email: ");
                string EmailC = Console.ReadLine();
                if (EmailC == "")
                {
                    Console.WriteLine("------------------------------------");

                    Console.WriteLine("Email Inválido!");
                    Console.WriteLine("Inicie Novamente!");

                    Console.WriteLine("------------------------------------");

                    IncluirCadastro();

                }

                Console.Write("Informe Profissao: ");
                string Prof = Console.ReadLine();

                using (var conn = new SqlConnection(conexao))
                {


                    var Nome = conn.Execute("insert into TB_Clientes ( Nome ,Data_Nascimento ,Email ,Profissao) values (@Nome, @Data_Nascimento, @Email, @Profissao)",
                    new { Nome = NomeCliente, Data_Nascimento = Datan, Email = EmailC, Profissao = Prof });
                    Console.WriteLine("registros Alterados: " + Nome);
                    Console.Write("Continuar a Inserir? (S/N): ");
                    Console.ReadKey();
                }

                resposta = Console.ReadLine();
                Console.WriteLine(resposta);
            } while (resposta == "s");
            ExibeMenu();
        }


        static void ListarCadastros()
        {

            using (var conn = new SqlConnection(conexao))
            {
                var consulta = conn.Query("Select * from TB_Clientes");
                foreach (var coluna in consulta)
                {
                    Console.WriteLine("Id do Cliente: " + coluna.Id_cliente);
                    Console.WriteLine("Nome: " + coluna.Nome);
                    Console.WriteLine("Data de Nascimento: " + coluna.Data_Nascimento);
                    Console.WriteLine("Email: " + coluna.Email);
                    Console.WriteLine("Profissão: " + coluna.Profissao);
                    Console.WriteLine("------------------------------------");

                }
                Console.WriteLine("Prescione uma tecla para continuar");
                Console.ReadKey();
                Console.Clear();
            }
            ExibeMenu();

        }

        static void AlterarCadastro()
        {
            //update nomedatabela SET Descricao=@Descricao where Id=@id;
            Console.Write("Informe o Código: ");
            int codigo = int.Parse(Console.ReadLine());

            Console.Write("Digite o Nome:");
            string Nomec = Console.ReadLine();
            if (Nomec == "")
            {
                Console.WriteLine("------------------------------------");

                Console.WriteLine("Nome Inválido!");
                Console.WriteLine("Inicie Novamente!");

                Console.WriteLine("------------------------------------");

                AlterarCadastro();


            }
            Console.Write("Data Nascimento:");
            string DataN = Console.ReadLine();
            if (DateTime.TryParseExact(DataN, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime Datan))
            {
                Console.WriteLine("Data válida: " + Datan.ToString("dd/MM/yyyy"));
            }
            else
            {
                Console.WriteLine("------------------------------------");

                Console.WriteLine("Data inválida !");
                Console.WriteLine("Inicie Novamente!");

                Console.WriteLine("------------------------------------");
                AlterarCadastro();
            }
            Console.Write("Email:");
            string Emailc = Console.ReadLine();
            if (Emailc == "")
            {
                Console.WriteLine("------------------------------------");

                Console.WriteLine("Email Inválido!");
                Console.WriteLine("Inicie Novamente!");

                Console.WriteLine("------------------------------------");

                AlterarCadastro();

            }

            Console.Write("Profissao:");
            string Profc = Console.ReadLine();


            using (var conn = new SqlConnection(conexao))
            {
                var Nomet = conn.Execute("Update TB_Clientes set Nome = @Nome, Data_Nascimento = @Data_Nascimento, Email = @Email,Profissao = @Profissao where Id_cliente = @Id ",
                     new { Id = codigo, Nome = Nomec, Data_Nascimento = Datan, Email = Emailc, Profissao = Profc });
                Console.WriteLine("registros Alterados: " + Nomet);

            }

            ExibeMenu();
        }

        static void ExcluirCadastro()
        {
            Console.Write("Informe o Código: ");
            int codigo = int.Parse(Console.ReadLine());
            using (var conn = new SqlConnection(conexao))
            {
                var registros = conn.Execute("Delete from TB_clientes where Id_cliente=@Id_cliente",
                    new { Id_cliente = codigo });
                Console.WriteLine("Registro Removido: " + registros);
            }

            Console.WriteLine("Pressione uma tecla para continuar");
            Console.ReadKey();
            Console.Clear();
            ExibeMenu();

        }


    }
}
