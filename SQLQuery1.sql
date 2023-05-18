CREATE database	Teste
go
use teste
CREATE TABLE Usuarios(
UsuarioId int PRIMARY KEY identity
,Nome VARCHAR(255) NOT NULL
,Email VARCHAR(255) NOT NULL UNIQUE
,Senha VARCHAR(255) NOT NULL

);

GO

CREATE TABLE Clientes(
ClienteId int PRIMARY KEY identity
,UsuarioId int FOREIGN KEY REFERENCES Usuarios(UsuarioId)
,Nome VARCHAR(50) NOT NULL
,Sobrenome VARCHAR(100) NOT NULL
,Email VARCHAR(100) NOT NULL UNIQUE
,Telefone VARCHAR(50) NOT NULL
,Endereco VARCHAR(100) NOT NULL
,DataNascimento DATE NOT NULL
,DataCriacao DATE NOT NULL

);
GO



