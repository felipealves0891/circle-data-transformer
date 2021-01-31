# CircleDataTransformer

O Circle é uma solução pequena para ETLs (Extract, Transformer, Load), onde voce pode adicionar transformações injetando 
codigo em C# ou Javascript atravez da biblioteca ClearScript

## Life Cycle

- Input e Output

> A Transformation espera dois parametros obrigatorios, uma classe que implemente a interface __IInputSource__ e __IOutputSource__.

```
var inputStream = new TextInputSource();
inputStream.Open(Path.Combine(path, "input.csv"));

var outputStream = new TextOutputSource();
outputStream.Open(Path.Combine(path, "output.csv"));

var transformation = new Transformation(_input, _output);
```

> Apenas com isso já é possivel ler e adicionar dados em outra fonte, de arquivo para arquivo, arquivo para tabela, tabela para arquivo e tabela para tabela.
> Podendo ser extendido para outras fontes implementando as interfaces __IInputSource__ e __IOutputSource__.
> As 2 interfaces herdam da __ISource__.

- Adicionando Transformações

> Existem dois tipos de transformações que podem ser adicionadas. 

> A primeira é para linha, onde cada linha que é lida é enviada para a transformação com todas as colunas.
> Este tipo de transformação espera uma interface __IRowTransformation__.

> A segunda é para coluna, onde cada coluna da linha é enviada para a transformação.
> Este tipo de transformação espera uma interface __ITransformation__.

```
transformation.AddRowTransformer(IRowTransformation);
transformation.AddTransformer(ITransformation);
```

- Eventos e Diagnostico

> A classe __Transformation__ herda a classe abstrata __TransformationDiagnostic__ que contem informações de Diagnostics e GC, como quantas coletas de cadas geração
> Quanto tempo percorrido em milesegundos e segundos a memoria usada.
> Diagnose é um evento que é acionado a cada um segundo.

```
transformation.Diagnose += Transformation_Diagnose;

private static void Transformation_Diagnose(object sender, EventArgs e)
{
    var t = sender as Transformation;

    Console.Clear();
    Console.WriteLine($"\nRows : {t.Counter}");
    Console.WriteLine($"Time : {t.ElapsedTimeInSeconds()} s");
    Console.WriteLine($"Gen0 : {t.Collected(0)}");
    Console.WriteLine($"Gen1 : {t.Collected(1)}");
    Console.WriteLine($"Gen2 : {t.Collected(2)}");
    Console.WriteLine($"Memory : {t.MemoryUsed()} mb");

}
```

- Executando e Finalizando

> O Processo de ETL acontece quando o metodo Execute() é chamado, após a execução o metodo Dispose o Circle.

```
transformation.Execute();
transformation.Dispose();
```
