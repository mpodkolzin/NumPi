using NumPi.Indices;
using NumPi.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Reflection
{

    internal class _MemberProjection
    {
        public Type fieldType;
        public string fieldName;
        public Expression expr;

    }

    public class ConvertorExpressions
    {
        public string Name;
        public ParameterExpression Input;
        public MethodCallExpression Convertor;
    }



    public static class FrameReflectionUtils
    {
        public static DataFrame<int, string> ConvertRecordSequence<T>(IEnumerable<T> data)
        {
            var convertors = getRecordConvertorExprs(typeof(T));
            var colIndex = LinearIndexBuilder.Instance.Create<string>(convertors.Select(c => c.Name), true);
            var convFuncs = convertors.Select(c =>
            {
                var cast = Expression.Convert(c.Convertor, typeof(IVector));
                return Expression.Lambda<Func<IEnumerable<T>, IVector>>(cast, c.Input).Compile();

            });
            var vecs = convFuncs.Select(cf => cf.Invoke(data));
            var frameData = VectorBuilder.Instance.Create(vecs.ToArray());

            var rowKeys = Index.ofKeys<int>(Enumerable.Range(0, data.Count()));
            var frame = new DataFrame<int, string>(rowKeys, colIndex, frameData, LinearIndexBuilder.Instance, VectorBuilder.Instance);
            return frame;

        }


        public static IEnumerable<ConvertorExpressions> getRecordConvertorExprs(Type recordType)
        {

            var vcMethodInfo = VectorBuilder.Instance.GetType().GetMethods().First(mi => mi.Name == "Create");
            var enumSelectMi = typeof(Enumerable).GetMethods().First(mi => mi.Name == "Select");
            var enumToArray = typeof(Enumerable).GetMethods().First(mi => mi.Name == "ToArray");

            var res = getMemberProjections(recordType).Select(mp =>
            {
                var input = Expression.Parameter(typeof(IEnumerable<>).MakeGenericType(recordType));
                var selected = Expression.Call(enumSelectMi.MakeGenericMethod(new Type[] { recordType, mp.fieldType }), input, mp.expr);
                var body = Expression.Call(enumToArray.MakeGenericMethod(new Type[] { mp.fieldType }), selected);
                var conv = Expression.Call(Expression.Constant(VectorBuilder.Instance), vcMethodInfo.MakeGenericMethod(new Type[] { mp.fieldType }), body);
                return new ConvertorExpressions(){ Name = mp.fieldName, Input = input, Convertor = conv};

            });

            return res;

        }

        private static IEnumerable<PropertyInfo> getExpandableProperties(Type type)
        {
            var res = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            res.Where(p => p.CanRead && p.GetIndexParameters().Length == 0).Select(p => p);
            return res;
        }
        private static IEnumerable<FieldInfo> getExpandableFields(Type type)
        {
            var res = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            return res;
        }

        private static IEnumerable<_MemberProjection> getMemberProjections(Type recType)
        {
            List<_MemberProjection> res = new List<_MemberProjection>();

            var props = getExpandableProperties(recType);
            var fields = getExpandableFields(recType);

            var propsProjections = props.Select(p =>
            {
                var recd = Expression.Parameter(recType);
                var call = Expression.Call(recd, p.GetGetMethod());
                var mp = new _MemberProjection()
                {
                    fieldName = p.Name,
                    fieldType = p.PropertyType,

                    expr = Expression.Lambda(call, new[] { recd })
                };
                return mp;
            });

            var fieldProjections = fields.Select(f =>
            {
                var recd = Expression.Parameter(recType);
                var call = Expression.Field(recd, f);
                var mp = new _MemberProjection()
                {
                    fieldName = f.Name,
                    fieldType = f.FieldType,

                    expr = Expression.Lambda(call, new[] { recd })
                };
                return mp;
            });

            return propsProjections.Concat(fieldProjections);

        }
    }
}
