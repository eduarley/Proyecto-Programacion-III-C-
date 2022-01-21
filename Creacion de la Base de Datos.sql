
GO  
CREATE DATABASE Floristeria  
ON   
( NAME = Floristeria_dat,  
    FILENAME = 'C:\SQL BASES DE DATOS\Floristeria\floristeriadat.mdf',  
    SIZE = 10,  
    MAXSIZE = unlimited,  
    FILEGROWTH = 5 )  
LOG ON  
( NAME = Floristeria_log,  
    FILENAME = 'C:\SQL BASES DE DATOS\Floristeria\floristerialog.ldf',  
    SIZE = 5MB,  
    MAXSIZE = unlimited,  
    FILEGROWTH = 5MB ) ;  
GO  







-------------------------------CREACION DE TABLAS---------------------------


use Floristeria

CREATE TABLE Usuario
(
	ID int not null,
	Nombre varchar(50),
	Apellido varchar(50),
	Direccion varchar(100),
	Contrasena varchar(50),
	Estado int,
	Telefono int,
	Correo varchar(50),
	IdCategoria int
)


CREATE TABLE CategoriaUsuario
(
	ID int not null,
	Descripcion varchar(50)
)


CREATE TABLE Proveedor
(
	ID int not null,
	Nombre varchar(50),
	Direccion varchar(100),
	Telefono int,
	Correo varchar(50),
	Estado int
)


CREATE TABLE MateriaPrima
(
	ID int not null,
	IdProveedor int,
	Descripcion varchar(50),
	PrecioUnitario money,
	Existencias int,
	Estado int
)

CREATE TABLE DetalleProducto
(
	IdProducto int not null,
	IdMateriaPrima int not null,
	PrecioDetalle money,
	Cantidad int
)



CREATE TABLE ProductoFinal
(
	ID int not null,
	Descripcion varchar(50),
	Precio money,
	IdCategoria int,
	Estado int
)

CREATE TABLE CategoriaProducto
(
	ID int not null,
	Descripcion varchar(50)
)

CREATE TABLE ImagenProducto
(
	IdProductoFinal int not null,
	Descripcion varchar(50) not null,
	Img image
)

CREATE TABLE DetalleFactura
(
	IdEncFact int not null,
	IdProductoFinal int not null,
	Cantidad int,
	Precio int
)

CREATE TABLE EncabezadoFactura
(
	ID int  not null,
	Fecha datetime not null,
	IdCliente int not null,
	Monto money,
	Pago varchar(50),
	Estado varchar(50),
	IdFuncionario int
)





CREATE TABLE Pedido
(
	IdFactura int not null,
	Direccion varchar(100),
	Telefono varchar(50),
	Fecha datetime
)




create table Envio
(
	IdPedidoFactura int not null,
	IdEmpleadoACargo int,
	Estado int
)



-------------------------------LLAVES PRIMARIAS--------------------------------

 
ALTER TABLE Usuario ADD CONSTRAINT PK_Usuario PRIMARY KEY (ID) 
ALTER TABLE CategoriaUsuario ADD CONSTRAINT PK_CategoriaUsuario PRIMARY KEY (ID) 
ALTER TABLE Proveedor ADD CONSTRAINT PK_Proveedor PRIMARY KEY (ID) 
ALTER TABLE TelefonoProveedor ADD CONSTRAINT PK_TelefonoProveedor PRIMARY KEY (IdProveedor, Telefono) 
ALTER TABLE CorreoProveedor ADD CONSTRAINT PK_CorreoProveedor PRIMARY KEY (IdProveedor, Correo) 
ALTER TABLE MateriaPrima ADD CONSTRAINT PK_MateriaPrima PRIMARY KEY (ID) 
ALTER TABLE DetalleProducto ADD CONSTRAINT PK_DetalleProducto PRIMARY KEY (IdProducto, IdMateriaPrima) 
ALTER TABLE ProductoFinal ADD CONSTRAINT PK_ProductoFinal PRIMARY KEY (ID) 
ALTER TABLE CategoriaProducto ADD CONSTRAINT PK_CategoriaProducto PRIMARY KEY (ID) 
ALTER TABLE ImagenProducto ADD CONSTRAINT PK_ImagenProducto PRIMARY KEY (Descripcion) 
ALTER TABLE DetalleFactura ADD CONSTRAINT PK_DetalleFactura PRIMARY KEY (IdEncFact,IdProductoFinal) 
ALTER TABLE EncabezadoFactura ADD CONSTRAINT PK_EncabezadoFactura PRIMARY KEY (ID) 
ALTER TABLE Pedido ADD CONSTRAINT PK_Pedido PRIMARY KEY (IdFactura) 
ALTER TABLE Envio ADD CONSTRAINT PK_Envio PRIMARY KEY (IdPedidoFactura) 
ALTER TABLE ProductoPersonalizado ADD CONSTRAINT PK_ProductoPesonalizado PRIMARY KEY (Id) 
--------------------------------LLAVES FORÁNEAS----------------------------------


