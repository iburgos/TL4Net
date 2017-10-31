using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Telegram4Net.Domain.TL
{
    public abstract class TLObject
    {
        //public virtual TLType TypeId => TLType.None;

        public virtual void Read(TLBinaryReader from)
        {
        }

        public virtual void Write(TLBinaryWriter to)
        {
        }

        public byte[] ToArray()
        {
            using (var stream = new MemoryStream())
            {
                using (var to = new TLBinaryWriter(stream))
                {
                    Write(to);
                    return stream.ToArray();
                }
            }
        }

        public virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
        }

        public void RaisePropertyChanged<TProperty>(Expression<Func<TProperty>> property)
        {
            RaisePropertyChanged(GetMemberInfo(property).Name);
        }

        private MemberInfo GetMemberInfo(Expression expression)
        {
            var lambdaExpression = (LambdaExpression) expression;
            return (!(lambdaExpression.Body is UnaryExpression) ? (MemberExpression) lambdaExpression.Body
                : (MemberExpression) ((UnaryExpression) lambdaExpression.Body).Operand).Member;
        }
    }
}