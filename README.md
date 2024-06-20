# Pet-shop System

Este projeto é um sistema para pet-shop que será utilizado pelos funcionários. O sistema permite gerenciar diversas funções de um pet-shop, com permissões de acesso configuradas de acordo com o perfil de cada usuário.

## Arquitetura do Projeto

O projeto está dividido nas seguintes camadas:

- **Core**: Contém as entidades de negócio e interfaces.
- **Infrastructure**: Contém a implementação das interfaces, acesso a dados e integração com outros serviços.
- **Services**: Contém a lógica de negócio e serviços da aplicação.
- **Tests**: Contém os testes unitários e de integração.
- **WebAppMVC**: Contém a aplicação web MVC para interação com os usuários finais.

## Funcionalidades do Sistema

A seguir, uma lista de exemplos de funções que representam o menu do sistema. As permissões para acessar essas funções são determinadas pelo perfil do usuário. Cada função possui uma indicação do progresso atual.

### Menu Principal

1. **Dashboard**     ![](https://geps.dev/progress/0?dangerColor=800000&warningColor=ff9900&successColor=006600)
    - Visão geral das atividades do pet-shop.

2. **Clientes**     ![](https://geps.dev/progress/0?dangerColor=800000&warningColor=ff9900&successColor=006600)
    - Cadastro de Clientes
    - Consulta de Clientes
    - Atualização de Dados dos Clientes

3. **Pets**     ![](https://geps.dev/progress/0?dangerColor=800000&warningColor=ff9900&successColor=006600)
    - Cadastro de Pets
    - Consulta de Pets
    - Atualização de Informações dos Pets

4. **Agendamentos**     ![](https://geps.dev/progress/0?dangerColor=800000&warningColor=ff9900&successColor=006600)
    - Agendamento de Serviços
    - Consulta de Agendamentos
    - Cancelamento de Agendamentos

5. **Serviços**     ![](https://geps.dev/progress/0?dangerColor=800000&warningColor=ff9900&successColor=006600)
    - Lista de Serviços Oferecidos
    - Preços e Descrições dos Serviços

6. **Produtos**     ![](https://geps.dev/progress/0?dangerColor=800000&warningColor=ff9900&successColor=006600)
    - Cadastro de Produtos
    - Consulta de Produtos
    - Atualização de Informações dos Produtos

7. **Vendas**     ![](https://geps.dev/progress/0?dangerColor=800000&warningColor=ff9900&successColor=006600)
    - Registro de Vendas
    - Histórico de Vendas
    - Relatórios de Vendas

8. **Funcionários**     ![](https://geps.dev/progress/90?dangerColor=800000&warningColor=ff9900&successColor=006600)
    - Cadastro de Funcionários
    - Consulta de Funcionários
    - Atribuição de Permissões e Perfis

9. **Configurações**     ![](https://geps.dev/progress/50?dangerColor=800000&warningColor=ff9900&successColor=006600)
    - Parâmetros do Sistema
    - Gestão de Perfis e Permissões
    - Configurações de Segurança

## Como Executar o Projeto

Para executar o projeto localmente, siga os passos abaixo:

1. Clone o repositório:
   ```sh
   git clone https://github.com/seu-usuario/pet-shop.git
