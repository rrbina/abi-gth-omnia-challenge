
## Guia para Rodar o Aplicativo

Este reposit�rio cont�m uma aplica��o **Angular 19.9.0** e **.NET 8**, preparada para rodar em um ambiente com **Docker** e **WSL2** (se estiver no windows).  

Siga cuidadosamente os passos abaixo para configurar e executar o projeto localmente em caso de voc� querer debugar.

---

## Pr�-requisitos para Rodar o projeto

- ter o `Docker` instalado na m�quina. Para tal � necess�rio uma distribui��o Linux instalada (se voc� estiver no Windows).
- V� em [https://www.docker.com/get-started/](https://www.docker.com/get-started/) e instale o docker

---

## Execu��o R�pida

Se voc� j� tem o Docker instalado e em execu��o, basta executar o script:

- No **Windows**: d� um duplo clique no arquivo `up.bat`
- Ou, via terminal:
  ```bash
  up.bat
  ```
- Abra o Navegador e Acesse:
    ```
    http://localhost:4200
    ```  
- obs: Demora bastante pra terminar e buildar a primeira vez, pois voc� ainda n�o tem as imagens dos containers na sua m�quina.
- **Infelizmente n�o houve tenpo suficiente para testar a aplica��o usando esse script, mas � poss�vel usar o script `up.dev.bat`, de forma an�loga**, ou veja a se��o `Executando o C�digo` mais adiante.

Isso iniciar� todos os servi�os necess�rios para rodar a aplica��o. Nenhum outro pr�-requisito � necess�rio al�m do Docker.

---

## Pr�-requisitos para Rodar Localmente (para debugar, caso haja interesse)

Caso queira rodar o projeto **localmente**, � poss�vel utilizar o script `up.dev.bat`, que se encontra na pasta docker, mas ser� necess�rio instalar a infraestrutura abaixo na sua m�quina (al�m do Docker):

## Infraestrutura Necess�ria
- **Node.js 19.9.0**  
  - Baixe a vers�o 19.9.0: [Node.js v19.9.0 Download](https://nodejs.org/download/release/v19.9.0/)  
    - Para Windows: baixe o arquivo `node-v19.9.0-x64.msi`  
    - Para outros sistemas, escolha a vvers�o compat�vel.  
  - Ap�s instalar, confirme a versão executando no terminal:
    ```bash
    node -v
    ```
    O retorno deve ser:
    ```
    v19.9.0
    ```

- **.NET 8**  
  - Baixe e instale: [Download .NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Instalar Uma Distribui��o Linux (caso sua m�quina n�o tenha)

1. Pressione `Win + S` e digite **"Windows subsystem for Linux"**.

![Descri��o da imagem](imagens-readme/ubuntu.png)

2. Clique em **OK** e reinicie o computador quando solicitado.

## Atualizar para o WSL2
- Na pasta `instalador-wsl2`, h� um `.msi` baixado de fontes oficiais para facilitar. � s� instalar ele.

**ou**
- Na pasta `instalador-wsl2`, rode o `wsl2.bat`.

**ou**

- V� em [https://aka.ms/wsl2kernel](https://aka.ms/wsl2kernel) e siga os passos

---

## Executando o C�digo
1. Garanta que voc� tem o **Docker**, o **.NET 8** e o **Node.js 19.9.0** (rodando nessa vers�o) na sua m�quina.

2. Abra o terminal e navegue at� a pasta **`docker`** do projeto:
    ```bash
    cd docker
    ```

3. Execute o Script **`up.dev.bat`**:
    - No **Windows**: d� um duplo clique no arquivo `up.dev.bat`
    - Ou, via terminal:
      ```bash
      up.dev.bat
      ```

4. Aguarde At� que Todos os Servi�os Sejam Iniciados Corretamente.

5. Abra o Navegador e Acesse:
    ```
    http://localhost:4200
    ```

---

## Observa��es
- Caso ocorra erro relacionado � vers�o do Node.js, verifique se est� realmente usando **v19.9.0**.  
- Certifique-se de que o **Docker** est� rodando antes de executar o `up.dev.bat` ou `up.bat`.  
- Se estiver usando **Linux ou MacOS**, ser� necess�rio adaptar os `.bat` para os scripts equivalente `.sh`.
