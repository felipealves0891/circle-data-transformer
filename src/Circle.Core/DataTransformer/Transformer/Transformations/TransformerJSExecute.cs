using System;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;

namespace Circle.Core.DataTransformer.Transformer.Transformations
{
    public class TransformerJSExecute : ITransformer
    {
        private V8ScriptEngine _engine;

        public TransformerJSExecute(string script)
        {
            _engine = new V8ScriptEngine();
            _engine.Execute(script);
        }

        public object Transform(object inputData, int inputIndex, int lineIndex)
        {
            return _engine.Script.transformer(inputData, inputIndex, lineIndex);
        }
    }
}
