CREATE DATABASE bd_teste;
USE bd_teste;

CREATE TABLE table_teste(
id INT NOT NULL AUTO_INCREMENT, 
nome VARCHAR(45), endereco VARCHAR(45),
salario DECIMAL(7,2),
CONSTRAINT id_pk PRIMARY KEY (id));

INSERT INTO table_teste(nome,endereco,salario) VALUES 
('Ana','Rua dos Gatos',7555.35),
('Rhuan','Rua das Praias',5360.20),
('Julio','Rua dos Predios',4300.90);

SELECT * FROM table_teste;

DROP DATABASE bd_teste;
