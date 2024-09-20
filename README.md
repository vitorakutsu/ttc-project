# Processamento de Imagens Digitais - Afinamento, Extração de Contornos e Retângulo Mínimo

## Descrição

Este projeto tem como objetivo realizar o **Afinamento**, a **Extração de Contornos** e o cálculo do **Retângulo Mínimo** de objetos contidos em uma imagem digitalizada, utilizando **Windows Forms** em **C#**. A imagem utilizada, `letraforma.jpg`, é composta por caracteres alfanuméricos e números decimais escritos à mão, digitalizados para processamento.

O projeto foi desenvolvido como parte da disciplina **TTC I – Tópicos em Tecnologia de Computação I**, ministrada pelo **Prof. Francisco Assis da Silva**, e envolve a implementação de algoritmos de processamento de imagens, tais como:

- Afinamento com o algoritmo de **Zhang-Suen**;
- Extração de Contornos usando o **algoritmo do ceguinho** (Contour Following);
- Cálculo do **Retângulo Mínimo** para os objetos contornados.

## Funcionalidades

1. **Afinamento**:
   - Implementação do algoritmo de **Zhang-Suen** para realizar o afinamento da imagem, que reduz as larguras dos objetos a linhas finas de um pixel de largura.
   - **Resultado**: Uma versão afinada da imagem original com os caracteres e números reduzidos a suas formas mínimas.

2. **Extração de Contornos**:
   - Implementação do **algoritmo do ceguinho** para encontrar os contornos de cada objeto na imagem.
   - **Resultado**: Contornos destacados dos objetos presentes na imagem afinada.

3. **Cálculo do Retângulo Mínimo**:
   - Implementação de um algoritmo para calcular o retângulo mínimo que envolve cada objeto contornado.
   - **Resultado**: Os retângulos mínimos que delimitam os objetos contornados são desenhados sobre a imagem processada.

## Estrutura do Projeto

- `letraforma.jpg`: Imagem utilizada para o processamento.
- `application.cs`: Formulário principal que controla a interface e os fluxos do processamento.
- `thinning.cs`: Implementação do algoritmo de **Afinamento** de Zhang-Suen.
- `contour-extraction.cs`: Implementação do **algoritmo do ceguinho** para extração de contornos.
- `minimum-rectangle.cs`: Implementação do cálculo do **Retângulo Mínimo**.

## Requisitos

- **Microsoft Visual Studio** com suporte para C# e Windows Forms.
- **Bibliotecas**:
  - `System.Drawing` (para manipulação de imagens)
  - `System.Windows.Forms` (para a interface gráfica)
