# 🚀 Guia para Rodar o Aplicativo

Este repositório contém uma aplicação **Angular 19.9.0** e **.NET 8**, preparada para rodar em um ambiente com **Docker** e **WSL2**.  
Siga cuidadosamente os passos abaixo para configurar e executar o projeto localmente.

---

## 📋 Pré-requisitos

Antes de iniciar, certifique-se de que possui a infraestrutura necessária instalada na sua máquina.

### 0️⃣ Infraestrutura Necessária
- **Node.js 19.9.0**  
  - Baixe a versão 19.9.0: [Node.js v19.9.0 Download](https://nodejs.org/download/release/v19.9.0/)  
    - Para Windows: baixe o arquivo `node-v19.9.0-x64.msi`  
    - Para outros sistemas, escolha a versão compatível.  
  - Após instalar, confirme a versão executando no terminal:
    ```bash
    node -v
    ```
    O retorno deve ser:
    ```
    v19.9.0
    ```

- **.NET 8**  
  - Baixe e instale: [Download .NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

- **Docker**  
  - Instale o Docker: [Get Started with Docker](https://www.docker.com/get-started/)

- **WSL2 (Windows Subsystem for Linux 2)**  
  - Guia de instalação: [Instalar WSL no Windows](https://learn.microsoft.com/pt-br/windows/wsl/install)

---

## ▶️ Passo a Passo para Rodar o Projeto

1. Abra o terminal e navegue até a pasta **`docker`** do projeto:
    ```bash
    cd docker
    ```

2. Execute o script **`up.dev.bat`**:
    - No **Windows**: dê um duplo clique no arquivo `up.dev.bat`
    - Ou, via terminal:
      ```bash
      up.dev.bat
      ```

3. Aguarde até que todos os serviços sejam iniciados corretamente.

4. Abra o navegador e acesse:
    ```
    http://localhost:4200
    ```

---

## 💡 Observações
- Caso ocorra erro relacionado à versão do Node.js, verifique se está realmente usando **v19.9.0**.  
- Certifique-se de que o **Docker** está rodando antes de executar o `up.dev.bat`.  
- Se estiver usando **Linux ou MacOS**, será necessário adaptar o comando do passo 2 para o script equivalente `.sh`.  
