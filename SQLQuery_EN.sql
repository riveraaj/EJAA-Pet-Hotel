------ DataBase ------

CREATE DATABASE PetHotel;
----------------------------------------------------------------------------------------------------------------

USE PetHotel;

------ TABLE PERSON ------

CREATE TABLE Person (
	personID INT NOT NULL,
	personName VARCHAR(50) NOT NULL, 
	personLastname VARCHAR(50) NOT NULL,
	personSecondname VARCHAR(50) NOT NULL,
	email VARCHAR(100) NOT NULL,
	phone INT NOT NULL,

	PRIMARY KEY(personID)
);

INSERT INTO Person VALUES(118800124, 'Jonathan', 'Rivera', 'Vasquez','jonathandavidr7@gmail.com', 84432412);

SELECT * FROM Person;
----------------------------------------------------------------------------------------------------------------

------ TABLE ROLE ------

CREATE TABLE "Role" (
	roleID INT NOT NULL,
	roleDescription VARCHAR(50) NOT NULL,

	PRIMARY KEY(roleID)
);

INSERT INTO "Role" VALUES(1, 'Administrador');
INSERT INTO "Role" VALUES(2, 'Especialista');
INSERT INTO "Role" VALUES(3, 'Mantenimiento');
INSERT INTO "Role" VALUES(4, 'Usuario');

SELECT * FROM "Role";
----------------------------------------------------------------------------------------------------------------

------ TABLE EMPLOYEE ------

CREATE TABLE Employee (
	employeeID INT NOT NULL IDENTITY(1,1),
	personID INT NOT NULL,
	roleID INT NOT NULL,
	"password" VARCHAR(200) NOT NULL,
	dateEntry DATE NOT NULL,

	PRIMARY KEY(employeeID),
	FOREIGN KEY(personID) REFERENCES Person(personID),
	FOREIGN KEY(roleID) REFERENCES "Role"(roleID)
);

INSERT INTO Employee VALUES(118800124, 1, '17ff2fc0c19375cb6a18d24d43163516f77356912312873ece30795aae656caa', '2023-07-22');

SELECT * FROM Employee;
----------------------------------------------------------------------------------------------------------------

------ TABLE USER ------

CREATE TABLE "User" (
	userID INT NOT NULL IDENTITY(1,1),
	personID INT NOT NULL, 
	roleID INT NOT NULL,
	"password" VARCHAR(200) NOT NULL,

	PRIMARY KEY(userID),
	FOREIGN KEY(personID) REFERENCES Person(personID),
	FOREIGN KEY(roleID) REFERENCES "Role"(roleID),
);

INSERT INTO "User" VALUES(118800124, 4, '3b612c75a7b5048a435fb6ec81e52ff92d6d795a8b5a9c17070f6a63c97a53b2');

SELECT * FROM "User"
----------------------------------------------------------------------------------------------------------------

------ TABLE PET ------

CREATE TABLE Pet (
	petID INT NOT NULL IDENTITY(1,1),
	userID INT NOT NULL,
	petName VARCHAR(50) NOT NULL,

	PRIMARY KEY(petID),
	FOREIGN KEY(userID) REFERENCES "User"(userID)
);

INSERT INTO Pet VALUES(1, 'Haru');

SELECT * FROM Pet;
----------------------------------------------------------------------------------------------------------------

------ TABLE ROOM STATUS ------

CREATE TABLE Room_Status(
	roomStatusID INT NOT NULL IDENTITY(1,1),
	roomStatusDescription VARCHAR(50) NOT NULL,

	PRIMARY KEY(roomStatusID)
);

INSERT INTO Room_Status VALUES('Disponible');
INSERT INTO Room_Status VALUES('Reservada');
INSERT INTO Room_Status VALUES('Mantenimiento');
INSERT INTO Room_Status VALUES('Cerrado');

SELECT * FROM Room_Status
----------------------------------------------------------------------------------------------------------------

------ TABLE ROOM TYPE ------

CREATE TABLE Room_Type(
	roomTypeID INT NOT NULL IDENTITY(1,1),
	roomTypeDescription VARCHAR(50) NOT NULL,

	PRIMARY KEY(RoomTypeID),
);

INSERT INTO Room_Type VALUES ('Individual');
INSERT INTO Room_Type VALUES ('Individual con cuidados especiales');
INSERT INTO Room_Type VALUES ('Individual con camara');

SELECT * FROM Room_Type
----------------------------------------------------------------------------------------------------------------

------ TABLE ROOM ------

CREATE TABLE Room(
	roomID INT NOT NULL IDENTITY(1,1),
	roomStatusID INT NOT NULL, 
	roomTypeID INT NOT NULL,

	PRIMARY KEY(roomID),
	FOREIGN KEY(roomStatusID) REFERENCES Room_Status(roomStatusID),
	FOREIGN KEY(roomTypeID) REFERENCES Room_Type(roomTypeID)
);