ALTER TABLE Usuario ADD CONSTRAINT FK_Categoria_Usuario FOREIGN KEY (IDCategoria) REFERENCES CategoriaUsuario(ID)
ALTER TABLE MateriaPrima ADD CONSTRAINT FK_Proveedor_MateriaPrima FOREIGN KEY (IdProveedor) REFERENCES Proveedor(ID)
ALTER TABLE DetalleProducto ADD CONSTRAINT FK_MateriaPrima_DetalleProducto FOREIGN KEY (IdMateriaPrima) REFERENCES MateriaPrima(ID)
ALTER TABLE DetalleProducto ADD CONSTRAINT FK_ProductoFinal_DetalleProducto FOREIGN KEY (IdProducto) REFERENCES ProductoFinal(ID)
ALTER TABLE Productofinal ADD CONSTRAINT FK_CategoriaProducto_ProductoFinal FOREIGN KEY (IdCategoria) REFERENCES CategoriaProducto(ID)
ALTER TABLE DetalleFactura ADD CONSTRAINT FK_ProductoFinal_DetalleFactura FOREIGN KEY (IdProductoFinal) REFERENCES ProductoFinal(ID)
ALTER TABLE DetalleFactura ADD CONSTRAINT FK_EncabezadoFactura_DetalleFactura FOREIGN KEY (IdEncFact) REFERENCES EncabezadoFactura(ID)
ALTER TABLE EncabezadoFactura ADD CONSTRAINT FK_Usuario_EncabezadoFactura FOREIGN KEY (IdCliente) REFERENCES Usuario(ID)
ALTER TABLE Pedido ADD CONSTRAINT FK_EncabezadoFactura_Pedido FOREIGN KEY (IdFactura) REFERENCES EncabezadoFactura(ID)
ALTER TABLE ImagenProducto ADD CONSTRAINT FK_ProductoFinal_ImagenProducto FOREIGN KEY (IdProductoFinal) REFERENCES ProductoFinal(ID)
ALTER TABLE Envio ADD CONSTRAINT FK_Pedido_Envio FOREIGN KEY (IdPedidoFactura) REFERENCES Pedido(IdFactura)







---------------------------------------PROCEDIMIENTOS ALMACENADOS-------------------------------------------



---------------------------------------------////USUARIO////------------------------------------------------



GO
create proc PA_SeleccionarTodosLosUsuarios
as
begin
	select * from Usuario
end
GO



-------------------

go
create proc PA_InsertarUsuario
@ID int,
@Nombre varchar(50),
@Apellido varchar(50),
@Direccion varchar(50),
@Contrasena varchar(50),
@Estado int,
@Telefono int,
@Correo varchar(50),
@IdCategoria int
as
begin
	
	insert into Usuario values (@id,@Nombre,@Apellido,@Direccion,HASHBYTES('MD5',@Contrasena),@Estado,@Telefono,@Correo,@IdCategoria)
end

---------------------


go
create proc PA_SeleccionarUsuarioPorId
@Id int
as
begin 
	select * from Usuario
	where ID=@id
end


---------------------


