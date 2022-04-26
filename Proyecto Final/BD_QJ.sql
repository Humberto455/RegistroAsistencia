CREATE DATABASE BD_QJ;
DROP DATABASE BD_QJ;
USE BD_QJ;

CREATE TABLE Maestro(
	NSS int not null,
	Nombre varchar(20) not null,
	ApellidoPa varchar(20) not null,
	ApellidoMa varchar(20) not null,
	Correo varchar(50) null,
	Especialidad varchar(35)null,
	Celular varchar(10) not null,
	Edad int null,
	FechaNac Date null
);

CREATE TABLE Alumno(
	Matricula int Identity(1000,1) not null,
	Nombre varchar(20) not null,
	ApellidoPa varchar(20) not null,
	ApellidoMa varchar(20) not null,
	Correo varchar(50) null,
	Carrera varchar(50) not null,
	Grupo char(2) not null,
	Celular varchar(10) not null,
	Edad int null,
	FechaNac Date null
);

CREATE TABLE Asistencia(
	IDasistencia int Identity(1,1) not null,
	IDmaestro int not null,
	IDalumno int not null,
	Fecha date,
	Asistencia char(1)
);

--Asignacion de llaves Primarias
ALTER TABLE Maestro
ADD CONSTRAINT PK_Maestro
PRIMARY KEY(NSS);

ALTER TABLE Alumno
ADD CONSTRAINT PK_Alumno
PRIMARY KEY(Matricula);

ALTER TABLE Asistencia
ADD CONSTRAINT PK_Asistencia
PRIMARY KEY(IDasistencia);

--Asignacion de llaves Foraneas
ALTER TABLE Asistencia
ADD CONSTRAINT PK_AsistenciaMaestroNSS
FOREIGN KEY (IDmaestro) REFERENCES Maestro(NSS);

ALTER TABLE Asistencia
ADD CONSTRAINT PK_AsistenciaAlumnoMatricula
FOREIGN KEY (IDalumno) REFERENCES Alumno(Matricula);

--Datos

--Maestros
INSERT INTO Maestro(NSS,Nombre,ApellidoPa,ApellidoMa,Correo,Especialidad,Celular,Edad,FechaNac)
VALUES(1111,'Sergio','Perez','Garcia','SPG@GMAIL.COM','INFORMATICA','8125343352',40,'1980-01-12');

INSERT INTO Maestro(NSS,Nombre,ApellidoPa,ApellidoMa,Correo,Especialidad,Celular,Edad,FechaNac)
VALUES(2222,'Margarita','Fernandez','Sanchez','MFS@GMAIL.COM','ADMINISTRACION','8144349952',30,'1981-05-20');

--Alumnos
INSERT INTO Alumno(Nombre,ApellidoPa,ApellidoMa,Correo,Carrera,Grupo,Celular,Edad,FechaNac)
VALUES('Juan','marcos','iturbide','EJEMPLO@GMAIL.COM','Ing.Mecatronica','2B','8125345422',19,'2002-01-12');

INSERT INTO Alumno(Nombre,ApellidoPa,ApellidoMa,Correo,Carrera,Grupo,Celular,Edad,FechaNac)
VALUES('Jimena','Navarro','Mateos','EJEMPLO@GMAIL.COM','Ing.Informatica','2A','8126354422',22,'2001-05-10');

INSERT INTO Alumno(Nombre,ApellidoPa,ApellidoMa,Correo,Carrera,Grupo,Celular,Edad,FechaNac)
VALUES('lisa','martinez','Olvera','EJEMPLO@GMAIL.COM','Lic.Administracion','2C','8123545822',19,'1998-08-01');

--Asistencia
INSERT INTO Asistencia(IDmaestro,IDalumno,Fecha,Asistencia)
VALUES(1111,1001,'2022-03-22','F');

INSERT INTO Asistencia(IDmaestro,IDalumno,Fecha,Asistencia)
VALUES(2222,1002,'2022-03-22','F');

INSERT INTO Asistencia(IDmaestro,IDalumno,Fecha,Asistencia)
VALUES(2222,1002,'2022-03-26','F');

INSERT INTO Asistencia(IDmaestro,IDalumno,Fecha,Asistencia)
VALUES(1111,1001,'2022-03-26','A');

INSERT INTO Asistencia(IDmaestro,IDalumno,Fecha,Asistencia)
VALUES(1111,1001,'2022-03-28','F');

INSERT INTO Asistencia(IDmaestro,IDalumno,Fecha,Asistencia)
VALUES(2222,1002,'2022-03-28','A');

INSERT INTO Asistencia(IDmaestro,IDalumno,Fecha,Asistencia)
VALUES(1111,1001,'2022-04-02','F');

INSERT INTO Asistencia(IDmaestro,IDalumno,Fecha,Asistencia)
VALUES(1111,1001,'2022-04-04','A');

INSERT INTO Asistencia(IDmaestro,IDalumno,Fecha,Asistencia)
VALUES(1111,1001,'2022-04-05','F');

INSERT INTO Asistencia(IDmaestro,IDalumno,Fecha,Asistencia)
VALUES(2222,1001,'2022-04-05','F');


CREATE PROCEDURE sp_add_Asistencia
@IDmaestro int,
@IDalumno int,
@Fecha date,
@Asistencia char(1)
AS
INSERT INTO Asistencia(IDmaestro,IDalumno,Fecha,Asistencia)
VALUES(@IDmaestro,@IDalumno,@Fecha,@Asistencia);



--Sintaxis en algunos SqlCommand
SELECT * FROM Asistencia WHERE IDmaestro = @IDmaestro and IDalumno = @IDalumno and Fecha = @Fecha;

UPDATE Asistencia SET Asistencia = @Asistencia WHERE IDmaestro = @IDmaestro and IDalumno = @IDalumno and Fecha = @Fecha;

SELECT * FROM Asistencia;

SELECT * FROM Alumno;

DROP TABLE Asistencia;


--Cantidad de Faltas
SELECT COUNT(Asistencia) AS Cant_Faltas, IDalumno, IDmaestro FROM Asistencia WHERE Asistencia = 'F' GROUP BY IDmaestro,IDalumno;

DELETE FROM Asistencia WHERE IDalumno = 1003;