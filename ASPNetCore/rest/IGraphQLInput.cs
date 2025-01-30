namespace rest
{
    public interface IGraphQLInput
    {
        void FromInput(GraphQLInputContext ctx);
    }
}
