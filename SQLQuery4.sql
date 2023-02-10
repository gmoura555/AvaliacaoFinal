create table TB_Clientes
(
Id_cliente int identity(1,1) not null,
Nome varchar (60)not null,
Data_Nascimento datetime not null,
Email varchar (60)not null,
Profissao varchar (60) not null
PRIMARY KEY(Id_cliente)
)