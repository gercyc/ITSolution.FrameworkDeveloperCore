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

* ITSolution.Framework.Servers.Core.CustomUserAPI
  * Projeto template para criação de APIs e utilização do Host para exposição;

* ITSolution.Framework.Core.Host
  * Aplicativo console para iniciar o WebHost;

* ITSolution.Framework.Core.AspHost
  * Aplicativo ASP.NET Core com hospedagem das APIs customizadas;
 
* Online no Azure
  https://itsolutionapiframework.azurewebsites.net/
 
 [![Build Status](https://dev.azure.com/gercyc/MyDevelopments/_apis/build/status/gercyc.ITSolution.FrameworkDeveloperCore?branchName=master)](https://dev.azure.com/gercyc/MyDevelopments/_build/latest?definitionId=1&branchName=master)