go
create proc PA_ActualizarUsuario
@ID int,
@Nombre varchar(50),
@Apellido varchar(50),
@Direccion varchar(50),
@Contrasena varchar(50),
@Estado int,
@Telefono int,
@Correo varchar(50),
@IdCategoria int
as 
begin
	update Usuario set Nombre=@Nombre,
	Apellido=@Apellido,
	Direccion=@Direccion,
	Contrasena=HASHBYTES('MD5',@Contrasena),
	Estado=@Estado,
	Telefono=@Telefono,
	Correo=@Correo,
	IdCategoria=@IdCategoria
	where ID=@ID

end


--------------------------




go
create proc PA_ActivarUsuario
@id int
as
begin
	update Usuario set Estado=1
	where ID=@id
end
go

--------------------------------

go
create proc PA_DesactivarUsuario
@id int
as
begin
	update Usuario set Estado=0
	where ID=@id
end
go

------------------------------


go
create proc PA_LoginUsuario
@ID int,
@Contrasena varchar(50)
as 
begin
if exists(select * from Usuario where Contrasena=HASHBYTES('MD5',@Contrasena) and ID= @ID)
begin 

	
	select 1 as Resultado,* from Usuario where ID=@ID
end

	
end
go

-------------






-----------------------------------------------////CATEGORIAS////-------------------------------------------------

go
create proc PA_SeleccionarCategoriasUsuario
as
begin
	select * from CategoriaUsuario
end
go

---------------------------------

go
create proc PA_SeleccionarCategoriaPorId
@id int
as
begin
	select * from CategoriaUsuario where ID= @id
end
go


------------------------------
go
create proc PA_SeleccionarGerentes
as
begin
	select * from Usuario where IdCategoria=2
end
go

---------------------------------

go
create proc PA_SeleccionarClientes
as
begin
	select * from Usuario where IdCategoria=0
end
go

----------------------------------

go
create proc PA_SeleccionarCajeros
as
begin
	select * from Usuario where IdCategoria=1
end
go

-------------------------------


create proc PA_SeleccionarCategoriasProducto
as
begin
	select * from CategoriaProducto;
end


------------------------------

go
create proc PA_SeleccionarCategoriasProductoPorId
@ID int
as
begin
	select * from CategoriaProducto where ID=@ID
end








---------------------------------------------------////PROVEEDORES////---------------------------------------------

go
create proc PA_InsertarProveedor
@ID int,
@Nombre varchar(50),
@Direccion varchar(100),
@Telefono int,
@Correo varchar(50),
@Estado int
as
begin
	
		insert into Proveedor(ID,Nombre,Direccion,Telefono,Correo,Estado) values (@id,@Nombre,@direccion,@Telefono,@Correo,@Estado)
end

------------------------------------------



go
create proc PA_SeleccionarTodosLosProveedores
as
begin
	select * from Proveedor
end
go

----------------------------------------

go
create proc PA_SeleccionarProveedorPorId
@Id int
as
begin 
	select * from Proveedor
	where ID=@id
end


-----------------------------------------


go
create proc PA_ActualizarProveedor
@ID int,
@Nombre varchar(50),
@Direccion varchar(100),
@Telefono int,
@Correo varchar(50),
@Estado int
as 
begin
	update Proveedor set Nombre=@Nombre,
	Direccion=@Direccion,
	Telefono=@Telefono,
	Correo=@Correo,
	Estado=@Estado
	where ID=@ID

end

---------------------------------------------


go
create proc PA_ActivarProveedor
@id int
as
begin
	update Proveedor set Estado=1
	where ID=@id
end
go

--------------------------------

go
create proc PA_DesactivarProveedor
@id int
as
begin
	update Proveedor set Estado=0
	where ID=@id
end
go





---------------------------------------------------////MATERIA PRIMA////-------------------------------------------



go
create proc PA_SeleccionarTodosLasMateriasPrimas
as
begin
	select MateriaPrima.ID,MateriaPrima.IdProveedor,Proveedor.Nombre as Proveedor,MateriaPrima.Descripcion, PrecioUnitario,Existencias, MateriaPrima.Estado
	from MateriaPrima,Proveedor where MateriaPrima.IdProveedor=Proveedor.ID
	order by MateriaPrima.Descripcion asc
end


