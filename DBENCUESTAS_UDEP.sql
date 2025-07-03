-- =====================================================
-- MODELO DE BASE DE DATOS - SISTEMA DE ENCUESTAS UDEP
-- =====================================================
-- Basado en las Especificaciones Técnicas Funcionales
-- Convenciones aplicadas según documento de estándares
-- =====================================================

-- =====================================================
-- 1. MÓDULO: GESTIÓN DE PERIODOS Y DEPARTAMENTOS
-- =====================================================

-- Tabla: Departamentos
CREATE TABLE Departamento (
    iIdDepartamento INT IDENTITY(1,1) PRIMARY KEY,
    cNombreDepartamento VARCHAR(256) NOT NULL UNIQUE,
    cCorreoInstitucional VARCHAR(100) NOT NULL,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1
);

-- Tabla: Periodos Académicos
CREATE TABLE Periodo (
    iIdPeriodo INT IDENTITY(1,1) PRIMARY KEY,
    iAnioAcademico INT NOT NULL,
    cNumeroPeriodo VARCHAR(10) NOT NULL, -- 'Sin Numero', '0', 'I', 'II'
    cNombrePeriodo VARCHAR(100) NOT NULL,
    fFechaInicio DATE NOT NULL,
    fFechaCulminacion DATE NOT NULL,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT UK_Periodo_Anio_Numero UNIQUE (iAnioAcademico, cNumeroPeriodo)
);

-- =====================================================
-- 2. MÓDULO: GESTIÓN DE ASIGNATURAS
-- =====================================================

-- Tabla: Componentes Académicos
CREATE TABLE Componente (
    iIdComponente INT IDENTITY(1,1) PRIMARY KEY,
    cSiglas VARCHAR(10) NOT NULL UNIQUE,
    cNombreComponente VARCHAR(100) NOT NULL UNIQUE,
    cDescripcion VARCHAR(8000) NULL,
    bEliminarOpcionesEncuesta BIT NOT NULL DEFAULT 0,
    bVisibleReportes BIT NOT NULL DEFAULT 1,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1
);

-- Tabla: Asignaturas/Cursos
CREATE TABLE Asignatura (
    iIdAsignatura INT IDENTITY(1,1) PRIMARY KEY,
    iIdDepartamento INT NOT NULL,
    iIdComponente INT NULL, -- Puede ser NULL (Sin Componente)
    cSiglas VARCHAR(10) NOT NULL UNIQUE,
    cNombreAsignatura VARCHAR(256) NOT NULL,
    iCiclo INT NOT NULL CHECK (iCiclo BETWEEN 1 AND 14),
    iAnio INT NOT NULL CHECK (iAnio BETWEEN 1 AND 7),
    iCreditos INT NOT NULL CHECK (iCreditos BETWEEN 1 AND 20),
    bTieneCapitulos BIT NOT NULL DEFAULT 0,
    iNumeroCapitulos INT NOT NULL DEFAULT 0 CHECK (iNumeroCapitulos BETWEEN 0 AND 5),
    bTieneSedeHospitalaria BIT NOT NULL DEFAULT 0,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Asignatura_Departamento FOREIGN KEY (iIdDepartamento) REFERENCES Departamento(iIdDepartamento),
    CONSTRAINT FK_Asignatura_Componente FOREIGN KEY (iIdComponente) REFERENCES Componente(iIdComponente)
);

-- Tabla: Capítulos de Asignaturas
CREATE TABLE Capitulo (
    iIdCapitulo INT IDENTITY(1,1) PRIMARY KEY,
    iIdAsignatura INT NOT NULL,
    cNumeroCapitulo VARCHAR(10) NOT NULL, -- Números romanos: I, II, III, etc.
    cNombreCapitulo VARCHAR(128) NOT NULL,
    cDescripcionCapitulo VARCHAR(8000) NULL,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Capitulo_Asignatura FOREIGN KEY (iIdAsignatura) REFERENCES Asignatura(iIdAsignatura),
    CONSTRAINT UK_Capitulo_Asignatura_Numero UNIQUE (iIdAsignatura, cNumeroCapitulo)
);

