﻿using FreeSql.Internal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FreeSql.Internal.CommonProvider {

	abstract class Select1Provider<T1> : Select0Provider<ISelect<T1>, T1>, ISelect<T1>
			where T1 : class {
		public Select1Provider(IFreeSql orm, CommonUtils commonUtils, CommonExpression commonExpression, object dywhere) : base(orm, commonUtils, commonExpression, dywhere) {

		}

		protected ISelect<T1> InternalFrom(Expression exp) {
			if (exp.NodeType == ExpressionType.Call) {
				var expCall = exp as MethodCallExpression;
				var stockCall = new Stack<MethodCallExpression>();
				while (expCall != null) {
					stockCall.Push(expCall);
					expCall = expCall.Object as MethodCallExpression;
				}
				while (stockCall.Any()) {
					expCall = stockCall.Pop();

					switch (expCall.Method.Name) {
						case "Where": this.InternalWhere(expCall.Arguments[0]); break;
						case "WhereIf":
							var whereIfCond = _commonExpression.ExpressionSelectColumn_MemberAccess(null, null, SelectTableInfoType.From, expCall.Arguments[0], false);
							if (whereIfCond == "1" || whereIfCond == "'t'")
								this.InternalWhere(expCall.Arguments[1]);
							break;
						case "GroupBy": this.InternalGroupBy(expCall.Arguments[0]); break;
						case "OrderBy": this.InternalOrderBy(expCall.Arguments[0]); break;
						case "OrderByDescending": this.InternalOrderByDescending(expCall.Arguments[0]); break;

						case "LeftJoin": this.InternalJoin(expCall.Arguments[0], SelectTableInfoType.LeftJoin); break;
						case "InnerJoin": this.InternalJoin(expCall.Arguments[0], SelectTableInfoType.InnerJoin); break;
						case "RightJoin": this.InternalJoin(expCall.Arguments[0], SelectTableInfoType.RightJoin); break;

						default: throw new NotImplementedException($"未现实 {expCall.Method.Name}");
					}
				}
			}
			return this;
		}

		public ISelect<T1> As(string alias) {
			var oldAs = _tables.First().Alias;
			var newAs = string.IsNullOrEmpty(alias) ? "a" : alias;
			if (oldAs != newAs) {
				_tables.First().Alias = newAs;
				var wh = _where.ToString();
				_where.Replace($" {oldAs}.", $" {newAs}.");
			}
			return this;
		}

		public TMember Avg<TMember>(Expression<Func<T1, TMember>> column) => this.InternalAvg<TMember>(column?.Body);

		public abstract ISelect<T1, T2, T3> From<T2, T3>(Expression<Func<ISelectFromExpression<T1>, T2, T3, ISelectFromExpression<T1>>> exp) where T2 : class where T3 : class;// { this.InternalFrom(exp?.Body); var ret = new Select3Provider<T1, T2, T3>(_orm, _commonUtils, _commonExpression, null); Select0Provider<ISelect<T1>, T1>.CopyData(this, ret); return ret; }

		public abstract ISelect<T1, T2, T3, T4> From<T2, T3, T4>(Expression<Func<ISelectFromExpression<T1>, T2, T3, T4, ISelectFromExpression<T1>>> exp) where T2 : class where T3 : class where T4 : class;// { this.InternalFrom(exp?.Body); var ret = new Select4Provider<T1, T2, T3, T4>(_orm, _commonUtils, _commonExpression, null); Select0Provider<ISelect<T1>, T1>.CopyData(this, ret); return ret; }

		public abstract ISelect<T1, T2, T3, T4, T5> From<T2, T3, T4, T5>(Expression<Func<ISelectFromExpression<T1>, T2, T3, T4, T5, ISelectFromExpression<T1>>> exp) where T2 : class where T3 : class where T4 : class where T5 : class;// { this.InternalFrom(exp?.Body); var ret = new Select5Provider<T1, T2, T3, T4, T5>(_orm, _commonUtils, _commonExpression, null); Select0Provider<ISelect<T1>, T1>.CopyData(this, ret); return ret; }

		public abstract ISelect<T1, T2, T3, T4, T5, T6> From<T2, T3, T4, T5, T6>(Expression<Func<ISelectFromExpression<T1>, T2, T3, T4, T5, T6, ISelectFromExpression<T1>>> exp) where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class;// { this.InternalFrom(exp?.Body); var ret = new Select6Provider<T1, T2, T3, T4, T5, T6>(_orm, _commonUtils, _commonExpression, null); Select0Provider<ISelect<T1>, T1>.CopyData(this, ret); return ret; }

		public abstract ISelect<T1, T2, T3, T4, T5, T6, T7> From<T2, T3, T4, T5, T6, T7>(Expression<Func<ISelectFromExpression<T1>, T2, T3, T4, T5, T6, T7, ISelectFromExpression<T1>>> exp) where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class;// { this.InternalFrom(exp?.Body); var ret = new Select7Provider<T1, T2, T3, T4, T5, T6, T7>(_orm, _commonUtils, _commonExpression, null); Select0Provider<ISelect<T1>, T1>.CopyData(this, ret); return ret; }

		public abstract ISelect<T1, T2, T3, T4, T5, T6, T7, T8> From<T2, T3, T4, T5, T6, T7, T8>(Expression<Func<ISelectFromExpression<T1>, T2, T3, T4, T5, T6, T7, T8, ISelectFromExpression<T1>>> exp) where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class;// { this.InternalFrom(exp?.Body); var ret = new Select8Provider<T1, T2, T3, T4, T5, T6, T7, T8>(_orm, _commonUtils, _commonExpression, null); Select0Provider<ISelect<T1>, T1>.CopyData(this, ret); return ret; }

		public abstract ISelect<T1, T2, T3, T4, T5, T6, T7, T8, T9> From<T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<ISelectFromExpression<T1>, T2, T3, T4, T5, T6, T7, T8, T9, ISelectFromExpression<T1>>> exp) where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class where T9 : class;// { this.InternalFrom(exp?.Body); var ret = new Select9Provider<T1, T2, T3, T4, T5, T6, T7, T8, T9>(_orm, _commonUtils, _commonExpression, null); Select0Provider<ISelect<T1>, T1>.CopyData(this, ret); return ret; }

		public abstract ISelect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> From<T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<ISelectFromExpression<T1>, T2, T3, T4, T5, T6, T7, T8, T9, T10, ISelectFromExpression<T1>>> exp) where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class where T9 : class where T10 : class;// { this.InternalFrom(exp?.Body); var ret = new Select10Provider<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(_orm, _commonUtils, _commonExpression, null); Select0Provider<ISelect<T1>, T1>.CopyData(this, ret); return ret; }

		public ISelect<T1> GroupBy<TReturn>(Expression<Func<T1, TReturn>> columns) => this.InternalGroupBy(columns?.Body);

		public TMember Max<TMember>(Expression<Func<T1, TMember>> column) => this.InternalMax<TMember>(column?.Body);

		public TMember Min<TMember>(Expression<Func<T1, TMember>> column) => this.InternalMin<TMember>(column?.Body);

		public ISelect<T1> OrderBy<TMember>(Expression<Func<T1, TMember>> column) => this.InternalOrderBy(column?.Body);

		public ISelect<T1> OrderByDescending<TMember>(Expression<Func<T1, TMember>> column) => this.InternalOrderByDescending(column?.Body);

		public TMember Sum<TMember>(Expression<Func<T1, TMember>> column) => this.InternalSum<TMember>(column?.Body);

		public List<TReturn> ToList<TReturn>(Expression<Func<T1, TReturn>> select) => this.InternalToList<TReturn>(select?.Body);

		public ISelect<T1> Where(Expression<Func<T1, bool>> exp) => this.InternalWhere(exp?.Body);

		public ISelect<T1> Where<T2>(Expression<Func<T1, T2, bool>> exp) where T2 : class => this.InternalWhere(exp?.Body);

		public ISelect<T1> Where<T2, T3>(Expression<Func<T1, T2, T3, bool>> exp) where T2 : class where T3 : class => this.InternalWhere(exp?.Body);

		public ISelect<T1> Where<T2, T3, T4>(Expression<Func<T1, T2, T3, T4, bool>> exp) where T2 : class where T3 : class where T4 : class => this.InternalWhere(exp?.Body);

		public ISelect<T1> Where<T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, bool>> exp) where T2 : class where T3 : class where T4 : class where T5 : class => this.InternalWhere(exp?.Body);

		public ISelect<T1> WhereIf(bool condition, Expression<Func<T1, bool>> exp) => condition ? this.InternalWhere(exp?.Body) : this;
	}
}