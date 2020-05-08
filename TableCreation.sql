create table City(
	Id int not null primary key(Id),
	CityName varchar(20)
)

create table Customer(
	Id int not null primary key(Id),
	CustName varchar(20) not null,
	CustAddress varchar(30) not null,
	ContactNumber varchar(15) not null,
	CityId int not null,
	constraint City1ForeignKey  foreign key(CityId) references City(Id) on update cascade 
)

create table Product(
	Id int not null primary key(Id),
	ProdName varchar(20) not null,
	Price int not null,
	DateOfManufacturing date
)

create table Seller(
	Id int not null primary key(Id),
	SellerName varchar(20) not null,	
	ContactNumber varchar(15) not null,
	CityId int not null,
	constraint City2ForeignKey  foreign key(CityId) references City(Id) on update cascade
)

create table Buys(
	Id int not null primary key(Id),
	CustId int not null,
	ProdId int not null,
	SellerId int not null,
	constraint CustomerForeignKey  foreign key(CustId) references Customer(Id),
	constraint ProductForeignKey  foreign key(ProdId) references Product(Id),
	constraint SellerForeignKey foreign key(SellerId) references Seller(Id)
)