-- Tabla: Actividades Académicas
CREATE TABLE Actividad (
    iIdActividad INT IDENTITY(1,1) PRIMARY KEY,
    cNombreActividad VARCHAR(128) NOT NULL,
    cDescripcionActividad VARCHAR(8000) NULL,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1
);

-- Tabla: Asignación de Actividades por Capítulo/Asignatura
CREATE TABLE Asignacion_Actividad_Capitulo (
    iIdAsignacion INT IDENTITY(1,1) PRIMARY KEY,
    iIdAsignatura INT NOT NULL,
    iIdCapitulo INT NULL, -- NULL para "Sin Capítulos"
    iIdActividad INT NOT NULL,
    cUsuarioAsignador VARCHAR(100) NOT NULL,
    fFechaAsignacion DATETIME2 NOT NULL DEFAULT GETDATE(),
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_AsigActCap_Asignatura FOREIGN KEY (iIdAsignatura) REFERENCES Asignatura(iIdAsignatura),
    CONSTRAINT FK_AsigActCap_Capitulo FOREIGN KEY (iIdCapitulo) REFERENCES Capitulo(iIdCapitulo),
    CONSTRAINT FK_AsigActCap_Actividad FOREIGN KEY (iIdActividad) REFERENCES Actividad(iIdActividad),
    CONSTRAINT UK_AsigActCap_Actividad_Capitulo UNIQUE (iIdActividad, iIdCapitulo, iIdAsignatura)
);

-- Tabla: Asignación de Componentes a Actividades
CREATE TABLE Asignacion_Componente_Actividad (
    iIdAsignacion INT IDENTITY(1,1) PRIMARY KEY,
    iIdActividad INT NOT NULL,
    iIdComponente INT NOT NULL,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_AsigCompAct_Actividad FOREIGN KEY (iIdActividad) REFERENCES Actividad(iIdActividad),
    CONSTRAINT FK_AsigCompAct_Componente FOREIGN KEY (iIdComponente) REFERENCES Componente(iIdComponente),
    CONSTRAINT UK_AsigCompAct_Actividad_Componente UNIQUE (iIdActividad, iIdComponente)
);

-- Tabla: Dimensiones Académicas
CREATE TABLE Dimension (
    iIdDimension INT IDENTITY(1,1) PRIMARY KEY,
    cNombreDimension VARCHAR(128) NOT NULL UNIQUE,
    cDescripcion VARCHAR(8000) NOT NULL UNIQUE,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1
);

-- Tabla: Preguntas del Sistema
CREATE TABLE Pregunta (
    iIdPregunta INT IDENTITY(1,1) PRIMARY KEY,
    iIdComponente INT NOT NULL,
    cTipoPregunta CHAR(1) NOT NULL CHECK (cTipoPregunta IN ('O', 'T', 'C', 'L')), -- Opción, Texto, Calificación, Likert
    cDescripcion VARCHAR(8000) NOT NULL UNIQUE,
    bVariasRespuestas BIT NULL, -- Solo para Tipo 'O'
    bRespuestaLarga BIT NULL DEFAULT 1, -- Solo para Tipo 'T'
    iNivelCalificacion INT NULL CHECK (iNivelCalificacion BETWEEN 1 AND 10), -- Solo para Tipo 'C'
    bObligatoria BIT NOT NULL DEFAULT 0,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Pregunta_Componente FOREIGN KEY (iIdComponente) REFERENCES Componente(iIdComponente)
);

-- Tabla: Opciones de Preguntas (Para tipos O y L)
CREATE TABLE Opcion_Pregunta (
    iIdOpcion INT IDENTITY(1,1) PRIMARY KEY,
    iIdPregunta INT NOT NULL,
    cDescripcionOpcion VARCHAR(8000) NOT NULL,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_OpcionPregunta_Pregunta FOREIGN KEY (iIdPregunta) REFERENCES Pregunta(iIdPregunta)
);

