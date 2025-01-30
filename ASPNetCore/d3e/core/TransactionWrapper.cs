using System.Transactions;
using Classes;
using rest.ws;
using store;

namespace d3e.core
{
    public class TransactionWrapper
    {
        private D3ESubscription Subscription;

        private TransactionDeligate Deligate;

        private D3EEntityManagerProvider ManagerProvider;

        private DataChangeTracker ChangeTracker;

        private EntityMutator Mutator;

        public void DoInTransaction(TransactionDeligate.ToRun run)
        {
            bool created = CreateTransactionManager();
            bool success = false;
            try
            {
                if (created)
                {
                    ManagerProvider.Create(null);
                }
                Deligate.run(created, run);
                if (created)
                {
                    PublishEvents();
                }
                success = true;
            }
            catch (Exception e)
            {
                if (created)
                {
                    Log.Info("Transaction failed: " + e.Message);
                }
                throw e;
            }
            finally
            {
                if(created)
                {
                    if(!success)
                    {
                        TransactionManager manager = TransactionManager.Get();
                        if(manager != null)
                        {
                            manager.ClearChanges();
                        }
                    }
                    TransactionManager.Remove();
                    Mutator.Clear();
                    ManagerProvider.Clear();
                }
            }
        } 
        private void PublishEvents()
        {
            Deligate.ReadOnly(() =>
            {
                // Get the current transaction manager
                var manager = TransactionManager.Get();

                // Remove the current transaction manager from the context
                TransactionManager.Remove();

                // Create a new transaction manager
                CreateTransactionManager();

                // Get the changes from the transaction manager
                List<DataStoreEvent> changes = manager.GetChanges();

                // Fire the changes through the change tracker
                ChangeTracker.Fire(changes);

                // Handle each event
                foreach (var eventItem in changes)
                {
                    try
                    {
                        // Handle the context start for each event
                        Subscription.HandleContextStart(eventItem);
                    }
                    catch (Exception e)
                    {
                        Log.Info(e.Message);
                    }
                }

                // Clear the changes from the transaction manager
                manager.ClearChanges();
            });

            // If there are still changes in the transaction manager, publish events again
            if (!TransactionManager.Get().IsEmpty())
            {
                PublishEvents();
            }
        }

        private bool CreateTransactionManager()
        {
            TransactionManager manager = TransactionManager.Get();
            if (manager == null)
            {
                manager = new TransactionManager();
                TransactionManager.Set(manager);
                return true;
            }
            return false;
        }
    }
}