INSERT INTO Room VALUES (1, 1);
INSERT INTO Room VALUES (1, 2);
INSERT INTO Room VALUES (1, 3);
INSERT INTO Room VALUES (1, 1);
INSERT INTO Room VALUES (1, 2);
INSERT INTO Room VALUES (1, 3);
INSERT INTO Room VALUES (1, 1);
INSERT INTO Room VALUES (1, 2);
INSERT INTO Room VALUES (1, 3);
INSERT INTO Room VALUES (1, 1);
INSERT INTO Room VALUES (1, 2);
INSERT INTO Room VALUES (1, 3);
INSERT INTO Room VALUES (1, 1);
INSERT INTO Room VALUES (1, 2);
INSERT INTO Room VALUES (1, 3);



SELECT * FROM Room
----------------------------------------------------------------------------------------------------------------

------ TABLE MAINTENANCE TYPE ------

CREATE TABLE Maintenance_Type(
	maintenanceTypeID INT NOT NULL IDENTITY(1,1),
	maintenanceTypeDescription VARCHAR(50) NOT NULL

	PRIMARY KEY(maintenanceTypeID)
);

INSERT INTO Maintenance_Type VALUES ('Limpieza');
INSERT INTO Maintenance_Type VALUES ('Reparacion');
INSERT INTO Maintenance_Type VALUES ('Actualizacion de mobiliario');
INSERT INTO Maintenance_Type VALUES ('Inspeccion');

SELECT * FROM Maintenance_Type
----------------------------------------------------------------------------------------------------------------

------ TABLE MAINTENANCE ------

CREATE TABLE Maintenance(
	maintenanceID INT NOT NULL IDENTITY(1,1),
	maintenanceTypeID INT NOT NULL,
	employeeID INT NOT NULL,
	roomID INT NOT NULL,
	maintenanceState BIT NOT NULL, ---- 1 = activo. 0 = finalizado
	dateEnd DATE NOT NULL, 
	maintenanceDescription VARCHAR(200) NOT NULL,

	PRIMARY KEY (maintenanceID),
	FOREIGN KEY (maintenanceTypeID) REFERENCES Maintenance_Type(maintenanceTypeID),
	FOREIGN KEY (employeeID) REFERENCES Employee(employeeID),
	FOREIGN KEY (roomID) REFERENCES Room(roomID)
)
----------------------------------------------------------------------------------------------------------------

------ TABLE STAY TYPE ------

CREATE TABLE Stay_Type(
	stayTypeID INT NOT NULL IDENTITY(1,1),
	stayTypeDescription	VARCHAR(50)

	PRIMARY KEY(stayTypeID)
)

INSERT INTO Stay_Type VALUES('Horario 1: 6am - 3pm');
INSERT INTO Stay_Type VALUES('Horario 2: 3pm - 10pm');
INSERT INTO Stay_Type VALUES('Horario 3: 10pm - 6pm');
INSERT INTO Stay_Type VALUES('Horario 4: Full Estancia');

SELECT * FROM Stay_Type;
----------------------------------------------------------------------------------------------------------------

------ TABLE PACKAGE TYPE ------

CREATE TABLE Package_Type(
	packageTypeID INT NOT NULL IDENTITY(1,1),
	price INT NOT NULL, 
	packageTypeDescription VARCHAR(50),
	PRIMARY KEY(packageTypeID)
)

INSERT INTO Package_Type VALUES(30000,'Gold');
INSERT INTO Package_Type VALUES(40000,'Platino');
INSERT INTO Package_Type VALUES(50000,'Diamante');

SELECT * FROM Package_Type
----------------------------------------------------------------------------------------------------------------

------ TABLE RESERVATION ------

CREATE TABLE Reservation(
	reservationID INT NOT NULL IDENTITY(1,1),
	petID INT NOT NULL, 
	employeeID INT NOT NULL,
	roomID INT NOT NULL, 
	dateStart DATE NOT NULL, 
	dateEnd DATE NOT NULL,
	dateUndefined BIT NOT NULL,
	stayTypeID INT NOT NULL,
	packageTypeID INT NOT NULL,
	descriptionPetCare VARCHAR(200) NOT NULL,
	finalPrice INT NOT NULL,
	reservationState CHAR(1) NOT NULL, ---se confirma o no se confirma la reservacion
	CONSTRAINT reservationState CHECK (reservationState IN ('A','R','P')), -- A = Aceptado.  R = Rechazado.  P = Pendiente.
	
	PRIMARY KEY(reservationID),
	FOREIGN KEY(petID) REFERENCES Pet(petID),
	FOREIGN KEY(employeeID) REFERENCES Employee(employeeID),
	FOREIGN KEY(roomID) REFERENCES Room(roomID),
	FOREIGN KEY(stayTypeID) REFERENCES	Stay_Type(stayTypeID),
	FOREIGN KEY(packageTypeID) REFERENCES Package_Type(packageTypeID)
)