-- Tabla: Instrucciones para Preguntas Likert (Solo tipo L)
CREATE TABLE Instruccion_Pregunta (
    iIdInstruccion INT IDENTITY(1,1) PRIMARY KEY,
    iIdPregunta INT NOT NULL,
    cDescripcionInstruccion VARCHAR(8000) NOT NULL,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_InstruccionPregunta_Pregunta FOREIGN KEY (iIdPregunta) REFERENCES Pregunta(iIdPregunta)
);

-- =====================================================
-- 3. MÓDULO: GESTIÓN DE OFERTAS ACADÉMICAS
-- =====================================================

-- Tabla: Ofertas Académicas
CREATE TABLE Oferta_Academica (
    iIdOfertaAcademica INT IDENTITY(1,1) PRIMARY KEY,
    iIdPeriodo INT NOT NULL,
    iIdAsignatura INT NOT NULL,
    iNumeroMatriculados INT NOT NULL CHECK (iNumeroMatriculados >= 0),
    cJefeCurso VARCHAR(100) NOT NULL,
    iAprobados INT NOT NULL CHECK (iAprobados >= 0),
    iDesaprobados INT NOT NULL CHECK (iDesaprobados >= 0),
    iRetirados INT NOT NULL CHECK (iRetirados >= 0),
    iAnulaciones INT NOT NULL CHECK (iAnulaciones >= 0),
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_OfertaAcademica_Periodo FOREIGN KEY (iIdPeriodo) REFERENCES Periodo(iIdPeriodo),
    CONSTRAINT FK_OfertaAcademica_Asignatura FOREIGN KEY (iIdAsignatura) REFERENCES Asignatura(iIdAsignatura),
    CONSTRAINT UK_OfertaAcademica_Periodo_Asignatura UNIQUE (iIdPeriodo, iIdAsignatura),
    CONSTRAINT CK_OfertaAcademica_Matriculados CHECK (iAprobados + iDesaprobados + iAnulaciones = iNumeroMatriculados),
    CONSTRAINT CK_OfertaAcademica_Retirados CHECK (iRetirados <= iNumeroMatriculados)
);

-- =====================================================
-- 4. MÓDULO: SISTEMA DE ENCUESTAS
-- =====================================================

-- Tabla: Encuestas
CREATE TABLE Encuesta (
    iIdEncuesta INT IDENTITY(1,1) PRIMARY KEY,
    iIdOfertaAcademica INT NOT NULL,
    cTitulo VARCHAR(2048) NOT NULL,
    cInstrucciones VARCHAR(8000) NOT NULL,
    fFechaHoraInicio DATETIME2 NOT NULL,
    fFechaHoraFin DATETIME2 NOT NULL,
    bEsAnonima BIT NOT NULL DEFAULT 0,
    bActiva BIT NOT NULL DEFAULT 1,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Encuesta_OfertaAcademica FOREIGN KEY (iIdOfertaAcademica) REFERENCES Oferta_Academica(iIdOfertaAcademica),
    CONSTRAINT CK_Encuesta_FechaInicio CHECK (fFechaHoraInicio < fFechaHoraFin)
);

-- Tabla: Preguntas de Encuesta (Copia de preguntas originales)
CREATE TABLE Pregunta_Encuesta (
    iIdPreguntaEncuesta INT IDENTITY(1,1) PRIMARY KEY,
    iIdEncuesta INT NOT NULL,
    iIdPregunta INT NOT NULL, -- Referencia a pregunta original
    cTipoPregunta CHAR(1) NOT NULL CHECK (cTipoPregunta IN ('O', 'T', 'C', 'L')),
    cDescripcion VARCHAR(8000) NOT NULL,
    bVariasRespuestas BIT NULL,
    bRespuestaLarga BIT NULL,
    iNivelCalificacion INT NULL,
    bObligatoria BIT NOT NULL DEFAULT 0,
    iOrden INT NOT NULL DEFAULT 0,
    bActiva BIT NOT NULL DEFAULT 1,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_PreguntaEncuesta_Encuesta FOREIGN KEY (iIdEncuesta) REFERENCES Encuesta(iIdEncuesta),
    CONSTRAINT FK_PreguntaEncuesta_Pregunta FOREIGN KEY (iIdPregunta) REFERENCES Pregunta(iIdPregunta)
);

