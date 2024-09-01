# Projeto Clean Architecture Essencial - ASP .NET Core com C#

## Descrição

Este repositório contém um projeto de exemplo implementando os princípios de Clean Architecture. O objetivo é demonstrar como estruturar um projeto para manter a independência entre camadas, facilitar testes e evoluções do sistema.

## Estrutura do Projeto
- Core: Contém as entidades, interfaces, e casos de uso.
- Application: Implementa os casos de uso, manipuladores de comandos, e validações.
- Infrastructure: Contém a implementação de repositórios, serviços externos e dependências de infraestrutura.
- Presentation: Camada responsável pela interface com o usuário, seja via API ou interface gráfica.

## Instalação

1. Clone o repositório:
    ```bash
    git clone [https://github.com/seu-usuario/projeto-clean-architecture.git](https://github.com/GabrielHNE/CursoCleanArch.git)
    ```
2. Navegue até a pasta do projeto:
    ```bash
    cd CursoCleanArch
    ```
3. Restaure as dependências:
    ```bash
    dotnet restore
    ```

4. Execute o projeto: Escolha executar o projeto WebUI ou WebApi
    1. Vá até a pasta do projeto e execute o comando:
        ```bash
        dotnet run
        ```
## Como Usar

Após iniciar o projeto, acesse a rota configurada no seu projeto para visualizar a aplicação em funcionamento. Utilize as rotas API documentadas para interagir com as funcionalidades.

## Testes

## Contribuições

Contribuições são bem-vindas! Sinta-se à vontade para abrir um `issue` ou enviar um `pull request`.

## Referências
- [Clean Architecture por Robert C. Martin](https://amzn.to/3Qs8SzY)
- [Documentação do .NET](https://docs.microsoft.com/en-us/dotnet/)
- [Clean Architecture Essencial - ASP .NET Core com C#](https://www.udemy.com/course/clean-architecture-essencial-asp-net-core-com-c/)
