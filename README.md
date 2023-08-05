Esse arquivo possui os passos para rodar a aplicação localmente.

1 - rode o seguinte comando no terminal para criar um container postgres.
docker run -p 5432:5432 -e POSTGRES_PASSWORD=@OfferAdmin -v postgres-data:/var/lib/postgresql/data postgres

2 - como alternativa ao uso do docker, pode ser usado o pgadmin para criar o banco de dados.

3 - Execute os script abaixo no pgadmin ou outro client postgres conectado em (localhost:5432, user: postgres, senha:@OfferAdmin)

CREATE DATABASE offer_db
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'en_US.utf8'
    LC_CTYPE = 'en_US.utf8'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

CREATE TABLE user_account (
id BIGSERIAL primary key not null,
name varchar not null,
cpf varchar(11) not null,
email varchar(50) not null);

insert into user_account(name, cpf, email)
values( 'João Silva', '01901298237', 'joao@hotmail.com');
insert into user_account(name, cpf, email)
values( 'Maria Santana Carsozo', '23465178654', 'mariasantana@hotmail.com');

CREATE TABLE coin (
  id BIGSERIAL PRIMARY KEY,
  token UUID NOT NULL UNIQUE,
  saldo DECIMAL(10,2) NOT NULL,
  type varchar(30) NOT NULL);

INSERT INTO coin (token, saldo, type) VALUES('12345678-90ab-cdef-1234-567890abcdef', 100.00, 'bitcoin');
INSERT INTO coin (token, saldo, type) VALUES('20D75A59-D5FD-4ADB-8B5A-BC19EE8014AF', 200.00, 'ethereum');
INSERT INTO coin (token, saldo, type) VALUES('30670B63-9350-4397-A7FC-507982BD6D71', 300.00, 'litecoin');
INSERT INTO coin (token, saldo, type) VALUES('43E6A4C2-FE85-4720-8545-D6C8CA8E4633', 400.00, 'ripple');
INSERT INTO coin (token, saldo, type) VALUES('D873AE2B-AB9A-45E2-9DD9-457E38FA0A41', 500.00, 'dogecoin');

CREATE TABLE wallet (
  id BIGSERIAL PRIMARY KEY,
  coin_id bigint NOT NULL unique,
  foreign key(coin_id)references coin(id),
  user_id bigint NOT NULL,
  foreign key(user_id)references user_account(id));

insert into wallet(coin_id, user_id)values(1, 1);
insert into wallet(coin_id, user_id)values(2, 1);

CREATE TABLE offer (
  id BIGSERIAL PRIMARY KEY,
  unit_price DECIMAL(10,2) NOT NULL,
  quantity INT NOT NULL,
  date_creation timestamp with time zone NOT NULL,
  user_account_id bigint NOT NULL,
  foreign key(user_account_id)references user_account(id),
  wallet_id bigint NOT NULL,
  foreign key(wallet_id)references wallet(id),
  is_deleted BOOLEAN NOT NULL DEFAULT false);

INSERT INTO offer (unit_price, quantity, date_creation, user_account_id, wallet_id) VALUES
(100.00, 1, now(), 1, 1);
INSERT INTO offer (unit_price, quantity, date_creation, user_account_id, wallet_id) VALUES
(50.00, 3, now(), 1, 1);
INSERT INTO offer (unit_price, quantity, date_creation, user_account_id, wallet_id) VALUES
(50.00, 3, now(), 2, 1);
--------------------------------------------------------------------------------------------------------------

4 - abra o arquivo Eclipseworks.API.sln no vscode, visual studio ou rider e aguarde a restauração do projeto e depois execute o projeto.




5 - melhorias
Como melhorias poderiamos rever a implementação do relacionamento entre as classes carteira e moedas.Na implementação assumi que uma carteira possui somente uma moeda que tem um saldo. 
Pode ser adicionado futuramente uma divisão de responsabilidades das camadas por project/library pois hoje está somente com algumas pastas. Padrões como UnitOfWork podem melhorar o controle das transações que ocorrem nos repositorios, e uma camada de AppService pode ser adicionada entre API e camada de infra/repositorios.
Pensando em distribuição o docker poderia ser usado e arquivos como docker-compose facilitariam a execução local, e posteriormene distribuito em container no kubernetes em alguma cloud com Amazon, azure.




passos para logar no postgres:

 docker exec -it postgres_db bash
 psql -U postgres -d offer_db 