-- Tabla: Opciones de Pregunta Encuesta
CREATE TABLE Opcion_Pregunta_Encuesta (
    iIdOpcionPreguntaEncuesta INT IDENTITY(1,1) PRIMARY KEY,
    iIdEncuesta INT NOT NULL,
    iIdPreguntaEncuesta INT NOT NULL,
    iIdPregunta INT NOT NULL,
    iIdOpcionPregunta INT NOT NULL, -- Referencia a opción original
    cDescripcionOpcion VARCHAR(8000) NOT NULL,
    bActiva BIT NOT NULL DEFAULT 1,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_OpcionPreguntaEnc_Encuesta FOREIGN KEY (iIdEncuesta) REFERENCES Encuesta(iIdEncuesta),
    CONSTRAINT FK_OpcionPreguntaEnc_PreguntaEncuesta FOREIGN KEY (iIdPreguntaEncuesta) REFERENCES Pregunta_Encuesta(iIdPreguntaEncuesta),
    CONSTRAINT FK_OpcionPreguntaEnc_Pregunta FOREIGN KEY (iIdPregunta) REFERENCES Pregunta(iIdPregunta),
    CONSTRAINT FK_OpcionPreguntaEnc_OpcionPregunta FOREIGN KEY (iIdOpcionPregunta) REFERENCES Opcion_Pregunta(iIdOpcion)
);

-- Tabla: Instrucciones de Pregunta Encuesta
CREATE TABLE Instruccion_Pregunta_Encuesta (
    iIdInstruccionPreguntaEncuesta INT IDENTITY(1,1) PRIMARY KEY,
    iIdEncuesta INT NOT NULL,
    iIdPreguntaEncuesta INT NOT NULL,
    iIdPregunta INT NOT NULL,
    iIdInstruccionPregunta INT NOT NULL, -- Referencia a instrucción original
    cDescripcionInstruccion VARCHAR(8000) NOT NULL,
    bActiva BIT NOT NULL DEFAULT 1,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_InstruccionPreguntaEnc_Encuesta FOREIGN KEY (iIdEncuesta) REFERENCES Encuesta(iIdEncuesta),
    CONSTRAINT FK_InstruccionPreguntaEnc_PreguntaEncuesta FOREIGN KEY (iIdPreguntaEncuesta) REFERENCES Pregunta_Encuesta(iIdPreguntaEncuesta),
    CONSTRAINT FK_InstruccionPreguntaEnc_Pregunta FOREIGN KEY (iIdPregunta) REFERENCES Pregunta(iIdPregunta),
    CONSTRAINT FK_InstruccionPreguntaEnc_InstruccionPregunta FOREIGN KEY (iIdInstruccionPregunta) REFERENCES Instruccion_Pregunta(iIdInstruccion)
);

-- =====================================================
-- 5. MÓDULO: RESPUESTAS DE ENCUESTAS
-- =====================================================

-- Tabla: Sesiones de Encuesta
CREATE TABLE Sesion_Encuesta (
    iIdSesion INT IDENTITY(1,1) PRIMARY KEY,
    iIdEncuesta INT NOT NULL,
    cUsuarioRespuesta VARCHAR(100) NULL, -- NULL para encuestas anónimas
    fFechaInicio DATETIME2 NOT NULL DEFAULT GETDATE(),
    fFechaCompletado DATETIME2 NULL,
    cEstado VARCHAR(20) NOT NULL DEFAULT 'EN_PROGRESO' CHECK (cEstado IN ('EN_PROGRESO', 'COMPLETADA')),
    cIPAddress VARCHAR(45) NULL,
    cCodigoConfirmacion VARCHAR(50) NULL,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_SesionEncuesta_Encuesta FOREIGN KEY (iIdEncuesta) REFERENCES Encuesta(iIdEncuesta)
);

