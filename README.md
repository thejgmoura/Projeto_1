## Introdução

Este repositório contém um exemplo de aplicação com CRUD (Create, Read, Update, Delete) utilizando MySQL e C#. O projeto permite realizar operações com nome, telefone e e-mail em uma tabela, com uma interface gráfica simples utilizando o Windows Forms. Segue no repositório um vídeo mostrando como o sistema funciona, tendo em vista que o maior objetivo deste é apenas mostrar o código.

## Funcionalidades

- Criar (Create): Inserir novos registros de nome, telefone e e-mail na tabela.
- Ler (Read): Visualizar registros existentes na tabela, com suporte para filtro de pesquisa por nome.
- Atualizar (Update): Modificar registros existentes com novos valores para nome, telefone e e-mail.
- Excluir (Delete): Remover registros existentes.

## Banco de dados

Antes de executar, você precisará criar o banco de dados e uma tabela no MySQL onde os dados serão armazenados. Siga os passos abaixo para configurar corretamente:

- Faça o download e instale o MySQL Community Server.
- Abra o MySQL Workbench ou um terminal para se conectar.
- Você pode usar o seguinte comando SQL:

```sql
CREATE DATABASE db_cadastros;

CREATE TABLE new_table (
	id INT AUTO_INCREMENT PRIMARY KEY,
	nome VARCHAR (100) NOT NULL,
	telefone VARCHAR (15),
	email VARCHAR (100)
);
```

## Conclusão

Obrigado por acompanhar este projeto! Se houver qualquer dúvida ou sugestão, abra uma issue ou entre em contato.
