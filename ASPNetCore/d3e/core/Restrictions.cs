using d3e.core;

namespace d3e.core
{
    public class Restrictions
    {
        public static SimpleExpression Eq(PropertyExpression prop, Object val)
        {
            return new BinaryCriterion("=", prop, val);
        }

        public static SimpleExpression NotEq(PropertyExpression prop, Object val)
        {
            return new BinaryCriterion("!=", prop, val);
        }

        public static SimpleExpression Lt(PropertyExpression prop, Object val)
        {
            return new BinaryCriterion("<", prop, val);
        }

        public static SimpleExpression Le(PropertyExpression prop, Object val)
        {
            return new BinaryCriterion("<=", prop, val);
        }

        public static SimpleExpression Gt(PropertyExpression prop, Object val)
        {
            return new BinaryCriterion(">", prop, val);
        }

        public static SimpleExpression Ge(PropertyExpression prop, Object val)
        {
            return new BinaryCriterion(">=", prop, val);
        }

        public static Criterion Between(PropertyExpression prop, Object lo, Object hi)
        {
            return new BetweenCriterion(prop, lo, hi);
        }

        public static Criterion In(PropertyExpression name, List<object> list)
        {
            return new InCriterion(name, list);
        }

        public static Criterion NotIn(PropertyExpression name, List<object> list)
        {
            return new NotInCriterion(name, list);
        }

        public static Criterion Contains(PropertyExpression name, String value)
        {
            return new ContainsCriterion(name, value, true);
        }

        public static Criterion NotContains(PropertyExpression name, String value)
        {
            return new ContainsCriterion(name, value, false);
        }

        public static Criterion StartsWith(PropertyExpression name, String value)
        {
            return new StartsWithCriterion(name, value, true);
        }

        public static Criterion EndsWith(PropertyExpression name, String value)
        {
            return new StartsWithCriterion(name, value, false);
        }

        public static Criterion Is(PropertyExpression name, Object value)
        {
            return new IsCriterion(name, value, true);
        }

        public static Criterion IsNot(PropertyExpression name, Object value)
        {
            return new IsCriterion(name, value, false);
        }
        public static Criterion Now()
        {
            return new DateNowExpression();
        }
    }
}
