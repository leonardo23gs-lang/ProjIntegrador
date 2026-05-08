-- Criação e seleção do banco
CREATE DATABASE SistemaAlocacaoLab;
USE SistemaAlocacaoLab;

-- PERFIL
CREATE TABLE Perfil (
    id_perfil INT NOT NULL IDENTITY(1,1),
    tipo_perfil VARCHAR(50) NOT NULL,

    CONSTRAINT PK_Perfil PRIMARY KEY (id_perfil)
);

-- USUARIO
CREATE TABLE Usuario (
    id_usuario INT NOT NULL IDENTITY(1,1),
    nome_usuario VARCHAR(100) NOT NULL,
    email_usuario VARCHAR(100) NOT NULL,
    senha_usuario VARCHAR(255) NOT NULL,
    id_perfil INT NOT NULL,

    CONSTRAINT PK_Usuario PRIMARY KEY (id_usuario),
    CONSTRAINT UQ_Email UNIQUE (email_usuario),
    CONSTRAINT FK_Usuario_Perfil FOREIGN KEY (id_perfil) REFERENCES Perfil(id_perfil)
);

-- SOFTWARE
CREATE TABLE Software (
    id_software INT NOT NULL IDENTITY(1,1),
    nome_software VARCHAR(100) NOT NULL,
    versao_software VARCHAR(50)  NOT NULL,

    CONSTRAINT PK_Software PRIMARY KEY (id_software)
);

-- DISCIPLINA
CREATE TABLE Disciplina (
    id_disciplina INT NOT NULL IDENTITY(1,1),
    nome_disciplina VARCHAR(100) NOT NULL,
    id_coordenador INT NOT NULL,

    CONSTRAINT PK_Disciplina PRIMARY KEY (id_disciplina),
    CONSTRAINT FK_Disciplina_Coordenador FOREIGN KEY (id_coordenador) REFERENCES Usuario(id_usuario)
);

-- DISCIPLINA_SOFTWARE
CREATE TABLE Disciplina_Software (
    id_disciplina INT NOT NULL,
    id_software INT NOT NULL,

    CONSTRAINT PK_Disciplina_Software PRIMARY KEY (id_disciplina, id_software),
    CONSTRAINT FK_DS_Disciplina FOREIGN KEY (id_disciplina) REFERENCES Disciplina(id_disciplina),
    CONSTRAINT FK_DS_Software FOREIGN KEY (id_software) REFERENCES Software(id_software)
);

-- LABORATORIO
CREATE TABLE Laboratorio (
    id_laboratorio INT NOT NULL IDENTITY(1,1),
    nome_laboratorio VARCHAR(100) NOT NULL,
    qtd_computadores INT NOT NULL CHECK (qtd_computadores > 0),

    CONSTRAINT PK_Laboratorio PRIMARY KEY (id_laboratorio)
);

-- LABORATORIO_SOFTWARE (tabela associativa N:N)
CREATE TABLE Laboratorio_Software (
    id_laboratorio INT NOT NULL,
    id_software INT NOT NULL,

    CONSTRAINT PK_Laboratorio_Software PRIMARY KEY (id_laboratorio, id_software),
    CONSTRAINT FK_LS_Laboratorio FOREIGN KEY (id_laboratorio) REFERENCES Laboratorio(id_laboratorio),
    CONSTRAINT FK_LS_Software FOREIGN KEY (id_software) REFERENCES Software(id_software)
);

-- TURMAS
CREATE TABLE Turmas (
    id_turma INT NOT NULL IDENTITY(1,1),
    quantidade_alunos INT NOT NULL CHECK (quantidade_alunos > 0),
    horario_inicio TIME NOT NULL,
    horario_fim TIME NOT NULL,
    id_disciplina INT NOT NULL,

    CONSTRAINT PK_Turmas PRIMARY KEY (id_turma),
    CONSTRAINT CHK_Horario CHECK (horario_fim > horario_inicio),
    CONSTRAINT FK_Turmas_Disciplina FOREIGN KEY (id_disciplina) REFERENCES Disciplina(id_disciplina)
);

-- ALOCACAO
CREATE TABLE Alocacao (
    id_alocacao INT NOT NULL IDENTITY(1,1),
    status VARCHAR(50) NOT NULL DEFAULT 'Pendente'
        CHECK (status IN ('Pendente', 'Aprovada', 'Rejeitada', 'Cancelada')),
    id_turma INT NOT NULL,
    id_laboratorio INT NOT NULL,
    id_coordenador INT NOT NULL,

    CONSTRAINT PK_Alocacao PRIMARY KEY (id_alocacao),
    CONSTRAINT UQ_Turma UNIQUE(id_turma),
    CONSTRAINT FK_Alocacao_Turma
        FOREIGN KEY (id_turma) REFERENCES Turmas(id_turma),
    CONSTRAINT FK_Alocacao_Laboratorio
        FOREIGN KEY (id_laboratorio) REFERENCES Laboratorio(id_laboratorio),
    CONSTRAINT FK_Alocacao_Coordenador
        FOREIGN KEY (id_coordenador) REFERENCES Usuario(id_usuario)
);

ALTER TABLE Laboratorio
ADD codigo_sala VARCHAR(50),
    bloco_localizacao VARCHAR(100),
    responsavel_ti VARCHAR(100),
    cap_max_alunos INT,
    status_operacional VARCHAR(20) DEFAULT 'Disponível',
    observacoes TEXT;

    SELECT * FROM Laboratorio