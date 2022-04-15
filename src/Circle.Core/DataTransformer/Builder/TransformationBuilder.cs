using Circle.Core.DataTransformer.Input;
using Circle.Core.DataTransformer.Output;
using Circle.Core.DataTransformer.Transformer;
using System;
using System.Collections.Generic;

namespace Circle.Core.DataTransformer.Builder
{
    public class TransformationBuilder : IBuilder, IBuilderInput, IBuilderOutput, IBuilderTransformation
    {
        private IInputSource _input;

        private IOutputSource _output;

        private List<ITransformer> _transformers;

        private List<IRowTransformer> _rowTransformers;

        private TransformationBuilder()
        {
            _transformers = new List<ITransformer>();
            _rowTransformers = new List<IRowTransformer>();
        }

        public static IBuilderInput Instance()
        {
            return new TransformationBuilder();
        }
        
        public IBuilderOutput SetInput(IInputSource input)
        {
            _input = input;
            return this;
        }

        public IBuilderTransformation SetOutput(IOutputSource output)
        {
            _output = output;
            return this;
        }

        public IBuilderTransformation AddTransformerByType(Type type, params object[] args)
        {
            var t = (ITransformer)Activator.CreateInstance(type, args);
            _transformers.Add(t);
            return this;
        }

        public IBuilderTransformation AddTransformer(ITransformer t)
        {
            _transformers.Add(t);
            return this;
        }

        public IBuilderTransformation AddRowTransformerByType(Type type, params object[] args)
        {
            var t = (IRowTransformer)Activator.CreateInstance(type, args);
            _rowTransformers.Add(t);
            return this;
        }

        public IBuilderTransformation AddRowTransformer(IRowTransformer rt)
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