-- Tabla: Respuestas Tipo Opción
CREATE TABLE Respuesta_Opcion (
    iIdRespuesta INT IDENTITY(1,1) PRIMARY KEY,
    iIdSesion INT NOT NULL,
    iIdPreguntaEncuesta INT NOT NULL,
    iIdOpcionSeleccionada INT NOT NULL,
    cUsuarioRespuesta VARCHAR(100) NULL, -- NULL para encuestas anónimas
    fFechaRespuesta DATETIME2 NOT NULL DEFAULT GETDATE(),
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_RespuestaOpcion_Sesion FOREIGN KEY (iIdSesion) REFERENCES Sesion_Encuesta(iIdSesion),
    CONSTRAINT FK_RespuestaOpcion_PreguntaEncuesta FOREIGN KEY (iIdPreguntaEncuesta) REFERENCES Pregunta_Encuesta(iIdPreguntaEncuesta),
    CONSTRAINT FK_RespuestaOpcion_OpcionEncuesta FOREIGN KEY (iIdOpcionSeleccionada) REFERENCES Opcion_Pregunta_Encuesta(iIdOpcionPreguntaEncuesta)
);

-- Tabla: Respuestas Tipo Texto
CREATE TABLE Respuesta_Texto (
    iIdRespuesta INT IDENTITY(1,1) PRIMARY KEY,
    iIdSesion INT NOT NULL,
    iIdPreguntaEncuesta INT NOT NULL,
    cTextoRespuesta VARCHAR(8000) NULL,
    cUsuarioRespuesta VARCHAR(100) NULL, -- NULL para encuestas anónimas
    fFechaRespuesta DATETIME2 NOT NULL DEFAULT GETDATE(),
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_RespuestaTexto_Sesion FOREIGN KEY (iIdSesion) REFERENCES Sesion_Encuesta(iIdSesion),
    CONSTRAINT FK_RespuestaTexto_PreguntaEncuesta FOREIGN KEY (iIdPreguntaEncuesta) REFERENCES Pregunta_Encuesta(iIdPreguntaEncuesta)
);

-- Tabla: Respuestas Tipo Calificación
CREATE TABLE Respuesta_Calificacion (
    iIdRespuesta INT IDENTITY(1,1) PRIMARY KEY,
    iIdSesion INT NOT NULL,
    iIdPreguntaEncuesta INT NOT NULL,
    iValorCalificacion INT NOT NULL CHECK (iValorCalificacion BETWEEN 1 AND 10),
    cUsuarioRespuesta VARCHAR(100) NULL, -- NULL para encuestas anónimas
    fFechaRespuesta DATETIME2 NOT NULL DEFAULT GETDATE(),
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_RespuestaCalificacion_Sesion FOREIGN KEY (iIdSesion) REFERENCES Sesion_Encuesta(iIdSesion),
    CONSTRAINT FK_RespuestaCalificacion_PreguntaEncuesta FOREIGN KEY (iIdPreguntaEncuesta) REFERENCES Pregunta_Encuesta(iIdPreguntaEncuesta)
);

-- Tabla: Respuestas Tipo Likert
CREATE TABLE Respuesta_Likert (
    iIdRespuesta INT IDENTITY(1,1) PRIMARY KEY,
    iIdSesion INT NOT NULL,
    iIdPreguntaEncuesta INT NOT NULL,
    iIdOpcionSeleccionada INT NOT NULL,
    iIdInstruccion INT NOT NULL,
    cUsuarioRespuesta VARCHAR(100) NULL, -- NULL para encuestas anónimas
    fFechaRespuesta DATETIME2 NOT NULL DEFAULT GETDATE(),
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_RespuestaLikert_Sesion FOREIGN KEY (iIdSesion) REFERENCES Sesion_Encuesta(iIdSesion),
    CONSTRAINT FK_RespuestaLikert_PreguntaEncuesta FOREIGN KEY (iIdPreguntaEncuesta) REFERENCES Pregunta_Encuesta(iIdPreguntaEncuesta),
    CONSTRAINT FK_RespuestaLikert_OpcionEncuesta FOREIGN KEY (iIdOpcionSeleccionada) REFERENCES Opcion_Pregunta_Encuesta(iIdOpcionPreguntaEncuesta),
    CONSTRAINT FK_RespuestaLikert_InstruccionEncuesta FOREIGN KEY (iIdInstruccion) REFERENCES Instruccion_Pregunta_Encuesta(iIdInstruccionPreguntaEncuesta)
);

