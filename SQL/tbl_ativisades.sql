CREATE TABLE TBL_ATIVIDADE (
    id_atividade INT IDENTITY(1,1) PRIMARY KEY,
    id_usuario INT NOT NULL,
    nome VARCHAR(50) NOT NULL,
    descricao VARCHAR(500) NOT NULL,
    data_cadastro DATETIME NOT NULL,
    data_termino DATETIME NULL,
    CONSTRAINT FK_TBL_atividade_Usuario FOREIGN KEY (id_usuario) REFERENCES TBL_USUARIO(id_usuario) ON DELETE CASCADE
);