------------------
go
create proc PA_SeleccionarMateriaPrimaPorId
@Id int
as
begin 
	
	select MateriaPrima.*,Proveedor.Nombre as ProveedorNombre from MateriaPrima, Proveedor
	where MateriaPrima.ID=@id and Proveedor.ID= MateriaPrima.IdProveedor
end

-------------------

go
create proc PA_InsertarMateria
@id int,
@idProveedor int,
@descripcion varchar(50),
@PrecioUnitario money,
@Existencias int,
@Estado int
as
begin
	insert into MateriaPrima values(@id,@idProveedor,@descripcion,@PrecioUnitario,@Existencias,@Estado)
end
-------------------




go
create proc PA_ActualizarMateria
@id int,
@idProveedor int,
@descripcion varchar(50),
@PrecioUnitario money,
@Existencias int,
@Estado int
as
begin
	
	update MateriaPrima set IdProveedor=@idProveedor,
	Descripcion=Descripcion,
	PrecioUnitario=@PrecioUnitario,
	Existencias=@Existencias,
	Estado=@Estado
	where ID=@id

end

-------------------
go
create proc PA_ActivarMateriaPrima
@id int
as
begin
	update MateriaPrima set Estado=1
	where ID=@id
end
go

--------------------

go
create proc PA_DesactivarMateriaPrima
@id int
as
begin
	update MateriaPrima set Estado=0
	where ID=@id
end
go



------------------------------------------------------////PRODUCTOS////------------------------------------------------





go
create proc PA_InsertarProducto
@ID int,
@Descripcion varchar(50),
@Precio money,
@IdCategoria varchar (50),
@Estado int
as 
begin	
	insert into ProductoFinal values (@id,@Descripcion,@Precio,@IdCategoria,@Estado)
end
go




	
------------------------------




create proc PA_SeleccionarTodosLosProductos
as
begin 
	select p.ID,p.Descripcion,p.Precio,c.Descripcion as DescripcionCategoria,p.Estado,c.ID as IdCategoriaProducto
	from ProductoFinal p, CategoriaProducto c
	where IdCategoria=c.id

										 
end




---------------------------------------------------------------

go
create proc PA_SeleccionarProductosActivos
as
begin
	select p.ID,p.Descripcion,p.Precio,c.Descripcion as DescripcionCategoria,p.Estado,c.ID as IdCategoriaProducto from ProductoFinal p, CategoriaProducto c
	where IdCategoria=c.id and Estado=1
end







----------------------------


go
create proc PA_SeleccionarProductoPorId
@Id int
as
begin 
	select * from ProductoFinal
	where ID=@id
end


--------------------------------------------------------------



go
create proc PA_ActualizarProducto
@ID int,
@Descripcion varchar(50),
@Precio money,
@IdCategoria varchar (50)
as 
begin
	update ProductoFinal set Descripcion=@Descripcion,
	Precio=@Precio,
	IdCategoria=@IdCategoria
	where ID=@ID

end




------------------------------------------------




go
create proc PA_DesactivarProducto
@id int
as
begin

	
		update ProductoFinal set Estado=0
		where ID=@id
	
	
end
go

--------------------------------------------------------

go
create proc PA_ActivarProducto
@id int
as
begin

		update ProductoFinal set Estado=1
		where ID=@id

end
go

-------------------------------------------------------------



create proc PA_AgregarManoObra
@IdProducto int,
@Monto money
as 
begin
	update ProductoFinal set Precio=Precio+@Monto
	where ID=@IdProducto
end 

-------------------------









---------------------------------------------------////DetalleProducto////-------------------------------------


CREATE PROC PA_SeleccionarDetalleProductoPorProducto
@IdProducto int
AS
BEGIN

	SELECT * FROM DetalleProducto
	where IdProducto=@IdProducto
	
END



-----------------------------------




	
go
CREATE PROC PA_InsertarDetalleProducto
@IdProducto int,
@IdMateriaPrima int,
@Cantidad int,
@PrecioDetalle money
AS
BEGIN
		INSERT INTO DetalleProducto values(@IdProducto,@IdMateriaPrima,@Cantidad,@PrecioDetalle)
END





