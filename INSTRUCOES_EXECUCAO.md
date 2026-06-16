# Guia de Execução de Ponta a Ponta: SaasRh (Local ➡️ Nuvem)

Este guia apresenta o passo a passo exato para inicializar e testar toda a aplicação integrada (Frontend, BFF, Microsserviço de People, AWS API Gateway e Azure SQL Database).

---

## 📐 Fluxo da Arquitetura de Comunicação

As requisições trafegam pelo seguinte caminho:
```text
Frontend (Shell: 5173) ──> AWS API Gateway (dev) ──> ngrok ──> BFF Local (8080) ──> People API Local (5096) ──> Azure SQL Database
```

---

## 🛠️ Pré-requisitos
* **.NET 9.0 SDK** (ou superior) instalado.
* **Node.js** (v18+) e **npm** instalados.
* **ngrok** instalado e configurado com o authtoken.
* **Azure SQL Database** criado.
* **AWS API Gateway** criado (tipo REST API).

---

## 🏃 Passo a Passo para Inicialização do Projeto

### Passo 1: Iniciar os Serviços de Backend (APIs)

1. **Subir o Microsserviço `People.API`**:
   * Abra um terminal na pasta **`microservicos-AzureSQL-arquitetura`**.
   * Execute o comando:
     ```bash
     dotnet run --project src/People.API
     ```
   * *O serviço ficará escutando na porta **`5096`**.*
   * *Nota: Ele aplicará as migrations e o seed de dados automaticamente no Azure SQL ao iniciar.*

2. **Subir o BFF (`backend-arquitetura-cloud`)**:
   * Abra um novo terminal na pasta **`backend-arquitetura-cloud`**.
   * Execute o comando:
     ```bash
     dotnet run
     ```
   * *O BFF ficará escutando na porta **`8080`**.*

---

### Passo 2: Criar a Ponte ngrok para a AWS

Como seu BFF está local e a AWS está na nuvem pública, exponha a porta `8080`:

1. Abra um terminal e execute:
   ```bash
   ngrok http 8080
   ```
2. Copie a URL pública gerada (ex: `https://abcd-1234.ngrok-free.app`).

---

### Passo 3: Atualizar e Implantar o AWS API Gateway

1. Acesse o console da **AWS** -> **API Gateway** -> **SaasRh API Gateway**.
2. Vá em **Resources** (Recursos):
   * Selecione o método **ANY** sob o recurso **`/{proxy+}`**:
     * Atualize a **Endpoint URL** para: `https://SUA_URL_DO_NGROK.ngrok-free.app/{proxy}`
   * Selecione o método **ANY** sob o recurso raiz **`/`**:
     * Atualize a **Endpoint URL** para: `https://SUA_URL_DO_NGROK.ngrok-free.app/`
3. Vá em **Actions** (Ações) e clique em **Deploy API** (Implantar API).
4. Selecione o estágio **`dev`** e faça o deploy.
5. Copie a **Invoke URL** gerada no topo da página (ex: `https://mrq1y9z2g2.execute-api.us-east-2.amazonaws.com/dev`).

---

### Passo 4: Configurar e Rodar o Frontend

1. Vá para a pasta **`Arquitetura-GestaoRh-Front`**.
2. Verifique se o arquivo `.env` está configurado corretamente com a URL do API Gateway:
   ```env
   VITE_USE_MOCKS=false
   VITE_BFF_BASE_URL=https://mrq1y9z2g2.execute-api.us-east-2.amazonaws.com/dev
   VITE_BFF_API_PREFIX=
   VITE_REMOTE_PEOPLE_URL=http://localhost:4183/assets/remoteEntry.js
   VITE_REMOTE_DOCUMENTS_URL=http://localhost:4184/assets/remoteEntry.js
   ```
3. Instale as dependências (somente na primeira vez):
   ```bash
   npm install
   ```
4. Suba o Preview do **MFE People** (porta `4183`):
   ```bash
   npm run dev:people
   ```
5. Suba o Preview do **MFE Documents** (porta `4184`):
   ```bash
   npm run dev:documents
   ```
6. Inicie o Host Principal (**Shell**) em um novo terminal (porta `5173`):
   ```bash
   npm run dev:shell
   ```

---

### Passo 5: Acessar a Aplicação
Abra seu navegador em **`http://localhost:5173/`** e navegue para a aba de **Colaboradores** para gerenciar os dados persistidos no Azure SQL!
