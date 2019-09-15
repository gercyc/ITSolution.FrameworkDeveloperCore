# ITSolution.FrameworkDeveloperCore
Framework para desenvolvimento .NET Core

* ITSolution.Framework.Core
  * Classes comuns, referenciadas em assemblies Servers e Client;
  * Classes utilitárias (Conversões, tratamento de exceções, manipulação de arquivos, etc;
  * Configuração da aplicação:
    * Tipo de banco;
    * ConnectionString;
    * Pasta padrão de assemblies API;
    * Pasta padrão de assemblies referenciados por APIs;
    * Porta do host;

* ITSolution.Framework.Core.Server
  * DbContext (EntityFramework Core);
  * Repositório Genérico;

* ITSolution.Framework.Core.Host
  * Aplicativo console para iniciar o WebHost;
