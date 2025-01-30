using store;

namespace d3e.core
{
    public class TransactionDeligate
    {
        public delegate void ToRun();

        private EntityMutator Mutator;
        
        public void run(bool finish, ToRun run) 
        {
            run();
            if (finish) 
            {
                Mutator.Finish();
            }
        }

        public void ReadOnly(ToRun run) 
        {
            run();
        }

    }
}
