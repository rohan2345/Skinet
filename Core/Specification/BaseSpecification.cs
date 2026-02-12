using System;
using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specification;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
    protected BaseSpecification() : this(null){}
    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBY {get; private set;}
    public Expression<Func<T, object>>? OrderBYDescending {get; private set;}

    public bool IsDistinct  {get; private set;}

    protected void AddOrderBy(Expression<Func<T,object>> orderByExpression)
    {
        OrderBY= orderByExpression;
    }
    protected void AddOrderByDescending(Expression<Func<T,object>> orderByDescExpression)
    {
        OrderBYDescending= orderByDescExpression;
    }
    protected void ApplyDistinct()
    {
        IsDistinct=true;
    }
}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>> criteria)
: BaseSpecification<T>(criteria), ISpecification<T, TResult>
{
    protected BaseSpecification() : this(null!){}
    public Expression<Func<T, TResult>>? Select {get; private set;}
    protected void AddSelect(Expression<Func<T,TResult>> selectExppression)
    {
        Select=selectExppression;
    }
}
