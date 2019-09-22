-- Usuarios
create table Usuario(
	Id int primary key identity(1,1),
	Nome varchar(250) not null,
	Cpf varchar(30) null,
	Rg varchar(25) null,
	Crv varchar(30) null, --conselho de veterin�rio
	CodigoAcesso int,
	SenhaAcesso varchar(300),
	EhVet int, --se n�o for vet pode ser master
	EhAdministrador int, 
	DataCadastro date not null,
	Ativo int not null
)

-- Especialidades
create table TipoAnimal(
	Id int primary key identity(1,1),
	Nome varchar(250) not null,
	Ativo int not null
)

-- Usuários e as especialidades
create table UsuarioEspecialidade(
	Id int primary key identity(1,1),
	TipoAnimalId int foreign key references TipoAnimal(Id) not null,
	UsuarioId int foreign key references Usuario(Id) not null
)

-- Produtos
create table Produto(
	Id int primary key identity(1,1),
	Codigo int not null,
	Nome varchar(200) not null,
	Fabricante varchar(200),
	Especificacao text,
	Preco decimal(15,2) null,
	Estoque int null,
	Foto varchar(100),
	DataCadastro date not null,
	Ativo int not null
)

-- Servi�os
create table Servico(
	Id int primary key identity(1,1),
	Nome varchar(200) not null,
	TempoEstimado time null,
	Preco decimal(15,2) not null,
	PrecoAntigo decimal(15,2) null,
	RealizadoPorVet int not null,
	PatazRecebido int null,
	PatazNecessario int null,
	Ativo int not null
)

--Servi�o usu�rios
create table ServicoUsuario(
	Id int primary key identity(1,1),
	UsuarioId int foreign key references Usuario(Id) not null,
	ServicoId int foreign key references Servico(Id) not null
);

--Produtos no servi�o
create table ServicoProduto(
	Id int primary key identity(1,1),
	ProdutoId int foreign key references Produto(Id) not null,
	ServicoId int foreign key references Servico(Id) not null
)

--Log das altera��es
create table ServicoLog(
	Id int primary key identity(1,1),
	ServicoId int foreign key references Servico(Id) not null,
	UsuarioId int foreign key references Usuario(Id) not null, --quem alterou o produto
	PrecoAntigo decimal(15,2) not null,
	PrecoNovo decimal(15,2) null,
	DataAlteracao date not null
)

--Cadastro de cliente
create table Cliente(
	Id int primary key identity(1,1),
	Nome varchar(250) not null,
	Rg varchar(25) null,
	Cpf varchar(30) null,
	Endereco varchar(230),
	Email varchar(100),
	DataCadastro date not null,
	Ativo int not null
)

-- Ra�as de animais
create table RacaAnimal(
	Id int primary key identity(1,1),
	Nome varchar(250) not null
)

-- Porte do animal
create table PorteAnimal(
	Id int primary key identity(1,1),
	Nome varchar(250) not null
)

-- Animal
create table Animal(
	Id int primary key identity(1,1),
	TipoAnimalId int foreign key references TipoAnimal(Id) null,
	RacaAnimalId int foreign key references RacaAnimal(Id) null,
	PorteAnimalId int foreign key references PorteAnimal(Id) null,
	ClienteId int foreign key references Cliente(Id) not null,
	Nome varchar(120),
	Alergia varchar(300),
	Detalhes varchar(600),
	AutorizaDivulgacao int null --se for null, n�o respondeu
)

-- Promo��es
create table Promocao(
	Id int primary key identity(1,1),
	
	Nome varchar(150),
	Percentual decimal(15,2),
	DataInicio date,
	DataFim date
)

create table PromocaoProdServ(
	Id int primary key identity(1,1),
	PromocaoId int not null foreign key references Promocao(Id),
	ProdutoId int null foreign key references Produto(Id),
	ServicoId int null foreign key references Servico(Id)
)

-- Agendamento
create table Agendamento(
	Id int primary key identity(1,1),
	ClienteId int foreign key references Cliente(Id) not null,
	AnimalId int foreign key references Animal(Id) null,
	ServicoId int foreign key references Servico(Id) null,
	UsuarioId int foreign key references Usuario(Id) null, --quem ir� realizar o servi�o --Quando escolher o servi�o e o usu�rio, verificar nos agendamentos se n�o existe choque de hor�rio
	DiaMarcado date not null,
	HoraMarcado time not null,
	Observacao varchar(300),
	Ausente int null
)

--Tipo de pagamento
create table TipoPagamento(
	Id int primary key identity(1,1),
	Nome varchar(60)
)

-- Venda realizada
create table Venda(
	Id int primary key identity(1,1),
	AgendamentoId int foreign key references Agendamento(Id) not null,
	TipoPagamentoId int foreign key references TipoPagamento(Id) not null,
	ValorProdutos decimal(15,2) null,
	ValorProdutosDesconto decimal(15,2) null,
	ValorServico decimal(15,2) null,
	ValorServicoDesconto decimal(15,2) null,
	ValorPago decimal(15,2) not null,
	PatazTotalRecebido int null, --somat�rio do programa realizado nesta venda
	DataPagamento date not null
)

-- Produtos comprados
-- Caso o produto esteja com desconto, ser� adicionado na coluna ValorComDesconto
create table VendaProduto(
	Id int primary key identity(1,1),
	AgendamentoId int foreign key references Agendamento(Id) not null,
	ProdutoId int foreign key references Produto(Id),
	Valor decimal(15,2) not null,
	ValorComDesconto decimal(15,2) null,
	Quantidade int 
)

-- Servi�os na venda
create table VendaServico(
	Id int primary key identity(1,1),
	AgendamentoId int foreign key references Agendamento(Id) not null,
	ServicoId int foreign key references Servico(Id),
	Valor decimal(15,2) not null,
	ValorComDesconto decimal(15,2) null
)

-- Avalia��o da venda/agendamento
create table VendaAvaliacao(
	Id int primary key identity(1,1),
	AgendamentoId int foreign key references Agendamento(Id) not null,
	Nota int null,
	DataAvaliado datetime
)

-- Tabela que apresenta os pontos do cliente
create table ClientePontuacao(
	Id int primary key identity(1,1),
	ClienteId int not null foreign key references Cliente(Id),
	
	Pontos int,
	DataAtualizado datetime,	
);