-------------------------------------
go
create proc PA_EliminarDetalleProducto
@IdProducto int,
@IdMateria int
as
begin
	delete from DetalleProducto where IdProducto=@IdProducto and IdMateriaPrima=@IdMateria
end



--------------------------------------------////IMAGENPRODUCTO////----------------------------------------------



go
create PROC PA_InsertarImagenProducto
@IdProductoFinal int,
@Descripcion nchar(150),
@img image 
AS
BEGIN
	INSERT INTO ImagenProducto VALUES (@IdProductoFinal,@Descripcion,@img)
END

--------------------------------

go
CREATE PROC PA_SeleccionarDescripcionImagen
@IdProducto int
AS
BEGIN
	select Descripcion from ImagenProducto 
	where ImagenProducto.IdProductoFinal = @IdProducto
END

----------------------------------------


go
create proc PA_EliminarImagen
@Descripcion varchar(50)
as 
begin 
	delete from ImagenProducto where ImagenProducto.Descripcion = @Descripcion
end


-------------------------

create proc [dbo].[PA_ActualizarMateria]
@id int,
@idProveedor int,
@descripcion varchar(50),
@PrecioUnitario money,
@Existencias int,
@Estado int
as
begin
	update MateriaPrima set IdProveedor=@idProveedor,
	Descripcion=@Descripcion,
	PrecioUnitario=@PrecioUnitario,
	Existencias=@Existencias,
	Estado=@Estado
	where ID=@id

end





--------------------------------------------------------------////////DETALLE_FACTURA///////------------------------------------------------


go
CREATE PROC PA_InsertarDetalleFactura
@IdFactura int,
@IdProducto int,
@Cantidad int,
@Precio money
as
begin
	insert into DetalleFactura values(@IdFactura,@IdProducto,@Cantidad,@Precio)
end




-------------------



create proc PA_SeleccionarDetalleFacturaPorFactura
@IdFactura int
as
begin
	select * from DetalleFactura 
	where IdEncFact=@IdFactura
end






-----------------------------------------------------------////////FACTURA///////--------------------------------------------------------





go
CREATE PROC PA_InsertarFactura
@Id int,
@Fecha datetime,
@IdCliente int,
@Monto money,
@Estado varchar(50)
as
begin
	insert into EncabezadoFactura (ID,Fecha,IdCliente,Monto,Estado) values(@Id,@Fecha,@IdCliente,@Monto,@Estado)
end

-------------



go
create proc PA_ActualizarTipoPago
@IdFactura int,
@Pago varchar(50)
as
begin
	update EncabezadoFactura set Pago=@Pago
	where ID=@IdFactura
end




--------------

create proc PA_SeleccionarUltimoIDFactura
as
begin

	if exists(select * from EncabezadoFactura) 
		select max(Id) as ID from EncabezadoFactura
	else
		select 0 as ID
end


------------


create proc PA_SeleccionarTodasLasFacturasPendientes
as
begin
	select * from EncabezadoFactura where Estado='Pendiente'
end

----------------



create proc PA_PagarFactura
@Id int,
@TipoPago varchar(50),
@IdFuncionario int
as
begin
	update EncabezadoFactura set Pago=@TipoPago,
	IdFuncionario=@IdFuncionario,
	Estado='Pagado'
	where ID=@ID
end


---------------------------------






create proc PA_SeleccionarFacturaPorId
@Id int
as
begin 
	Select * from EncabezadoFactura
	where EncabezadoFactura.ID=@Id

end


------------------------------------------------------------------------//////PEDIDO///////-------------------------------------------------------------


create proc PA_InsertarPedido
@IdFactura int,
@Direccion varchar(50),
@Telefono varchar(50),
@Fecha datetime
as
begin
	insert into Pedido values (@IdFactura,@Direccion,@Telefono,@Fecha)
end




----------------



create proc PA_SeleccionarPedidoPorFactura
@IdFactura int
as
begin
	select * from Pedido
	where IdFactura=@IdFactura
end


--------------------------------



-----------------------------------------------------------//////ENVIO/////////-----------------------------------------------------------

CREATE PROC PA_SeleccionarTodosLosEnvios
as
begin 
	select * from Envio
