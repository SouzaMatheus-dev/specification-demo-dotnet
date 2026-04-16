using System.Linq.Expressions;

namespace SpecificationDemo.Modulo.Clientes.Dominio.Specifications;

/// <summary>
/// Specification genérica com composição traduzível pelo EF Core (sem Expression.Invoke).
/// </summary>
public abstract class Specification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();

    public bool IsSatisfiedBy(T entity) => ToExpression().Compile()(entity);

    public Specification<T> And(Specification<T> other) => new AndSpecification<T>(this, other);

    public Specification<T> Or(Specification<T> other) => new OrSpecification<T>(this, other);
}

internal sealed class AndSpecification<T> : Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;

    public AndSpecification(Specification<T> left, Specification<T> right)
    {
        _left = left;
        _right = right;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpr = _left.ToExpression();
        var rightExpr = _right.ToExpression();
        var parameter = Expression.Parameter(typeof(T), "x");
        var leftBody = ParameterReplacer.Replace(leftExpr.Parameters[0], parameter, leftExpr.Body);
        var rightBody = ParameterReplacer.Replace(rightExpr.Parameters[0], parameter, rightExpr.Body);
        var body = Expression.AndAlso(leftBody, rightBody);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}

internal sealed class OrSpecification<T> : Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;

    public OrSpecification(Specification<T> left, Specification<T> right)
    {
        _left = left;
        _right = right;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpr = _left.ToExpression();
        var rightExpr = _right.ToExpression();
        var parameter = Expression.Parameter(typeof(T), "x");
        var leftBody = ParameterReplacer.Replace(leftExpr.Parameters[0], parameter, leftExpr.Body);
        var rightBody = ParameterReplacer.Replace(rightExpr.Parameters[0], parameter, rightExpr.Body);
        var body = Expression.OrElse(leftBody, rightBody);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}

internal static class ParameterReplacer
{
    public static Expression Replace(ParameterExpression source, ParameterExpression target, Expression body)
        => new Replacer(source, target).Visit(body);

    private sealed class Replacer(ParameterExpression source, ParameterExpression target) : ExpressionVisitor
    {
        protected override Expression VisitParameter(ParameterExpression node)
            => node == source ? target : node;
    }
}
