
# Desafio Técnico AeC - Automação com Selenium e DDD

## Descrição do Projeto

Este projeto foi desenvolvido como parte do desafio técnico proposto pela AeC. O objetivo é criar uma automação utilizando **Selenium** em **C#**, com a abordagem de **Domain-Driven Design (DDD)**, para realizar uma busca no site da **Alura** e salvar os resultados em um banco de dados.

A aplicação realiza a seguinte funcionalidade principal:
1. Busca automática no site da Alura por um termo fornecido.
2. Filtra os resultados para exibir apenas cursos.
3. Extrai os dados relevantes dos cursos:
   - **Título**
   - **Professor**
   - **Carga Horária**
   - **Descrição**
4. Armazena os dados extraídos em um banco de dados SQLite.

---

## Estrutura do Projeto

A estrutura de pastas segue a recomendação de boas práticas em DDD:

- **Apresentation**: Classes relacionadas ao input/output da aplicação.
- **Application**: Contém os serviços de aplicação, que orquestram as operações e integram as camadas de domínio e infraestrutura.
- **Domain**: Contém as entidades, interfaces e regras de negócio.
- **Infra**: Implementações para persistência de dados, como o repositório que salva os cursos no banco de dados.
- **Infra.IoC**: Configuração dos serviços e repositórios.

---

## Tecnologias Utilizadas

- **C#**: Linguagem de programação
- **Selenium WebDriver**: Para automação do navegador.
- **SQLite**: Banco de dados leve para persistência local.
- **Entity Framework Core**: Para mapeamento objeto-relacional.
- **Injeção de Dependência**: Uso de abstrações para desacoplar dependências.

---

## Decisões Técnicas

1. **Uso de Selenium**: Escolhido para cumprir o requisito de automação de navegador.
2. **Domain-Driven Design (DDD)**: Facilita a separação de responsabilidades e torna o sistema mais modular e fácil de manter.
3. **Injeção de Dependência**: Permite substituir implementações com facilidade e melhora a testabilidade.
4. **SQLite com Entity Framework Core**: Escolhido por sua simplicidade e compatibilidade com o EF Core, que oferece suporte robusto a operações com banco de dados.

---

## Fluxo da Aplicação

1. O sistema inicializa o Selenium e navega até o site da Alura.
2. Realiza a busca pelo termo informado no código.
3. Aplica o filtro de cursos para garantir resultados relevantes.
4. Para cada curso listado:
   - Acessa os detalhes do curso.
   - Extrai informações como título, professor, carga horária e descrição.
   - Cria uma instância da entidade `Curso` e a insere no banco de dados.
5. Finaliza a execução.

---

## Tratamento de Erros

- **Validações de entrada**: Todos os parâmetros são validados antes de serem usados.
- **Erros no Selenium**: Tratados para garantir que a execução continua ou finaliza de forma controlada.
- **Banco de Dados**: Captura de exceções relacionadas a falhas na persistência dos dados.

---
