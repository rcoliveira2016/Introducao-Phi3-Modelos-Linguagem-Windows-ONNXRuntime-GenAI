using Microsoft.ML.OnnxRuntimeGenAI;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ChatIA
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private Model? model = null;
        private Tokenizer? tokenizer = null;
        private readonly string ModelDir =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"Models\directml\directml-int4-awq-block-128");

        public MainWindow()
        {
            this.InitializeComponent();
            this.Activated += MainWindow_Activated;
        }

        public Task InitializeModelAsync()
        {

            DispatcherQueue.TryEnqueue(() =>
            {
                responseTextBlock.Text = "Loading model...";
            });

            return Task.Run(() =>
            {
                try
                {
                    var sw = Stopwatch.StartNew();
                    model = new Model(ModelDir);
                    tokenizer = new Tokenizer(model);
                    sw.Stop();
                    DispatcherQueue.TryEnqueue(() =>
                    {
                        responseTextBlock.Text = $"Model loading took {sw.ElapsedMilliseconds} ms";
                    });
                }
                catch (Exception e)
                {
                    DispatcherQueue.TryEnqueue(() =>
                    {
                        responseTextBlock.Text = $"erro {e}";
                    });
                }
            });
        }
        private async void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
        {
            if (model == null)
            {
                await InitializeModelAsync();
            }
        }
        public async IAsyncEnumerable<string> InferStreaming(string prompt)
        {
            if (model == null || tokenizer == null)
            {
                throw new InvalidOperationException("Model is not ready");
            }

            var generatorParams = new GeneratorParams(model);

            var sequences = tokenizer.Encode(prompt);

            generatorParams.SetSearchOption("max_length", 2048);
            generatorParams.SetInputSequences(sequences);
            generatorParams.TryGraphCaptureWithMaxBatchSize(1);

            using var tokenizerStream = tokenizer.CreateStream();
            using var generator = new Generator(model, generatorParams);
            StringBuilder stringBuilder = new();
            while (!generator.IsDone())
            {
                string part;
                try
                {
                    await Task.Delay(10).ConfigureAwait(false);
                    generator.ComputeLogits();
                    generator.GenerateNextToken();
                    part = tokenizerStream.Decode(generator.GetSequence(0)[^1]);
                    stringBuilder.Append(part);
                    if (stringBuilder.ToString().Contains("<|end|>")
                        || stringBuilder.ToString().Contains("<|user|>")
                        || stringBuilder.ToString().Contains("<|system|>"))
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    break;
                }

                yield return part;
            }
        }

        private async void myButton_Click(object sender, RoutedEventArgs e)
        {
            responseTextBlock.Text = "";

            if (model != null)
            {
                var systemPrompt = "Voce é um assitente que se comunica com girias de gauchos. Gauchos são pessoas que moram no do rio grande do sul";
                var userPrompt = promptTextBox.Text;

                var prompt = $@"<|system|>{systemPrompt}<|end|><|user|>{userPrompt}<|end|><|assistant|>";

                await foreach (var part in InferStreaming(prompt))
                {
                    responseTextBlock.Text += part;
                }
            }
        }
    }
}
