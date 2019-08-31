# TicTacToe

Essa é uma aplicação com fim de fornecer uma API para o "jogo da velha", 
fornecendo operações de criação de um jogo e o realizar de movimentos

## Requisitos do projeto

Trata-se de um projeto feito em `.NET Core` e para sua publicação precisamos da instalação 
da versão do SDK 2.2 ou maior disponivel em https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.401-windows-x64-installer.

O projeto internamente ainda conta com outras dependencias internas configuradas para serem instaladas via nuget

## Instruções de publicação

A publicação se da pelo commando:

- `dotnet publish -o ./../dist`: Faz a publicação e coloca seu resultado em uma nova pasta `dist` do diretório atual

Também foi incluido um arquivo `publish.bat` que faz a execução destes comandos ao mandar executa-lo (Disponível apenas no windows). 

## Instruções de execução

Para execução, o projeto já precisa estar publicado e deve ser utilizado o comando `dotnet ./TicTacToe.dll` na pasta publicada

Como na publicação, foram adicionados 2 scripts que facilitam a execução:

- `run.bat`: Disponível na pasta publicada (dist), fará a execução deste software.
- `start.bat`: Disponóvel na pasta principal, faz a publicação e inicia automaticamente a aplicação,.

Ao fim da execução, será informado a url base (protocolo, host e porta) onde as chamadas podem ser feitas 

## Documentação da API

A documentação pode ser acessada através da uri /swagger/. 

## Testes unitários

O projeto conta com alguns testes unitários para garantir sua integridade, para executa-los, basta utilizar na IDE do visual studio o Test Explorer, ou executar o commando `dotnet test`. que os testes serão executados.