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
-- Actividad
CREATE PROCEDURE sp_Actividad_Listar
    @iIdActividad INT = NULL
AS
BEGIN
    SELECT * FROM Actividad
    WHERE @iIdActividad IS NULL OR iIdActividad = @iIdActividad;
END;
GO

CREATE PROCEDURE sp_Actividad_Mantenimiento
    @OPERACION INT,
    @iIdActividad INT = NULL,
    @cNombreActividad VARCHAR(128) = NULL,
    @cDescripcionActividad VARCHAR(8000) = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Actividad (cNombreActividad, cDescripcionActividad, cRegUser, fRegDate, bActive)
        VALUES (@cNombreActividad, @cDescripcionActividad, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Actividad SET
            cNombreActividad = @cNombreActividad,
            cDescripcionActividad = @cDescripcionActividad,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdActividad = @iIdActividad;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Actividad SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdActividad = @iIdActividad;
    END
END;
GO

-- Asignacion_Actividad_Capitulo
CREATE PROCEDURE sp_Asignacion_Actividad_Capitulo_Listar
    @iIdAsignacion INT = NULL
AS
BEGIN
    SELECT * FROM Asignacion_Actividad_Capitulo
    WHERE @iIdAsignacion IS NULL OR iIdAsignacion = @iIdAsignacion;
END;
GO

CREATE PROCEDURE sp_Asignacion_Actividad_Capitulo_Mantenimiento
    @OPERACION INT,
    @iIdAsignacion INT = NULL,
    @iIdAsignatura INT = NULL,
    @iIdCapitulo INT = NULL,
    @iIdActividad INT = NULL,
    @cUsuarioAsignador VARCHAR(100) = NULL,
    @fFechaAsignacion DATETIME2 = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Asignacion_Actividad_Capitulo (
            iIdAsignatura, iIdCapitulo, iIdActividad,
            cUsuarioAsignador, fFechaAsignacion, cRegUser, fRegDate, bActive)
        VALUES (
            @iIdAsignatura, @iIdCapitulo, @iIdActividad,
            @cUsuarioAsignador, GETDATE(), @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Asignacion_Actividad_Capitulo SET
            iIdAsignatura = @iIdAsignatura,
            iIdCapitulo = @iIdCapitulo,
            iIdActividad = @iIdActividad,
            cUsuarioAsignador = @cUsuarioAsignador,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdAsignacion = @iIdAsignacion;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Asignacion_Actividad_Capitulo SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdAsignacion = @iIdAsignacion;
    END
END;
GO

-- Asignacion_Componente_Actividad
CREATE PROCEDURE sp_Asignacion_Componente_Actividad_Listar
    @iIdAsignacion INT = NULL
AS
BEGIN
    SELECT * FROM Asignacion_Componente_Actividad
    WHERE @iIdAsignacion IS NULL OR iIdAsignacion = @iIdAsignacion;
END;
GO

CREATE PROCEDURE sp_Asignacion_Componente_Actividad_Mantenimiento
    @OPERACION INT,
    @iIdAsignacion INT = NULL,
    @iIdActividad INT = NULL,
    @iIdComponente INT = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Asignacion_Componente_Actividad (
            iIdActividad, iIdComponente, cRegUser, fRegDate, bActive)
        VALUES (@iIdActividad, @iIdComponente, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Asignacion_Componente_Actividad SET
            iIdActividad = @iIdActividad,
            iIdComponente = @iIdComponente,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdAsignacion = @iIdAsignacion;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Asignacion_Componente_Actividad SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdAsignacion = @iIdAsignacion;
    END
END;
GO

-- Dimension
CREATE PROCEDURE sp_Dimension_Listar
    @iIdDimension INT = NULL
AS
BEGIN
    SELECT * FROM Dimension
    WHERE @iIdDimension IS NULL OR iIdDimension = @iIdDimension;
END;
GO

CREATE PROCEDURE sp_Dimension_Mantenimiento
    @OPERACION INT,
    @iIdDimension INT = NULL,
    @cNombreDimension VARCHAR(128) = NULL,
    @cDescripcion VARCHAR(8000) = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Dimension (cNombreDimension, cDescripcion, cRegUser, fRegDate, bActive)
        VALUES (@cNombreDimension, @cDescripcion, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Dimension SET
            cNombreDimension = @cNombreDimension,
            cDescripcion = @cDescripcion,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdDimension = @iIdDimension;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Dimension SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdDimension = @iIdDimension;
    END
END;
GO

-- Encuesta
CREATE PROCEDURE sp_Encuesta_Listar
    @iIdEncuesta INT = NULL
AS
BEGIN
    SELECT * FROM Encuesta
    WHERE @iIdEncuesta IS NULL OR iIdEncuesta = @iIdEncuesta;
END;
GO

CREATE PROCEDURE sp_Encuesta_Mantenimiento
    @OPERACION INT,
    @iIdEncuesta INT = NULL,
    @iIdOfertaAcademica INT = NULL,
    @cTitulo VARCHAR(2048) = NULL,
    @cInstrucciones VARCHAR(8000) = NULL,
    @fFechaHoraInicio DATETIME2 = NULL,
    @fFechaHoraFin DATETIME2 = NULL,
    @bEsAnonima BIT = NULL,
    @bActiva BIT = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Encuesta (
            iIdOfertaAcademica, cTitulo, cInstrucciones, fFechaHoraInicio,
            fFechaHoraFin, bEsAnonima, bActiva, cRegUser, fRegDate, bActive)
        VALUES (
            @iIdOfertaAcademica, @cTitulo, @cInstrucciones, @fFechaHoraInicio,
            @fFechaHoraFin, @bEsAnonima, @bActiva, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Encuesta SET
            iIdOfertaAcademica = @iIdOfertaAcademica,
            cTitulo = @cTitulo,
            cInstrucciones = @cInstrucciones,
            fFechaHoraInicio = @fFechaHoraInicio,
            fFechaHoraFin = @fFechaHoraFin,
            bEsAnonima = @bEsAnonima,
            bActiva = @bActiva,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdEncuesta = @iIdEncuesta;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Encuesta SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdEncuesta = @iIdEncuesta;
    END
END;
GO

-- Instruccion_Pregunta
CREATE PROCEDURE sp_Instruccion_Pregunta_Listar
    @iIdInstruccion INT = NULL
AS
BEGIN
    SELECT * FROM Instruccion_Pregunta
    WHERE @iIdInstruccion IS NULL OR iIdInstruccion = @iIdInstruccion;
END;
GO

CREATE PROCEDURE sp_Instruccion_Pregunta_Mantenimiento
    @OPERACION INT,
    @iIdInstruccion INT = NULL,
    @iIdPregunta INT = NULL,
    @cDescripcionInstruccion VARCHAR(8000) = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Instruccion_Pregunta (
            iIdPregunta, cDescripcionInstruccion, cRegUser, fRegDate, bActive)
        VALUES (@iIdPregunta, @cDescripcionInstruccion, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Instruccion_Pregunta SET
            iIdPregunta = @iIdPregunta,
            cDescripcionInstruccion = @cDescripcionInstruccion,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdInstruccion = @iIdInstruccion;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Instruccion_Pregunta SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdInstruccion = @iIdInstruccion;
    END
END;
GO

-- Instruccion_Pregunta_Encuesta
CREATE PROCEDURE sp_Instruccion_Pregunta_Encuesta_Listar
    @iIdInstruccionPreguntaEncuesta INT = NULL
AS
BEGIN
    SELECT * FROM Instruccion_Pregunta_Encuesta
    WHERE @iIdInstruccionPreguntaEncuesta IS NULL OR iIdInstruccionPreguntaEncuesta = @iIdInstruccionPreguntaEncuesta;
END;
GO

CREATE PROCEDURE sp_Instruccion_Pregunta_Encuesta_Mantenimiento
    @OPERACION INT,
    @iIdInstruccionPreguntaEncuesta INT = NULL,
    @iIdEncuesta INT = NULL,
    @iIdPreguntaEncuesta INT = NULL,
    @iIdPregunta INT = NULL,
    @iIdInstruccionPregunta INT = NULL,
    @cDescripcionInstruccion VARCHAR(8000) = NULL,
    @bActiva BIT = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Instruccion_Pregunta_Encuesta (
            iIdEncuesta, iIdPreguntaEncuesta, iIdPregunta, iIdInstruccionPregunta,
            cDescripcionInstruccion, bActiva, cRegUser, fRegDate, bActive)
        VALUES (
            @iIdEncuesta, @iIdPreguntaEncuesta, @iIdPregunta, @iIdInstruccionPregunta,
            @cDescripcionInstruccion, @bActiva, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Instruccion_Pregunta_Encuesta SET
            iIdEncuesta = @iIdEncuesta,
            iIdPreguntaEncuesta = @iIdPreguntaEncuesta,
            iIdPregunta = @iIdPregunta,
            iIdInstruccionPregunta = @iIdInstruccionPregunta,
            cDescripcionInstruccion = @cDescripcionInstruccion,
            bActiva = @bActiva,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdInstruccionPreguntaEncuesta = @iIdInstruccionPreguntaEncuesta;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Instruccion_Pregunta_Encuesta SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdInstruccionPreguntaEncuesta = @iIdInstruccionPreguntaEncuesta;
    END
END;
GO

-- Oferta_Academica
CREATE PROCEDURE sp_Oferta_Academica_Listar
    @iIdOfertaAcademica INT = NULL
AS
BEGIN
    SELECT * FROM Oferta_Academica
    WHERE @iIdOfertaAcademica IS NULL OR iIdOfertaAcademica = @iIdOfertaAcademica;
END;
GO

CREATE PROCEDURE sp_Oferta_Academica_Mantenimiento
    @OPERACION INT,
    @iIdOfertaAcademica INT = NULL,
    @iIdPeriodo INT = NULL,
    @iIdAsignatura INT = NULL,
    @iNumeroMatriculados INT = NULL,
    @cJefeCurso VARCHAR(100) = NULL,
    @iAprobados INT = NULL,
    @iDesaprobados INT = NULL,
    @iRetirados INT = NULL,
    @iAnulaciones INT = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Oferta_Academica (
            iIdPeriodo, iIdAsignatura, iNumeroMatriculados, cJefeCurso,
            iAprobados, iDesaprobados, iRetirados, iAnulaciones,
            cRegUser, fRegDate, bActive)
        VALUES (
            @iIdPeriodo, @iIdAsignatura, @iNumeroMatriculados, @cJefeCurso,
            @iAprobados, @iDesaprobados, @iRetirados, @iAnulaciones,
            @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Oferta_Academica SET
            iIdPeriodo = @iIdPeriodo,
            iIdAsignatura = @iIdAsignatura,
            iNumeroMatriculados = @iNumeroMatriculados,
            cJefeCurso = @cJefeCurso,
            iAprobados = @iAprobados,
            iDesaprobados = @iDesaprobados,
            iRetirados = @iRetirados,
            iAnulaciones = @iAnulaciones,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdOfertaAcademica = @iIdOfertaAcademica;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Oferta_Academica SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdOfertaAcademica = @iIdOfertaAcademica;
    END
END;
GO

-- Opcion_Pregunta
CREATE PROCEDURE sp_Opcion_Pregunta_Listar
    @iIdOpcion INT = NULL
AS
BEGIN
    SELECT * FROM Opcion_Pregunta
    WHERE @iIdOpcion IS NULL OR iIdOpcion = @iIdOpcion;
END;
GO

CREATE PROCEDURE sp_Opcion_Pregunta_Mantenimiento
    @OPERACION INT,
    @iIdOpcion INT = NULL,
    @iIdPregunta INT = NULL,
    @cDescripcionOpcion VARCHAR(8000) = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Opcion_Pregunta (iIdPregunta, cDescripcionOpcion, cRegUser, fRegDate, bActive)
        VALUES (@iIdPregunta, @cDescripcionOpcion, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Opcion_Pregunta SET
            iIdPregunta = @iIdPregunta,
            cDescripcionOpcion = @cDescripcionOpcion,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdOpcion = @iIdOpcion;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Opcion_Pregunta SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdOpcion = @iIdOpcion;
    END
END;
GO

-- Opcion_Pregunta_Encuesta
CREATE PROCEDURE sp_Opcion_Pregunta_Encuesta_Listar
    @iIdOpcionPreguntaEncuesta INT = NULL
AS
BEGIN
    SELECT * FROM Opcion_Pregunta_Encuesta
    WHERE @iIdOpcionPreguntaEncuesta IS NULL OR iIdOpcionPreguntaEncuesta = @iIdOpcionPreguntaEncuesta;
END;
GO

CREATE PROCEDURE sp_Opcion_Pregunta_Encuesta_Mantenimiento
    @OPERACION INT,
    @iIdOpcionPreguntaEncuesta INT = NULL,
    @iIdEncuesta INT = NULL,
    @iIdPreguntaEncuesta INT = NULL,
    @iIdPregunta INT = NULL,
    @iIdOpcionPregunta INT = NULL,
    @cDescripcionOpcion VARCHAR(8000) = NULL,
    @bActiva BIT = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Opcion_Pregunta_Encuesta (
            iIdEncuesta, iIdPreguntaEncuesta, iIdPregunta, iIdOpcionPregunta,
            cDescripcionOpcion, bActiva, cRegUser, fRegDate, bActive)
        VALUES (
            @iIdEncuesta, @iIdPreguntaEncuesta, @iIdPregunta, @iIdOpcionPregunta,
            @cDescripcionOpcion, @bActiva, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Opcion_Pregunta_Encuesta SET
            iIdEncuesta = @iIdEncuesta,
            iIdPreguntaEncuesta = @iIdPreguntaEncuesta,
            iIdPregunta = @iIdPregunta,
            iIdOpcionPregunta = @iIdOpcionPregunta,
            cDescripcionOpcion = @cDescripcionOpcion,
            bActiva = @bActiva,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdOpcionPreguntaEncuesta = @iIdOpcionPreguntaEncuesta;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Opcion_Pregunta_Encuesta SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdOpcionPreguntaEncuesta = @iIdOpcionPreguntaEncuesta;
    END
END;
GO

-- Pregunta
CREATE PROCEDURE sp_Pregunta_Listar
    @iIdPregunta INT = NULL
AS
BEGIN
    SELECT * FROM Pregunta
    WHERE @iIdPregunta IS NULL OR iIdPregunta = @iIdPregunta;
END;
GO

CREATE PROCEDURE sp_Pregunta_Mantenimiento
    @OPERACION INT,
    @iIdPregunta INT = NULL,
    @iIdComponente INT = NULL,
    @cTipoPregunta CHAR(1) = NULL,
    @cDescripcion VARCHAR(8000) = NULL,
    @bVariasRespuestas BIT = NULL,
    @bRespuestaLarga BIT = NULL,
    @iNivelCalificacion INT = NULL,
    @bObligatoria BIT = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Pregunta (
            iIdComponente, cTipoPregunta, cDescripcion, bVariasRespuestas,
            bRespuestaLarga, iNivelCalificacion, bObligatoria,
            cRegUser, fRegDate, bActive)
        VALUES (
            @iIdComponente, @cTipoPregunta, @cDescripcion, @bVariasRespuestas,
            @bRespuestaLarga, @iNivelCalificacion, @bObligatoria,
            @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Pregunta SET
            iIdComponente = @iIdComponente,
            cTipoPregunta = @cTipoPregunta,
            cDescripcion = @cDescripcion,
            bVariasRespuestas = @bVariasRespuestas,
            bRespuestaLarga = @bRespuestaLarga,
            iNivelCalificacion = @iNivelCalificacion,
            bObligatoria = @bObligatoria,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdPregunta = @iIdPregunta;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Pregunta SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdPregunta = @iIdPregunta;
    END
END;
GO

-- Pregunta_Encuesta
CREATE PROCEDURE sp_Pregunta_Encuesta_Listar
    @iIdPreguntaEncuesta INT = NULL
AS
BEGIN
    SELECT * FROM Pregunta_Encuesta
    WHERE @iIdPreguntaEncuesta IS NULL OR iIdPreguntaEncuesta = @iIdPreguntaEncuesta;
END;
GO

CREATE PROCEDURE sp_Pregunta_Encuesta_Mantenimiento
    @OPERACION INT,
    @iIdPreguntaEncuesta INT = NULL,
    @iIdEncuesta INT = NULL,
    @iIdPregunta INT = NULL,
    @cTipoPregunta CHAR(1) = NULL,
    @cDescripcion VARCHAR(8000) = NULL,
    @bVariasRespuestas BIT = NULL,
    @bRespuestaLarga BIT = NULL,
    @iNivelCalificacion INT = NULL,
    @bObligatoria BIT = NULL,
    @iOrden INT = NULL,
    @bActiva BIT = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Pregunta_Encuesta (
            iIdEncuesta, iIdPregunta, cTipoPregunta, cDescripcion,
            bVariasRespuestas, bRespuestaLarga, iNivelCalificacion,
            bObligatoria, iOrden, bActiva, cRegUser, fRegDate, bActive)
        VALUES (
            @iIdEncuesta, @iIdPregunta, @cTipoPregunta, @cDescripcion,
            @bVariasRespuestas, @bRespuestaLarga, @iNivelCalificacion,
            @bObligatoria, @iOrden, @bActiva, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Pregunta_Encuesta SET
            iIdEncuesta = @iIdEncuesta,
            iIdPregunta = @iIdPregunta,
            cTipoPregunta = @cTipoPregunta,
            cDescripcion = @cDescripcion,
            bVariasRespuestas = @bVariasRespuestas,
            bRespuestaLarga = @bRespuestaLarga,
            iNivelCalificacion = @iNivelCalificacion,
            bObligatoria = @bObligatoria,
            iOrden = @iOrden,
            bActiva = @bActiva,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdPreguntaEncuesta = @iIdPreguntaEncuesta;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Pregunta_Encuesta SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdPreguntaEncuesta = @iIdPreguntaEncuesta;
    END
END;
GO

-- Respuesta_Calificacion
CREATE PROCEDURE sp_Respuesta_Calificacion_Listar
    @iIdRespuesta INT = NULL
AS
BEGIN
    SELECT * FROM Respuesta_Calificacion
    WHERE @iIdRespuesta IS NULL OR iIdRespuesta = @iIdRespuesta;
END;
GO

CREATE PROCEDURE sp_Respuesta_Calificacion_Mantenimiento
    @OPERACION INT,
    @iIdRespuesta INT = NULL,
    @iIdSesion INT = NULL,
    @iIdPreguntaEncuesta INT = NULL,
    @iValorCalificacion INT = NULL,
    @cUsuarioRespuesta VARCHAR(100) = NULL,
    @fFechaRespuesta DATETIME2 = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Respuesta_Calificacion (
            iIdSesion, iIdPreguntaEncuesta, iValorCalificacion,
            cUsuarioRespuesta, fFechaRespuesta, cRegUser, fRegDate, bActive)
        VALUES (
            @iIdSesion, @iIdPreguntaEncuesta, @iValorCalificacion,
            @cUsuarioRespuesta, GETDATE(), @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Respuesta_Calificacion SET
            iIdSesion = @iIdSesion,
            iIdPreguntaEncuesta = @iIdPreguntaEncuesta,
            iValorCalificacion = @iValorCalificacion,
            cUsuarioRespuesta = @cUsuarioRespuesta,
            fFechaRespuesta = @fFechaRespuesta,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdRespuesta = @iIdRespuesta;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Respuesta_Calificacion SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdRespuesta = @iIdRespuesta;
    END
END;
GO

-- Respuesta_Likert
CREATE PROCEDURE sp_Respuesta_Likert_Listar
    @iIdRespuesta INT = NULL
AS
BEGIN
    SELECT * FROM Respuesta_Likert
    WHERE @iIdRespuesta IS NULL OR iIdRespuesta = @iIdRespuesta;
END;
GO

CREATE PROCEDURE sp_Respuesta_Likert_Mantenimiento
    @OPERACION INT,
    @iIdRespuesta INT = NULL,
    @iIdSesion INT = NULL,
    @iIdPreguntaEncuesta INT = NULL,
    @iIdOpcionSeleccionada INT = NULL,
    @iIdInstruccion INT = NULL,
    @cUsuarioRespuesta VARCHAR(100) = NULL,
    @fFechaRespuesta DATETIME2 = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Respuesta_Likert (
            iIdSesion, iIdPreguntaEncuesta, iIdOpcionSeleccionada, iIdInstruccion,
            cUsuarioRespuesta, fFechaRespuesta, cRegUser, fRegDate, bActive)
        VALUES (
            @iIdSesion, @iIdPreguntaEncuesta, @iIdOpcionSeleccionada, @iIdInstruccion,
            @cUsuarioRespuesta, GETDATE(), @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Respuesta_Likert SET
            iIdSesion = @iIdSesion,
            iIdPreguntaEncuesta = @iIdPreguntaEncuesta,
            iIdOpcionSeleccionada = @iIdOpcionSeleccionada,
            iIdInstruccion = @iIdInstruccion,
            cUsuarioRespuesta = @cUsuarioRespuesta,
            fFechaRespuesta = @fFechaRespuesta,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdRespuesta = @iIdRespuesta;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Respuesta_Likert SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdRespuesta = @iIdRespuesta;
    END
END;
GO

-- Respuesta_Opcion
CREATE PROCEDURE sp_Respuesta_Opcion_Listar
    @iIdRespuesta INT = NULL
AS
BEGIN
    SELECT * FROM Respuesta_Opcion
    WHERE @iIdRespuesta IS NULL OR iIdRespuesta = @iIdRespuesta;
END;
GO

CREATE PROCEDURE sp_Respuesta_Opcion_Mantenimiento
    @OPERACION INT,
    @iIdRespuesta INT = NULL,
    @iIdSesion INT = NULL,
    @iIdPreguntaEncuesta INT = NULL,
    @iIdOpcionSeleccionada INT = NULL,
    @cUsuarioRespuesta VARCHAR(100) = NULL,
    @fFechaRespuesta DATETIME2 = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Respuesta_Opcion (
            iIdSesion, iIdPreguntaEncuesta, iIdOpcionSeleccionada,
            cUsuarioRespuesta, fFechaRespuesta, cRegUser, fRegDate, bActive)
        VALUES (
            @iIdSesion, @iIdPreguntaEncuesta, @iIdOpcionSeleccionada,
            @cUsuarioRespuesta, GETDATE(), @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Respuesta_Opcion SET
            iIdSesion = @iIdSesion,
            iIdPreguntaEncuesta = @iIdPreguntaEncuesta,
            iIdOpcionSeleccionada = @iIdOpcionSeleccionada,
            cUsuarioRespuesta = @cUsuarioRespuesta,
            fFechaRespuesta = @fFechaRespuesta,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdRespuesta = @iIdRespuesta;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Respuesta_Opcion SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdRespuesta = @iIdRespuesta;
    END
END;
GO

-- Respuesta_Texto
CREATE PROCEDURE sp_Respuesta_Texto_Listar
    @iIdRespuesta INT = NULL
AS
BEGIN
    SELECT * FROM Respuesta_Texto
    WHERE @iIdRespuesta IS NULL OR iIdRespuesta = @iIdRespuesta;
END;
GO

CREATE PROCEDURE sp_Respuesta_Texto_Mantenimiento
    @OPERACION INT,
    @iIdRespuesta INT = NULL,
    @iIdSesion INT = NULL,
    @iIdPreguntaEncuesta INT = NULL,
    @cTextoRespuesta VARCHAR(8000) = NULL,
    @cUsuarioRespuesta VARCHAR(100) = NULL,
    @fFechaRespuesta DATETIME2 = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Respuesta_Texto (
            iIdSesion, iIdPreguntaEncuesta, cTextoRespuesta,
            cUsuarioRespuesta, fFechaRespuesta, cRegUser, fRegDate, bActive)
        VALUES (
            @iIdSesion, @iIdPreguntaEncuesta, @cTextoRespuesta,
            @cUsuarioRespuesta, GETDATE(), @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Respuesta_Texto SET
            iIdSesion = @iIdSesion,
            iIdPreguntaEncuesta = @iIdPreguntaEncuesta,
            cTextoRespuesta = @cTextoRespuesta,
            cUsuarioRespuesta = @cUsuarioRespuesta,
            fFechaRespuesta = @fFechaRespuesta,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdRespuesta = @iIdRespuesta;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Respuesta_Texto SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdRespuesta = @iIdRespuesta;
    END
END;
GO

-- Sesion_Encuesta
CREATE PROCEDURE sp_Sesion_Encuesta_Listar
    @iIdSesion INT = NULL
AS
BEGIN
    SELECT * FROM Sesion_Encuesta
    WHERE @iIdSesion IS NULL OR iIdSesion = @iIdSesion;
END;
GO

CREATE PROCEDURE sp_Sesion_Encuesta_Mantenimiento
    @OPERACION INT,
    @iIdSesion INT = NULL,
    @iIdEncuesta INT = NULL,
    @cUsuarioRespuesta VARCHAR(100) = NULL,
    @fFechaInicio DATETIME2 = NULL,
    @fFechaCompletado DATETIME2 = NULL,
    @cEstado VARCHAR(20) = NULL,
    @cIPAddress VARCHAR(45) = NULL,
    @cCodigoConfirmacion VARCHAR(50) = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Sesion_Encuesta (
            iIdEncuesta, cUsuarioRespuesta, fFechaInicio, fFechaCompletado,
            cEstado, cIPAddress, cCodigoConfirmacion, cRegUser, fRegDate, bActive)
        VALUES (
            @iIdEncuesta, @cUsuarioRespuesta, GETDATE(), @fFechaCompletado,
            @cEstado, @cIPAddress, @cCodigoConfirmacion, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Sesion_Encuesta SET
            iIdEncuesta = @iIdEncuesta,
            cUsuarioRespuesta = @cUsuarioRespuesta,
            fFechaInicio = @fFechaInicio,
            fFechaCompletado = @fFechaCompletado,
            cEstado = @cEstado,
            cIPAddress = @cIPAddress,
            cCodigoConfirmacion = @cCodigoConfirmacion,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdSesion = @iIdSesion;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Sesion_Encuesta SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdSesion = @iIdSesion;
    END
END;
GO

-- =====================================================
-- 8. RESTRICCIONES ADICIONALES DE INTEGRIDAD
-- =====================================================

-- Restricción para validar que preguntas tipo Opción tengan al menos 2 opciones
-- (Se implementará a nivel de aplicación)

-- =====================================================
-- 9. SEGURIDAD Y MENÚ
-- =====================================================

-- Tabla: Rol
CREATE TABLE Rol (
    iIdRol INT IDENTITY(1,1) PRIMARY KEY,
    cNombreRol VARCHAR(100) NOT NULL,
    cDescripcion VARCHAR(256) NULL,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1
);

-- Tabla: Usuario
CREATE TABLE Usuario (
    iIdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    cUsuario VARCHAR(100) NOT NULL UNIQUE,
    cPasswordHash VARCHAR(256) NOT NULL,
    cCorreo VARCHAR(256) NULL,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1
);

-- Tabla: Usuario_Rol
CREATE TABLE Usuario_Rol (
    iIdUsuarioRol INT IDENTITY(1,1) PRIMARY KEY,
    iIdUsuario INT NOT NULL,
    iIdRol INT NOT NULL,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_UsuarioRol_Usuario FOREIGN KEY (iIdUsuario) REFERENCES Usuario(iIdUsuario),
    CONSTRAINT FK_UsuarioRol_Rol FOREIGN KEY (iIdRol) REFERENCES Rol(iIdRol)
);

-- Tabla: Menu
CREATE TABLE Menu (
    iIdMenu INT IDENTITY(1,1) PRIMARY KEY,
    cNombre VARCHAR(100) NOT NULL,
    cUrl VARCHAR(256) NULL,
    iNivel INT NOT NULL,
    iIdPadre INT NULL,
    iOrden INT NOT NULL,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Menu_Padre FOREIGN KEY (iIdPadre) REFERENCES Menu(iIdMenu)
);

-- Tabla: Rol_Menu
CREATE TABLE Rol_Menu (
    iIdRolMenu INT IDENTITY(1,1) PRIMARY KEY,
    iIdRol INT NOT NULL,
    iIdMenu INT NOT NULL,
    cRegUser VARCHAR(100) NOT NULL,
    fRegDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    cUpdUser VARCHAR(100) NOT NULL,
    fUpdDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    bActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_RolMenu_Rol FOREIGN KEY (iIdRol) REFERENCES Rol(iIdRol),
    CONSTRAINT FK_RolMenu_Menu FOREIGN KEY (iIdMenu) REFERENCES Menu(iIdMenu)
);

-- Procedimientos almacenados

CREATE PROCEDURE sp_Rol_Listar
    @iIdRol INT = NULL
AS
BEGIN
    SELECT * FROM Rol
    WHERE @iIdRol IS NULL OR iIdRol = @iIdRol;
END;
GO

CREATE PROCEDURE sp_Rol_Mantenimiento
    @OPERACION INT,
    @iIdRol INT = NULL,
    @cNombreRol VARCHAR(100) = NULL,
    @cDescripcion VARCHAR(256) = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Rol (cNombreRol, cDescripcion, cRegUser, fRegDate, bActive)
        VALUES (@cNombreRol, @cDescripcion, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Rol SET
            cNombreRol = @cNombreRol,
            cDescripcion = @cDescripcion,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdRol = @iIdRol;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Rol SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdRol = @iIdRol;
    END
END;
GO

CREATE PROCEDURE sp_Usuario_Listar
    @iIdUsuario INT = NULL
AS
BEGIN
    SELECT * FROM Usuario
    WHERE @iIdUsuario IS NULL OR iIdUsuario = @iIdUsuario;
END;
GO

CREATE PROCEDURE sp_Usuario_Mantenimiento
    @OPERACION INT,
    @iIdUsuario INT = NULL,
    @cUsuario VARCHAR(100) = NULL,
    @cPasswordHash VARCHAR(256) = NULL,
    @cCorreo VARCHAR(256) = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Usuario (cUsuario, cPasswordHash, cCorreo, cRegUser, fRegDate, bActive)
        VALUES (@cUsuario, @cPasswordHash, @cCorreo, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Usuario SET
            cUsuario = @cUsuario,
            cPasswordHash = @cPasswordHash,
            cCorreo = @cCorreo,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdUsuario = @iIdUsuario;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Usuario SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdUsuario = @iIdUsuario;
    END
END;
GO

CREATE PROCEDURE sp_Usuario_Rol_Listar
    @iIdUsuarioRol INT = NULL
AS
BEGIN
    SELECT * FROM Usuario_Rol
    WHERE @iIdUsuarioRol IS NULL OR iIdUsuarioRol = @iIdUsuarioRol;
END;
GO

CREATE PROCEDURE sp_Usuario_Rol_Mantenimiento
    @OPERACION INT,
    @iIdUsuarioRol INT = NULL,
    @iIdUsuario INT = NULL,
    @iIdRol INT = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Usuario_Rol (iIdUsuario, iIdRol, cRegUser, fRegDate, bActive)
        VALUES (@iIdUsuario, @iIdRol, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Usuario_Rol SET
            iIdUsuario = @iIdUsuario,
            iIdRol = @iIdRol,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdUsuarioRol = @iIdUsuarioRol;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Usuario_Rol SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdUsuarioRol = @iIdUsuarioRol;
    END
END;
GO

CREATE PROCEDURE sp_Menu_Listar
    @iIdMenu INT = NULL
AS
BEGIN
    SELECT * FROM Menu
    WHERE @iIdMenu IS NULL OR iIdMenu = @iIdMenu;
END;
GO

CREATE PROCEDURE sp_Menu_Mantenimiento
    @OPERACION INT,
    @iIdMenu INT = NULL,
    @cNombre VARCHAR(100) = NULL,
    @cUrl VARCHAR(256) = NULL,
    @iNivel INT = NULL,
    @iIdPadre INT = NULL,
    @iOrden INT = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Menu (cNombre, cUrl, iNivel, iIdPadre, iOrden, cRegUser, fRegDate, bActive)
        VALUES (@cNombre, @cUrl, @iNivel, @iIdPadre, @iOrden, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Menu SET
            cNombre = @cNombre,
            cUrl = @cUrl,
            iNivel = @iNivel,
            iIdPadre = @iIdPadre,
            iOrden = @iOrden,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdMenu = @iIdMenu;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Menu SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdMenu = @iIdMenu;
    END
END;
GO

CREATE PROCEDURE sp_Rol_Menu_Listar
    @iIdRolMenu INT = NULL
AS
BEGIN
    SELECT * FROM Rol_Menu
    WHERE @iIdRolMenu IS NULL OR iIdRolMenu = @iIdRolMenu;
END;
GO

CREATE PROCEDURE sp_Rol_Menu_Mantenimiento
    @OPERACION INT,
    @iIdRolMenu INT = NULL,
    @iIdRol INT = NULL,
    @iIdMenu INT = NULL,
    @cRegUser VARCHAR(100) = NULL,
    @cUpdUser VARCHAR(100) = NULL
AS
BEGIN
    IF @OPERACION = 1
    BEGIN
        INSERT INTO Rol_Menu (iIdRol, iIdMenu, cRegUser, fRegDate, bActive)
        VALUES (@iIdRol, @iIdMenu, @cRegUser, GETDATE(), 1);
    END
    ELSE IF @OPERACION = 2
    BEGIN
        UPDATE Rol_Menu SET
            iIdRol = @iIdRol,
            iIdMenu = @iIdMenu,
            cUpdUser = @cUpdUser,
            fUpdDate = GETDATE()
        WHERE iIdRolMenu = @iIdRolMenu;
    END
    ELSE IF @OPERACION = 3
    BEGIN
        UPDATE Rol_Menu SET bActive = 0, cUpdUser = @cUpdUser, fUpdDate = GETDATE()
        WHERE iIdRolMenu = @iIdRolMenu;
    END
END;
GO

CREATE PROCEDURE sp_MenuPorRol_Listar
    @iIdRol INT
AS
BEGIN
    SELECT M.* FROM Menu M
    INNER JOIN Rol_Menu RM ON M.iIdMenu = RM.iIdMenu
    WHERE RM.iIdRol = @iIdRol AND M.bActive = 1 AND RM.bActive = 1
    ORDER BY M.iNivel, M.iOrden;
END;
GO

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