end



------------------------------------

create proc PA_AsignarEmpleadoAlPedido
@IdEmpleado int,
@idPedidoFactura int
as
begin 
	update Envio set IdEmpleadoACargo=@IdEmpleado
	where IdPedidoFactura=@idPedidoFactura
end



-----------------

create proc PA_RealizarEnvio
@IdPedidoFactura int
as
begin
	update Envio set Estado = 1
	where IdPedidoFactura=@IdPedidoFactura
end



--------------------



create proc PA_SeleccionarFacturaPorEnvio
@idPedidoFactura int
as
begin
	select EncabezadoFactura.ID 
	from EncabezadoFactura,DetalleFactura,Pedido,Envio
	where Envio.IdPedidoFactura=@IdPedidoFactura and EncabezadoFactura.ID=DetalleFactura.IdEncFact and Pedido.IdFactura=EncabezadoFactura.id 
	and Envio.IdPedidoFactura=Pedido.IdFactura
end

exec PA_SeleccionarFacturaPorEnvio 2


--------------------------------------------//////////////////POSIBLES TRIGGERS///////////////////////-------------------------------------


------------------------------------------------------------------------------------


create trigger Tr_Activar_Desactivar_Producto_Por_MAteria_Al_No_Haber_Existencias
on MateriaPrima
after update
as
begin
	set nocount on
	declare @IdMateriaNueva int
	select @IdMateriaNueva = id from inserted


	declare @IdProducto int
	declare cProd cursor for
	select distinct ProductoFinal.ID 
	from ProductoFinal,DetalleProducto,MateriaPrima 
	where DetalleProducto.IdProducto=ProductoFinal.id and MateriaPrima.ID= DetalleProducto.IdMateriaPrima and
	MateriaPrima.ID= @IdMateriaNueva
	open cProd
	fetch cProd into @IdProducto
	while @@FETCH_STATUS=0
	begin
		

		if(select estado from inserted)<>0
		begin
			if(select existencias from inserted)=0
			begin				
				update ProductoFinal set estado =0 where ID = @IdProducto
				fetch cProd into @IdProducto
			end
			else
			begin				
				update ProductoFinal set estado =1 where ID = @IdProducto
				fetch cProd into @IdProducto
			end
		end
		else
		begin
			update ProductoFinal set estado =0 where ID = @IdProducto
			fetch cProd into @IdProducto
		end
						
	end
	close cProd
	deallocate cProd
end


-----------------------------------------------------------------------------------






create trigger TR_Activar_Desactivar_Materia_Al_No_Haber_Existencias
on MateriaPrima
after update
as
begin
	

	set nocount on
	declare @IdMateriaNueva int
	select @IdMateriaNueva = id from deleted

	if(select existencias from inserted)=0 
	begin
		if(select Estado from inserted)=1 
			update MateriaPrima set Estado =0 where ID = @IdMateriaNueva
	end
	else
	begin
		if(select existencias from inserted)=0 
			update MateriaPrima set Estado =1 where ID = @IdMateriaNueva
	end

end









-------------------





create trigger TR_CrearEnvio
on Pedido
after insert, update
as
begin
	set nocount on
	Declare @IdPedidoNuevo int
	select @IdPedidoNuevo = IdFactura from inserted


	insert into Envio values (@IdPedidoNuevo,null,0)


end

---------------------------------------------------



------------------------------------------------------------------


create trigger Tr_Activar_Desactivar_Material_Por_Proveedor
on Proveedor
after update
as
begin
	set nocount on
	declare @IdProveedor int
	select @IdProveedor = id from inserted


	declare @IdMaterial int
	declare cMat cursor for
	select distinct MateriaPrima.ID
	from MateriaPrima, Proveedor
	where Proveedor.ID=@IdProveedor and MateriaPrima.IdProveedor=@IdProveedor
	open cMat
	fetch cMat into @IdMaterial
	while @@FETCH_STATUS=0
	begin
		

		if(select estado from inserted)<>0
		begin
			update MateriaPrima set Estado = 1 where IdProveedor=@IdProveedor and ID=@IdMaterial
			fetch cMat into @IdMaterial	
		end
		else
		begin
			update MateriaPrima set Estado = 0 where IdProveedor=@IdProveedor and ID=@IdMaterial
			fetch cMat into @IdMaterial	
		end
			
	end
	close cMat
	deallocate cMat
