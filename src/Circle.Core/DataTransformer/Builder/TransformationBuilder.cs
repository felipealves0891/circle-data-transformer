using Circle.Core.DataTransformer.Input;
using Circle.Core.DataTransformer.Output;
using Circle.Core.DataTransformer.Transformer;
using System;
using System.Collections.Generic;

namespace Circle.Core.DataTransformer.Builder
{
    public class TransformationBuilder
    {
        private IInputSource _input;

        private IOutputSource _output;

        private List<ITransformer> _transformers;

        private List<IRowTransformer> _rowTransformers;

        public TransformationBuilder()
        {
            _transformers = new List<ITransformer>();
            _rowTransformers = new List<IRowTransformer>();
        }

        public TransformationBuilder SetInput(IInputSource input)
        {
            _input = input;
            return this;
        }

        public TransformationBuilder SetOutput(IOutputSource output)
        {
            _output = output;
            return this;
        }

        public TransformationBuilder AddTransformerByType(Type type, params object[] args)
        {
            var t = (ITransformer)Activator.CreateInstance(type, args);
            _transformers.Add(t);
            return this;
        }

        public TransformationBuilder AddTransformer(ITransformer t)
        {
            _transformers.Add(t);
            return this;
        }

        public TransformationBuilder AddRowTransformerByType(Type type, params object[] args)
        {
            var t = (IRowTransformer)Activator.CreateInstance(type, args);
            _rowTransformers.Add(t);
            return this;
        }

        public TransformationBuilder AddRowTransformer(IRowTransformer rt)
        {
            _rowTransformers.Add(rt);
            return this;
        }

        public Transformation Build()
        {
            var transformation = new Transformation(_input, _output);
            foreach (var t in _transformers)
                transformation.AddTransformer(t);

            foreach (var t in _rowTransformers)
                transformation.AddRowTransformer(t);

            return transformation;
        }

    }
}
