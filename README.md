# Controle-Gastos-C-Sharp

<p>Uma api feita para controle de gastos de um usuário, baseado no curso da Udemy ministrado por Weberson Rodrigues</p>

<h2>Sobre o projeto</h2>
<p>O projeto consiste em um gerenciador de gastos do usuário, onde ele consegue visualizar as contas a <strong>pagar</strong> e a <strong>receber</strong> dele, além da <strong>Natureza de Lançamento</strong> de cada uma. 

<p>O projeto foi feito seguindo modelos de Clean Arch e DDD, além de seguir os padrões de uma API Restful. Mais abaixo tem um guia de pastas e o que significa cada uma delas. Venha conferir!</p>

<h2>Estrutura de Pastas</h2>

```
├── src/ 
│   ├── AutoMapper/
│   ├── Contracts/
│   ├── Controllers/
|   ├── Data/
|   ├── Domain/
|   |   ├── Models/
|   |   ├── Repositories/
|   |   ├── Services/
|   ├── Exceptions/
|   ├── Migrations/
```

``` AutoMapper ``` uma pasta que contém classes para conversão de dados de uma classe para outra, por exemplo de um DTO para uma classe e vice-versa.

``` Contracts ``` aqui ficam alguns modelos de entrada e saída de dados, que também estão separados em pastas para maior organização.

``` Controllers ``` pasta de controladores do sistema de cada entidade. Serve como entrada e saída de requisições HTTP da aplicação. É por aqui que os dados entram e saem, além de outras configurações a mais.

``` Data ``` camada que contém os mapeamentos das tabelas no banco de dados, assim como as suas configurações, relações, tipos de dados e outros.

``` Domain ``` camada principal da aplicação, o coração do sistema. Aqui se encontram entidades, objetos de valor, interface dos Repositories e outros.

``` Models ``` cada modelo ou entidade que a aplicação possui se encontra nessa camada, com cada modelo tendo seus atributos e métodos específicos.

``` Repositories ``` camada de persistência de dados no banco de dados. A comunicação entre banco e aplicação acontece por aqui.

``` Services ``` aqui se encontra as regras de negócio da aplicação, e também onde é chamado os serviços dos repositories

``` Exceptions ``` camada de exceções da aplicação onde é tratado o erro, afim de enviar uma mensagem amigável ao usuário.

``` Migrations ``` tudo que é relacionado diretamente aos dados e manipulação de dados no banco de dados fica aqui. Por exemplo a criação de tabelas e inserção de dados que devem ser registrados inicialmente no banco ficam aqui. Também é comum encontrar essa camada com comandos SQL, pois é uma forma mais segura de se criar as tabelas e manipular os dados da mesma.
