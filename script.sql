CREATE DATABASE SistemaAcademico;
GO

USE SistemaAcademico;
GO

CREATE TABLE Alumno (
    AlumnoId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Grado VARCHAR(50) NOT NULL
);
GO

CREATE TABLE Materia (
    MateriaId INT IDENTITY(1,1) PRIMARY KEY,
    NombreMateria VARCHAR(100) NOT NULL,
    Docente VARCHAR(100) NOT NULL
);
GO

CREATE TABLE Expediente (
    ExpedienteId INT IDENTITY(1,1) PRIMARY KEY,
    AlumnoId INT NOT NULL,
    MateriaId INT NOT NULL,
    NotaFinal DECIMAL(5,2) NULL,
    Observaciones VARCHAR(500) NULL,

    CONSTRAINT FK_Expediente_Alumno
        FOREIGN KEY (AlumnoId)
        REFERENCES Alumno(AlumnoId),

    CONSTRAINT FK_Expediente_Materia
        FOREIGN KEY (MateriaId)
        REFERENCES Materia(MateriaId),

    CONSTRAINT UQ_Expediente_Alumno_Materia
        UNIQUE (AlumnoId, MateriaId),

    CONSTRAINT CK_Expediente_NotaFinal
        CHECK (NotaFinal IS NULL OR NotaFinal BETWEEN 0 AND 10)
);
GO

--Datos de pruebas
INSERT INTO Alumno (Nombre, Apellido, FechaNacimiento, Grado)
VALUES
('Carlos', 'Martínez', '2010-05-12', '7°'),
('Ana', 'López', '2011-08-20', '6°'),
('Luis', 'Ramírez', '2009-03-15', '8°');

INSERT INTO Materia (NombreMateria, Docente)
VALUES
('Matemática', 'Juan Pérez'),
('Lenguaje', 'María García'),
('Ciencias', 'Roberto Hernández');

INSERT INTO Expediente (AlumnoId, MateriaId, NotaFinal, Observaciones)
VALUES
(1, 1, 8.50, 'Buen desempeño'),
(1, 2, 9.00, 'Excelente participación'),
(2, 1, 7.25, 'Debe reforzar ejercicios'),
(3, 3, 8.00, 'Participación aceptable');


SELECT 
    e.ExpedienteId,
    a.Nombre + ' ' + a.Apellido AS Alumno,
    a.Grado,
    m.NombreMateria,
    m.Docente,
    e.NotaFinal,
    e.Observaciones
FROM Expediente e
INNER JOIN Alumno a ON e.AlumnoId = a.AlumnoId
INNER JOIN Materia m ON e.MateriaId = m.MateriaId;