-- =====================================================
-- 6. TABLAS DE AUDITORÍA Y LOGS
-- =====================================================

-- Tabla: Logs de Acceso al Sistema
CREATE TABLE Log_Acceso (
    iIdLogAcceso INT IDENTITY(1,1) PRIMARY KEY,
    cUsuario VARCHAR(100) NULL, -- NULL para accesos anónimos
    cIPAddress VARCHAR(45) NOT NULL,
    fTimestamp DATETIME2 NOT NULL DEFAULT GETDATE(),
    cResultado VARCHAR(20) NOT NULL CHECK (cResultado IN ('EXITO', 'FALLO', 'MFA_REQUERIDO')),
    cUserAgent VARCHAR(500) NULL,
    cDetallesError VARCHAR(1000) NULL,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1
);

-- Tabla: Logs de Auditoría General
CREATE TABLE Log_Auditoria (
    iIdLogAuditoria INT IDENTITY(1,1) PRIMARY KEY,
    cTablaAfectada VARCHAR(100) NOT NULL,
    iIdRegistroAfectado INT NOT NULL,
    cOperacion VARCHAR(20) NOT NULL CHECK (cOperacion IN ('INSERT', 'UPDATE', 'DELETE')),
    cUsuario VARCHAR(100) NOT NULL,
    fTimestamp DATETIME2 NOT NULL DEFAULT GETDATE(),
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    cValoresAnteriores VARCHAR(8000) NULL,
    cValoresNuevos VARCHAR(8000) NULL,
    cIPAddress VARCHAR(45) NULL
);

-- =====================================================
-- 7. ÍNDICES PARA OPTIMIZACIÓN
-- =====================================================

-- Índices para mejorar rendimiento de consultas frecuentes
CREATE INDEX IX_Asignatura_Departamento ON Asignatura(iIdDepartamento);
CREATE INDEX IX_Asignatura_Componente ON Asignatura(iIdComponente);
CREATE INDEX IX_Capitulo_Asignatura ON Capitulo(iIdAsignatura);
CREATE INDEX IX_Pregunta_Componente ON Pregunta(iIdComponente);
CREATE INDEX IX_OfertaAcademica_Periodo ON Oferta_Academica(iIdPeriodo);
CREATE INDEX IX_OfertaAcademica_Asignatura ON Oferta_Academica(iIdAsignatura);
CREATE INDEX IX_Encuesta_OfertaAcademica ON Encuesta(iIdOfertaAcademica);
CREATE INDEX IX_Encuesta_FechaInicio ON Encuesta(fFechaHoraInicio);
CREATE INDEX IX_PreguntaEncuesta_Encuesta ON Pregunta_Encuesta(iIdEncuesta);
CREATE INDEX IX_SesionEncuesta_Usuario ON Sesion_Encuesta(cUsuarioRespuesta);
CREATE INDEX IX_SesionEncuesta_Encuesta ON Sesion_Encuesta(iIdEncuesta);
CREATE INDEX IX_LogAcceso_Usuario ON Log_Acceso(cUsuario);
CREATE INDEX IX_LogAcceso_Timestamp ON Log_Acceso(fTimestamp);
GO

-- Procedimientos almacenados de mantenimiento y consulta
-- Departamento
CREATE PROCEDURE sp_Departamento_Listar
    @iIdDepartamento INT = NULL
AS
BEGIN
    SELECT * FROM Departamento
    WHERE @iIdDepartamento IS NULL OR iIdDepartamento = @iIdDepartamento;
END;
GO

