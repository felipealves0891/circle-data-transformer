using Circle.Core.DataTransformer.Transformer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Circle.Core.DataTransformer.Builder
{
    public interface IBuilderTransformation : IBuilder
    {
        IBuilderTransformation AddRowTransformerByType(Type type, params object[] args);

        IBuilderTransformation AddRowTransformer(IRowTransformer rt);

        IBuilderTransformation AddTransformerByType(Type type, params object[] args);

        IBuilderTransformation AddTransformer(ITransformer t);

    }
}