end

-------------------------------------------



create trigger TR_RestarMateriaPrima
on DetalleFactura
after insert 
as
begin
	set nocount on
	
	declare @IdProductoNuevo int
	select @IdProductoNuevo=IdProductoFinal from inserted

	declare @IdMaterial int
	declare cP cursor for
	select distinct 
	MateriaPrima.ID from MateriaPrima,DetalleProducto,ProductoFinal
	where DetalleProducto.IdMateriaPrima=MateriaPrima.ID and
	DetalleProducto.IdProducto=@IdProductoNuevo and ProductoFinal.ID= @IdProductoNuevo
	open cP
	fetch cP into @IdMaterial
	while @@FETCH_STATUS=0
	begin
		
		if(select MateriaPrima.Existencias from MateriaPrima where ID= @IdMaterial )>=
		(select DetalleProducto.Cantidad from DetalleProducto,ProductoFinal,MateriaPrima
		where DetalleProducto.IdProducto=@IdProductoNuevo and MateriaPrima.ID =@IdMaterial
		and MateriaPrima.ID=DetalleProducto.IdMateriaPrima
		and ProductoFinal.ID= @IdProductoNuevo)
		begin
			update MateriaPrima set Existencias=Existencias-(select DetalleProducto.Cantidad 
			from DetalleProducto,ProductoFinal,MateriaPrima
			where DetalleProducto.IdProducto=@IdProductoNuevo and MateriaPrima.ID = @IdMaterial
			and @IdMaterial=DetalleProducto.IdMateriaPrima
			and ProductoFinal.ID= @IdProductoNuevo)*Cantidad from inserted where ID =@IdMaterial

			if(select MateriaPrima.Existencias from MateriaPrima where ID=@IdMaterial)<0
				rollback

			select 1 as resultado
		fetch cP into @IdMaterial
		end

	
		else
		begin
			rollback
			
		end

	end
	close cP
	deallocate cP
end














--------------------------------------------------//////////////REPORTES////////////////-------------------------------------------




create proc PA_ReporteMasVendidos
as
begin
	select top 5  ProductoFinal.Descripcion, sum(detallefactura.cantidad) as Cantidad
	from DetalleFactura, ProductoFinal
	where DetalleFactura.IdProductoFinal=ProductoFinal.ID
	group by DetalleFactura.IdProductoFinal, ProductoFinal.Descripcion
	order by count(1) desc
end





create proc PA_ReporteMasVendidos
as
begin
	select top 5  ProductoFinal.Descripcion, sum(detallefactura.cantidad) as Cantidad
	from DetalleFactura, ProductoFinal,EncabezadoFactura
	where DetalleFactura.IdProductoFinal=ProductoFinal.ID and EncabezadoFactura.ID=DetalleFactura.IdEncFact
	and EncabezadoFactura.Estado ='Pagado'
	group by DetalleFactura.IdProductoFinal, ProductoFinal.Descripcion
	order by count(1) desc
end


--------------------------------


create proc PA_ReporteProductos
as
begin 
	select ProductoFinal.Descripcion,ProductoFinal.Precio,CategoriaProducto.Descripcion as Categoria 
	from ProductoFinal, CategoriaProducto
	where ProductoFinal.IdCategoria=CategoriaProducto.ID
end

-------------------------------

create proc PA_ReporteVentas
@FechaInicio date,
@FechaFinal date
as
begin
	
	select  EncabezadoFactura.Fecha,COUNT(EncabezadoFactura.ID) as Cantidad,EncabezadoFactura.Pago,
	sum(EncabezadoFactura.Monto) as Monto
	from EncabezadoFactura
	where EncabezadoFactura.Fecha>=@FechaInicio and EncabezadoFactura.Fecha<=@FechaFinal
	and estado = 'Pagado'
	group by EncabezadoFactura.Fecha,EncabezadoFactura.Pago
end