CREATE PROCEDURE sp_Departamento_Mantenimiento
    @OPERACION INT,
    @iIdDepartamento INT = NULL,
    @cNombreDepartamento VARCHAR(256) = NULL,
    @cCorreoInstitucional VARCHAR(100) = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Departamento (cNombreDepartamento, cCorreoInstitucional, cRegUser, fRegDate, bActive)
        VALUES (@cNombreDepartamento, @cCorreoInstitucional, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Departamento SET
            cNombreDepartamento = @cNombreDepartamento,
            cCorreoInstitucional = @cCorreoInstitucional,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdDepartamento = @iIdDepartamento;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Departamento SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdDepartamento = @iIdDepartamento;
    END
END;
GO

-- Periodo
CREATE PROCEDURE sp_Periodo_Listar
    @iIdPeriodo INT = NULL
AS
BEGIN
    SELECT * FROM Periodo
    WHERE @iIdPeriodo IS NULL OR iIdPeriodo = @iIdPeriodo;
END;
GO

CREATE PROCEDURE sp_Periodo_Mantenimiento
    @OPERACION INT,
    @iIdPeriodo INT = NULL,
    @iAnioAcademico INT = NULL,
    @cNumeroPeriodo VARCHAR(10) = NULL,
    @cNombrePeriodo VARCHAR(100) = NULL,
    @fFechaInicio DATE = NULL,
    @fFechaCulminacion DATE = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Periodo (iAnioAcademico, cNumeroPeriodo, cNombrePeriodo, fFechaInicio, fFechaCulminacion, cRegUser, fRegDate, bActive)
        VALUES (@iAnioAcademico, @cNumeroPeriodo, @cNombrePeriodo, @fFechaInicio, @fFechaCulminacion, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Periodo SET
            iAnioAcademico = @iAnioAcademico,
            cNumeroPeriodo = @cNumeroPeriodo,
            cNombrePeriodo = @cNombrePeriodo,
            fFechaInicio = @fFechaInicio,
            fFechaCulminacion = @fFechaCulminacion,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdPeriodo = @iIdPeriodo;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Periodo SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdPeriodo = @iIdPeriodo;
    END
END;
GO

-- Componente
CREATE PROCEDURE sp_Componente_Listar
    @iIdComponente INT = NULL
AS
BEGIN
    SELECT * FROM Componente
    WHERE @iIdComponente IS NULL OR iIdComponente = @iIdComponente;
END;
GO

CREATE PROCEDURE sp_Componente_Mantenimiento
    @OPERACION INT,
    @iIdComponente INT = NULL,
    @cSiglas VARCHAR(10) = NULL,
    @cNombreComponente VARCHAR(100) = NULL,
    @cDescripcion VARCHAR(8000) = NULL,
    @bEliminarOpcionesEncuesta BIT = NULL,
    @bVisibleReportes BIT = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Componente (cSiglas, cNombreComponente, cDescripcion, bEliminarOpcionesEncuesta, bVisibleReportes, cRegUser, fRegDate, bActive)
        VALUES (@cSiglas, @cNombreComponente, @cDescripcion, @bEliminarOpcionesEncuesta, @bVisibleReportes, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Componente SET
            cSiglas = @cSiglas,
            cNombreComponente = @cNombreComponente,
            cDescripcion = @cDescripcion,
            bEliminarOpcionesEncuesta = @bEliminarOpcionesEncuesta,
            bVisibleReportes = @bVisibleReportes,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdComponente = @iIdComponente;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Componente SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdComponente = @iIdComponente;
    END
END;
GO

-- Asignatura
CREATE PROCEDURE sp_Asignatura_Listar
    @iIdAsignatura INT = NULL
AS
BEGIN
    SELECT * FROM Asignatura
    WHERE @iIdAsignatura IS NULL OR iIdAsignatura = @iIdAsignatura;
END;
GO

CREATE PROCEDURE sp_Asignatura_Mantenimiento
    @OPERACION INT,
    @iIdAsignatura INT = NULL,
    @iIdDepartamento INT = NULL,
    @iIdComponente INT = NULL,
    @cSiglas VARCHAR(10) = NULL,
    @cNombreAsignatura VARCHAR(256) = NULL,
    @iCiclo INT = NULL,
    @iAnio INT = NULL,
    @iCreditos INT = NULL,
    @bTieneCapitulos BIT = NULL,
    @iNumeroCapitulos INT = NULL,
    @bTieneSedeHospitalaria BIT = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Asignatura (
            iIdDepartamento, iIdComponente, cSiglas, cNombreAsignatura, iCiclo,
            iAnio, iCreditos, bTieneCapitulos, iNumeroCapitulos,
            bTieneSedeHospitalaria, cRegUser, fRegDate, bActive)
        VALUES (
            @iIdDepartamento, @iIdComponente, @cSiglas, @cNombreAsignatura, @iCiclo,
            @iAnio, @iCreditos, @bTieneCapitulos, @iNumeroCapitulos,
            @bTieneSedeHospitalaria, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Asignatura SET
            iIdDepartamento = @iIdDepartamento,
            iIdComponente = @iIdComponente,
            cSiglas = @cSiglas,
            cNombreAsignatura = @cNombreAsignatura,
            iCiclo = @iCiclo,
            iAnio = @iAnio,
            iCreditos = @iCreditos,
            bTieneCapitulos = @bTieneCapitulos,
            iNumeroCapitulos = @iNumeroCapitulos,
            bTieneSedeHospitalaria = @bTieneSedeHospitalaria,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdAsignatura = @iIdAsignatura;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Asignatura SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdAsignatura = @iIdAsignatura;
    END
END;
GO

-- Capitulo
CREATE PROCEDURE sp_Capitulo_Listar
    @iIdCapitulo INT = NULL
AS
BEGIN
    SELECT * FROM Capitulo
    WHERE @iIdCapitulo IS NULL OR iIdCapitulo = @iIdCapitulo;
END;
GO

CREATE PROCEDURE sp_Capitulo_Mantenimiento
    @OPERACION INT,
    @iIdCapitulo INT = NULL,
    @iIdAsignatura INT = NULL,
    @cNumeroCapitulo VARCHAR(10) = NULL,
    @cNombreCapitulo VARCHAR(128) = NULL,
    @cDescripcionCapitulo VARCHAR(8000) = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Capitulo (
            iIdAsignatura, cNumeroCapitulo, cNombreCapitulo, cDescripcionCapitulo,
            cRegUser, fRegDate, bActive)
        VALUES (
            @iIdAsignatura, @cNumeroCapitulo, @cNombreCapitulo, @cDescripcionCapitulo,
            @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Capitulo SET
            iIdAsignatura = @iIdAsignatura,
            cNumeroCapitulo = @cNumeroCapitulo,
            cNombreCapitulo = @cNombreCapitulo,
            cDescripcionCapitulo = @cDescripcionCapitulo,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdCapitulo = @iIdCapitulo;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Capitulo SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdCapitulo = @iIdCapitulo;
    END
END;
GO
-- =====================================================
-- 8. RESTRICCIONES ADICIONALES DE INTEGRIDAD
-- =====================================================

-- Restricción para validar que preguntas tipo Opción tengan al menos 2 opciones
-- (Se implementará a nivel de aplicación)

-- Restricción para validar que preguntas tipo Likert tengan opciones e instrucciones
-- (Se implementará a nivel de aplicación)

-- Restricción para validar fechas de encuesta válidas
CREATE TRIGGER TRG_Validar_Fecha_Encuesta
ON Encuesta
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1 FROM inserted 
        WHERE fFechaHoraInicio < CAST(GETDATE() AS DATE)
    )
    BEGIN
        RAISERROR('La fecha de inicio no puede ser menor que la fecha actual.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;


-- Restricción para validar que solo usuarios con @udep.edu.pe puedan autenticarse
-- (Se implementará a nivel de aplicación)

-- =====================================================
-- COMENTARIOS FINALES
-- =====================================================
-- Este modelo sigue las convenciones establecidas:
-- - Campos PK: iId[NombreTabla] como entero Identity
-- - Campos texto: c[NombreDescriptivo] 
-- - Campos fecha: f[NombreDescriptivo]
-- - Campos booleanos: b[NombreDescriptivo]
-- - Campos enteros: i[NombreDescriptivo]
-- - Nombres de tablas en singular con palabras separadas por guion bajo
-- - Máximo 20 caracteres por nombre
-- - FK claramente referenciadas
-- - Constraints de integridad implementados