----------------------------------------------------------------------------------------------------------------

------ PROCEDURE UPDATE ROOM STATUS ------

GO
	CREATE PROCEDURE UpdateRoomStatus
		@roomID INT,
		@maintenanceState BIT
	AS
	BEGIN
		-- Actualizar el roomStatusID a 3 cuando se crea un nuevo Maintenance
		IF @maintenanceState = 1
		BEGIN
			UPDATE Room
			SET roomStatusID = 3
			WHERE roomID = @roomID;
		END
		-- Cambiar el roomStatusID a 1 cuando el Maintenance se marca como finalizado
		ELSE IF @maintenanceState = 0
		BEGIN
			UPDATE Room
			SET roomStatusID = 1
			WHERE roomID = @roomID;
		END
	END;
GO

SELECT * FROM Room;

SELECT * FROM Maintenance;

INSERT INTO Maintenance VALUES (1, 2, 3, 1, GETDATE(), 'Limpieza');
EXEC UpdateRoomStatus @roomID = 3, @maintenanceState = 1;

UPDATE Maintenance SET maintenanceState = 0 WHERE maintenanceID = 16;
EXEC UpdateRoomStatus @roomID = 3, @maintenanceState = 0;
----------------------------------------------------------------------------------------------------------------

------ PROCEDURE UPDATE RESERVATION STATUS ------

GO
	CREATE PROCEDURE UpdateReservationStatus
		@reservationID INT,
		@reservationState CHAR(1)
	AS
	BEGIN
		-- Actualizar el estado de la reservación y el estado de la habitación según el estado proporcionado
		IF @reservationState = 'P' -- Pendiente
		BEGIN
			UPDATE Room
			SET roomStatusID = 2 -- Reservada
			WHERE roomID = (SELECT roomID FROM Reservation WHERE reservationID = @reservationID);
		END
		ELSE IF @reservationState = 'R' -- Rechazado
		BEGIN

			UPDATE Room
			SET roomStatusID = 1 -- Disponible
			WHERE roomID = (SELECT roomID FROM Reservation WHERE reservationID = @reservationID);
		END
		ELSE IF @reservationState = 'A' -- Aceptado
		BEGIN
			UPDATE Room
			SET roomStatusID = 2 -- Reservada
			WHERE roomID = (SELECT roomID FROM Reservation WHERE reservationID = @reservationID);
		END
	END;
GO



SELECT * FROM Room;

SELECT * FROM Reservation;

INSERT INTO Reservation VALUES(1, 13, 3, '2023-07-22', '2023-07-25', 0, 4, 3, 'N/A', 50000, 'P');
EXEC UpdateReservationStatus @reservationID = 5, @reservationState = 'P'; -- Crear una nueva reservación (Pendiente)

UPDATE Reservation SET reservationState = 'R' WHERE reservationID = 5;
EXEC UpdateReservationStatus @reservationID = 5, @reservationState = 'R'; -- Actualizar la reservación (Rechazado)

UPDATE Reservation SET reservationState = 'A' WHERE reservationID = 5;
EXEC UpdateReservationStatus @reservationID = 5, @reservationState = 'A'; -- Actualizar la reservación (Aceptado)
----------------------------------------------------------------------------------------------------------------

GO
	CREATE VIEW VIEW_RESERVE AS 

	SELECT RE.reservationID AS No_Reservation, PE.personName AS Constumer_Name, PE.personLastname AS Costumer_Lastname,
		   PE.personSecondname AS Costumer_Secondname, PE.email AS Costumer_Email, PE.phone AS Costumer_Phone, 
		   PT.petName AS Pet_Name, RE.descriptionPetCare AS Pet_Care, PEM.personName as Employee_Name, 
		   PEM.personLastname as Employee_Lastname, CTP.packageTypeDescription AS Package, RO.roomID AS No_Room, RE.finalPrice AS Final_Price
	FROM Reservation RE
	JOIN Pet PT ON PT.petID = RE.petID
	JOIN "User" US ON US.userID = PT.userID
	JOIN Person PE ON PE.personID = US.personID
	JOIN Room RO ON RO.roomID = RE.roomID
	JOIN Employee EM ON EM.employeeID = RE.employeeID
	JOIN Person PEM ON PEM.personID = EM.personID
	JOIN Package_Type CTP ON CTP.packageTypeID = RE.packageTypeID;
GO

SELECT * FROM VIEW_RESERVE

DROP VIEW VIEW_RESERVE;

ALTER TABLE Maintenance
ADD CONSTRAINT FK_Maintenance_Employee
FOREIGN KEY (employeeID)
REFERENCES Employee (employeeID)
ON DELETE CASCADE;