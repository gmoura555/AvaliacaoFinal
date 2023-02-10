create database DB_CadastroClientes
use DB_CadastroClientes

create table TB_Clientes
(
Id_cliente int identity(1,1),
Nome varchar (60),
Data_Nascimento datetime ,
Email varchar (60),
Profissao varchar (60)
PRIMARY KEY(Id_cliente)

)

create table TB_Endereco
(
Id_cliente INT IDENTITY(1,1),
Logadouro varchar (60),
Bairro varchar (20),
Cidade varchar (10),
Estado varchar (10),
PRIMARY KEY(Id_cliente),
FOREIGN KEY(Id_cliente) REFERENCES TB_clientes (Id_cliente)

)