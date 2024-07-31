# Introducao-Phi3-Modelos-Linguagem-Windows-ONNXRuntime-GenAI
Contém um exemplo de aplicativo WinUI 3 que usa IA generativa com o modelo Phi3 e a biblioteca ONNX Runtime Generative AI para implementar uma conversa simples. É baseado em um tutorial da Microsoft Learn.
# Introdução ao Phi3 e a outros modelos de linguagem no aplicativo do Windows com o ONNX Runtime Generative AI

Este repositório é baseado no tutorial da Microsoft Learn¹. O objetivo é criar um aplicativo do WinUI 3 que usa um modelo Phi3 e a biblioteca do ONNX Runtime Generative AI para implementar um aplicativo simples de conversa usando IA generativa.

## Pré-requisitos

- O dispositivo precisa ter o modo de desenvolvedor habilitado. Para obter mais informações, confira Habilitar o dispositivo para desenvolvimento.
- Visual Studio 2022 ou posterior com a carga de trabalho de desenvolvimento do .NET para área de trabalho.

## Criar um aplicativo do WinUI em C#

No Visual Studio, crie um novo projeto. Na caixa de diálogo Criar um projeto, defina o filtro de linguagem como "C#" e o filtro do tipo de projeto como "winui"; em seguida, selecione o modelo Aplicativo em branco, Empacotado (WinUI3 na Área de Trabalho). Nomeie o novo projeto como "GenAIExample".

## Adicionar referências ao pacote NuGet ONNX Runtime Generative AI

No Gerenciador de Soluções, clique com o botão direito do mouse em Dependências e selecione Gerenciar pacotes NuGet.... No gerenciador de pacotes NuGet, selecione a guia Navegar. Procure por "Microsoft.ML.OnnxRuntimeGenAI.DirectML", selecione a versão estável mais recente na lista suspensa Versão e clique em Instalar.

## Adicionar um arquivo de modelo e vocabulário ao projeto

No Gerenciador de Soluções, clique com o botão direito do mouse no projeto e selecione Adicionar->Nova Pasta. Nomeie a nova pasta como "Models". Neste exemplo, usaremos o modelo de [Hugging Face](^4^). Existem várias maneiras diferentes de recuperar modelos. Para este passo a passo, usaremos a interface de linha de comando (CLI) do Hugging Face. Caso obtenha os modelos usando outro método, talvez seja necessário ajustar os caminhos do arquivo ao modelo no código de exemplo.

## Referências

- [Artigo original](^1^)

(1) Introdução ao Phi3 e a outros modelos de ... - learn.microsoft.com. https://learn.microsoft.com/pt-br/windows/ai/models/get-started-models-genai.
(2) undefined. https://huggingface.co/microsoft/Phi-3-mini-4k-instruct-onnx/tree/main/directml/directml-int4-awq-block-128.
(3) Introdução ao Phi3 e a outros modelos de ... - learn.microsoft.com. https://learn.microsoft.com/pt-br/windows/ai/models/get-started-models-genai.
(4) Tutorial do Windows Machine Learning para área de trabalho (C++). https://learn.microsoft.com/pt-br/windows/ai/windows-ml/get-started-desktop.
(5) undefined. https://onnxruntime.ai/docs/.
