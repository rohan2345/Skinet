using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria{ get; }
    Expression<Func<T,object>>? OrderBY {get;}
    Expression<Func<T,object>>? OrderBYDescending {get;}
    bool IsDistinct {get;}
    int Take{get;}
    int Skip{get;}
    bool IsPaginEnabled{get;}
    IQueryable<T> ApplyCriteira(IQueryable<T> query);
    

}

public interface ISpecification<T,TResult> : ISpecification<T>
{
    Expression<Func<T,TResult>>? Select{get;}
    
}
