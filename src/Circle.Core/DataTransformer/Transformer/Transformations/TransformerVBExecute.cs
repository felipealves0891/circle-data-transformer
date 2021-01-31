using Microsoft.ClearScript.Windows;

namespace Circle.Core.DataTransformer.Transformer.Transformations
{
    public class TransformerVBExecute : ITransformer
    {
        private VBScriptEngine _engine;

        public TransformerVBExecute(string script)
        {
            _engine = new VBScriptEngine();
            _engine.Execute(script);
        }

        public object Transform(object inputData, int inputIndex, int lineIndex)
        {
            return _engine.Script.transformer(inputData, inputIndex, lineIndex);
        }
    }
}
